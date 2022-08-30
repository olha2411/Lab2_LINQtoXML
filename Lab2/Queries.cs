using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Hotel.Models;
using Hotel.Models.Enum;
using Lab2.Constants;
using Lab2.ViewModels;
using Lab2.Xml;

namespace Lab2
{
    public class Queries
    {
        private readonly FileNames _fileNames = new FileNames();

        private readonly XElement _clients;
        private readonly XElement _rooms;
        private readonly XElement _reservations;
        private readonly XElement _roomtypes;

        private readonly XmlParser _xmlParser = new XmlParser();
        private readonly XmlDataReader dataReader = new XmlDataReader();

        public Queries()
        {
            _clients = dataReader.LoadFile(_fileNames.xmlClientsFile);
            _rooms = dataReader.LoadFile(_fileNames.xmlRoomsFile);
            _reservations = dataReader.LoadFile(_fileNames.xmlReservationsFile);
            _roomtypes = dataReader.LoadFile(_fileNames.xmlRoomTypesFile);
        }


        //1 AllRooms
        public IEnumerable<Room> GetAllRooms()
        {
            var query = from r in _rooms.Elements("room")
                        select _xmlParser.ToRoom(r);

            return query;
        }

        //2. All Reservations With Clients And Rooms Info
        public IEnumerable<ReservationViewModel> GetAllReservations()
        {
            return from reservation in _reservations.Elements("reservation")
                   join client in _clients.Elements("client") on reservation.Element("clientid")?.Value
                   equals client.Element("id")?.Value
                   join room in _rooms.Elements("room") on reservation.Element("roomid")?.Value
                   equals room.Element("id")?.Value
                   join type in _roomtypes.Elements("roomtype") on room.Element("typeid")?.Value
                   equals type.Element("id")?.Value
                   select new ReservationViewModel
                   {
                       Client = _xmlParser.ToClient(client),
                       Room = _xmlParser.ToRoom(room),
                       Reservation = _xmlParser.ToReservation(reservation),
                       RoomType = _xmlParser.ToRoomType(type),
                   };
        }

        // 3. All Reservations With Room Info Grouped By Client, Sorted By Client Surname And CheckIn Date 
        public Dictionary<Client, IEnumerable<ReservationViewModel>> GetAllReservationsGroupedByClient()
        {
            var query = from reservation in _reservations.Elements("reservation")
                        join client in _clients.Elements("client") on reservation.Element("clientid")?.Value
                        equals client.Element("id")?.Value
                        join room in _rooms.Elements("room") on reservation.Element("roomid")?.Value
                        equals room.Element("id")?.Value
                        join type in _roomtypes.Elements("roomtype") on room.Element("typeid")?.Value
                        equals type.Element("id")?.Value
                        orderby client.Element("surname")?.Value, DateTime
                        .Parse(reservation.Element("checkindate")?.Value)
                        group new ReservationViewModel()
                        {
                            Client = _xmlParser.ToClient(client),
                            Room = _xmlParser.ToRoom(room),
                            Reservation = _xmlParser.ToReservation(reservation),
                            RoomType = _xmlParser.ToRoomType(type),
                        }
                        by _xmlParser.ToClient(client);

            return query.ToDictionary(m => m.Key, n => n.Select(room => room));
        }

        // 4. Rooms With Their Types
        public IEnumerable<RoomTypeViewModel> GetAllRoomsWithTheirTypes()
        {
            return _rooms.Elements("room").Join(_roomtypes.Elements("roomtype"),
                   r => r.Element("typeid")?.Value,
                   t => t.Element("id")?.Value,
                   (r, t) => new RoomTypeViewModel
                   {
                       Room = _xmlParser.ToRoom(r),
                       RoomType = _xmlParser.ToRoomType(t)
                   });
        }

        // 5. Rooms Grouped By Type
        public Dictionary<string, IEnumerable<Room>> GetRoomsGroupedByType()
        {
            var result = _rooms.Elements("room").Join(_roomtypes.Elements("roomtype"),
                         r => r.Element("typeid")?.Value,
                         t => t.Element("id")?.Value,
                         (r, rooms) => new
                         {
                             Room = _xmlParser.ToRoom(r),
                             RoomType = _xmlParser.ToRoomType(rooms),

                         }).GroupBy(q => q.RoomType.Type);

            return result.ToDictionary(m => m.Key, n => n.Select(room => room.Room));

        }


        // 6. Clients With More Than 1 Reservation 
        public IEnumerable<Client> GetClientsWithFewReservations()
        {
            var query = from client in _clients.Elements("client")
                        join reservation in _reservations.Elements("reservation")
                        on client.Element("id")?.Value equals reservation.Element("clientid")?.Value
                        group reservation by client into reserv
                        where reserv.Count() > 1
                        select _xmlParser.ToClient(reserv.Key);

            return query;
        }

        // 7. Deluxe Rooms
        public IEnumerable<Room> GetDeluxeRooms()
        {
            var query = from room in _rooms.Elements("room")
                        join type in _roomtypes.Elements("roomtype")
                        on room.Element("typeid")?.Value equals type.Element("id")?.Value
                        where (type.Element("type")?.Value).Equals("Deluxe", StringComparison.OrdinalIgnoreCase)
                        select _xmlParser.ToRoom(room);

            return query;
        }

        // 8. Clients With Reservations Count
        public Dictionary<Client, int> GetClientsWithReservationsCount()
        {
            var query = _clients.Elements("client").GroupJoin(_reservations.Elements("reservation"),
                        c => c.Element("id")?.Value,
                        r => r.Element("clientid")?.Value,
                        (c, reservations) => new
                        {
                            Client = _xmlParser.ToClient(c),
                            Reserv = reservations.Count(),
                        }).OrderByDescending(m => m.Reserv);

            return query.ToDictionary(g => g.Client, g => g.Reserv);           
        }


        // 9. Rooms With Reservations, Sorted By CheckIn Date
        public Dictionary<RoomTypeViewModel, IEnumerable<Reservation>> GetRoomsWithReservations()
        {
            var query = from room in _rooms.Elements("room")
                        join type in _roomtypes.Elements("roomtype")
                        on room.Element("typeid")?.Value equals type.Element("id")?.Value
                        join reservation in _reservations.Elements("reservation")
                        on room.Element("id")?.Value equals reservation.Element("roomid")?.Value
                        orderby Convert.ToDateTime(reservation.Element("checkindate").Value)
                        group _xmlParser.ToReservation(reservation) 
                        by new RoomTypeViewModel()
                        {
                            Room = _xmlParser.ToRoom(room),
                            RoomType = _xmlParser.ToRoomType(type),
                        };

            return query.ToDictionary(g => g.Key, g => g.Select(r => r));
        }

        // 10. Currently Occupied Rooms
        public IEnumerable<RoomTypeViewModel> GetOccupiedRooms()
        {
            return from room in _rooms.Elements("room")
                   join type in _roomtypes.Elements("roomtype")
                   on room.Element("typeid")?.Value equals type.Element("id")?.Value
                   join reservation in _reservations.Elements("reservation")
                   on room.Element("id")?.Value equals reservation.Element("roomid")?.Value
                   where DateTime.Parse(reservation.Element("checkindate")?.Value) <= DateTime.Today
                   && DateTime.Today <= DateTime.Parse(reservation.Element("checkoutdate")?.Value)
                   select new RoomTypeViewModel
                   {
                       Room = _xmlParser.ToRoom(room),
                       RoomType = _xmlParser.ToRoomType(type)

                   };
        }

        // 11. Client Who Are More Than "Age"       
        public IEnumerable<Client> GetAdultClients(int AgeNum)
        {
            return _clients.Elements("client").Where(d =>
                   {
                       var current = DateTime.Now;
                       var age = current.Year - DateTime.Parse(d.Element("birthdate")?.Value).Year;
                       if (DateTime.Parse(d.Element("birthdate")?.Value) > current.AddYears(-age)) age--;
                       return age > AgeNum;
                   }).Select(p => _xmlParser.ToClient(p));
        }

        


        // 12. Clients With Patronymic
        public IEnumerable<Client> GetClientsWithPatronymic()
        {
            return _clients.Elements("client").Where(client => !string
            .IsNullOrEmpty(client.Element("patronymic")?.Value)).Select(p => _xmlParser.ToClient(p));

        }

        // 13. Rooms With Refrigerator
        public IEnumerable<RoomTypeViewModel> GetRoomsWithRefrigerator()
        {
            return _rooms.Elements("room").Where(o => o.Element("options").Elements("option")
                            .FirstOrDefault(p => p.Value == Options.Refrigerator.ToString()) != null)
                            .Join(_roomtypes.Elements("roomtype"),
                            r => r.Element("typeid")?.Value,
                            t => t.Element("id")?.Value,
                            (r, t) => new RoomTypeViewModel
                            {
                                Room = _xmlParser.ToRoom(r),
                                RoomType = _xmlParser.ToRoomType(t),
                            });

        }

        // 14. Average age of clients        
        public double GetAverageClientsAge()
        {
            return _clients.Elements("client").Select(d =>
                   {
                       var current = DateTime.Now;
                       var age = current.Year - DateTime.Parse(d.Element("birthdate")?.Value).Year;
                       if (DateTime.Parse(d.Element("birthdate")?.Value) > current.AddYears(-age)) age--;
                       return age;
                   }).Average();

        }
        
        public DateTime GetLastReservations()
        {
            return DateTime.Parse(_reservations.Elements("reservation")
                .Select(p => p.Element("checkindate").Value).Max());           
        }
    }




}

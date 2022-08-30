using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hotel.Models;
using Lab2.Constants;

namespace Lab2.Xml
{
    public class XmlDataReader
    {
        private readonly FileNames _fileNames = new FileNames();
        private readonly XmlParser _xmlParser = new XmlParser();

        public XElement LoadFile(string file)
        {
            XDocument xmlDoc = XDocument.Load(file);
            XElement rootElement = xmlDoc.Root;
            return rootElement;
        }

        public List<Client> GetClients()
        {
            var clients = LoadFile(_fileNames.xmlClientsFile);

            var resultList = new List<Client>();

            foreach (var client in clients.Elements("client"))
            {
                resultList.Add(new Client()
                {
                    Id = Int32.Parse(client.Element("id").Value),
                    Surname = client.Element("surname")?.Value,
                    Name = client.Element("name")?.Value,
                    Patronymic = client.Element("patronymic")?.Value,
                    BirthDate = DateTime.Parse(client.Element("birthdate")?.Value),
                    PhoneNumber = client.Element("phonenumber")?.Value,
                });
            }

            return resultList;
        }
        
        public List<RoomType> GetRoomTypes()
        {
            var roomTypes = LoadFile(_fileNames.xmlRoomTypesFile);
            var resultList = new List<RoomType>();

            foreach (var type in roomTypes.Elements("roomtype"))
            {
                resultList.Add(new RoomType()
                {
                    Id = Int32.Parse(type.Element("id")?.Value),
                    Type = type.Element("type")?.Value,
                });
            }

            return resultList;
        }
        
        public List<Room> GetRooms()
        {
            var rooms = LoadFile(_fileNames.xmlRoomsFile);
            var resultList = new List<Room>();

            foreach (var room in rooms.Elements("room"))
            {
                resultList.Add(new Room()
                {
                    Id = Int32.Parse(room.Element("id").Value),
                    Number = Int32.Parse(room.Element("number")?.Value),
                    Capacity = Int32.Parse(room.Element("capacity")?.Value),
                    Options = _xmlParser.ToOptionsType(room.Elements("options")),
                    TypeId = Int32.Parse(room.Element("typeid")?.Value),
                });

            }
            return resultList;
        }

        
        public List<Reservation> GetReservations()
        {
            var reservations = LoadFile(_fileNames.xmlReservationsFile);

            var resultList = new List<Reservation>();

            foreach (var reserv in reservations.Elements("reservation"))
            {
                resultList.Add(new Reservation()
                {
                    CheckInDate = DateTime.Parse(reserv.Element("checkindate")?.Value),
                    CheckOutDate = DateTime.Parse(reserv.Element("checkoutdate")?.Value),
                    ClientId = Int32.Parse(reserv.Element("clientid")?.Value),
                    RoomId = Int32.Parse(reserv.Element("roomid")?.Value),
                });

            }

            return resultList;
        }
    }

}

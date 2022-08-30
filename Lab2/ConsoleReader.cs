using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.Models;
using Hotel.Models.Enum;
using Lab2.Xml;

namespace Lab2
{
    public class ConsoleReader
    {
        private readonly XmlDataReader _dataReader = new XmlDataReader();
        public RoomType ReadNewRoomType()
        {
            Console.WriteLine("Add new room type");
            Console.Write("Enter the room type id: ");
            var idStr = Console.ReadLine();
            int id = 0;

            while (!(Int32.TryParse(idStr, out id) && id >= 0))
            {
                Console.Write("Enter the room type id: ");
                idStr = Console.ReadLine();
            }

            Console.Write("Enter name of type: ");
            var type = Console.ReadLine();

            while (_dataReader.GetRoomTypes().FirstOrDefault(i => i.Type == type) != null)
            {
                Console.Write("Enter name of type: ");
                type = Console.ReadLine();
            }

            return new RoomType()
            {
                Id = id,
                Type = type,
            };
        }

        public Room ReadNewRoom()
        {
            Console.WriteLine("\tAdd new room");
            Console.Write("Enter id: ");
            var idStr = Console.ReadLine();
            int id = 0;

            while (!(Int32.TryParse(idStr, out id) && id >= 0))
            {
                Console.Write("\tEnter room id: ");
                idStr = Console.ReadLine();
            }

            Console.Write("Enter room number: ");
            var numberStr = Console.ReadLine();
            int number = 0;

            while (!(Int32.TryParse(numberStr, out number) && number >= 0))
            {
                Console.Write("\tEnter room number: ");
                numberStr = Console.ReadLine();
            }

            Console.Write("Enter room capacity: ");
            var capacityStr = Console.ReadLine();
            int capacity = 0;

            while (!(Int32.TryParse(capacityStr, out capacity) && capacity >= 0 && capacity < 5))
            {
                Console.Write("\tEnter room capacity: ");
                capacityStr = Console.ReadLine();
            }
            

            Console.Write("Enter type id: ");
            var typeidStr = Console.ReadLine();
            int typeid = 0;

            while (_dataReader.GetRoomTypes().FirstOrDefault(i => i.Id == typeid) == null)
            {
                while (!(Int32.TryParse(typeidStr, out typeid) && typeid >= 0))
                {
                    Console.Write("\tEnter the type id: ");
                    idStr = Console.ReadLine();
                }
            }

            return new Room()
            {
                Id = id,
                Number = number,
                Capacity = capacity,
                Options = new List<Options>(),
                TypeId = typeid,

            };


        }

        public Client ReadNewClient()
        {
            Console.WriteLine("\tAdd new client");
            Console.Write("Enter client id: ");
            var idStr = Console.ReadLine();
            int id;

            while (!(Int32.TryParse(idStr, out id) && id >= 0))
            {
                Console.Write("\tEnter client id: ");
                idStr = Console.ReadLine();
            }

            Console.Write("\tEnter Surname: ");
            var surname = Console.ReadLine();

            Console.Write("\tEnter Name: ");
            var name = Console.ReadLine();


            Console.Write("\tEnter Patronymic: ");
            var patronymic = Console.ReadLine();

            Console.Write("\tEnter BirthDate (dd/mm/yyyy): ");
            var birthDate = Console.ReadLine();
            DateTime date;

            while (!DateTime.TryParse(birthDate, out date) && date.CompareTo(DateTime.Now) > 0)
            {
                Console.Write("\tEnter BirthDate (dd/mm/yyyy): ");
                birthDate = Console.ReadLine();
            }

            Console.Write("\tEnter PhoneNumber: ");
            var phoneNumber = Console.ReadLine();

            return new Client()
            {
                Id = id,
                Surname = surname,
                Name = name,
                Patronymic = patronymic,
                BirthDate = date,
                PhoneNumber = phoneNumber,
            };
        }

        public Reservation ReadNewReservation()
        {
            Console.WriteLine("\tAdd new resevation");

            Console.Write("Enter CheckInDate (dd/mm/yyyy): ");
            var checkInDateStr = Console.ReadLine();
            DateTime dateIn;

            while (!(DateTime.TryParse(checkInDateStr, out dateIn)))
            {
                Console.Write("\tEnter CheckInDate (dd/mm/yyyy): ");
                checkInDateStr = Console.ReadLine();
            }

            Console.Write("Enter CheckOutDate (dd/mm/yyyy): ");
            var checkOutDateStr = Console.ReadLine();
            DateTime dateOut;

            while (!(DateTime.TryParse(checkOutDateStr, out dateOut) && dateOut.CompareTo(dateIn) > 0))
            {
                Console.Write("\tEnter CheckOutDate (dd/mm/yyyy): ");
                checkOutDateStr = Console.ReadLine();
            }

            Console.Write("Enter client id: ");
            var clientIdStr = Console.ReadLine();
            int clientId = 0;

            while (_dataReader.GetClients().FirstOrDefault(i => i.Id == clientId) == null)
            {
                Console.Write("\tEnter client id: ");
                clientIdStr = Console.ReadLine();

                while (!(Int32.TryParse(clientIdStr, out clientId) && clientId >= 0))
                {
                    Console.Write("\tEnter client id: ");
                    clientIdStr = Console.ReadLine();
                }
            }

            Console.Write("Enter room id: ");
            var roomIdStr = Console.ReadLine();
            int roomId = 0;

            while (_dataReader.GetRoomTypes().FirstOrDefault(i => i.Id == roomId) == null)
            {
                Console.Write("\tEnter room id: ");
                roomIdStr = Console.ReadLine();
                while (!(Int32.TryParse(roomIdStr, out roomId) && roomId >= 0))
                {
                    Console.Write("\tEnter room id: ");
                    roomIdStr = Console.ReadLine();
                }
            }

            return new Reservation()
            {
                CheckInDate = dateIn,
                CheckOutDate = dateOut,
                ClientId = clientId,
                RoomId = roomId,
            };
        }
    }

}

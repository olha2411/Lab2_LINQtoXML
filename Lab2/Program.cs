using System;
using System.Collections.Generic;
using System.Linq;
using Lab2.Xml;
using Hotel;
using Hotel.Models;
using System.Xml;

namespace Lab2
{    
    class Program
    {
        static void ShowQueries()
        {
            Queries queries = new Queries();

            ConsolePrinter.PrintAllRooms(queries.GetAllRooms());
            ConsolePrinter.PrintAllReservations(queries.GetAllReservations());
            ConsolePrinter.PrintAllReservationsGroupedByClient(queries.GetAllReservationsGroupedByClient());
            ConsolePrinter.PrintAllRoomsWithTheirTypes(queries.GetAllRoomsWithTheirTypes());
            ConsolePrinter.PrintRoomsGroupedByType(queries.GetRoomsGroupedByType());
            ConsolePrinter.PrintClientsWithFewReservations(queries.GetClientsWithFewReservations());
            ConsolePrinter.PrintDeluxeRooms(queries.GetDeluxeRooms());
            ConsolePrinter.PrintClientsWithReservationsCount(queries.GetClientsWithReservationsCount());
            ConsolePrinter.PrintRoomsWithReservations(queries.GetRoomsWithReservations());
            ConsolePrinter.PrintOccupiedRooms(queries.GetOccupiedRooms());
            ConsolePrinter.PrintAdultClients(queries.GetAdultClients(21));            
            ConsolePrinter.PrintClientsWithPatronymic(queries.GetClientsWithPatronymic());
            ConsolePrinter.PrintRoomsWithRefrigerator(queries.GetRoomsWithRefrigerator());
            ConsolePrinter.PrintAverageClientsAge(queries.GetAverageClientsAge());
            ConsolePrinter.PrintLastReservation(queries.GetLastReservations());
        }
        static void AddItemToFile()
        {
            ConsoleReader _consoleReader = new ConsoleReader();
            XmlDataWriter writer = new XmlDataWriter();
            var end = false;
            while (!end)
            {
                Console.WriteLine("1- Add Client / 2- Add Room / 3- Add RoomType / 4- Add Reservation/ 0 - quit");

                int num;
                var numStr = Console.ReadLine();
                while (!int.TryParse(numStr, out num))
                {
                    Console.Write("Level Type: ");
                    numStr = Console.ReadLine();
                }
                switch (num)
                {
                    case 0:
                        Console.WriteLine("");
                        end = true;
                        break;
                    case 1:
                        Console.WriteLine("\tAdd Client");
                        writer.AddNewClientElement(_consoleReader.ReadNewClient());
                        break;
                    case 2:
                        Console.WriteLine("\tAdd Room");
                        writer.AddNewRoomElement(_consoleReader.ReadNewRoom());
                        break;
                    case 3:
                        Console.WriteLine("\tAdd RoomType");
                        writer.AddNewRoomTypeElement(_consoleReader.ReadNewRoomType());
                        break;
                    case 4:
                        Console.WriteLine("\tAdd Reservation");
                        writer.AddNewReservationElement(_consoleReader.ReadNewReservation());
                        break;
                }
            }              
                
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Lab2");

            XmlDataReader _reader = new XmlDataReader();
            XmlDataWriter writer = new XmlDataWriter();
            
            writer.CreateClientFile();
            foreach (Client client in Data.Clients)
            {
                writer.AddNewClientElement(client);
            }

            writer.CreateRoomTypesFile();
            foreach (RoomType roomType in Data.RoomTypes)
            {
                writer.AddNewRoomTypeElement(roomType);
            }
            writer.CreateRoomFile();
            foreach(Room room in Data.Rooms)
            {
                writer.AddNewRoomElement(room);
            }
            writer.CreateReservationFile();
            foreach (Reservation reserv in Data.Reservations)
            {
                writer.AddNewReservationElement(reserv);
            }
            
            var finish = true;
            while (finish)
            {
                Console.WriteLine("1 - Print queries / 2 - Print files / 3 - Add item");
                Console.Write("Enter number: ");
                int num;
                var numStr = Console.ReadLine();
                while (!int.TryParse(numStr, out num))
                {
                    Console.Write("Level Type: ");
                    numStr = Console.ReadLine();
                }
                switch (num)
                {
                    case 0:
                        Console.WriteLine("finished");
                        finish = false;
                        break;
                    case 1:
                        ShowQueries();
                        break;
                    case 2:
                        Console.WriteLine("\t\tRooms");
                        ConsolePrinter.PrintRoomFileData(_reader.GetRooms());
                        Console.WriteLine("\t\tClients");
                        ConsolePrinter.PrintFileData(_reader.GetClients());
                        Console.WriteLine("\t\tRoomTypes");
                        ConsolePrinter.PrintFileData(_reader.GetRoomTypes());
                        Console.WriteLine("\t\tReservations");
                        ConsolePrinter.PrintFileData(_reader.GetReservations());
                        Console.WriteLine();
                        break;
                    case 3:
                        AddItemToFile();
                        break;
                }
            }


            using (XmlWriter xwriter = XmlWriter.Create("newfile.xml"))
            {
                xwriter.WriteStartElement("start");
                xwriter.WriteElementString("block", "1");
                xwriter.WriteElementString("block", "2");
                xwriter.WriteEndElement();

                
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("newfile.xml");

            Console.ReadKey();
        }
    }
}

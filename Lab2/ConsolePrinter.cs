using System;
using System.Collections.Generic;
using Hotel.Models;
using Lab2.ViewModels;

namespace Lab2
{
    public static class ConsolePrinter
    {
        public static void PrintAllRooms(IEnumerable<Room> rooms)
        {
            Console.WriteLine("All Rooms:");

            foreach (var item in rooms)
                Console.WriteLine($"\t{item}");
            Console.WriteLine();
        }

        public static void PrintAllClients(IEnumerable<Client> clients)
        {
            Console.WriteLine("All Clients:");

            foreach (var item in clients)
                Console.WriteLine($"\t{item}");
            Console.WriteLine();
        }

        public static void PrintAllReservations(IEnumerable<ReservationViewModel> reservations)
        {
            Console.WriteLine("All Reservations:");

            foreach (var reservation in reservations)
            {
                Console.WriteLine($"\t{reservation.Reservation} | {reservation.Room}, {reservation.RoomType} | {reservation.Client}");

            }
            Console.WriteLine();
        }
        //3
        public static void PrintAllReservationsGroupedByClient(Dictionary<Client, IEnumerable<ReservationViewModel>> reservations)
        {
            Console.WriteLine("All Reservations With Room Info Grouped By Client, Sorted By Client Surname And CheckIn Date:");

            foreach (var client in reservations)
            {
                Console.WriteLine($"\t{client.Key}");
                foreach (var reservation in client.Value)
                    Console.WriteLine($"\t\t{reservation.Reservation} | {reservation.Room}, {reservation.RoomType}");
            }
            Console.WriteLine();
        }
        //4
        public static void PrintAllRoomsWithTheirTypes(IEnumerable<RoomTypeViewModel> rooms)
        {
            Console.WriteLine("Rooms With Their Types:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"\t{room.Room}, {room.RoomType}");
            }
            Console.WriteLine();
        }
        //5
        public static void PrintRoomsGroupedByType(Dictionary<string, IEnumerable<Room>> rooms)
        {
            Console.WriteLine("Rooms Grouped By Type:");

            foreach (var type in rooms)
            {
                Console.WriteLine($"\t{type.Key}");
                foreach (var room in type.Value)
                    Console.WriteLine($"\t\t{room}");
            }
            Console.WriteLine();
        }


        //6
        public static void PrintClientsWithFewReservations(IEnumerable<Client> reservations)
        {
            Console.WriteLine("Clients With More Than 1 Reservation:");

            foreach (var client in reservations)
                Console.WriteLine($"\t{client}");
            Console.WriteLine();
        }
        //7
        public static void PrintDeluxeRooms(IEnumerable<Room> rooms)
        {
            Console.WriteLine("Deluxe Rooms:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"\t{room}");
            }
            Console.WriteLine();
        }
        //8
        public static void PrintClientsWithReservationsCount(Dictionary<Client, int> reservations)
        {

            Console.WriteLine("Clients With Reservations Count:");

            foreach (var item in reservations)
            {
                Console.WriteLine($"\t{item.Key}, reservation number: { item.Value}");
            }
            Console.WriteLine();

        }
        //9
        public static void PrintRoomsWithReservations(Dictionary<RoomTypeViewModel, IEnumerable<Reservation>> rooms)
        {
            Console.WriteLine("Rooms With Reservations, Sorted By CheckIn Date:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"\t{room.Key.Room}, {room.Key.RoomType}");
                foreach (var reservation in room.Value)
                    Console.WriteLine($"\t\t{reservation}");
            }
            Console.WriteLine();
        }

        public static void PrintOccupiedRooms(IEnumerable<RoomTypeViewModel> rooms)
        {
            Console.WriteLine("Currently Occupied Rooms:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"\t{room.Room}, {room.RoomType}");
            }
            Console.WriteLine();
        }

        public static void PrintAdultClients(IEnumerable<Client> clients)
        {
            Console.WriteLine("Client Who Are More Than 21:");

            foreach (var client in clients)
                Console.WriteLine($"\t{client}");
            Console.WriteLine();
        }

        public static void PrintUnoccupiedRooms(IEnumerable<RoomTypeViewModel> rooms)
        {
            Console.WriteLine("Currently Unoccupied Rooms: ");

            foreach (var room in rooms)
                Console.WriteLine($"\t{room.Room}, {room.RoomType}");
            Console.WriteLine();
        }

        public static void PrintClientsWithPatronymic(IEnumerable<Client> clients)
        {
            Console.WriteLine("Clients With Patronymic:");

            foreach (var client in clients)
                Console.WriteLine($"\t{client}");
            Console.WriteLine();
        }

        public static void PrintRoomsWithRefrigerator(IEnumerable<RoomTypeViewModel> rooms)
        {
            Console.WriteLine("Rooms With Refrigerator:");

            foreach (var room in rooms)
                Console.WriteLine($"\t{room.Room}, {room.RoomType}");
            Console.WriteLine();
        }

        public static void PrintAverageClientsAge(double elem)
        {
            Console.WriteLine($"Average Client Age is: {elem}");
            Console.WriteLine();
        }

        public static void PrintLastReservation(DateTime elem)
        {
            Console.WriteLine($"Last Reservation is: {elem:d}");
            Console.WriteLine();
        }


        public static void PrintRoomFileData(List<Room> rooms)
        {
            foreach (var item in rooms)
            {
                Console.WriteLine($"{item}");
                foreach (var opt in item.Options)
                {
                    Console.WriteLine(opt);
                }

            }
            Console.WriteLine();
        }

        public static void PrintFileData<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
        }
    }

}

using System;
using System.Collections.Generic;
using Hotel.Models;
using Hotel.Models.Enum;

namespace Hotel
{
    public static class Data
    {
        public static List<Room> Rooms = new List<Room>
        {
            new Room
            {
                Id = 1,
                Number = 34,
                Capacity = 1,
                TypeId = 1,
                Options = new List<Options> {Options.Conditioner, Options.Crib, Options.Refrigerator}
            },

            new Room
            {
                Id = 2,
                Number = 36,
                Capacity = 2,
                TypeId = 2,
                Options = new List<Options> {Options.Conditioner,Options.Refrigerator}
            },

            new Room
            {
                Id = 3,
                Number = 41,
                Capacity = 2,
                TypeId = 2,
                Options = new List<Options> {Options.Conditioner,Options.Refrigerator}
            },

            new Room
            {
                Id = 4,
                Number = 42,
                Capacity = 2,
                TypeId = 1
            }
        };

        public static List<RoomType> RoomTypes = new List<RoomType>
        {
            new RoomType
            {
                Id = 1,
                Type = "Standard"
            },

            new RoomType
            {
                Id = 2,
                Type = "Deluxe"
            },

            new RoomType
            {
                Id = 3,
                Type = "Joint"
            },

        };

        public static List<Client> Clients = new List<Client>
        {
            new Client
            {
                Id = 1,
                Name = "Andriy",
                Surname = "Buhrovuch",
                Patronymic = "Valeriiovych",
                BirthDate = DateTime.Today.AddYears(-18),
                PhoneNumber = "0984517836",
            },

            new Client
            {
                Id = 2,
                Name = "Ameliya",
                Surname = "Polyuhovuch",
                Patronymic = "Viktorivna",
                BirthDate = DateTime.Today.AddYears(-30),
                PhoneNumber = "0984511336",
            },


            new Client
            {
                Id = 3,
                Name = "Nekras",
                Surname = "Kurbas",
                Patronymic = "Viktorovich",
                BirthDate = DateTime.Today.AddYears(-31),
                PhoneNumber = "0989511336",
            },

            new Client
            {
                Id = 4,
                Name = "Adolphus",
                Surname = "Bush",
                BirthDate = DateTime.Today.AddYears(-28),
                PhoneNumber = "0984517836",
            },

            new Client
            {
                Id = 5,
                Name = "Nick",
                Surname = "Aberdeen",
                BirthDate = DateTime.Today.AddYears(-33),
                PhoneNumber = "0989511336",
            },

        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation
            {
                ClientId = 3,
                RoomId = 1,
                CheckInDate = DateTime.Today.AddDays(-3),
                CheckOutDate = DateTime.Today.AddDays(2),
            },

            new Reservation
            {
                ClientId = 2,
                RoomId = 3,
                CheckInDate = DateTime.Today.AddDays(-18),
                CheckOutDate = DateTime.Today.AddDays(-16),
            },

            new Reservation
            {
                ClientId = 2,
                RoomId = 2,
                CheckInDate = DateTime.Today.AddDays(-5),
                CheckOutDate = DateTime.Today.AddDays(-2),
            },

            new Reservation
            {
                ClientId = 3,
                RoomId = 4,
                CheckInDate = DateTime.Today.AddDays(-3),
                CheckOutDate = DateTime.Today.AddDays(1),
            },

            new Reservation
            {
                ClientId = 1,
                RoomId = 4,
                CheckInDate = DateTime.Today.AddDays(2),
                CheckOutDate = DateTime.Today.AddDays(5),
            },
        };
    }
}



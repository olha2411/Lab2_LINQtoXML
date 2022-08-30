using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hotel.Models;
using Hotel.Models.Enum;

namespace Lab2.Xml
{
    public class XmlParser
    {
        public List<Options> ToOptionsType(IEnumerable<XElement> toList)
        {
            return toList.Descendants("option").Select(p => (Options)Enum.Parse(typeof(Options), p?.Value)).ToList();
        }
        public Room ToRoom(XElement element)
        {
            return new Room()
            {
                Id = Int32.Parse(element.Element("id")?.Value),
                Number = Int32.Parse(element.Element("number")?.Value),
                Capacity = Int32.Parse(element.Element("capacity")?.Value),
                Options = ToOptionsType(element.Elements("options")),
                TypeId = Int32.Parse(element.Element("typeid")?.Value),
            };

        }

        public Client ToClient(XElement element)
        {
            return new Client()
            {
                Id = Int32.Parse(element.Element("id")?.Value),
                Surname = element.Element("surname")?.Value,
                Name = element.Element("name")?.Value,
                Patronymic = element.Element("patronymic")?.Value,
                BirthDate = DateTime.Parse(element.Element("birthdate")?.Value),
                PhoneNumber = element.Element("phonenumber")?.Value,
            };
        }

        public RoomType ToRoomType(XElement element)
        {
            return new RoomType()
            {
                Id = Int32.Parse(element.Element("id")?.Value),
                Type = element.Element("type")?.Value,
            };

        }

        public Reservation ToReservation(XElement element)
        {
            return new Reservation()
            {
                CheckInDate = DateTime.Parse(element.Element("checkindate")?.Value),
                CheckOutDate = DateTime.Parse(element.Element("checkoutdate")?.Value),
                ClientId = Int32.Parse(element.Element("clientid")?.Value),
                RoomId = Int32.Parse(element.Element("roomid")?.Value),
            };
        }

    }

}

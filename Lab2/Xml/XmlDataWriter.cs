using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Hotel;
using Hotel.Models;
using Hotel.Models.Enum;


namespace Lab2.Xml
{
    public class XmlDataWriter
    {

        private readonly XmlWriterSettings _settings = new XmlWriterSettings() { Indent = true };

        public void CreateClientFile()
        {
            using (XmlWriter writer = XmlWriter.Create("clients.xml", _settings))
            {
                writer.WriteStartElement("clients");
                writer.WriteEndElement();
            }
        }
       
        public void AddNewClientElement(Client element)
        {
            XmlDocument xmlRoomTypeDoc = new XmlDocument();            
            try
            {
                xmlRoomTypeDoc.Load("clients.xml");
            }
            catch
            {
                CreateClientFile();
                xmlRoomTypeDoc.Load("clients.xml");
            }

            XmlElement Root = xmlRoomTypeDoc.DocumentElement;
            XmlElement clientElem = xmlRoomTypeDoc.CreateElement("client");

            var idElem = xmlRoomTypeDoc.CreateElement("id");
            idElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.Id.ToString()));
            clientElem.AppendChild(idElem);

            var surnameElem = xmlRoomTypeDoc.CreateElement("surname");
            surnameElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.Surname));            
            clientElem.AppendChild(surnameElem);

            var nameElem = xmlRoomTypeDoc.CreateElement("name");
            nameElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.Name));
            clientElem.AppendChild(nameElem);

            var patronymicElem = xmlRoomTypeDoc.CreateElement("patronymic");
            patronymicElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.Patronymic));
            clientElem.AppendChild(patronymicElem);

            var birthdateElem = xmlRoomTypeDoc.CreateElement("birthdate");
            birthdateElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.BirthDate.ToString()));
            clientElem.AppendChild(birthdateElem);

            var phoneNumberElem = xmlRoomTypeDoc.CreateElement("phonenumber");
            phoneNumberElem.AppendChild(xmlRoomTypeDoc.CreateTextNode(element.PhoneNumber));
            clientElem.AppendChild(phoneNumberElem);

            Root.AppendChild(clientElem);

            xmlRoomTypeDoc.Save("clients.xml");
        }


        public void CreateRoomTypesFile()
        {
            using (XmlWriter writer = XmlWriter.Create("roomtypes.xml", _settings))
            {

                writer.WriteStartElement("roomtypes");                
                writer.WriteEndElement();
            }
        }

        public void AddNewRoomTypeElement(RoomType element)
        {
            XDocument xmlRoomTypeDoc = XDocument.Load("roomtypes.xml");

            if (xmlRoomTypeDoc != null)
            {
                if (xmlRoomTypeDoc.Root == null)
                {
                    CreateRoomTypesFile();
                }
            }
            xmlRoomTypeDoc.Root.Add(new XElement("roomtype",
                                    new XElement("id", element.Id),
                                    new XElement("type", element.Type)));

            xmlRoomTypeDoc.Save("roomtypes.xml");
        }

        public void CreateRoomFile()
        {
            using (XmlWriter writer = XmlWriter.Create("rooms.xml", _settings))
            {

                writer.WriteStartElement("rooms");                
                writer.WriteEndElement();
            }
        }

        public void AddNewRoomElement(Room element)
        {
            XDocument xmlRoomTypeDoc = XDocument.Load("rooms.xml");

            if (xmlRoomTypeDoc != null)
            {
                if (xmlRoomTypeDoc.Root == null)
                {
                    CreateRoomFile();
                }
                List<XElement> list = new List<XElement>();

                foreach (var i in element.Options)
                {
                    list.Add(new XElement("option", i));
                }

                xmlRoomTypeDoc.Root.Add(new XElement("room",
                                    new XElement("id", element.Id),
                                    new XElement("number", element.Number),
                                    new XElement("capacity", element.Capacity),
                                    new XElement("options", list),
                                    new XElement("typeid", element.TypeId)));

            }
            xmlRoomTypeDoc.Save("rooms.xml");
        }

        public void CreateReservationFile()
        {
            using (XmlWriter writer = XmlWriter.Create("reservations.xml", _settings))
            {
                writer.WriteStartElement("reservations");
                writer.WriteEndElement();
            }
        }

        public void AddNewReservationElement(Reservation element)
        {
            XDocument xmlRoomTypeDoc = XDocument.Load("reservations.xml");

            if (xmlRoomTypeDoc != null)
            {
                if (xmlRoomTypeDoc.Root == null)
                {
                    CreateReservationFile();
                }
                xmlRoomTypeDoc.Root.Add(new XElement("reservation",
                                    new XElement("checkindate", element.CheckInDate),
                                    new XElement("checkoutdate", element.CheckOutDate),
                                    new XElement("clientid", element.ClientId),
                                    new XElement("roomid", element.RoomId)));
            }

            xmlRoomTypeDoc.Save("reservations.xml");
        }

    }

}

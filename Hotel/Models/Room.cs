using System;
using System.Collections.Generic;
using Hotel.Models.Enum;

namespace Hotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public List<Options> Options { get; set; } = new List<Options>();
        public int TypeId { get; set; }

        public override string ToString()
        {
            return $"Room {Number}, {Capacity} {(Capacity > 1 ? "people" : "person")}";
        }
    }
}

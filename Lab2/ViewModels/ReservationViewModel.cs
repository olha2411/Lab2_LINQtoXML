using Hotel.Models;

namespace Lab2.ViewModels
{
    public class ReservationViewModel
    {
        public Client Client { get; set; }
        public Room Room { get; set; }

        public Reservation Reservation { get; set; }

        public RoomType RoomType { get; set; }
    }
}

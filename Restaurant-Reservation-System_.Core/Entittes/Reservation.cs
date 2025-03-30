namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Time { get; set; } = null!;
        public int NumberOfPeople { get; set; }
    }
}

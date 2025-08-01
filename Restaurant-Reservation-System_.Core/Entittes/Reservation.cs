﻿namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Reservation:BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool IsDone { get; set; } = false;
        //public ICollection<Product> Products { get; set; } = [];
        //public string PhoneNumber { get; set; } = null!;

    }
}

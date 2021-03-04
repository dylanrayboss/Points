using System;

namespace Points.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Payer { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

namespace BE
{
    public class Hit
    {

        public int Id { get; set; } 
        public Event Event { get; set; }
        public double RealLatitude { get; set; }
        public double RealLongitude { get; set; }
        public double ApproxLatitude { get; set; }
        public double ApproxLongitude { get; set; }
    }
    }
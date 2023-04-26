namespace TeamVaxxers
{


    public class Beacon
    {
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }
        public double D4 { get; set; }
        public long Id { get; set; }
        public long Time { get; set; }

    }
    public class Beacons
    {
        public int Total { get; set; }
        public Beacon[] data { get; set; }

    }
   
    public class Response

    {
        public bool success { get; set; }
        public int index { get; set; }
        public string message { get; set; }
        public string received { get; set; }
        public string companyId { get; set; }
        public string color { get; set; }
        public int[] infected { get; set; }

    }
}


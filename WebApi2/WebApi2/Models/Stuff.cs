using System;
namespace WebApi2.Models
{
    public class Stuff
    {
        public int Id { get; set; }
        public int Category_Id{ get; set; }
        public string Brand { get; set; }
        public string Seria { get; set; }
		public string Model { get; set; }
		public int Count { get; set; }
        public decimal Price { get; set; }
    }
}

    
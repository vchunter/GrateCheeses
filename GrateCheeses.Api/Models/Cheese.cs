using System;

namespace GrateCheeses.Api.Models
{
    public class Cheese
    {
        public int CheeseId { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Type { get; set; }
        public float PricePerKg { get; set; }
        public string ImageName { get; set; }
    }
}

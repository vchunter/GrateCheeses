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
        public float CostByWeight (float weight)
        {
            //TODO: assumption is that the weight is in grams, so divide the weight by 1000 to calculate the correct price per kg
            return (weight/1000) * PricePerKg;
        }
    }
}

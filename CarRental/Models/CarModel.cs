namespace CarRental.Models
{
    public class CarModel
    {
        public int CarId { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

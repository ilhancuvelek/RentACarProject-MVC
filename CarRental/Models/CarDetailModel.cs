﻿using Entities.Concrete;
using System.Collections.Generic;

namespace CarRental.Models
{
    public class CarDetailModel
    {
        public int CarId { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public decimal DailyPrice { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDetailDto:IDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string ColorName { get; set; }
        public int BrandID { get; set; }
        public int ColorID { get; set; }
        public string BrandName { get; set; }
        public decimal DailyPrice { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
    }
}

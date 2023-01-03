using System.Collections.Generic;
using System;
using Entities.Concrete;

namespace CarRental.Models
{

    public class PageInfo
    {
        public int TotalItems { get; set; }//Veri tabanında kaç ürün var (filtrlenmemiş)
        public int ItemsPerPage { get; set; }//sayfa başı kaç ürün
        public int CurrentPage { get; set; }
        public string CurrentBrand { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
    public class CarListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Car> Cars { get; set; }

    }

}

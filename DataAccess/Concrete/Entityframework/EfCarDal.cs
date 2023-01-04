using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.Entityframework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context=new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join clr in context.Colors on c.ColorId equals clr.ColorId
                             select new CarDetailDto { CarId = c.CarId, BrandName = b.Name, ColorName = clr.Name, DailyPrice = c.DailyPrice ,ModelYear=c.ModelYear,ImageUrl=c.ImageUrl,Description=c.Description,CarName=c.CarName,IsApproved=c.IsApproved,IsHome=c.IsHome};
                return result.ToList();

            }
        }
        public Car GetCarDetailsById(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                return context.Cars.Where(c => c.CarId == id).Include(c => c.Brand).ThenInclude(c => c.BrandColors).ThenInclude(c => c.Color).FirstOrDefault();
               
            }
        }
        //(sayfalama) kategori seçildikten sonra sayfalama yapılmak istenirse diye kategoriye göre yaptık kategori yoksa normal
        public int GetCountByBrand(string brand)
        {
            using (var context = new RentACarContext())
            {
                var cars = context.Cars.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(brand))
                {
                    cars = cars
                    .Include(p => p.Brand)
                    .ThenInclude(b => b.Cars).Where(i => i.Brand.Url == brand);
                }
                return cars.Count();

            }
        }
        //Kategori adına göre ürün filtreleme
        public List<Car> GetCarsByBrand(string name, int page, int pageSize)
        {
            using (var context = new RentACarContext())
            {
                var cars = context.Cars.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    cars = cars
                    .Include(p => p.Brand)
                    .ThenInclude(b => b.Cars).Where(i => i.Brand.Url == name);
                }
                return cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }
        }
        //anasayfa ürünleri
        public List<Car> GetHomePageCars()
        {
            using (var context = new RentACarContext())
            {
                return context.Cars.Where(i => i.IsApproved && i.IsHome).ToList();
            }
        }

        public List<Car> GetSearchResult(string searchString)
        {
            using (var context = new RentACarContext())
            {
                var cars= context.Cars.Where(i => i.IsApproved && (((i.Brand.Name.ToLower().Contains(searchString))) ||((i.Description.ToLower().Contains(searchString))) || ((i.Color.Name.ToLower().Contains(searchString))))).AsQueryable();
                return cars.ToList();
            }
        }

        public Car GetProductDetails(string carNameUrl)
        {
            using (var context = new RentACarContext())
            {
                var car=context.Cars.Where(c=>c.Url==carNameUrl).Include(c => c.Brand).ThenInclude(c => c.BrandColors).ThenInclude(c => c.Color).FirstOrDefault();
                return car;
            }
        }
    }
}

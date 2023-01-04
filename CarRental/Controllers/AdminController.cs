using Business.Abstract;
using Business.Concrete;
using CarRental.Models;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        private ICarService _carService;
        private IBrandService _brandService;
        BrandManager brandManager = new BrandManager(new EfBrandDal());
        ColorManager colorManager=new ColorManager(new EfColorDal());
        public AdminController(ICarService carService, IBrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;   
        }
        public IActionResult CarList()
        {
            var carListViewModel = new CarDetailList()
            {
               Cars=_carService.GetCarDetails().Data
            };
            return View(carListViewModel);
        }
        [HttpGet]
        public IActionResult CarAdd()
        {
            List<SelectListItem> bradValues = (from x in brandManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.BrandId.ToString() }).ToList();
            ViewBag.Brands = bradValues;
            List<SelectListItem> colorValues = (from x in colorManager.GetAll().Data select new SelectListItem { Text = x.Name, Value = x.ColorId.ToString() }).ToList();
            ViewBag.Colors = colorValues;
            return View();
        }
        [HttpPost]
        public IActionResult CarAdd(CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new Car()
                {
                   CarName=carModel.CarName,
                   DailyPrice=carModel.DailyPrice,
                   ImageUrl=carModel.ImageUrl,
                   Description=carModel.Description,
                   Url = carModel.Url,
                   ModelYear=carModel.ModelYear,
                   Brand=carModel.Brand,
                   Color=carModel.Color
                };

                if (_carService.Add(entity).Success) // iş kuralı için create yi bool a çevirdik
                {
                    //bilgilendirme mesajı
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kayıt eklendi",
                        Message = "kayıt eklendi.",
                        AlertType = "success"
                    });
                    //bilgilendirme mesajı -son-
                    return RedirectToAction("CarList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "hata",
                    AlertType = "success"
                });
                return View(carModel);

            }
            return View(carModel);
        }
    }
}

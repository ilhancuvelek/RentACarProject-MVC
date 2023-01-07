using Business.Abstract;
using Business.Concrete;
using CarRental.Models;
using Core.Entities;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        private ICarService _carService;
        private IBrandService _brandService;
        BrandManager brandManager = new BrandManager(new EfBrandDal());
        ColorManager colorManager = new ColorManager(new EfColorDal());
        public AdminController(ICarService carService, IBrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;
        }
        public IActionResult CarList()
        {
            var carListViewModel = new CarDetailList()
            {
                Cars = _carService.GetCarDetails().Data
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
                    CarName = carModel.CarName,
                    DailyPrice = carModel.DailyPrice,
                    ImageUrl = carModel.ImageUrl,
                    Description = carModel.Description,
                    Url = carModel.Url,
                    ModelYear = carModel.ModelYear,
                    Brand = carModel.Brand,
                    Color = carModel.Color
                };

                if (_carService.Add(entity).Success)
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
        [HttpGet]
        public IActionResult CarEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = _carService.GetCarDetailsById((int)id);
            if (car == null)
            {
                return NotFound();
            }
            var carModel = new CarModel
            {
                CarName = car.Data.CarName,
                CarId = car.Data.CarId,
                DailyPrice = car.Data.DailyPrice,
                Description = car.Data.Description,
                Brand = car.Data.Brand,
                Color = car.Data.Color,
                ImageUrl = car.Data.ImageUrl,
                IsApproved = car.Data.IsApproved,
                IsHome = car.Data.IsHome,
                ModelYear = car.Data.ModelYear,
                Url = car.Data.Url
            };
            
            List<SelectListItem> bradValues = (from x in brandManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.BrandId.ToString() }).ToList();
            ViewBag.Brands = bradValues;
            List<SelectListItem> colorValues = (from x in colorManager.GetAll().Data select new SelectListItem { Text = x.Name, Value = x.ColorId.ToString() }).ToList();
            ViewBag.Colors = colorValues;
            return View(carModel);
        }
        [HttpPost]
        public async Task<IActionResult> CarEdit(CarModel carModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(carModel.CarId);
                var car = _carService.GetById(carModel.CarId).Data;
                
                car.CarName = carModel.CarName;
                car.Url = carModel.Url;
                car.DailyPrice = carModel.DailyPrice;
                car.Description = carModel.Description;
                car.Color = carModel.Color;
                car.Brand = carModel.Brand;
                car.ModelYear = carModel.ModelYear;
                car.IsApproved = carModel.IsApproved;
                car.IsHome = carModel.IsHome;

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    car.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
          
                if (_carService.Update(car).Success) // iş kuralı için update i bool a çevirdik
                {
                    //bilgilendirme mesajı
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kayıt güncellendi",
                        Message = "kayıt güncellendi.",
                        AlertType = "success"
                    });
                    //bilgilendirme mesajı -son-
                    return RedirectToAction("CarList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "Hata",
                    AlertType = "danger"
                });
                
            }
            return View(carModel);
        }
        public IActionResult DeleteCar(int carId)
        {
            var car = _carService.GetById(carId);
            if (car.Data != null)
            {
                _carService.Delete(car.Data);
            }

            //bilgilendirme mesajı
            TempData.Put("message", new AlertMessage()
            {
                Title = "kayıt silindi",
                Message = "kayıt silindi.",
                AlertType = "danger"
            });
            //bilgilendirme mesajı -son-
            return RedirectToAction("CarList");
        }

    }
}

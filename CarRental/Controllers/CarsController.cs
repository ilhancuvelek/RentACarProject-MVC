using Business.Abstract;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CarRental.Models.CarListViewModel;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }
        [Route("cars")]
        public IActionResult CarList(string brand, int page = 1)
        {
            //sayfalama
            const int pageSize = 3;
            var carViewModel = new CarListViewModel
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _carService.GetCountByBrand(brand),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentBrand = brand
                },
                Cars = _carService.GetCarsByBrand(brand, page, pageSize)
            };
            return View(carViewModel);
        }
        [Route("car-detail/{id}")]
        public IActionResult CarDetails(int id)
        {
            var car=_carService.GetCarDetailsById(id);
            if (car.Success)
            {
                return View(new CarDetailModel { 
                    BrandName=car.Data.Brand.Name,
                    ColorName=car.Data.Color.Name,
                    DailyPrice=car.Data.DailyPrice,
                    Description=car.Data.Description,
                    ImageUrl = car.Data.ImageUrl,
                    ModelYear = car.Data.ModelYear
                }); 
            }
            return RedirectToAction("CarList");
        }
        [Route("search")]
        public IActionResult Search(string q)
        {
            var carListView = new CarListViewModel
            {
                Cars=_carService.GetSearchResult(q)
            };
            return View(carListView);
        }

    }
}

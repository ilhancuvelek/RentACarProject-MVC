using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private IBrandService _brandService;
        public BrandsViewComponent(IBrandService brandService)
        {
            _brandService= brandService;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["brand"] != null)
            {
                ViewBag.SelectedBrand = RouteData?.Values["brand"];
            }

            return View(_brandService.GetAll());


        }
    }
}

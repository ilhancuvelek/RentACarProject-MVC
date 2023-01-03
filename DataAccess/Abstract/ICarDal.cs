using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        Car GetCarDetailsById(int id);
        //Kategori adına göre ürün filtreleme
        List<Car> GetCarsByBrand(string name, int page, int pageSize);

        //sayfalama
        int GetCountByBrand(string brand);
        List<Car> GetHomePageCars();
        List<Car> GetSearchResult(string searchString);


    }
}

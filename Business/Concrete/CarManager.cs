using Business.Abstract;
using Business.BusinesAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Car car)
        {
            IResult result=BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId));
            if (result!=null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
        //[CacheAspect]
        //[PerformanceAspect(5)]//bu metot 5 sn geçerse uyar
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int Bid)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == Bid), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetAllByColorId(int Cid)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == Cid), Messages.CarListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult< Car > (_carDal.Get(c => c.CarId == id), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarListed);
        }

        public IDataResult<Car> GetCarDetailsById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.GetCarDetailsById(id), Messages.CarListed);
        }

        public List<Car> GetCarsByBrand(string name, int page, int pageSize)
        {
            return _carDal.GetCarsByBrand(name,page,pageSize);
        }

        public int GetCountByBrand(string brand)
        {
            return _carDal.GetCountByBrand(brand);
        }

        public List<Car> GetHomePageCars()
        {
            return _carDal.GetHomePageCars();
        }

        public List<Car> GetSearchResult(string searchString)
        {
            return _carDal.GetSearchResult(searchString);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        // BUSİNESS RULES
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result>=10)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }
    }
}

using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<string>> GetUserClaims(int userId);
        IDataResult<User> GetByMail(string email);
    }
}

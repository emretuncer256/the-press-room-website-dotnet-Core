using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities.Concrete;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Core.Utilities.Security.JWT;
using TPR.Entities.DTOs;

namespace TPR.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto user);
        IDataResult<User> Login(UserForLoginDto user);
        IResult UserExists(string email);
        IResult AddRole(User user, OperationClaim claim);
        IDataResult<List<Claim>> GetClaims(User user);
    }
}

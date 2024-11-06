using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TPR.Business.Abstract;
using TPR.Core.Entities.Concrete;
using TPR.Core.Utilities.Business;
using TPR.Core.Utilities.Results;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Core.Utilities.Security.Hashing;
using TPR.Core.Utilities.Security.JWT;
using TPR.Entities.DTOs;

namespace TPR.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IOperationClaimService _operationClaimService;

        public AuthManager(IUserService userService, IOperationClaimService operationClaimService)
        {
            _userService = userService;
            _operationClaimService = operationClaimService;
        }

        public IResult AddRole(User user, OperationClaim claim)
        {
            _userService.AddRole(user, claim);
            return new SuccessResult($"{claim.Name} role added to user {user.Id}");
        }

        public IDataResult<List<Claim>> GetClaims(User user)
        {
            var operationClaims = _userService.GetClaims(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(operationClaims.Select(c => new Claim(ClaimTypes.Role, c.Name)));
            return new SuccessDataResult<List<Claim>>(claims);
        }

        public IDataResult<User> Login(UserForLoginDto user)
        {
            var userToCheck = _userService.GetByMail(user.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User not found");
            }
            if (!HashingHelper.VerifyPasswordHash(user.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Invalid password");
            }
            return new SuccessDataResult<User>(userToCheck, "Logged in");
        }

        public IDataResult<User> Register(UserForRegisterDto user)
        {
            var result = BusinessRules.Run(
                    UserExists(user.Email),
                    CheckPasswordsMatch(user.Password, user.RetypePassword)
                );
            if (result != null) return new DataResult<User>(result.Success, result.Message);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            var userForRegister = new User
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Birthdate = user.Birthdate,
                CreatedAt = DateTime.Now,
                Status = true
            };
            _userService.AddUser(userForRegister);
            _userService.AddRole(userForRegister, _operationClaimService.GetByName("User").Data);
            return new SuccessDataResult<User>(userForRegister, "Registered");

        }

        private IResult CheckPasswordsMatch(string password, string retypePassword)
        {
            if (password == retypePassword)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Password does not match");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("User is already exists");
            }
            return new SuccessResult();
        }
    }
}

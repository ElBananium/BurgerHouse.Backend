using BurgerHouse.Services.WorkersService;
using Data;
using Data.Models;
using Data.Models.NonDb;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BurgerHouse.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private IWorkersService _workersService;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IWorkersService workersService)
        {
            _context = context;
            _configuration = configuration;
            _workersService = workersService;
        }

        public void ConfirmUser(int userId)
        {
            _context.Users.First(x => x.Id == userId).IsConfirmed = true;

            _context.SaveChanges();
        }

        public int GetUserId(string phoneNumber)
        {
            return _context.Users.First(x => x.MobilePhone == phoneNumber).Id;
        }

        public bool isUserExist(string phoneNumber)
        {
            return _context.Users.Any(x => x.MobilePhone == phoneNumber);
        }

        public bool isUserRegistratedAndConfirmed(string phoneNumber)
        {
            if (isUserExist(phoneNumber))
            {
                return _context.Users.First(x => x.MobilePhone == phoneNumber).IsConfirmed;
            }
            return false;
        }

        public string GetUserToken(int userId)
        {

            var user = _context.Users.First(x => x.Id == userId);

            var claims = new List<Claim>();

            claims.Add(new Claim("Id", user.Id.ToString()));
            if (user.IsWorker)
            {
                claims.Add(new Claim("isWorker", user.IsWorker.ToString()));

                claims.Add(new Claim("RestrauntId", _workersService.GetRestrauntIdByWorkerId(user.Id).ToString()));
            }
            
            
            
            claims.Add(new Claim("isAdmin", user.IsAdmin.ToString()));
            
            var jwt = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],

                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"])), SecurityAlgorithms.HmacSha256));



            return new JwtSecurityTokenHandler().WriteToken(jwt);


        }



        public void RegistrateUser(string phoneNumber)
        {
            var user = new User() { IsAdmin = false, IsConfirmed = false, IsWorker = false, MobilePhone = phoneNumber };
            _context.Users.Add(user);
            _context.SaveChanges();

        }
    }
}

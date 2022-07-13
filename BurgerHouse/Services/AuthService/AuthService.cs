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



        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public bool CanRegistrateThisUser(string phoneNumber)
        {
            var isUserExist = _context.Users.Any(x => x.MobilePhone == phoneNumber);

            if (isUserExist)
            {
                return !_context.Users.First(x => x.MobilePhone == phoneNumber).IsConfirmed;
            }


            return !isUserExist;
        }

        public UserPermissions GetPermissions(List<Claim> claims)
        {
            return new UserPermissions()
            {
                UserId = int.Parse(claims.First(x => x.Type == "Id").Value),
                CanMakeOrders = !bool.Parse(claims.First(x => x.Type == "isWorker").Value) || bool.Parse(claims.First(x => x.Type == "isAdmin").Value),
                CanViewOrders = bool.Parse(claims.First(x => x.Type == "isWorker").Value) || bool.Parse(claims.First(x => x.Type == "isAdmin").Value)
            };
        }

        public int GetUserId(string phoneNumber)
        {
            return _context.Users.First(x => x.MobilePhone == phoneNumber).Id;
        }

        public string GetUserToken(int userId)
        {

            var user = _context.Users.First(x => x.Id == userId);

            var claims = new List<Claim>();

            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("isWorker", user.IsWorker.ToString()));
            claims.Add(new Claim("isAdmin", user.IsAdmin.ToString()));

            var jwt = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],

                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"])), SecurityAlgorithms.HmacSha256));



            return new JwtSecurityTokenHandler().WriteToken(jwt);


        }

        public string RegistrateUser(string phoneNumber)
        {
            var user = new User() { IsAdmin = false, IsConfirmed = false, IsWorker = false, MobilePhone = phoneNumber };
            _context.Users.Add(user);
            _context.SaveChanges();

            return GetUserToken(user.Id);
        }
    }
}

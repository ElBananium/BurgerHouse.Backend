using BurgerHouse.Services.AuthService;
using BurgerHouse.Services.ConfirmService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerHouse.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class authController : ControllerBase
    {
        private IAuthService _authService;
        private IConfirmService _confirmService;

        [HttpGet("getcode/{phoneNumber}")]

        public ActionResult GetCode(string phoneNumber)
        {
            if (!_authService.isUserExist(phoneNumber))
            {
                _authService.RegistrateUser(phoneNumber);
            }
            var id = _authService.GetUserId(phoneNumber);
            _confirmService.SendCode(id);

            return Ok();
        }

        [HttpGet("login/{phoneNumber}/{code}")]
        public ActionResult<string> Login(string phoneNumber, string code)
        {
            if (!_authService.isUserExist(phoneNumber)) return BadRequest();

            var id = _authService.GetUserId(phoneNumber);

            if (_confirmService.IsCodeRight(id, code))
            {
                if (!_authService.isUserRegistratedAndConfirmed(phoneNumber)) _authService.ConfirmUser(id);

                return Ok(_authService.GetUserToken(id));
            
            }
            return BadRequest();
        }

        public authController(IAuthService authService, IConfirmService confirmService)
        {
            _authService = authService;

            _confirmService = confirmService;
        }


        
    }
}

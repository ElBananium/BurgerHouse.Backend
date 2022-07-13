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

        [HttpGet("registrate/{phoneNumber}")]
        public ActionResult<string> Registrate(string phoneNumber) 
        {
            if (!_authService.CanRegistrateThisUser(phoneNumber)) return BadRequest();

            return Ok(_authService.RegistrateUser(phoneNumber));

        }

        [HttpGet("login/{phoneNumber}")]
        public ActionResult Login(string phoneNumber)
        {
            var info = _authService.GetUserId(phoneNumber);

            _confirmService.SendLoginCode(info);

            return Ok();
        }

        [HttpGet("login/code/{phoneNumber}/{code}")]
        public ActionResult<string> LoginCode(string phoneNumber, string code)
        {
            var info = _authService.GetUserId(phoneNumber);


            var token =_confirmService.LoginByCode(info, code);

            

            return Ok(token);
        }

        [Authorize]
        [HttpGet("confirm/send")]
        public ActionResult SendConfirmCode() 
        {
           var info =  _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());
            if (_confirmService.IsConfirmedUser(info.UserId)) return BadRequest();

            _confirmService.SendConfirmCode(info.UserId);

            return Ok();
        
        }

        [Authorize]
        [HttpGet("confirm/{code}")]
        public ActionResult<string> SendCode(string code)
        {
            var info = _authService.GetPermissions(HttpContext.User.Identities.First().Claims.ToList());
            if (_confirmService.IsConfirmedUser(info.UserId)) return BadRequest();


            _confirmService.ConfirmByCode(info.UserId, code);

            if (!_confirmService.IsConfirmedUser(info.UserId)) return BadRequest();

            return Ok(_authService.GetUserToken(info.UserId));

        }

        public authController(IAuthService authService, IConfirmService confirmService)
        {
            _authService = authService;

            _confirmService = confirmService;
        }


        
    }
}

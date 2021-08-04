using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.API.Models;
using TodoApp.API.Repositories;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AccountsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var result = _userRepository.Login(email, password);
            if (result != null)
            {
                HttpContext.Session.SetString("id", result.Id);
                HttpContext.Session.SetString("fullname", result.Name + "" + result.Surname);
                return Ok("giriş yapıldı");
            }
            return BadRequest("Email veya şifre yanlış");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok("Çıkış yapıldı");
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            var result = await _userRepository.SignUp(user);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

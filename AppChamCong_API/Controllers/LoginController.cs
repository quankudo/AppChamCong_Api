using AppChamCong_API.Dto.Request;
using AppChamCong_API.Dto.Respone;
using AppChamCong_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppChamCong_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly QuanLyChamCongContext _context;

        public LoginController(IConfiguration config, QuanLyChamCongContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            if (userRegister == null || !ModelState.IsValid)
            {
                return BadRequest(new ResponeApplication<string>
                {
                    message = "Dữ liệu không hợp lệ",
                    data = null,
                    success = false
                });
            }

            Nguoidung user = new Nguoidung
            {
                Email = userRegister.email,
                Hovaten = userRegister.email.Split("@")[0],
                Passwords = userRegister.password
            };

            try
            {
                _context.Nguoidungs.Add(user);
                _context.SaveChanges();

                return Ok(new ResponeApplication<string>
                {
                    message = "Đăng ký tài khoản thành công!",
                    data = null,
                    success = true
                });
            }
            catch (Exception ex)
            {
                // Phản hồi lỗi máy chủ
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponeApplication<string>
                {
                    message = "Máy chủ gặp lỗi. Vui lòng thử lại sau!",
                    data = null,
                    success = false
                });
            }
        }


        private string Generate(Nguoidung user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Iduser.ToString()), // Lưu ID ở đây
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user?.Hovaten ?? "not name")
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(1000),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Nguoidung Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.Nguoidungs.FirstOrDefault(o => o.Email.ToLower() == userLogin.email.ToLower() && o.Passwords == userLogin.password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}

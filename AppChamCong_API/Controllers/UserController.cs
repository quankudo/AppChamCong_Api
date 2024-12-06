using AppChamCong_API.Dto.Request;
using AppChamCong_API.Dto.Respone;
using AppChamCong_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppChamCong_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QuanLyChamCongContext _context;

        public UserController(QuanLyChamCongContext context)
        {
            _context = context;
        }

        [HttpGet("getMyInfo")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            Nguoidung currentUser = GetCurrentUser();

            UserRespone respone = new UserRespone();
            respone.Iduser = currentUser.Iduser;
            respone.Email = currentUser.Email;
            respone.Hovaten = currentUser.Hovaten;
            respone.Trangthai = currentUser.Trangthai;
            respone.Sdt = currentUser.Sdt;

            return Ok(new ResponeApplication<UserRespone>
            {
                message = "Thong tin ca nhan",
                data = respone,
                success = true
            });
        }
        [HttpPost("update")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUser user)
        {
            
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(new ResponeApplication<string>
                {
                    message = "Dữ liệu không hợp lệ",
                    data = null,
                    success = false
                });
            }
            if(user.id != GetCurrentUser().Iduser)
            {
                return BadRequest();
            }
            try
            {
                Nguoidung nguoidung = _context.Nguoidungs.Where(x => x.Iduser == user.id).FirstOrDefault();
                if (user.HoTen != null)
                {
                    nguoidung.Hovaten = user.HoTen;
                }
                if (user.SDT != null)
                {
                    nguoidung.Sdt = user.SDT;
                }
                _context.SaveChanges();
                return Ok(new ResponeApplication<UserRespone>
                {
                    message = "Cap nhat thanh cong!",
                    data = new UserRespone(user.id, user.HoTen, nguoidung.Email, user.SDT),
                    success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        private Nguoidung GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new Nguoidung
                {
                    Iduser = Int16.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Hovaten = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value
                };
            }
            return null;
        }
    }
}

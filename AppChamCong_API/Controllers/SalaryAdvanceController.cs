using AppChamCong_API.Dto.Request;
using AppChamCong_API.Dto.Respone;
using AppChamCong_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AppChamCong_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryAdvanceController : ControllerBase
    {
        private readonly QuanLyChamCongContext _context;
        public SalaryAdvanceController(QuanLyChamCongContext context)
        {
            _context = context;
        }
        [HttpGet("getSalaryAdvance/{id}")]
        [Authorize]
        public IActionResult getSalaryAdvance(int id)
        {
            int Iduser = getIdUser();
            try
            {
                List<SalaryAdvance> list = _context.Ungluongs
               .Join(_context.Chitietcongviecs,
                     ul => ul.Idctcv,
                     ctcv => ctcv.Idctcv,
                     (ul, ctcv) => new { UngLuong = ul, ChiTietCongViec = ctcv })
               .Where(x => x.ChiTietCongViec.Idnhanvien == Iduser && x.ChiTietCongViec.Idcv == id)
               .Select(x => new SalaryAdvance
               {
                   Idungluong = x.UngLuong.Idungluong,
                   Idctcv = x.UngLuong.Idctcv,
                   Sotienung = x.UngLuong.Sotienung,
                   Hinhanh = x.UngLuong.Hinhanh,
                   Ghichu = x.UngLuong.Ghichu,
                   Ngayung = x.UngLuong.Ngayung
               })
               .ToList();
                return Ok(new ResponeApplication<List<SalaryAdvance>>
                {
                    data = list,
                    message = "Danh sach ung luong",
                    success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponeApplication<string>
                {
                    message = "Máy chủ gặp lỗi. Vui lòng thử lại sau!",
                    data = null,
                    success = false
                });
            }
        }

        [HttpPost("add/{id}")]
        [Authorize]
        public IActionResult add([FromBody] SalaryAdvanceReq salary)
        {
            if (salary == null || !ModelState.IsValid)
            {
                return BadRequest(new ResponeApplication<string>
                {
                    message = "Dữ liệu không hợp lệ",
                    data = null,
                    success = false
                });
            }
            Ungluong ungluong = new Ungluong();
            ungluong.Sotienung = salary.Sotienung;
            ungluong.Ngayung = salary.Ngayung;
            ungluong.Ghichu = salary.Ghichu;
            ungluong.Hinhanh = salary.Hinhanh;
            ungluong.Idctcv = salary.Idctcv;
            try
            {
                ungluong.IdctcvNavigation = _context.Chitietcongviecs.Where(x => x.Idctcv == salary.Idctcv).FirstOrDefault();

                _context.Ungluongs.Add(ungluong);
                _context.SaveChanges();
                return Ok(new ResponeApplication<SalaryAdvance>
                {
                    data = new SalaryAdvance
                    {
                        Idctcv = salary.Idctcv,
                        Sotienung = salary.Sotienung,
                        Ngayung = salary.Ngayung,
                        Ghichu = salary.Ghichu,
                        Hinhanh = salary.Hinhanh
                    },
                    success = true,
                    message = "Them ung luong thanh cong!"
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponeApplication<string>
                {
                    message = "Máy chủ gặp lỗi. Vui lòng thử lại sau!",
                    data = null,
                    success = false
                });
            }
        }


        private int getIdUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int Iduser = 0;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var nameIdentifierClaim = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(nameIdentifierClaim, out int idUser))
                {
                    Iduser = idUser;
                }
                else
                {
                    Iduser = 0;
                }
            }
            return Iduser;
        }
    }
}

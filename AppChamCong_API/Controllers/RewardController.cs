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
    public class RewardController : ControllerBase
    {
        private readonly QuanLyChamCongContext _context;
        public RewardController(QuanLyChamCongContext context)
        {
            _context = context;
        }

        [HttpGet("getAll/{id}")]
        [Authorize]
        public IActionResult getAll(int id)
        {
            int Iduser = getIdUser();

            try
            {
                List<RewardRespone> list = _context.Phucaps
               .Join(_context.Chitietcongviecs,
                     ul => ul.Idctcv,
                     ctcv => ctcv.Idctcv,
                     (ul, ctcv) => new { PhuCap = ul, ChiTietCongViec = ctcv })
               .Where(x => x.ChiTietCongViec.Idnhanvien == Iduser && x.ChiTietCongViec.Idcv == id)
               .Select(x => new RewardRespone
               {
                   Idpc = x.PhuCap.Idpc,
                   Idctcv = x.PhuCap.Idctcv,
                   Sotien = x.PhuCap.Sotien,
                   Ngaytao = x.PhuCap.Ngaytao,
                   Ghichu = x.PhuCap.Ghichu,
                   Idloaipc = x.PhuCap.Idloaipc
               })
               .ToList();
                return Ok(new ResponeApplication<List<RewardRespone>>
                {
                    data = list,
                    message = "Danh sach phu cap",
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
        public IActionResult add([FromBody] RewardReq overtime)
        {
            if (overtime == null || !ModelState.IsValid)
            {
                return BadRequest(new ResponeApplication<string>
                {
                    message = "Dữ liệu không hợp lệ",
                    data = null,
                    success = false
                });
            }
            Phucap phucap = new Phucap();
            phucap.Idpc = overtime.Idpc;
            phucap.Idctcv = overtime.Idctcv;
            phucap.Sotien = overtime.Sotien;
            phucap.Ngaytao = overtime.Ngaytao;
            phucap.Ghichu = overtime.Ghichu;
            phucap.Idloaipc = overtime.Idloaipc;
            try
            {
                phucap.IdctcvNavigation = _context.Chitietcongviecs.Where(x => x.Idctcv == overtime.Idctcv).FirstOrDefault();

                _context.Phucaps.Add(phucap);
                _context.SaveChanges();
                return Ok(new ResponeApplication<RewardRespone>
                {
                    data = new RewardRespone
                    {
                        Idpc = overtime.Idpc,
                        Idctcv = overtime.Idctcv,
                        Sotien = overtime.Sotien,
                        Ngaytao = overtime.Ngaytao,
                        Ghichu = overtime.Ghichu,
                        Idloaipc = overtime.Idloaipc
                    },
                    success = true,
                    message = "Them phu cap thanh cong!"
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

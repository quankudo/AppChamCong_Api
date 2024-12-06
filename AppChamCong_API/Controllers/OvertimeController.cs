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
    public class OvertimeController : ControllerBase
    {
        private readonly QuanLyChamCongContext _context;
        public OvertimeController(QuanLyChamCongContext context)
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
                List<OvertimeRespone> list = _context.Tangcas
               .Join(_context.Chitietcongviecs,
                     ul => ul.Idctcv,
                     ctcv => ctcv.Idctcv,
                     (ul, ctcv) => new { TangCa = ul, ChiTietCongViec = ctcv })
               .Where(x => x.ChiTietCongViec.Idnhanvien == Iduser && x.ChiTietCongViec.Idcv == id)
               .Select(x => new OvertimeRespone
               {
                   Idtangca = x.TangCa.Idtangca,
                   Idctcv = x.TangCa.Idctcv,
                   Sotien = x.TangCa.Sotien,
                   Sophut = x.TangCa.Sophut,
                   Ghichu = x.TangCa.Ghichu,
                   Heso = x.TangCa.Heso,
                   Hinhanh = x.TangCa.Hinhanh,
                   Ngaytangca = x.TangCa.Ngaytangca
               })
               .ToList();
                return Ok(new ResponeApplication<List<OvertimeRespone>>
                {
                    data = list,
                    message = "Danh sach tang ca",
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
        public IActionResult add([FromBody] OvertimeReq overtime)
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
            Tangca tangca = new Tangca();
            tangca.Idtangca = overtime.Idtangca;
            tangca.Idctcv = overtime.Idctcv;
            tangca.Sotien = overtime.Sotien;
            tangca.Sophut = overtime.Sophut;
            tangca.Ghichu = overtime.Ghichu;
            tangca.Heso = overtime.Heso;
            tangca.Hinhanh = overtime.Hinhanh;
            tangca.Ngaytangca = overtime.Ngaytangca;
            try
            {
                tangca.IdctcvNavigation = _context.Chitietcongviecs.Where(x => x.Idctcv == overtime.Idctcv).FirstOrDefault();

                _context.Tangcas.Add(tangca);
                _context.SaveChanges();
                return Ok(new ResponeApplication<OvertimeRespone>
                {
                    data = new OvertimeRespone
                    {
                        Idtangca = overtime.Idtangca,
                        Idctcv = overtime.Idctcv,
                        Sotien = overtime.Sotien,
                        Sophut = overtime.Sophut,
                        Ghichu = overtime.Ghichu,
                        Heso = overtime.Heso,
                        Hinhanh = overtime.Hinhanh,
                        Ngaytangca = overtime.Ngaytangca
                    },
                    success = true,
                    message = "Them tang ca thanh cong!"
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

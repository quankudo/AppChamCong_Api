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
    public class WorkController : ControllerBase
    {
        private readonly QuanLyChamCongContext _context;

        public WorkController(QuanLyChamCongContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("getWorkGroup")]
        public IActionResult getWorkGroup()
        {
            int Iduser = getIdUser();
            List<Congviec> list = _context.Congviecs.Where(x => x.IsGroup == true && x.Idchunhom == Iduser).ToList();
            List<Congviec> congViec = _context.Congviecs
                .Join(_context.Chitietcongviecs,
                      congViec => congViec.Idcv,
                      chiTiet => chiTiet.Idcv,
                      (congViec, chiTiet) => new { CongViec = congViec, ChiTiet = chiTiet })
                .Where(x => x.ChiTiet.Idnhanvien == Iduser)
                .Select(x => x.CongViec)
                .ToList();

            list.AddRange(congViec);
            List<WorkResponse> workResponse = new List<WorkResponse>();
            try
            {
                foreach (Congviec congviec in list)
                {
                    workResponse.Add(new WorkResponse
                    {
                        workName = congviec.Tencv,
                        dateSalary = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25).ToString("dd/MM/yyyy"),
                        method = _context.Hinhthucchamcongs.First(x => x.Idhtcc == congviec.Idhtcc).Tenhtcc,
                        accountEmployee = congviec.Chitietcongviecs.Count()
                    });
                }

                return Ok(new ResponeApplication<List<WorkResponse>>
                {
                    message ="Danh sach nhóm",
                    data = workResponse,
                    success = true
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("getWordPerson")]
        public IActionResult getWorkPerson()
        {
            int Iduser = getIdUser();
            List<Congviec> list = _context.Chitietcongviecs
                .Join(_context.Congviecs,
                      ctcv => ctcv.Idcv,       
                      cv => cv.Idcv,         
                      (ctcv, cv) => new       
                      {
                          ChitietCongviec = ctcv,
                          Congviec = cv
                      })
                .Where(x => x.ChitietCongviec.Idnhanvien == x.Congviec.Idchunhom && x.Congviec.Idchunhom==Iduser)
                .Select(x=>x.Congviec)// Điều kiện lọc
                .ToList();

            List<WorkResponse> workResponse = new List<WorkResponse>();
            try
            {
                foreach (Congviec congviec in list)
                {
                    workResponse.Add(new WorkResponse
                    {
                        workName = congviec.Tencv,
                        dateSalary = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25).ToString("dd/MM/yyyy"),
                        method = _context.Hinhthucchamcongs.First(x => x.Idhtcc == congviec.Idhtcc).Tenhtcc,
                        accountEmployee = congviec.Chitietcongviecs.Count()
                    });
                }

                return Ok(new ResponeApplication<List<WorkResponse>>
                {
                    message = "Danh sach nhóm",
                    data = workResponse,
                    success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

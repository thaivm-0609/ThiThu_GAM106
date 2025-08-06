using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThiThu.Models;

namespace ThiThu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThiThuController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ThiThuController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{playerID}")] //url: /api/thithu/{playerID}
        public async Task<IActionResult> GetQuest(int playerID)
        {
            try
            {

            } catch (Exception ex) {
            {
                    return BadRequest(ex.Message);
            }
        }
    }
}

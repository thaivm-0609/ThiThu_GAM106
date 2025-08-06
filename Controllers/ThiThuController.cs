using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var quests = await _db.PlayerQuest
                    .Include(q => q.Quest)    
                    .Where(q => q.playerID == playerID && q.completionStatus == false)                            
                    .Select(q => q.Quest)
                    .ToListAsync();
                if (quests.Count > 0)
                {
                    return Ok(quests);
                } else
                {
                    return BadRequest("Khoong tim thay");
                }
            } catch (Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpPost("newItem")]
        public async Task<IActionResult> CreateItem(string ItemName, string ItemType, int ItemValue)
        {
            try
            {
                var newItem = new Item();
                newItem.itemName = ItemName;
                newItem.itemType = ItemType;
                newItem.itemValue = ItemValue;

                _db.Item.Add(newItem);
                await _db.SaveChangesAsync();

                return Ok("Them moi thanh cong");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("finishQuest")]
        public async Task<IActionResult> CompleteQuest(int playerID, int questID)
        {
            try
            {
                var quest = await _db.PlayerQuest.FirstOrDefaultAsync(pq => pq.playerID == playerID
                    && pq.questID == questID
                    && pq.completionStatus == false);
                if (quest != null)
                {
                    quest.completionStatus = true;
                    await _db.SaveChangesAsync();

                    return Ok($"Cap nhat thanh cong quest {questID} cua player {playerID}");
                } else
                {
                    return BadRequest("Du lieu khong hop le");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getTransaction")]
        public async Task<IActionResult> GetTransaction(int playerID)
        {
            try
            {
                var trans = await _db.GTransaction.Where(t => t.playerID == playerID)
                        .OrderByDescending(o => o.transactionDate)
                        .ToListAsync();

                if (trans.Count > 0)
                {
                    return Ok(trans);
                } else
                {
                    return BadRequest("Khong co du lieu");
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

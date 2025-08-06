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
                    .Include(pq => pq.Quest) //lay them du lieu cua quest tu bang Quest
                    .Where(pq => pq.playerID == playerID && pq.completionStatus == false)
                    //.Select(pq => pq.Quest) //chi lay thong tin quest tu bang Quest
                    .ToListAsync();
                if (quests.Count > 0)
                {
                    return Ok(quests);
                }
                else
                {
                    return BadRequest("Khong co ban ghi nao phu hop");
                }
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("newItem")]
        public async Task<IActionResult> CreateItem(string itemName, string itemType, int itemValue)
        {
            try
            {
                var newItem = new Item(); //khoi tao object moi

                //gan gia tri cho cac thuoc tinh cua newItem
                newItem.itemName = itemName;
                newItem.itemType = itemType;
                newItem.itemValue = itemValue;

                _db.Item.Add(newItem);
                await _db.SaveChangesAsync(); //luu vao database

                return Ok("Them moi thanh cong");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("finishQuest")]
        public async Task<IActionResult> FinishQuest(int playerID, int questID)
        {
            try
            {
                //tim kiem ban ghi co playerID, questID va trang thai chua hoan thanh
                var quest = await _db.PlayerQuest.FirstOrDefaultAsync(pq => 
                    pq.playerID == playerID 
                    && pq.questID == questID 
                    && pq.completionStatus == false);

                if (quest != null) //kiem tra xem co ton tai ban ghi hop le hay ko
                { //neu co thi thuc hien update gia tri completionStatus = true;
                    quest.completionStatus = true;
                    await _db.SaveChangesAsync();

                    return Ok("Cap nhat thanh cong");
                } else
                { //khong co thi bao loi
                    return BadRequest("Khong tim thay du lieu nhiem vu");
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTransaction")]
        public async Task<IActionResult> GetTransaction(int playerID)
        {
            try
            {
                var trans = await _db.GTransaction
                    .Where(t => t.playerID == playerID) //tim ban ghi co playerID trung voi playerID truyen vao
                    .OrderByDescending(t => t.transactionDate) //sap xep ban ghi theo thu tu giam dan transactionDate
                    .ToListAsync();
                if (trans.Count > 0)
                {
                    return Ok(trans);
                } else
                {
                    return BadRequest("Khong tim thay du lieu");
                }
               
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("availableItem")]
        public async Task<IActionResult> GetAvailableItems(int playerID)
        {
            try
            {
                var player = await _db.Player.FirstOrDefaultAsync(p => p.playerId == playerID);
                var availableItems = await _db.Item
                    .Where(i => i.itemValue <= player.experiencePoints)
                    .ToListAsync();
                if (availableItems.Count > 0)
                {
                    return Ok(availableItems);
                } else
                {
                    return BadRequest("Khong co item co the mua");
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

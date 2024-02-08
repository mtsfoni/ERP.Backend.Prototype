using ERP.Backend.Models;
using ERP.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Backend.REST.Controllers
{    
    [Route("api/articles/{articleId}/prices")]
    [ApiController]
    public class PriceController
        (IPriceService priceService)
        : Controller
    {
        // GET: api/articles/5/prices
        [HttpGet]
        public async Task<ActionResult<List<Price>>> GetPricesByArticleId(int articleId)
        {
            try
            {
                var prices = await priceService.GetPricesByArticleId(articleId);
                if (prices == null) return NotFound();
                return Ok(prices);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/articles/5/prices/bydate?date=2023-01-01
        [HttpGet("bydate")]
        public async Task<ActionResult<Price>> GetPriceByDate(int articleId, DateTime date)
        {
            try
            {
                var price = await priceService.GetPriceByDate(articleId, date);
                if (price == null) return NotFound();
                return Ok(price);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST: api/articles/5/prices
        [HttpPost]
        public async Task<ActionResult<Price>> CreatePrice([FromBody] Price price)
        {
            try
            {
                var priceId = await priceService.CreatePrice(price);
                return CreatedAtAction(nameof(GetPriceByDate), new { articleId = price.ArticleId, date = price.ValidFrom }, price);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT: api/articles/5/prices/10
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            try
            {
                await priceService.UpdatePrice(price);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE: api/articles/5/prices/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(int id, int articleId)
        {
            try
            {
                await priceService.DeletePrice(id, articleId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

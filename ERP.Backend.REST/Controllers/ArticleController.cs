using ERP.Backend.Models;
using ERP.Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Backend.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController
        (IArticleService articleService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleList()
        {
            return await articleService.GetArticleList();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle([FromBody] Article article)
        {
            var id = await articleService.CreateArticle(article);
            return CreatedAtAction(nameof(GetArticleById), new { id }, article);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticleById(int id)
        {
            var article = await articleService.GetArticleById(id);
            if (article == null) return NotFound();
            return article;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await articleService.DeleteArticle(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateArticle([FromBody] Article article)
        {
            await articleService.UpdateArticle(article);
            return NoContent();
        }

    }
}

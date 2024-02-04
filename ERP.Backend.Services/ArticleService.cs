using ERP.Backend.Models;

namespace ERP.Backend.Services
{
    public class ArticleService
        (IRepository<Article> articleRepository) : IArticleService
    {
        private IRepository<Article> articleRepository { get; set; } = articleRepository;

        public async Task<List<Article>> GetArticleList()
        {
            return await articleRepository.GetAll();
        }

        public async Task<Article?> GetArticleById(int id)
        {
            return await articleRepository.GetById(id);
        }

        public async Task<int> CreateArticle(Article article)
        {
            article.Id = 0;
            await articleRepository.Add(article);
            return article.Id;
        }

        public async Task UpdateArticle(Article article)
        {
            await articleRepository.Update(article);
        }
        public async Task DeleteArticle(int id)
        {
            var article = await articleRepository.GetById(id) ?? throw new ArgumentException($"Article with id {id} not found");
            await articleRepository.Delete(article);
        }
    }
}

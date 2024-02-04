using ERP.Backend.Models;

namespace ERP.Backend.Services
{
    public interface IArticleService
    {
        Task<int> CreateArticle(Article article);
        Task DeleteArticle(int id);
        Task<Article?> GetArticleById(int id);
        Task<List<Article>> GetArticleList();
        Task UpdateArticle(Article article);
    }
}
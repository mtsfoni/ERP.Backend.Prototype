using ERP.Backend.Models;

namespace ERP.Backend.Services
{
    public interface IPriceService
    {
        Task<List<Price>> GetPricesByArticleId(int articleId);
        Task<Price?> GetPriceByDate(int articleId, DateTime date);        
        Task<int> CreatePrice(Price price);
        Task UpdatePrice(Price price);
        Task DeletePrice(int id, int articleId);
    }
}
using ERP.Backend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Services
{
    public class PriceService 
        (IRepository<Article> articleRepository)
        : IPriceService
    {
        private IRepository<Article> articleRepository { get; set; } = articleRepository;

        public async Task<int> CreatePrice(Price price)
        {
            var article = await articleRepository.GetById(price.ArticleId) ?? throw new ArgumentException($"Artikel mit Id {price.Id} nicht gefunden");
            price.ArticleId = article.Id;
            article.Prices.Add(price);
            await articleRepository.Update(article);
            return price.Id;
        }

        public async Task DeletePrice(int id, int articleId)
        {            
            var article = await articleRepository.GetById(articleId) ?? throw new ArgumentException($"Artikel mit Id {articleId} nicht gefunden");
            var price = article.Prices.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException($"Preis mit Id {id} bei Artikel mit Id {articleId} nicht gefunden");
            article.Prices.Remove(price);
            await articleRepository.Update(article);            
        }

        public async Task<Price?> GetPriceByDate(int articleId, DateTime date)
        {
            var article = await articleRepository.GetById(articleId) ?? throw new ArgumentException($"Artikel mit Id {articleId} nicht gefunden");
            var price = article.Prices.Where(p => p.ValidFrom <= date.Date).OrderBy(p => p.ValidFrom).FirstOrDefault();
            return price;
        }

        public async Task<List<Price>> GetPricesByArticleId(int articleId)
        {
            var article = await articleRepository.GetById(articleId) ?? throw new ArgumentException($"Artikel mit Id {articleId} nicht gefunden");
            return article.Prices;
        }


        public async Task UpdatePrice(Price price)
        {
            var article = await articleRepository.GetById(price.ArticleId) ?? throw new ArgumentException($"Artikel mit Id {price.ArticleId} nicht gefunden");
            var existingPrice = article.Prices.Find(p => p.Id == price.Id) ?? throw new ArgumentException($"Preis mit Id {price.Id} in Artikel mit Id {price.ArticleId} nicht gefunden");
            existingPrice.ValidFrom = price.ValidFrom;
            existingPrice.Amount = price.Amount;
            await articleRepository.Update(article);
        }
    }
}

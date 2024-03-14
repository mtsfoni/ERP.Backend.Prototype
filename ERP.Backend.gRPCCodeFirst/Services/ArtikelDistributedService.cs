using ERP.Backend.gRPCCodeFirst.Interfaces;
using ERP.Backend.Models;
using ERP.Backend.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Grpc;

namespace ERP.Backend.gRPCCodeFirst.Services
{
    public class ArtikelDistributedService
        (IArticleService articleService)
        : IArtikelDistributedService
    {
        public Task<List<Article>> GetArticleList(Empty request, CallContext context = default)
        {
            var retVal = articleService.GetArticleList();
            return retVal;
        }

        public Task<Article?> GetArticleById(Int32Value id, CallContext context = default)
        {
            return articleService.GetArticleById(id.Value);
        }

        public Task UpdateArticle(Article article, CallContext context = default)
        {
             return articleService.UpdateArticle(article);
        }

        public async Task<Int32Value> CreateArticle(Article article, CallContext context = default)
        {
            return new Int32Value { Value = await articleService.CreateArticle(article) };
        }

        public Task DeleteArticle(Int32Value id, CallContext context = default) 
        {
            return articleService.DeleteArticle(id.Value);
        }
    }
}

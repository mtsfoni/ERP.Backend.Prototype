using ERP.Backend.Models;
using ERP.Backend.Services;
using ERP.Backend.gRPC.Services;
using Grpc.Core;
using static ERP.Backend.gRPC.ArticleService;

namespace ERP.Backend.gRPC.Services
{
    public class ProtoArticleService
        (IArticleService articleService) 
        : ArticleServiceBase
    {
        private readonly IArticleService articleService = articleService;

        public override async Task<ArticleListResponse> GetArticleList(Empty request, ServerCallContext context)
        {
            var articles = articleService.GetArticleList();
            var response = new ArticleListResponse();
            response.Articles.AddRange((await articles).Select(a => new ArticleInfo { Id = a.Id, Name = a.Name }));
            return response;
        }

        public override async Task<ArticleResponse> GetArticleById(ArticleByIdRequest request, ServerCallContext context)
        {
            var article = await articleService.GetArticleById(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, "Article not found"));
            return new ArticleResponse
            {
                Id = article.Id,
                Name = article.Name,
                Brand = article.Brand
            };
        }

        public override async Task<CreateArticleResponse> CreateArticle(CreateArticleRequest request, ServerCallContext context)
        {
            var article = new Article
            {
                Name = request.Name,
                Brand = request.Brand,
                Prices = [] // Assuming initial prices are empty
            };
            await articleService.CreateArticle(article);
            return new CreateArticleResponse { Id = article.Id };
        }

        public override async Task<UpdateArticleResponse> UpdateArticle(UpdateArticleRequest request, ServerCallContext context)
        {
            var article = await articleService.GetArticleById(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, "Article not found"));
            article.Name = request.Name;
            article.Brand = request.Brand;
            await articleService.UpdateArticle(article);
            return new UpdateArticleResponse { Success = true };
        }
        public override async Task<Empty> DeleteArticle(ArticleByIdRequest request, ServerCallContext context)
        {
            var article = await articleService.GetArticleById(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, "Article not found"));
            await articleService.DeleteArticle(article.Id);
            return new Empty();
        }
        
        
    }
}

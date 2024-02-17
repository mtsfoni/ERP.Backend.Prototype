using System.Runtime.Serialization;
using System.ServiceModel;
using ERP.Backend.Models;
using Google.Protobuf.WellKnownTypes;
using ProtoBuf.Grpc;

namespace ERP.Backend.gRPCCodeFirst.Interfaces
{
    [ServiceContract]
    public interface IArtikelDistributedService
    {
        Task<Int32Value> CreateArticle(Article article, CallContext context = default);
        Task DeleteArticle(Int32Value id, CallContext context = default);
        Task<Article?> GetArticleById(Int32Value id, CallContext context = default);
        Task<List<Article>> GetArticleList(Empty request, CallContext context = default);
        Task UpdateArticle(Article article, CallContext context = default);
    }
}

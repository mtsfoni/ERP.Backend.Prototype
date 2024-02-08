using ERP.Backend.Models;
using Google.Protobuf.WellKnownTypes;
using ProtoBuf.Grpc;

namespace ERP.Backend.gRPCCodeFirst.Interfaces
{
    public interface IPriceDistributedService
    {
        Task<Int32Value> CreatePrice(Price Price, CallContext context = default);
        Task DeletePrice(Int32Value id, Int32Value articleId, CallContext context = default);
        Task<Price?> GetPriceByDate(Int32Value articleId, DateTime date, CallContext context = default);
        Task<List<Price>> GetPricesByArticleId(Int32Value articleId, CallContext context = default);
        Task UpdatePrice(Price Price, CallContext context = default);
    }
}
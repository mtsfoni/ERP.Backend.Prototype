using ERP.Backend.gRPCCodeFirst.Interfaces;
using ERP.Backend.Models;
using ERP.Backend.Services;
using Google.Protobuf.WellKnownTypes;
using ProtoBuf.Grpc;

namespace ERP.Backend.gRPCCodeFirst.Services
{
    public class PriceDistributedService
        (IPriceService priceService) : IPriceDistributedService
    {
        public Task<List<Price>> GetPricesByArticleId(Int32Value articleId, CallContext context = default)
        {
            return priceService.GetPricesByArticleId(articleId.Value);
        }
        public Task<Price?> GetPriceByDate(Int32Value articleId, DateTime date, CallContext context = default)
        {
            return priceService.GetPriceByDate(articleId.Value, date);
        }

        public Task UpdatePrice(Price Price, CallContext context = default)
        {
            return priceService.UpdatePrice(Price);
        }

        public async Task<Int32Value> CreatePrice(Price Price, CallContext context = default)
        {
            return new Int32Value { Value = await priceService.CreatePrice(Price) };
        }

        public Task DeletePrice(Int32Value id, Int32Value articleId, CallContext context = default)
        {
            return priceService.DeletePrice(id.Value, articleId.Value);
        }



    }
}

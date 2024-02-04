using Grpc.Core;
using ERP.Backend.gRPC;
using ERP.Backend.Services;
using static ERP.Backend.gRPC.PriceService;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Data;

namespace ERP.Backend.gRPC.Services
{
    public class ProtoPriceService 
        (IPriceService priceService)
        : PriceServiceBase
    {
        public override async Task<CreatePriceResponse> CreatePrice(CreatePriceRequest request, ServerCallContext context)
        {
            var price = new Models.Price
            {
                Amount = Convert.ToDecimal(request.Amount),
                ArticleId = request.ArticleId,
                ValidFrom = request.ValidFrom.ToDateTime(),
            };

            await priceService.CreatePrice(price);

            return new CreatePriceResponse { Id = price.Id };
        }

        public override async Task<Empty> DeletePrice(DeletePriceRequest request, ServerCallContext context)
        {
            await priceService.DeletePrice(request.Id, request.ArticleId);
            return new Empty();
        }

        public override async Task<PriceResponse> GetPriceByDate(PriceByDateRequest request, ServerCallContext context)
        {
            var result = await priceService.GetPriceByDate(request.ArticleId, request.Date.ToDateTime()) ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "no price found"));

            return new PriceResponse 
            { 
                Amount = Convert.ToDouble(result.Amount),
                ArticleId = result.ArticleId,
                Id = result.Id,
                ValidFrom = DateTime.SpecifyKind(result.ValidFrom, DateTimeKind.Utc).ToTimestamp()                
            };
        }

        public override async Task<PriceListResponse> GetPricesByArticleId(PriceByArticleIdRequest request, ServerCallContext context)
        {
            try
            {
                var response = new PriceListResponse();
                response.Prices.AddRange((await priceService.GetPricesByArticleId(request.Id))
                    .Select(p => new PriceInfo
                    {
                        Amount = Convert.ToDouble(p.Amount),
                        Id = p.Id,
                        ValidFrom = DateTime.SpecifyKind(p.ValidFrom, DateTimeKind.Utc).ToTimestamp()
                    }));
                return response;
            }
            catch (ArgumentException ex) 
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }

        }

        public override async Task<Empty> UpdatePrice(UpdatePriceRequest request, ServerCallContext context)
        {
            await priceService.UpdatePrice(new Models.Price
            {
                Amount = Convert.ToDecimal(request.Amount),
                ArticleId = request.ArticleId,
                Id = request.Id,
                ValidFrom = request.ValidFrom.ToDateTime()
            });
            return new Empty();
            
        }
    }
}

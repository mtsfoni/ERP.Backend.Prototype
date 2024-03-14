// See https://aka.ms/new-console-template for more information
using ERP.Backend.gRPCCodeFirst.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

Console.WriteLine("Press any key to start");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress("http://localhost:5198");
var ArtikelClient = channel.CreateGrpcService<IArtikelDistributedService>();

var reply = await ArtikelClient.GetArticleList(new Empty());
Console.WriteLine(string.Join(Environment.NewLine, reply.Select(art => $"{art.Name}, {art.Brand}")));

using var channel2 = GrpcChannel.ForAddress("http://localhost:5198");
var PriceClient = channel2.CreateGrpcService<IPriceDistributedService>();

await PriceClient.GetPricesByArticleId(new Int32Value() { Value = 1});
    
    //.CreatePrice(new ERP.Backend.Models.Price { ArticleId = 19, ValidFrom = new DateTime(1, 1, 1), Amount = 128 });

Console.WriteLine( "");
Console.WriteLine("Press enter to exit...");
Console.ReadLine();
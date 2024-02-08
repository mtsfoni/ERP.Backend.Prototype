// See https://aka.ms/new-console-template for more information
using ERP.Backend.gRPCCodeFirst.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProtoBuf.Grpc.Client;

Console.WriteLine("Press any key to start");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress("http://localhost:5198");
var client = channel.CreateGrpcService<IArtikelDistributedService>();

var reply = await client.GetArticleList(new Empty());
Console.WriteLine(string.Join(Environment.NewLine, reply.Select(art => $"{art.Name}, {art.Brand}")));

Console.WriteLine( "");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
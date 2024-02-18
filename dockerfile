# https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0#the-dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

#Copy only projects to enable caching of restore
COPY *.sln .
COPY Directory.*.props .
COPY ERP.Backend.gRPC/*.csproj ./ERP.Backend.gRPC/
COPY ERP.Backend.gRPCCodeFirst/*.csproj ./ERP.Backend.gRPCCodeFirst/
COPY ERP.Backend.Interface/*.csproj ./ERP.Backend.Interface/
COPY ERP.Backend.Models/*.csproj ./ERP.Backend.Models/
COPY ERP.Backend.Persistence/*.csproj ./ERP.Backend.Persistence/
COPY ERP.Backend.REST/*.csproj ./ERP.Backend.REST/
COPY ERP.Backend.Services/*.csproj ./ERP.Backend.Services/
COPY ERP.Frontend.CLI.gRPC/*.csproj ./ERP.Frontend.CLI.gRPC/

RUN dotnet restore

#Copy everything else
COPY ERP.Backend.gRPC/. ./ERP.Backend.gRPC/
COPY ERP.Backend.gRPCCodeFirst/. ./ERP.Backend.gRPCCodeFirst/
COPY ERP.Backend.Interface/. ./ERP.Backend.Interface/
COPY ERP.Backend.Models/. ./ERP.Backend.Models/
COPY ERP.Backend.Persistence/. ./ERP.Backend.Persistence/
COPY ERP.Backend.REST/. ./ERP.Backend.REST/
COPY ERP.Backend.Services/. ./ERP.Backend.Services/
COPY ERP.Frontend.CLI.gRPC/. ./ERP.Frontend.CLI.gRPC/


WORKDIR /source/ERP.Backend.REST
RUN dotnet publish -c Debug -o /app 


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app ./

EXPOSE 5340

ENV ASPNETCORE_URLS http://*:5340
ENTRYPOINT ["dotnet", "ERP.Backend.REST.dll"]
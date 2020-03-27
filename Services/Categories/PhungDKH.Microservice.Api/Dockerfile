#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Categories/PhungDKH.Microservice.Api/PhungDKH.Microservice.Api.csproj", "Services/Categories/PhungDKH.Microservice.Api/"]
COPY ["Services/Categories/PhungDKH.Microservice.Core/PhungDKH.Microservice.Core.csproj", "Services/Categories/PhungDKH.Microservice.Core/"]
COPY ["Services/Categories/PhungDKH.Microservice.Service/PhungDKH.Microservice.Service.csproj", "Services/Categories/PhungDKH.Microservice.Service/"]
COPY ["Services/Categories/PhungDKH.Microservice.Domain/PhungDKH.Microservice.Domain.csproj", "Services/Categories/PhungDKH.Microservice.Domain/"]
RUN dotnet restore "Services/Categories/PhungDKH.Microservice.Api/PhungDKH.Microservice.Api.csproj"
COPY . .
WORKDIR "/src/Services/Categories/PhungDKH.Microservice.Api"
RUN dotnet build "PhungDKH.Microservice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.Microservice.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhungDKH.Microservice.Api.dll"]
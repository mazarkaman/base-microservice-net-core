#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Catalog/PhungDKH.Catalog.Api/PhungDKH.Catalog.Api.csproj", "Services/Catalog/PhungDKH.Catalog.Api/"]
COPY ["Sharing/PhungDKH.Core/PhungDKH.Core.csproj", "Sharing/PhungDKH.Core/"]
COPY ["Sharing/PhungDKH.EventBusRabbitMQ/PhungDKH.EventBusRabbitMQ.csproj", "Sharing/PhungDKH.EventBusRabbitMQ/"]
COPY ["Sharing/PhungDKH.EvenBus/PhungDKH.EvenBus.csproj", "Sharing/PhungDKH.EvenBus/"]
RUN dotnet restore "Services/Catalog/PhungDKH.Catalog.Api/PhungDKH.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/PhungDKH.Catalog.Api"
RUN dotnet build "PhungDKH.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "PhungDKH.Catalog.Api.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Ordering/PhungDKH.Ordering.Api/PhungDKH.Ordering.Api.csproj", "Services/Ordering/PhungDKH.Ordering.Api/"]
COPY ["Services/Ordering/PhungDKH.Ordering.Api/appsettings.Development.json", "Services/Ordering/PhungDKH.Ordering.Api/"]
COPY ["Sharing/PhungDKH.Core/PhungDKH.Core.csproj", "Sharing/PhungDKH.Core/"]
COPY ["Sharing/PhungDKH.EventBusRabbitMQ/PhungDKH.EventBusRabbitMQ.csproj", "Sharing/PhungDKH.EventBusRabbitMQ/"]
COPY ["Sharing/PhungDKH.EvenBus/PhungDKH.EvenBus.csproj", "Sharing/PhungDKH.EvenBus/"]
RUN dotnet restore "Services/Ordering/PhungDKH.Ordering.Api/PhungDKH.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/PhungDKH.Ordering.Api"
RUN dotnet build "PhungDKH.Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.Ordering.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "PhungDKH.Ordering.Api.dll"]
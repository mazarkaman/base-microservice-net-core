#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Categories/PhungDKH.Microservice.MigrateApp/PhungDKH.Microservice.MigrateApp.csproj", "Services/Categories/PhungDKH.Microservice.MigrateApp/"]
COPY ["Services/Categories/PhungDKH.Microservice.Domain/PhungDKH.Microservice.Domain.csproj", "Services/Categories/PhungDKH.Microservice.Domain/"]
RUN dotnet restore "Services/Categories/PhungDKH.Microservice.MigrateApp/PhungDKH.Microservice.MigrateApp.csproj"
COPY . .
WORKDIR "/src/Services/Categories/PhungDKH.Microservice.MigrateApp"
RUN dotnet build "PhungDKH.Microservice.MigrateApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.Microservice.MigrateApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhungDKH.Microservice.MigrateApp.dll"]
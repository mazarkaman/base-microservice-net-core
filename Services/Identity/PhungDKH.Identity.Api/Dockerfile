#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Identity/PhungDKH.Identity.Api/PhungDKH.Identity.Api.csproj", "Services/Identity/PhungDKH.Identity.Api/"]
COPY ["Services/Identity/PhungDKH.Identity.Api/appsettings.Development.json", "Services/Identity/PhungDKH.Identity.Api/"]
RUN dotnet restore "Services/Identity/PhungDKH.Identity.Api/PhungDKH.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/PhungDKH.Identity.Api"
RUN dotnet build "PhungDKH.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "PhungDKH.Identity.Api.dll"]
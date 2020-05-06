#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ApiGateway/PhungDKH.ApiGateway/PhungDKH.ApiGateway.csproj", "ApiGateway/PhungDKH.ApiGateway/"]
COPY ["ApiGateway/PhungDKH.ApiGateway/configuration/configuration.json", "ApiGateway/PhungDKH.ApiGateway/configuration/"]
RUN dotnet restore "ApiGateway/PhungDKH.ApiGateway/PhungDKH.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway/PhungDKH.ApiGateway"
RUN dotnet build "PhungDKH.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhungDKH.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "PhungDKH.ApiGateway.dll"]
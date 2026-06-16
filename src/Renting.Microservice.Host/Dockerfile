#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0.0-noble-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0.100-noble-amd64 AS build
WORKDIR /src
COPY Directory.Build.* ./
COPY NuGet.config .
COPY ["src/Renting.Microservice.Host/Renting.Microservice.Host.csproj", "Renting.Microservice.Host/"]
COPY ["src/Renting.Microservice.Api/Renting.Microservice.Api.csproj", "Renting.Microservice.Api/"]
COPY ["src/Renting.Microservice.ApplicationCore/Renting.Microservice.ApplicationCore.csproj", "Renting.Microservice.ApplicationCore/"]
COPY ["src/Renting.Microservice.Domain/Renting.Microservice.Domain.csproj", "Renting.Microservice.Domain/"]
COPY ["src/Renting.Microservice.Infrastructure/Renting.Microservice.Infrastructure.csproj", "Renting.Microservice.Infrastructure/"]
RUN dotnet restore "Renting.Microservice.Host/Renting.Microservice.Host.csproj"
COPY src .
WORKDIR "/src/Renting.Microservice.Host"
RUN dotnet build "Renting.Microservice.Host.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Renting.Microservice.Host"
RUN dotnet publish "Renting.Microservice.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Renting.Microservice.Host.dll"]
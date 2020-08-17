#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Boticario.CashBack.Api/Boticario.CashBack.Api.csproj", "Boticario.CashBack.Api/"]
COPY ["Boticario.CashBack.Repositories/Boticario.CashBack.Repositories.csproj", "Boticario.CashBack.Repositories/"]
COPY ["Boticario.CashBack.Models/Boticario.CashBack.Models.csproj", "Boticario.CashBack.Models/"]
COPY ["Boticario.CashBack.Core/Boticario.CashBack.Core.csproj", "Boticario.CashBack.Core/"]
COPY ["Boticario.CashBack.Services/Boticario.CashBack.Services.csproj", "Boticario.CashBack.Services/"]
RUN dotnet restore "Boticario.CashBack.Api/Boticario.CashBack.Api.csproj"
COPY . .
WORKDIR "/src/Boticario.CashBack.Api"
RUN dotnet build "Boticario.CashBack.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Boticario.CashBack.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boticario.CashBack.Api.dll"]
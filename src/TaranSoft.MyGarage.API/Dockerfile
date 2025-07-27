FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src

COPY ["src/TaranSoft.MyGarage.API/TaranSoft.MyGarage.API.csproj", "TaranSoft.MyGarage.API/TaranSoft.MyGarage.API.csproj"]
COPY ["src/TaranSoft.MyGarage.Contracts/TaranSoft.MyGarage.Contracts.csproj", "TaranSoft.MyGarage.Contracts/TaranSoft.MyGarage.Contracts.csproj"]
COPY ["src/TaranSoft.MyGarage.Models/TaranSoft.MyGarage.Models.csproj", "TaranSoft.MyGarage.Models/TaranSoft.MyGarage.Models.csproj"]
COPY ["src/TaranSoft.MyGarage.Repository.EntityFramework/TaranSoft.MyGarage.Repository.EntityFramework.csproj", "TaranSoft.MyGarage.Repository.EntityFramework/TaranSoft.MyGarage.Repository.EntityFramework.csproj"]
COPY ["src/TaranSoft.MyGarage.Repository.Interfaces/TaranSoft.MyGarage.Repository.Interfaces.csproj", "TaranSoft.MyGarage.Repository.Interfaces/TaranSoft.MyGarage.Repository.Interfaces.csproj"]
COPY ["src/TaranSoft.MyGarage.Services/TaranSoft.MyGarage.Services.csproj", "TaranSoft.MyGarage.Services/TaranSoft.MyGarage.Services.csproj"]
COPY ["src/TaranSoft.MyGarage.Services.Interfaces/TaranSoft.MyGarage.Services.Interfaces.csproj", "TaranSoft.MyGarage.Services.Interfaces/TaranSoft.MyGarage.Services.Interfaces.csproj"]

RUN dotnet restore "TaranSoft.MyGarage.API/TaranSoft.MyGarage.API.csproj"
COPY ["/src", "."]
WORKDIR "/src/TaranSoft.MyGarage.API"
RUN dotnet build "TaranSoft.MyGarage.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaranSoft.MyGarage.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TaranSoft.MyGarage.API.dll"]
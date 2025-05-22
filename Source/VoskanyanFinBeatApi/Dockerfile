FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8200
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Source/VoskanyanFinBeatApi/VoskanyanFinBeatApi.csproj", "src/VoskanyanFinBeatApi/"]
COPY ["Source/DB/DB.csproj", "src/DB/"]
COPY ["Source/Library/Library.csproj", "src/Library/"]
COPY ["Source/Contracts/Contracts.csproj", "src/Contracts/"]
RUN dotnet restore "src/VoskanyanFinBeatApi/VoskanyanFinBeatApi.csproj"

COPY . .

WORKDIR "/src/Source/VoskanyanFinBeatApi"
RUN dotnet build "VoskanyanFinBeatApi.csproj" -c Release

FROM build AS publish
RUN dotnet publish "VoskanyanFinBeatApi.csproj" --no-build -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoskanyanFinBeatApi.dll"]
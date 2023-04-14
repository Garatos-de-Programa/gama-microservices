FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/GamaCore/Gama.Api/", "service/GamaCore/Gama.Api/"]
COPY ["src/services/GamaCore/Gama.Application/", "service/GamaCore/Gama.Application/"]
COPY ["src/services/GamaCore/Gama.Domain/", "service/GamaCore/Gama.Domain/"]
COPY ["src/services/GamaCore/Gama.Infrastructure/", "service/GamaCore/Gama.Infrastructure/"]
COPY ["src/services/GamaCore/Gama.Shared/", "service/GamaCore/Gama.Shared/"]
RUN dotnet restore "service/GamaCore/Gama.Api/Gama.Api.csproj"
COPY . .
WORKDIR "/src/service/GamaCore/Gama.Api"
RUN dotnet build "Gama.Api.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "Gama.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gama.Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/GamaCore/Gama.Api/Gama.Api.csproj", "src/services/GamaCore/Gama.Api/"]
RUN dotnet restore "src/services/GamaCore/Gama.Api/Gama.Api.csproj"
COPY . .
WORKDIR "/src/src/services/GamaCore/Gama.Api"
RUN dotnet build "Gama.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gama.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gama.Api.dll"]

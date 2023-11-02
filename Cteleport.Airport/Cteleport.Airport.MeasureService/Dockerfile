FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cteleport.Airport.MeasureService/Cteleport.Airport.MeasureService.csproj", "Cteleport.Airport.MeasureService/"]
RUN dotnet restore "Cteleport.Airport.MeasureService/Cteleport.Airport.MeasureService.csproj"
COPY . .
WORKDIR "/src/Cteleport.Airport.MeasureService"
RUN dotnet build "Cteleport.Airport.MeasureService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cteleport.Airport.MeasureService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cteleport.Airport.MeasureService.dll"]

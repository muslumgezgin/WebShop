FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Src/Clients/WepShop.Ui/WepShop.Ui.csproj", "WepShop.Ui/"]
RUN dotnet restore "Src/Clients/WepShop.Ui/WepShop.Ui.csproj"
COPY . .
WORKDIR "/src/WepShop.Ui"
RUN dotnet build "WepShop.Ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WepShop.Ui.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WepShop.Ui.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashFlow.Report.Api/CashFlow.Report.Api.csproj", "CashFlow.Report.Api/"]
RUN dotnet restore "CashFlow.Report.Api/CashFlow.Report.Api.csproj"
COPY . .
WORKDIR "/src/CashFlow.Report.Api"
RUN dotnet build "CashFlow.Report.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlow.Report.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.Report.Api.dll"]
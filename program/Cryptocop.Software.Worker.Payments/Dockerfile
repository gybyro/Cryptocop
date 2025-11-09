FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Cryptocop.Software.Worker.Payments/Cryptocop.Software.Worker.Payments.csproj", "Cryptocop.Software.Worker.Payments/"]
COPY ["Cryptocop.Software.API.Models/Cryptocop.Software.API.Models.csproj", "Cryptocop.Software.API.Models/"]
RUN dotnet restore "Cryptocop.Software.Worker.Payments/Cryptocop.Software.Worker.Payments.csproj"
COPY . .
WORKDIR "/src/Cryptocop.Software.Worker.Payments"
RUN dotnet build "Cryptocop.Software.Worker.Payments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cryptocop.Software.Worker.Payments.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptocop.Software.Worker.Payments.dll"]
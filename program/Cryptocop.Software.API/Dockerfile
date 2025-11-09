FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["Cryptocop.Software.API/Cryptocop.Software.API.csproj", "Cryptocop.Software.API/"]
COPY ["Cryptocop.Software.API.Models/Cryptocop.Software.API.Models.csproj", "Cryptocop.Software.API.Models/"]
COPY ["Cryptocop.Software.API.Repositories/Cryptocop.Software.API.Repositories.csproj", "Cryptocop.Software.API.Repositories/"]
COPY ["Cryptocop.Software.API.Services/Cryptocop.Software.API.Services.csproj", "Cryptocop.Software.API.Services/"]
RUN dotnet restore "Cryptocop.Software.API/Cryptocop.Software.API.csproj"

# Copy everything and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port ASP.NET Core listens on (default 8080)
EXPOSE 5001

ENTRYPOINT ["dotnet", "Cryptocop.Software.API.dll"]
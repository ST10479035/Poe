# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["RaceDay.MVC.csproj", "./"]
RUN dotnet restore "RaceDay.MVC.csproj"

# Copy the remaining source code and publish the app
COPY . .
RUN dotnet publish "RaceDay.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false
# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RaceDay.MVC.dll"]

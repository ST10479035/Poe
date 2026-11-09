# 🏁 RACEDAY Platform - ASP.NET Core MVC & REST API

---

## 📌 Project Overview
RaceDay is a comprehensive athletic event management web application built using **ASP.NET Core MVC**, **ASP.NET Core REST API**, **Entity Framework Core**, **Azure Blob Storage**, and **Docker**.

---

## 🏗 System Architecture & Setup Notes
The platform follows a decoupled architecture where the MVC web application consumes the REST API backend for all data and authentication operations.

### Local Setup Instructions
1. Clone the repository: `git clone https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git`
2. Open `RaceDayAPI.sln` in Visual Studio 2022.
3. Update database connection strings in `appsettings.json` target local SQL Server (`localhost\SQLEXPRESS01`).
4. Apply EF Core Migrations: `Update-Database`.
5. Run both the API project (`RaceDayAPI`) and MVC project (`RaceDayAPI3`).

---

## 🐳 Docker Deployment Instructions
To run the containerised MVC application:

```bash
# Build the Docker image from solution root
docker build -t raceday-mvc -f RaceDayAPI3/Dockerfile .

# Run the container
docker run -d -p 8080:80 --name raceday-app raceday-mvc

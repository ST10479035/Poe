# 🏁 RACEDAY Platform - ASP.NET Core MVC & REST API


[![.NET CI/CD Build](https://github.com/ST10479035/Poe/actions/workflows/build.yml/badge.svg)](https://github.com/ST10479035/Poe/actions)

---


## 📌 Project Overview
RaceDay is a comprehensive athletic event management web application built using **ASP.NET Core MVC**, **ASP.NET Core REST API**, **Entity Framework Core**, **Azure Blob Storage**, and **Docker**. It provides role-aware interfaces for both Organisers and Participants.


---

## 🏗 System Architecture & Setup Notes
The platform follows a decoupled architecture where the MVC web application communicates securely with the backend REST API.

### Local Setup Instructions
1. Clone the repository: `git clone https://github.com/ST10479035/Poe.git`
2. Open `RaceDayAPI.sln` inside Visual Studio 2022.
3. Configure your local connection string targeting `localhost\SQLEXPRESS01` inside `appsettings.json`.
4. Apply Entity Framework migrations via Package Manager Console: `Update-Database`.
5. Run both the API and MVC projects.


---

## 🐳 Docker Deployment Instructions
To run the containerised application locally:

```bash
# Build the Docker image from the solution root
docker build -t raceday-mvc -f RaceDayAPI3/Dockerfile .

# Run the container mapping port 8080
docker run -d -p 8080:80 --name raceday-app raceday-mvc


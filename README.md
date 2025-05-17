# Employee Management System (ASP.NET Core + MySQL + Docker)

This is a simple ASP.NET Core MVC application for managing employees. It uses MySQL for data storage and Docker for containerized deployment.

## 🚀 Technologies Used

- ASP.NET Core 8.0
- MySQL 8.0
- Entity Framework Core
- Docker & Docker Compose

---

## 📦 Setup & Run

### 1. Clone the Repo
```bash
git clone https://github.com/MoNai-01/employee-management-system.git
cd employee-management-system
```

## 🔧 Configuration

Before running the project, update your MySQL root password and your database name in `appsettings.json` and docker-compose.yml:

```json
- appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=YOUR_DB_HERE;user=root;password=YOUR_PASSWORD_HERE;"
}
```
```yml
- docker-compose.yml
MYSQL_ROOT_PASSWORD: YOUR_PASSWORD_HERE
MYSQL_DATABASE: YOUR_DB_HERE
```

### 3. Build and Run with Docker Compose
```docker run
docker-compose up --build
```

### 4. Access the app at: http://localhost:5044

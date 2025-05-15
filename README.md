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

### 2. Create a `.env` File
In the root directory, create a `.env` file and add the following environment variables:
```env
MYSQL_ROOT_PASSWORD=your_mysql_root_password
MYSQL_DATABASE=EmployeeManagementDB
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:5044
```

### 3. Build and Run with Docker Compose
```docker run
docker-compose up --build
```

### 4. Access the app at: http://localhost:5044

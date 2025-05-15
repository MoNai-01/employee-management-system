# Employee Management System (ASP.NET Core + MySQL + Docker)

This is a simple ASP.NET Core MVC application for managing employees. It uses MySQL for data storage and Docker for containerized deployment.

## 🚀 Technologies Used

- ASP.NET Core 8.0
- MySQL 8.0
- Entity Framework Core
- Docker & Docker Compose

---

## 📦 Setup & Run
```bash
### 1. Clone the Repo
git clone https://github.com/MoNai-01/employee-management-system.git
cd employee-management-system

### 2. Create a .env File with your MySQL creds
MYSQL_ROOT_PASSWORD=your_mysql_root_pass
MYSQL_DATABASE=EmployeeManagementDB

### 3. Build and Run with Docker Compose
docker-compose up --build

### 4. Access the app at: http://localhost:5044

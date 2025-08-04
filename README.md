# TaranSoft.MyGarage

A comprehensive vehicle management system designed for car owners to track their vehicles and log every activity related to their cars, motorcycles, and trailers.

## 🚗 What is TaranSoft.MyGarage?

TaranSoft.MyGarage is a modern web API that helps car owners maintain detailed records of their vehicles and activities. Whether you own cars, motorcycles, or trailers, this system provides a centralized platform to track maintenance, modifications, fuel consumption, and other vehicle-related activities.

## ✨ Key Features

### Vehicle Management
- **Multi-Vehicle Support**: Track cars, motorcycles, and trailers
- **Detailed Vehicle Profiles**: Store manufacturer info, specifications, and custom details
- **Vehicle History**: Complete activity logging and maintenance records

### Activity Logging
- **Maintenance Records**: Log service appointments, repairs, and maintenance activities
- **Fuel Tracking**: Monitor fuel consumption and costs
- **Modification Log**: Track upgrades, modifications, and customizations
- **Expense Tracking**: Record all vehicle-related expenses

### Garage Management
- **Personal Garages**: Organize vehicles by garage/location
- **Inventory Management**: Track spare parts and accessories
- **Service History**: Complete maintenance and service records

### User Features
- **User Authentication**: Secure login and user management
- **Personal Dashboard**: Individual user profiles and vehicle collections
- **Data Export**: Export vehicle and activity data

## 🛠 Technology Stack

- **Backend**: ASP.NET Core 8.0 Web API
- **Database**: SQL Server 2019 (primary) / MongoDB (alternative)
- **Authentication**: JWT Bearer tokens
- **Documentation**: Swagger/OpenAPI
- **Containerization**: Docker/Podman support
- **Testing**: Unit and Integration tests

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK
- SQL Server 2019 (or use containerized version)
- Podman/Docker (optional, for containerized deployment)

### Local Development
```bash
# Clone the repository
git clone <repository-url>
cd TaranSoft.MyGarage

# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project src/TaranSoft.MyGarage.API
```

The API will be available at `https://localhost:5001` with Swagger documentation at `https://localhost:5001/swagger`.

### Containerized Deployment
```bash
# Build and run with Podman
podman-compose up --build

# Or with Docker
docker-compose up --build
```

The containerized API will be available at `http://localhost:5000`.

## 📚 Documentation

- **[Architecture Overview](overview.md)** - Detailed technical architecture and project structure
- **[API Documentation](https://localhost:5001/swagger)** - Interactive API documentation (when running)
- **[Database Setup](DbScripts/README.md)** - Database initialization and setup instructions

## 🏗 Project Structure

```
TaranSoft.MyGarage/
├── src/
│   ├── TaranSoft.MyGarage.API/          # Web API endpoints
│   ├── TaranSoft.MyGarage.Contracts/    # DTOs and interfaces
│   ├── TaranSoft.MyGarage.Models/       # Entity models
│   ├── TaranSoft.MyGarage.Services/     # Business logic
│   └── TaranSoft.MyGarage.Repository/   # Data access layer
├── Tests/                               # Unit and integration tests
├── DbScripts/                           # Database setup scripts
├── docker-compose.yml                   # Container orchestration
└── overview.md                          # Technical architecture
```

## 🔧 Configuration

The application supports multiple configuration options:

- **Database Selection**: Choose between SQL Server and MongoDB
- **Environment Settings**: Development and Production configurations
- **Container Deployment**: Docker/Podman containerization
- **Authentication**: JWT-based security

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For questions, issues, or feature requests, please open an issue in the repository.

---

**Built with ❤️ for car enthusiasts who love to track every detail of their vehicles.**
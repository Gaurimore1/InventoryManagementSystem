# Inventory Management System

A comprehensive ASP.NET Core MVC web application for managing inventory, products, suppliers, and generating reports with role-based access control.

## Description

This Inventory Management System provides a complete solution for businesses to manage their inventory operations. The system includes:

- **Product Management**: Add, edit, delete, and view products with stock tracking
- **Category Management**: Organize products into categories with filtering
- **Supplier Management**: Manage supplier information and contacts
- **Stock Management**: Track inventory levels with low-stock alerts
- **Reports & Analytics**: Generate inventory reports and export data
- **Role-Based Access**: Admin and Staff roles with different permissions
- **User Authentication**: Secure login/registration system
- **CSV Export**: Export product data for external analysis

### Key Features

- ✅ **Product CRUD Operations**: Full create, read, update, delete functionality
- ✅ **Stock Management**: Real-time stock quantity tracking
- ✅ **Low Stock Alerts**: Automatic highlighting of products below threshold
- ✅ **Category Filtering**: Filter products by categories
- ✅ **Search Functionality**: Search products by name or description
- ✅ **CSV Export**: Export product data to CSV format
- ✅ **Role-Based Dashboard**: Different interfaces for Admin and Staff
- ✅ **Responsive Design**: Modern, mobile-friendly interface
- ✅ **Authentication & Authorization**: Secure user management

## Technology Stack

- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript
- **ORM**: Entity Framework Core
- **Architecture**: Model-View-Controller (MVC) Pattern

## Installation Steps

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- Git (optional, for cloning)

### Installation

1. **Clone the repository** (if using Git):
   ```bash
   git clone <repository-url>
   cd InventoryManagementSystem
   ```

2. **Navigate to the project directory**:
   ```bash
   cd InventoryManagementSystem
   ```

3. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

## How to Run the Project

### Method 1: Using .NET CLI (Recommended)

1. **Open Command Prompt/Terminal** in the project directory

2. **Run the application**:
   ```bash
   dotnet run
   ```

3. **Open your browser** and navigate to:
   - `https://localhost:5001` (HTTPS - recommended)
   - `http://localhost:5000` (HTTP - fallback)

### Method 2: Using Visual Studio

1. **Open the project** in Visual Studio 2022
2. **Press F5** or click the "Start" button
3. **The application will launch** in your default browser

### Method 3: Using Visual Studio Code

1. **Open the project folder** in VS Code
2. **Open the integrated terminal** (Ctrl+`)
3. **Run the command**:
   ```bash
   dotnet run
   ```

## First-Time Setup

### Default User Accounts

The system comes with pre-configured demo accounts:

**Admin Account:**
- Email: `admin@example.com`
- Password: `Pass@12345`
- Access: Full system access (Products, Suppliers, Categories, Reports)

**Staff Account:**
- Email: `staff@example.com`
- Password: `Pass@12345`
- Access: View products and reports only

### Creating Your Own Account

1. **Click "Register"** on the login page
2. **Enter your email** and password
3. **Complete registration** - you'll automatically get Admin role
4. **Login** with your credentials

## Project Structure

```
InventoryManagementSystem/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   ├── SuppliersController.cs
│   ├── DashboardController.cs
│   └── ReportsController.cs
├── Models/               # Data Models
│   ├── Product.cs
│   ├── Category.cs
│   └── Supplier.cs
├── Views/                # Razor Views
│   ├── Home/
│   ├── Products/
│   ├── Categories/
│   ├── Suppliers/
│   ├── Dashboard/
│   ├── Reports/
│   └── Shared/
├── Data/                 # Database Context & Seeding
│   ├── ApplicationDbContext.cs
│   └── IdentitySeed.cs
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── Program.cs            # Application Entry Point
├── appsettings.json     # Configuration
└── README.md            # This file
```

## Features Overview

### Admin Features
- **Product Management**: Create, edit, delete products
- **Category Management**: Manage product categories
- **Supplier Management**: Manage supplier information
- **Reports**: View inventory analytics
- **User Management**: Access to all system features

### Staff Features
- **Product Viewing**: View product information
- **Reports**: Access to inventory reports
- **Limited Access**: Read-only access to most features

### Common Features
- **Dashboard**: Role-based dashboard
- **Search & Filter**: Find products quickly
- **CSV Export**: Export data for analysis
- **Low Stock Alerts**: Automatic notifications
- **Responsive Design**: Works on all devices

## Configuration

### Database
- **Default**: SQLite database (`inventory.db`)
- **Location**: Project root directory
- **Auto-created**: Database and tables are created automatically

### Low Stock Threshold
- **Default**: 5 units
- **Configurable**: Edit `appsettings.json`
- **Usage**: Products below threshold are highlighted

## Troubleshooting

### Common Issues

1. **"Database locked" error**:
   - Stop the application (Ctrl+C)
   - Delete `inventory.db` file
   - Restart the application

2. **"Access denied" error**:
   - Register a new account or use demo accounts
   - Check if you're logged in properly

3. **Build errors**:
   - Run `dotnet clean`
   - Run `dotnet restore`
   - Run `dotnet build`

4. **Port already in use**:
   - Change the port in `Properties/launchSettings.json`
   - Or kill the process using the port

### Getting Help

- Check the console output for error messages
- Ensure .NET 9.0 SDK is installed
- Verify all NuGet packages are restored
- Check database file permissions

## Development

### Adding New Features

1. **Create Model** in `Models/` folder
2. **Add DbSet** to `ApplicationDbContext`
3. **Create Controller** in `Controllers/` folder
4. **Create Views** in `Views/` folder
5. **Update Navigation** in `_Layout.cshtml`

### Database Changes

1. **Modify Models** as needed
2. **Delete existing database** (`inventory.db`)
3. **Restart application** - new schema will be created

## License

This project is created for educational purposes. Feel free to use and modify as needed.

## Support

For issues or questions:
1. Check the troubleshooting section above
2. Review the console output for errors
3. Ensure all prerequisites are installed
4. Verify the application is running on the correct port

---



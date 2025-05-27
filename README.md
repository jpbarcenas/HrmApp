# Simple Employee CRUD Mini App

## Project Brief
Aplicación de gestión de empleados con CRUD básico desarrollada con ASP.NET Core 8 y Razor Pages.

## Project Description
This application allows HR administrators to:
- Manage employee records (create, read, update, delete)
- View employee statistics
- Search and filter employee data

The system uses LocalDB for local development and can be deployed to Azure with SQL Server.

## Project Structure
```
HrmApp/
├── Core/                               # Business Logic Layer
|   ├── Implementations/                    # Holds the Implementation Classes for the defined interfaces
│   │   └── EmployeeRepository.cs
│   ├── Interfaces/                         # Holds the Interfaces defining the required business logic
│   │   └── IEmployeeRepository.cs
│   └── RepositoryLocator.cs                # Setup the Service Collection for the Interfaces and its Implementations
│
├── Domain/                             # Domain Layer
|   ├── Dtos/                               # Data Transfers Objects
│   │   └── EmployeeDto.cs
|   ├── Entities/                           # Domain Entities
│   │   └── Employee.cs
|   ├── Mappings/                           # Setup the Mappers from DTO to Domain and vice versa
│   │   ├── DtoToEntityMapper.cs
│   │   └── EntityToDtoMapper.cs
|   ├── Migrations/                         # EntityFramework Migrations
|   ├── Seeders/                            # Data Seeder for sample or basic Data
│   │   └── DataSeeder.cs
│   └── HrmAppDbContext.cs                  # Application Database Context
│
└── Web/                                # Presentation Layer
    ├── wwwroot/                            # Static files
    ├── Components/                         # ViewComponents
    │   └── EmployeeStatsViewComponent.cs
    ├── Mappings/                           # Setup the Mappers from DTO to ViewModels and vice versa
    │   ├── DtoToViewModelMapper.cs
    │   └── ViewModelToDtoMapper.cs
    ├── Models/                             # View models
    │   ├── AddOrUpdateEmployeeViewModel.cs
    │   ├── EmployeeStatsViewModel.cs
    │   └── EmployeeViewModel.cs
    ├── Pages/                              # Razor Pages
    │   ├── Employees/
    │   │   ├── Index.cshtml
    │   │   ├── Create.cshtml
    │   │   ├── Edit.cshtml
    │   │   ├── Details.cshtml
    │   │   └── Delete.cshtml
    │   └──Shared/
    |       └── Components/                 # Reusable View Components
    │           └── EmployeeStats/
    │               └── Default.cshtml
    ├── Validators/                         # Custom Validators
    │    └── EmployeeValidator.cs
    ├── Program.cs                          # Startup configuration
    └── appsettings.json                    # Configuration
```

## Requirements
- .NET 8 SDK
- Visual Studio 2022 (or VS Code with C# extension)
- SQL Server Developer Ed (for local development)
- Azure account (for deployment)

## Functionalities
### Core Features
- **Employee CRUD Operations**:
  - Create new employee records
  - View employee details
  - Edit existing records
  - Delete employees
- **Data Validation**:
  - Client-side and server-side validation
  - Custom validation rules
- **Search & Filter**:
  - Search by employee name
  - Filter by department
- **Pagination**:
  - Configurable page sizes
  - Preserved filters during navigation

### Advanced Features
- **Employee Statistics Dashboard**:
  - Active/inactive employee counts
- **Responsive Design**:
  - Mobile-friendly interface
  - Accessible components

## Key Decisions
1. **Technology Stack**:
   - ASP.NET Core Razor Pages for rapid development
   - Entity Framework Core for data access
   - SQL LocalDB for local development
   - Azure SQL/SQL Server for production

2. **Architecture**:
   - Repository pattern for data access abstraction
   - Dependency Injection for loose coupling
   - ViewComponents for reusable UI components

3. **Validation Approach**:
   - DataAnnotations for model validation
   - Custom validation attributes for business rules
   - Client-side validation with jQuery Unobtrusive

4. **Pagination Solution**:
   - X.PagedList for efficient data paging
   - URL-preserved filter state
   - Configurable page sizes

## Deployment Instructions

### Local Environment Setup
1. **Clone the repository**:
   ```bash
   git clone https://github.com/jpbarcenas/HrmApp.git
   cd HrmApp
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Apply database migrations**:
   ```bash
   dotnet ef database update
   ```

4. **Seed initial data**:
   ```bash
   dotnet run seed
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```
   Access at: `https://localhost:7252`

### Azure Deployment
1. **Create Azure Resources**:
   - App Service
   - Azure SQL Database
   - Application Insights (optional)

2. **Configure Connection Strings**:
   Update `appsettings.Production.json` with:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:[server-name].database.windows.net;Database=HrmData;User ID=[username];Password=[password];"
     }
   }
   ```

3. **Deploy via Azure CLI**:
   ```bash
   az webapp up --name HrmApp --resource-group YourResourceGroup --runtime-dotnet 8.0
   ```

4. **Apply Migrations in Azure**:
   ```bash
   dotnet ef database update --connection "AzureSQLConnectionString"
   ```

## Environment Variables
| Variable | Description | Example |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | `Development`/`Production` |

## Troubleshooting
**Database Issues**:
- Delete the LocalDb database and re-run migrations
- For Azure SQL, verify firewall rules allow connections

## Contributing
1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## License
MIT License - see [LICENSE](LICENSE) file for details

## Roadmap
- [ ] Add employee photo upload capability
- [ ] Implement JWT authentication
- [ ] Add department management module
- [ ] Implement audit logging

## Support
For issues or questions, please open an issue in the GitHub repository.
# Azure Deployment Guide with GitHub Actions

This is a step-by-step guide to deploy the Hrm application to Azure using App Services and Azure SQL Database, and with the help of GitHub Actions for CI/CD.

## Part 1: Azure Setup

### 1.1 Create Azure Resources
1. **Login to Azure Portal**:
   - Go to [portal.azure.com](https://portal.azure.com)
   - Sign in with your Azure account

2. **Create Resource Group**:
   - Click "Create a resource"
   - Search for "Resource group"
   - Name: `hrm-rg`
   - Region: Choose one close to your users
   - Click "Review + Create" then "Create"

3. **Create Azure SQL Database**:
   - Click "Create a resource"
   - Search for "SQL Database"
   - Configure:
     - Subscription: Your subscription
     - Resource group: `hrm-rg`
     - Database name: `HrmData`
     - Server: Create new
       - Server name: `hrm-sql-server`
       - Location: Same as resource group
       - Authentication method: "Use both SQL and Microsoft Entra authentication"
       - Server admin login: `sqladmin`
       - Password: Create a strong password (save this!)
     - Want to use SQL elastic pool: No
     - Compute + storage: Basic (or adjust based on needs)
   - Click "Next: Networking"
     - Connectivity method: Public endpoint
     - Allow Azure services and resources to access this server: Yes
   - Click "Review + Create" then "Create"

4. **Create App Service**:
   - Click "Create a resource"
   - Search for "Web App"
   - Configure:
     - Subscription: Your subscription
     - Resource group: `hrm-rg`
     - Name: `hrm-app`
     - Runtime stack: `.NET 8 (LTS)`
     - Operating System: Windows (or Linux if preferred)
     - Region: Same as resource group
     - App Service Plan: Create new
       - Name: `hrm-asp`
       - SKU and size: Change to "Basic B1" (or higher for production)
   - Click "Review + Create" then "Create"

### 1.2 Configure Azure SQL
1. **Set Firewall Rules**:
   - Go to your SQL Server resource
   - Under "Security", select "Networking"
   - Add your current client IP address
   - Enable "Allow Azure services and resources to access this server"

2. **Get Connection String**:
   - Go to your SQL Database
   - Under "Settings", select "Connection strings"
   - Copy the ADO.NET connection string
   - Replace `{your_password}` with your admin password

## Part 2: Application Configuration

### 2.1 Create/Update appsettings.json
Create `appsettings.Production.json` in your project root:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:hrm-sql-server.database.windows.net,1433;Database=HrmData;User ID=sqladmin;Password={your_password};Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2.2 Update Program.cs
Ensure your database configuration is environment-aware; this is useful when using SQLite for local development.

```csharp
if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}
```

## Part 3: GitHub Actions Setup

### 3.1 Create GitHub Secrets
1. Go to your GitHub repository
2. Click "Settings" > "Secrets and variables" > "Actions"
3. Add these secrets:
   - `AZURE_CREDENTIALS`: See below how to get this
   - `AZURE_SQL_CONNECTION_STRING`: Your production connection string
   - `AZURE_WEBAPP_NAME`: `hrm-app`
   - `AZURE_WEBAPP_PUBLISH_PROFILE`: Get from Azure (see below)

**Getting Azure Credentials**:
Run this Azure CLI command (install it first if needed):
```bash
az ad sp create-for-rbac --name "HrmAppDeploy" --role contributor \
  --scopes /subscriptions/{your-subscription-id}/resourceGroups/hrm-rg \
  --sdk-auth
```
Copy the JSON output and save as `AZURE_CREDENTIALS` secret.

**Getting Publish Profile**:
- Go to your App Service in Azure portal
- Under "Overview", click "Get publish profile"
- Copy the contents and save as `AZURE_WEBAPP_PUBLISH_PROFILE` secret

### 3.2 Create GitHub Workflow
Create `.github/workflows/deploy.yml`:

```yaml
name: Deploy to Azure App Service

on:
  push:
    branches: [ "main" ]
  workflow_dispatch:

env:
  AZURE_WEBAPP_NAME: ${{ secrets.AZURE_WEBAPP_NAME }}
  DOTNET_VERSION: '8.0.x'

jobs:
  build-and-deploy:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v4

    - name: Setup .NET Core
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: Build with dotnet
      run: |
        dotnet restore
        dotnet build --configuration Release --no-restore
        dotnet publish -c Release -o publish --no-restore

    - name: Run EF Core Migrations
      run: |
        dotnet ef database update --connection "${{ secrets.AZURE_SQL_CONNECTION_STRING }}"
        
    - name: Upload artifact for deployment
      uses: actions/upload-artifact@v3
      with:
        name: .net-app
        path: publish/

    - name: Deploy to Azure App Service
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: publish
```

## Part 4: Deployment Steps

### 4.1 Initial Deployment
1. Commit and push your changes to main branch
2. GitHub Actions will automatically:
   - Build the application
   - Run database migrations
   - Deploy to Azure App Service

### 4.2 Verify Deployment
1. Go to your App Service in Azure portal
2. Under "Overview", click the URL to open your application
3. Test the application functionality

## Part 5: Post-Deployment Configuration

### 5.1 Configure Application Settings in Azure
1. Go to your App Service
2. Under "Settings", select "Configuration"
3. Add these Application Settings:
   - `ASPNETCORE_ENVIRONMENT`: `Production`
   - `ConnectionStrings__DefaultConnection`: Paste your production connection string

### 5.2 Set Up Continuous Deployment
1. In App Service, go to "Deployment Center"
2. Select "GitHub" as source
3. Authorize and select your repository and branch
4. Save the configuration

## Part 6: Troubleshooting

### Common Issues and Solutions:
1. **Database Connection Failures**:
   - Verify firewall rules allow Azure services
   - Check connection string in Azure App Settings
   - Test connection using SQL Server Management Studio

2. **Migration Errors**:
   - Run migrations locally against Azure SQL first:
     ```bash
     dotnet ef database update --connection "YOUR_AZURE_SQL_CONNECTION_STRING"
     ```

3. **Application Not Starting**:
   - Check logs in App Service > "Log stream"
   - Review "Diagnose and solve problems" in App Service

4. **Static Files Not Loading**:
   - Ensure `app.UseStaticFiles()` is in your Startup.cs
   - Verify files are included in publish (check .csproj)

## Part 7: Optional Enhancements

### 7.1 Custom Domain Setup
1. Go to App Service > "Custom domains"
2. Follow instructions to verify your domain
3. Configure SSL certificates

### 7.2 Auto-Scaling
1. Go to App Service Plan > "Scale out"
2. Configure rules based on CPU/Memory usage
3. Set instance limits

### 7.3 Backup Configuration
1. Go to App Service > "Backups"
2. Configure regular backups
3. Set up backup storage account

Would you like me to elaborate on any specific part of this deployment process or add any additional configurations?
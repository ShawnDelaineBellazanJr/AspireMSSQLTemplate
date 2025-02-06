# 🚀 ProjectName - .NET Aspire Project Template

This is a **.NET Aspire-based microservices template** designed for **API-first development, Blazor WebAssembly integration, and AI-driven applications**.  
It follows industry **best practices for modularity, OpenAPI documentation, service orchestration, and cloud-native deployment**.

---

## 🎯 **Key Features**
✅ **Microservices architecture** - API-first approach  
✅ **Pre-configured `ServiceDefaults` for common configurations**  
✅ **Swagger UI & OpenAPI support** for easy API testing  
✅ **Blazor WebAssembly (WASM) frontend for UI applications**  
✅ **JWT authentication-ready** (expandable)  
✅ **Docker support** for cloud-native deployment  
✅ **CI/CD automation via GitHub Actions**  

---

## 📌 **Getting Started**

### 1️⃣ **Install the Template Locally**
```sh
dotnet new --install .
```

### 2️⃣ **Generate a New Project**
```sh
dotnet new projectname-template -n MyNewProject
```
This creates a new project named **`MyNewProject`** with all configurations applied.

### 3️⃣ **Run the Solution**
```sh
cd MyNewProject
dotnet restore
dotnet run --project src/MyNewProject.ApiService
```

---

## 📂 **Project Structure**
```
ProjectName/
│── .template.config/                    # Template metadata
│── .github/                              # GitHub Actions for CI/CD automation
│   ├── workflows/
│
│── docs/                                 # Documentation
│
│── src/                                  # Source code
│   ├── ProjectName.ApiService/           # API Service (RESTful)
│   │   ├── Controllers/                   # API Controllers
│   │   ├── Models/                        # Data models
│   │   ├── Properties/
│   │   ├── appsettings.json
│   │   ├── Program.cs
│   │   ├── Dockerfile                     # Dockerized API service
│   │   ├── ProjectName.ApiService.csproj
│
│   ├── ProjectName.AppHost/               # Application host for service orchestration
│   │   ├── Properties/
│   │   ├── appsettings.json
│   │   ├── Program.cs
│   │   ├── ProjectName.AppHost.csproj
│
│   ├── ProjectName.ServiceDefaults/       # Shared Service Defaults
│   │   ├── Configurations/                # Common configurations
│   │   │   ├── SwaggerConfig.cs           # Centralized Swagger Configuration
│   │   ├── Extensions.cs
│   │   ├── ProjectName.ServiceDefaults.csproj
│
│   ├── ProjectName.Web/                   # Blazor WebAssembly frontend
│   │   ├── Layout/
│   │   ├── Pages/
│   │   ├── Properties/
│   │   ├── wwwroot/
│   │   ├── App.razor
│   │   ├── Program.cs
│   │   ├── _Imports.razor
│   │   ├── ProjectName.Web.csproj
│
│── tests/                                # Unit and Integration Tests
│── .dockerignore                         # Docker ignore settings
│── .gitattributes                        # Git attributes
│── .gitignore                            # Git ignore settings
│── LICENSE.txt                           # License information
│── README.md                             # Project documentation
│── ProjectName.sln                       # Solution file (dynamically renamed)
```

---

## 🌍 **Swagger & OpenAPI Documentation**
This template comes with **pre-configured Swagger UI & OpenAPI** using `ServiceDefaults`.

### **📜 Run Swagger UI**
After running the API, open:
```
http://localhost:5000/
```
Swagger UI will be available **at the root URL**.

### **🛠 API Endpoints**
| Method | Endpoint               | Description                     | Response |
|--------|------------------------|---------------------------------|----------|
| `GET`  | `/api/prompts`         | Retrieve all prompts           | 200 (OK) |
| `GET`  | `/api/prompts/{id}`    | Retrieve a specific prompt by ID | 200 (OK) / 404 (Not Found) |
| `POST` | `/api/prompts`         | Create a new prompt            | 201 (Created) |
| `PUT`  | `/api/prompts/{id}`    | Update an existing prompt      | 204 (No Content) / 404 (Not Found) |
| `DELETE` | `/api/prompts/{id}`  | Delete a prompt                | 204 (No Content) / 404 (Not Found) |

---

## 🔧 **ServiceDefaults**
This project uses **`ProjectName.ServiceDefaults`** to centralize configurations like:
- **Swagger/OpenAPI**
- **Authentication**
- **Common API behaviors**
- **Shared extension methods**

### **📜 How to Use ServiceDefaults**
In `Program.cs` (for `ProjectName.ApiService`):
```csharp
using System.Reflection;
using ProjectName.ServiceDefaults.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Define XML documentation path dynamically
string xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
builder.Services.AddSwaggerDocumentation(xmlFilePath);

// Register API controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Using ServiceDefaults for OpenAPI

var app = builder.Build();

app.MapDefaultEndpoints(); // Automatically maps common endpoints

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## 🏗 **Customization & Extension**
You can **extend this template** by:
- **Adding Authentication & Authorization**
- **Expanding `ProjectName.ServiceDefaults`**
- **Integrating AI-powered prompt generation**
- **Deploying to cloud-based hosting**  

---

## 🎯 **Ideal Use Cases**
- ✅ **Microservices & API Development**
- ✅ **Blazor WebAssembly Projects**
- ✅ **AI-Powered Applications**
- ✅ **Cloud-native & Scalable Architectures**

🔥 _Fork & customize this template to fit your needs!_ 🚀

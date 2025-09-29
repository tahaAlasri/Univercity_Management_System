# 🎓 University Management System

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## 🌟 Overview

A comprehensive University Management System built with C# .NET, designed to streamline academic operations and administrative tasks for modern educational institutions.

## ✨ Features

### 🎯 Core Modules
- **Student Management** 📚
- **Course Management** 🏫
- **Grade System** 📊
- **Attendance Tracking** ✅
- **Faculty Management** 👨‍🏫
- **Department Management** 🏢

## 🛠️ Technology Stack

### Backend
- **C# 11.0** - Primary programming language
- **.NET 8.0** - Framework
- **ASP.NET Core Web API** - RESTful APIs
- **Entity Framework Core 8.0** - ORM
- **SQL Server 2022** - Database
- **Dapper** - High-performance data access

### Frontend
- **Blazor WebAssembly** - Interactive web UI
- **MudBlazor** - Material Design components
- **ASP.NET Core MVC** - Traditional web interface

### Architecture & Patterns
- **Clean Architecture** 🏗️
- **Repository Pattern** 📁
- **CQRS with MediatR** 🚀
- **Domain-Driven Design (DDD)** 🎯
- **Unit of Work** ⚙️

## 💻 Project Structure


UniversityManagementSystem/
├── 📁 src/
│   ├── 📁 University.Core/          # Domain layer
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Interfaces/
│   │   └── Exceptions/
│   │
│   ├── 📁 University.Infrastructure/ # Data layer
│   │   ├── Data/
│   │   ├── Repositories/
│   │   ├── Migrations/
│   │   └── Configurations/
│   │
│   ├── 📁 University.Application/   # Business logic
│   │   ├── Features/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   └── Validators/
│   │
│   ├── 📁 University.API/           # Web API
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   └── Program.cs
│   │
│   └── 📁 University.Web/           # Blazor Frontend
│       ├── Components/
│       ├── Pages/
│       └── Services/
│
├── 📁 tests/
│   ├── University.UnitTests/
│   ├── University.IntegrationTests/
│   └── University.APITests/
│
└── 📁 docs/
    ├── Database/
    ├── API/
    └── Deployment/


## 🚀 Quick Start

### Prerequisites
- **.NET 8.0 SDK**
- **SQL Server 2019+**
- **Visual Studio 2022** or **Visual Studio Code**

### Installation Steps

1. **Clone Repository**
git clone https://github.com/senourian-developers/university-management.git
cd university-management

2. **Database Setup**

-- Create database
CREATE DATABASE UniversityManagementSystem;

-- Or use EF Core migrations
dotnet ef database update --project University.Infrastructure

3. **Configure Connection String**

// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=UniversityManagementSystem;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}


4. **Run Application**

# Restore packages
dotnet restore

# Run API
dotnet run --project University.API

# Run Web Client
dotnet run --project University.Web


## 📚 Code Examples

### 🎓 Student Entity

namespace University.Core.Entities;
public class Student : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime EnrollmentDate { get; private set; }
    public int DepartmentId { get; private set; }
    public Department Department { get; private set; }
    
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();
    
    // Domain methods
    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new DomainException("Email cannot be empty");
            
        Email = newEmail;
    }
    
    public void EnrollInCourse(Course course)
    {
        var enrollment = new Enrollment(this, course, DateTime.UtcNow);
        _enrollments.Add(enrollment);
    }
}


### 🏫 Course Service

namespace University.Application.Services;
public interface ICourseService
{
    Task<Result<CourseDto>> CreateCourseAsync(CreateCourseCommand command);
    Task<Result<List<CourseDto>>> GetAllCoursesAsync();
    Task<Result<CourseDto>> GetCourseByIdAsync(int id);
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    
    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<CourseDto>> CreateCourseAsync(CreateCourseCommand command)
    {
        var course = Course.Create(command.Code, command.Name, command.Credits, command.DepartmentId);
        
        await _courseRepository.AddAsync(course);
        await _courseRepository.UnitOfWork.SaveChangesAsync();
        
        return Result<CourseDto>.Success(_mapper.Map<CourseDto>(course));
    }
}

### 📊 Grade Controller

namespace University.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;
    
    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }
    
    [HttpPost]
    public async Task<ActionResult<GradeDto>> AssignGrade(AssignGradeCommand command)
    {
        var result = await _gradeService.AssignGradeAsync(command);
        
        if (!result.Success)
            return BadRequest(result.Error);
            
        return Ok(result.Value);
    }
    
    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<GradeDto>>> GetStudentGrades(int studentId)
    {
        var grades = await _gradeService.GetStudentGradesAsync(studentId);
        return Ok(grades);
    }
}


## 🗄️ Database Models

### Entity Relationships

// Core entity relationships
public class Student : BaseEntity
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<Grade> Grades { get; set; }
}

public class Course : BaseEntity
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}


## 🔧 Configuration

### Dependency Injection Setup

// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Database
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


## 🧪 Testing

### Unit Tests Example
namespace University.UnitTests.Services;
public class StudentServiceTests
{
    [Fact]
    public async Task CreateStudent_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IStudentRepository>();
        var service = new StudentService(mockRepo.Object, _mapper);
        var command = new CreateStudentCommand("John", "Doe", "john.doe@university.edu");
        
        // Act
        var result = await service.CreateStudentAsync(command);
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal("John", result.Value.FirstName);
    }
}
```

## 👨‍💻 Senourian Developers Team

### Core Architecture Team
- **Ahmed Senouari** - *Senior .NET Architect*
- **Youssef Senouari** - *Backend Development Lead*
- **Fatima Senouari** - *Database Specialist*
- **Mohammed Senouari** - *Frontend & Blazor Expert*

### Technical Expertise
```csharp
public class SenourianTeamExpertise
{
    public string[] BackendTechnologies = 
    {
        "C# .NET 8.0", "ASP.NET Core", "Entity Framework Core", 
        "SQL Server", "Redis", "Docker", "Azure"
    };
    
    public string[] ArchitecturePatterns = 
    {
        "Clean Architecture", "Domain-Driven Design", "CQRS", 
        "Microservices", "Event Sourcing"
    };
    
    public string[] FrontendTechnologies = 
    {
        "Blazor", "React", "Angular", "TypeScript", "MudBlazor"
    };
}
```

## 📊 Performance Features

- **Async/Await** throughout the application
- **Caching** with Redis or MemoryCache
- **Database Indexing** for optimal queries
- **Pagination** for large datasets
- **Compressed Responses** with GZip
- **Database Connection Pooling**

## 🔒 Security Implementation

```csharp
// JWT Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
        };
    });

// Role-based Authorization
[Authorize(Roles = "Administrator,Registrar")]
[HttpPost("courses")]
public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
```

## 🚀 Deployment

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["University.API/University.API.csproj", "University.API/"]
RUN dotnet restore "University.API/University.API.csproj"
COPY . .
RUN dotnet build "University.API/University.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "University.API/University.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "University.API.dll"]
```

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

### Development Workflow
1. Fork the repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open Pull Request

## 📞 Contact & Support

- **Email**: thalsry6@gmail.com

---

<div align="center">

### **🚀 Built with Excellence by Senourian Developers**

**Transforming Education Through .NET Technology** 🎓✨

![Senourian Tech](https://img.shields.io/badge/Senourian-.NET%20Experts-blue?style=for-the-badge&logo=dotnet)

</div>
```

This C# .NET README features:
- 🎯 **Professional structure** for enterprise applications
- 👨‍💻 **Senourian Developers team** highlight
- 🏗️ **Clean Architecture** and **DDD** patterns
- 📚 **Comprehensive code examples** in C#
- 🗄️ **Entity Framework Core** integration
- 🔒 **Security implementation** details
- 🐳 **Docker support** for deployment
- 🧪 **Testing strategies** with xUnit
- 🚀 **Performance optimization** techniques

Perfect for showcasing enterprise-level C# development expertise! 💻✨

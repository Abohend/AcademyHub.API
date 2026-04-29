# AcademyHub API

AcademyHub API is a robust, clean-code .NET Web API designed for managing student enrollments and academic records. It provides a modular architecture with a fully functional in-memory service layer.

## 🏗️ Architecture

The project follows the **Clean Architecture** pattern:
- **AcademyHub.Domain**: Contains core domain entities (Student, Class, Mark, Enrollment).
- **AcademyHub.Application**: Contains DTOs, interfaces, common logic, and request validators.
- **AcademyHub.Infrastructure**: Implements the persistence layer using thread-safe in-memory collections.
- **AcademyHub.API**: The presentation layer built with **FastEndpoints**.

## 🚀 Key Features

### 👨‍🎓 Student Management
- Create, update, and delete students.
- List all students with advanced filtering (name, age).
- Generate detailed student performance reports.

### 🏫 Class Management
- Create and delete academic classes.
- List all classes with filtering (name, teacher).
- Calculate average marks per class.
- Identify top-performing students in each class.

### 📈 Global Pagination
Pagination is applied across all collection-returning endpoints to ensure performance and scalability.
- **Request Parameters**: `Page` (default: 1), `PageSize` (default: 10).
- **Response Metadata**:
  - `TotalCount`: Total number of items.
  - `PageSize`: Number of items per page.
  - `CurrentPage`: The current page number.
  - `TotalPages`: Total number of pages available.
  - `HasNext` / `HasPrevious`: Navigation helpers.

## 🛠️ Technologies
- **.NET 8**
- **FastEndpoints**: For clean and efficient API endpoint definitions.
- **FluentValidation**: For robust request data validation.
- **InMemory Persistence**: Thread-safe collections for high-performance prototyping.

## 📍 API Endpoints

### 👨‍🎓 Students
| Method | Endpoint | Description | Paginated |
| :--- | :--- | :--- | :--- |
| `POST` | `/students` | Create a new student | No |
| `GET` | `/students` | List all students (filter by name/age) | **Yes** |
| `PUT` | `/students/{id}` | Update student details | No |
| `DELETE` | `/students/{id}` | Delete a student | No |
| `GET` | `/students/{id}/report` | Generate student performance report | No |

### 🏫 Classes
| Method | Endpoint | Description | Paginated |
| :--- | :--- | :--- | :--- |
| `POST` | `/classes` | Create a new class | No |
| `GET` | `/classes` | List all classes (filter by name/teacher) | **Yes** |
| `DELETE` | `/classes/{id}` | Delete a class | No |
| `GET` | `/classes/{id}/average-marks` | Get class average marks | No |
| `GET` | `/classes/{id}/top-students` | Get top performing students in class | **Yes** |

### 📝 Enrollments & Marks
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/enrollments` | Enroll a student in a class |
| `POST` | `/marks` | Record marks (exam/assignment) for a student |

## 📝 API Response Format

All responses follow a unified structure:

```json
{
  "success": true,
  "statusCode": 200,
  "message": "Success",
  "data": [...],
  "pagination": {
    "totalCount": 50,
    "pageSize": 10,
    "currentPage": 1,
    "totalPages": 5,
    "hasPrevious": false,
    "hasNext": true
  }
}
```

## 🛠️ Getting Started

1. Clone the repository.
2. Run `dotnet restore`.
3. Run `dotnet run --project AcademyHub.API`.
4. Open the Swagger UI (if configured) or use the `.http` file to test endpoints.

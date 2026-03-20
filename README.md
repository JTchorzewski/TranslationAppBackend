# Translation API Service

## How to Run

### Prerequisites
- .NET 10.0 SDK (or later)
- PostgreSQL Server
- Visual Studio 2022 or VS Code

### Setup Instructions
1. **Database Configuration:**
   Open `TranslationApp_API/appsettings.json` and update the `DefaultConnection` string with your PostgreSQL credentials.
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=TranslationDb;Username=postgres;Password=YourPassword"
   }
Apply Migrations:
Open the Package Manager Console in Visual Studio, select TranslationApp_Infrastructure as the Default project, and run:

PowerShell
Update-Database
(Alternatively, using .NET CLI: dotnet ef database update --project TranslationApp_Infrastructure --startup-project TranslationApp_API)

Run the Application:
Set TranslationApp_API as the startup project and run it (F5). Swagger UI will automatically open in your browser, allowing you to test the endpoints directly.

How to Test
The solution includes a dedicated xUnit test project (TranslationApp.Tests) that tests the core business logic without hitting the external FunTranslations API (using Moq).

To run the tests:

In Visual Studio: Go to Test -> Run All Tests (Ctrl+R, A).

Via CLI: Navigate to the root folder and run:

Bash
dotnet test
Design Decisions
Clean Architecture: The solution is divided into Domain, Application, Infrastructure, and API layers. This ensures that the core business logic (Application) and Domain entities never depend on data access or external web frameworks.

DTOs over Entities: Domain models (like RequestLog) are strictly confined to the backend. The API layer only communicates using DTOs (TranslationRequestDto, LogItemDto), preventing sensitive database schema details from leaking to the client.

PostgreSQL over SQLite: While SQLite was suggested, PostgreSQL was chosen as it reflects a true production-ready environment, handling concurrency and larger datasets much better for audit logging purposes.

Resilience & Rate Limit Handling: The FunTranslations API has strict rate limits (HTTP 429) and blocks unidentified clients (HTTP 403). Instead of throwing raw exceptions (HTTP 500), the TranslationService safely catches these provider errors, stores the failed attempt in the database audit log (with IsSuccess = false), and returns a meaningful, clean error message to the consumer.

How to Add a New Translator Provider
The architecture makes adding a new provider incredibly simple thanks to the Dependency Inversion Principle.

Create a new class in the TranslationApp_Infrastructure/Providers folder (e.g., LibreTranslateProvider).

Implement the existing ITranslationProvider interface:

```csharp
public class LibreTranslateProvider : ITranslationProvider
{
    public async Task<ProviderTranslationResult> TranslateAsync(string text, string translator)
    {
        // Implementation for the new API
    }
}
```
Update the Dependency Injection container in TranslationApp_Application/DependencyInjection.cs to use the new provider:

```csharp
// Swap FunTranslationService with LibreTranslateProvider
services.AddHttpClient<ITranslationProvider, LibreTranslateProvider>();
```
No changes are required in the TranslationService or TranslationController!
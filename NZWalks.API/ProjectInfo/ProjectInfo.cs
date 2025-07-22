namespace NZWalks.API.ProjectInfo
{
    public class ProjectInfo
    {

        // This class serves as a guide for creating RESTful APIs in ASP.NET Core.
        // It outlines the principles of REST, HTTP verbs, and the steps to create a Web API project.
        /*

    REST : 
        Representational State Transferfv

    HTTP Verbs :
        * GET : Retrieve data from the server
        * POST : Send data to the server
        * Put : Update existing data on the server
        * DELETE : Delete data from the server
        * PATCH : Partially update existing data on the server
        * OPTIONS : Describe the communication options for the target resource

    Project Steps :
    1. Create a new ASP.NET Core Web API project
    2. Add necessary NuGet packages 

        *Microsoft.EntityFrameworkCore.SqlServer
        *Microsoft.EntityFrameworkCore.Tools
        -------------------------------------------------------
        *Microsoft.AspNetCore.authentication.JwtBearer
        *microsoft.identity.tokens
        *system.identitymodel.tokens.jwt
        *microsoft.aspnetcore.identity.entityframeworkcore

    3.adjusting launchSettings.json file to use the correct port and launch URL
        * "launchUrl": "swagger/index.html"
        * "applicationUrl": "https://localhost:5001;http://localhost:5000"

    4. Adding DB Context class or reposotory that handles database operations CRUD operations
        * Add DbSet properties for each entity or model (Region, Walk, Difficulty)
        * Maintaining connection to DB
        * Perform CRUD operations
        * Bridge between domain models and the data base

    5. Adding ConnectionString to Database Appsettings.json

    6. Using Dependancy injection to inject the DB Context into the controllers
        this is gonna be written inside Programe.cs or the startup file that build all the project in it also u gonna need opject name in connection strings

    7. Running EF Core Migrations
        * Tools - NuGet Package Manager - Package Manager Console
        * add- migration "Name of migration"
        * Update Database 

    8. fill controllers with CRUD  methods 


    9.adding the DTO models and use it also in the controllers 

    10. use asynchronous programing by implmenting async/await logic to the controllers 
        * This is done by adding async to the method signature and using Task<T> as the return type.
        * Use await keyword when calling asynchronous methods, such as database queries.
        * public asyn Task<IActionResult> GetAllAsync()
        {
            var regions = await dbcontext.Regions.ToListAsync();
            // ... rest of the code

    11.Adding CRUD opreations to the repositories and use repository pattern then call it in the controllers 
       what it dose is sperates all database operations from the controller 
        - Decoupling
        - Reusability
        - Testability
        - changing database will be more easily


    12.Adding automapper to the project by download its packages and create the mapping class also injecting it to Program.cs
        this is used to map between domain models and DTOs, simplifying the conversion process and saving a lot of code that will be written
        to make autoMapper work propably u need to inhirt from parent class "Profile"
        packages
            - AutoMaper
            - automapper.extensions.microsoft.dependencyinjection
    13.JWt Authentication
        * Install the necessary NuGet packages for JWT authentication.
        * add jwt to appsettings.jason
        * injecting JWT authentication in Program.cs and also use it as middleware in the pipeline befor authorization
        */


    }
}

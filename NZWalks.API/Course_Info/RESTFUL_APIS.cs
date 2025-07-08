namespace NZWalks.API.Course_Info
{
    public class RESTFUL_APIS
    {
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
         */
    }
}

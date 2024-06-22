using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.core.Entities.Identity;
using Talabat.core.Repositorires;
using Talabat.Helpers;
using Talabat.Repositery.Data;
using Talabat.Repository;
using Talabat.Repository.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var IdentityConnection = builder.Configuration.GetConnectionString("IdentityConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(IdentityConnection);
});


builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
{
    var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(connection);
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Migrate asynchronously
    await dbContext.Database.MigrateAsync();
    // Ensure the database is created and apply any pending migrations
    // Add your migration logic here (if needed)

    var IdentiyContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

    await IdentiyContext.Database.MigrateAsync();




    await AppContextSeed.SeedAsync(dbContext, loggerFactory);

    await dbContext.Database.EnsureCreatedAsync();
    // Ensure the database is created if it doesn't exist asynchronously

    //----------
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    await AppIdentityDbContextSeed.SeedUserAsync(userManager);


}


builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddIdentityServices();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actioncontext) =>
    {
        var errors = actioncontext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                            .SelectMany(M => M.Value.Errors)
                            .Select(E => E.ErrorMessage)
                            .ToList();
        var vaildationErrorResponse = new ApiValidationErrorReponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(vaildationErrorResponse);
    };
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

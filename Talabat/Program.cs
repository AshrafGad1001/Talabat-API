using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Talabat.core.Repositorires;
using Talabat.Helpers;
using Talabat.Repositery.Data;
using Talabat.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Migrate asynchronously
    await dbContext.Database.MigrateAsync();
    // Ensure the database is created and apply any pending migrations
    // Add your migration logic here (if needed)

    await AppContextSeed.SeedAsync(dbContext, loggerFactory);

    await dbContext.Database.EnsureCreatedAsync();
    // Ensure the database is created if it doesn't exist asynchronously
}


builder.Services.AddAutoMapper(typeof(MappingProfiles));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

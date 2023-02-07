using Discount.API.Extensions;
using Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Code
builder.Services.AddScoped<IDiscountRepository,DiscountRepository>();

var app = builder.Build();


//Custom Code  //reference link :- https://stackoverflow.com/questions/68392429/how-to-run-net-core-console-app-using-generic-host-builder 

//var host = Host.CreateDefaultBuilder(args).Build();
//host.MigrateDatabase<Program>(app);
//host.Run();


//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Custom Code  Reference Link ---- https://code-maze.com/migrations-and-seed-data-efcore/

app.MigrateDatabase<Program>();

app.Run();

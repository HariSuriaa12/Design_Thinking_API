using backend_API.Database;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//For localhost debugging
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") //Allows react app to call API which is from PORT 3002
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//builder.WebHost.UseUrls($"http://*:{port}");
builder.Services.AddDbContext<Connection>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DBConnString"))); //To use actual database
builder.Services.AddControllers();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.UseCors("AllowReactApp");
app.MapControllers();

bool runAppTest = false;
if (!runAppTest)
{
    app.Run();
}
else
{
    app.Run("http://0.0.0.0:7283"); //To allow run on android studio
}

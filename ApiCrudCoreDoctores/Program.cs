using ApiCrudCoreDoctores.Data;
using ApiCrudCoreDoctores.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionStrings = builder.Configuration.GetConnectionString("SqlAzure");

builder.Services.AddTransient<RepositoryDoctores>();
builder.Services.AddDbContext<DoctoresContext>(options =>
{
    options.UseSqlServer(connectionStrings);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Crud Doctores",
        Description = "Api realizada en martes 09/04/2024 a las 13:50",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "Wende Tajamar 2024",
            Email = "wende.profesional@outlook.es"
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        //INDICAR EL ENDPOINT
        options.SwaggerEndpoint(
            url: "/swagger/v1/swagger.json", name: "Api Doctores v1");
        options.RoutePrefix = "";

    });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

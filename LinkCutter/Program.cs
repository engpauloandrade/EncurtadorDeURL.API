using LinkCutter.Application.InjecaoDependencia;
using LinkCutter.Domain.DependencyInjection;
using LinkCutter.Repository.Database;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Adiciona services to Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LinkCutter API",
        Version = "v1",
        Description = "API para encurtar links.",
        Contact = new OpenApiContact
        {
            Name = "Link Cutter",
            Email = "teste@gmail.com",
            Url = new Uri("https://github.com")
        }
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
builder.Services.AddSecurity();

builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddErrorMiddleware();
app.UseAuthorization();
//app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();

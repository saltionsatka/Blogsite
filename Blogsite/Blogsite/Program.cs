using Blogsite.Data;
using Blogsite.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(builder.Configuration["Data:ConnectionStrings:DefaultConnection"]));

//builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

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

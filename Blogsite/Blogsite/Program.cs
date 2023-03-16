using Blogsite.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Blogsite.Interfaces;
using Blogsite.Services;
using Blogsite.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(builder.Configuration["Data:ConnectionStrings:DefaultConnection"]));

//builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();


builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

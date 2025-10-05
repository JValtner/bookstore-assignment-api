using System;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using BookstoreApplication.Settings;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

// Configure PostgreSQL database connection
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//DI - Repositories and Services
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IPublishersRepository, PublishersRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IPublishersService, PublishersService>();

//Automapper
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<MappingProfile>();
});
//Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

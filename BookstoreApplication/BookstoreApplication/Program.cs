using System;
using System.Security.Claims;
using System.Text;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using BookstoreApplication.Services.IService;
using BookstoreApplication.Settings;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Registracija Identity-a
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
  .AddEntityFrameworkStores<BookStoreDbContext>()
  .AddDefaultTokenProviders();

// Definisanje uslova koje lozinka mora da ispuni
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;          // Ima bar jednu cifru
    options.Password.RequireLowercase = true;      // Ima bar jedno malo slovo
    options.Password.RequireUppercase = true;      // Ima bar jedno veliko slovo
    options.Password.RequireNonAlphanumeric = true;// Ima bar jedan specijalan karakter (!, @, #...)
    options.Password.RequiredLength = 8;           // Ima bar 8 karaktera
});

// Dodavanje autentifikacije
builder.Services.AddAuthentication();

// AddAsync services to the container.

builder.Services.AddControllers();
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building Example API", Version = "v1" });

    // Definisanje JWT Bearer autentifikacije
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    // Primena Bearer autentifikacije
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      Array.Empty<string>()
    }
  });
});
builder.Services.AddAuthorization(options =>
{
    // Publicly accessible endpoints
    options.AddPolicy("PublicGet",
        policy => policy.RequireAssertion(_ => true)); // anyone (authenticated or not)

    // Content management
    options.AddPolicy("RegisteredGet",
        policy => policy.RequireRole("Editor", "Librarian"));
    options.AddPolicy("RegisteredPost",
        policy => policy.RequireRole("Editor", "Librarian"));
    options.AddPolicy("EditContent",
        policy => policy.RequireRole("Editor"));

    // Anonymous-only (unregistered) users
    options.AddPolicy("UnregisteredGet",
        policy => policy.RequireAssertion(context =>
            context.User?.Identity == null || !context.User.Identity.IsAuthenticated));
});



// Configure PostgreSQL database connection
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//DI - Repositories and Services
builder.Services.AddScoped<IAuthService, AuthService>();
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

//Exception handling middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Uključivanje autentifikacije
builder.Services.AddAuthentication(options =>
{ // Naglašavamo da koristimo JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true, // Validacija da li je token istekao

        ValidateIssuer = true,   // Validacija URL-a aplikacije koja izdaje token
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // URL aplikacije koja izdaje token (čita se iz appsettings.json)

        ValidateAudience = true, // Validacija URL-a aplikacije koja koristi token
        ValidAudience = builder.Configuration["Jwt:Audience"], // URL aplikacije koja koristi token (čita se iz appsettings.json)

        ValidateIssuerSigningKey = true, // Validacija ključa za potpisivanje tokena (koji se koristi i pri proveri potpisa)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), //Ključ za proveru tokena (čita se iz appsettings.json)

        RoleClaimType = ClaimTypes.Role // Potrebno za kontrolu pristupa, što ćemo videti kasnije
    };
});

var app = builder.Build();


//Exception handling middleware use
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

// Seed default users and roles
await SeedData.SeedDataAsync(app);
app.Run();

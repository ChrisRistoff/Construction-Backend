using System.Text;
using construction.Data;
using construction.Repositories;
using construction.Seed;
using construction.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// set up cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});



// set up jwt authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });



// set up swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "Construction", Version = "v1"});

    // add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
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



builder.Services.AddControllers();



// get the environment
string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

// set the connection string based on the environment
string connectionStringName = "";

if (env == "Testing")
{
    connectionStringName = "TestConnection";
}

if (env == "Development")
{
    connectionStringName = "DefaultConnection";
}

if (env == "Production")
{
    connectionStringName = "ProductionConnection";
}

if (env == "DockerTest")
{
    connectionStringName = "DockerTestConnection";
}

// set up database context
builder.Services.AddDbContext<MyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionStringName)));



// repositories
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<BusinessInfoRepository>();
builder.Services.AddScoped<JobTypesRepository>();
builder.Services.AddScoped<JobsRepository>();



// services
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<StorageService>();


// build the app
var app = builder.Build();

// set up migrations and seeding
if (env == "Development" || env == "Testing")
{
    try
    {

        // migrate
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MyContext>();
            dbContext.Database.Migrate();
        }

        // seed
        await SeedAdmin.Seed(builder.Configuration.GetConnectionString(connectionStringName), builder.Configuration);
        await SeedBusinessInfo.Seed(builder.Configuration.GetConnectionString(connectionStringName), builder.Configuration);
        await SeedJobTypes.Seed(builder.Configuration.GetConnectionString(connectionStringName), builder.Configuration);
        await SeedJobs.Seed(builder.Configuration.GetConnectionString(connectionStringName), builder.Configuration);
        await SeedImages.Seed(builder.Configuration.GetConnectionString(connectionStringName), builder.Configuration);
    }

    catch (Exception ex)
    {
        Console.WriteLine("An error occurred during migration or seeding: " + ex.Message);
    }
}


// swagger in dev and production
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();

public partial class Program { }

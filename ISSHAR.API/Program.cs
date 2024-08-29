using CloudinaryDotNet;
using ISSHAR.API.Interfaces;
using ISSHAR.API.Middlewares;
using ISSHAR.API.Services;
using ISSHAR.Application.Profiles;
using ISSHAR.Application.Services;
using ISSHAR.DAL;
using ISSHAR.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

ConfigureSwaggerServices(builder.Services, configuration);
ConfigureJwtAuthentication(builder.Services, configuration);
ConfigureCloudinaryServices(builder.Services, configuration);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

InjectServicesAndRepositories(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<JwtWhitelistMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void ConfigureSwaggerServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ISHHAR", Version = "v1" });

        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtIssuer = configuration.GetSection("JwtSettings:Issuer").Get<string>();
    var jwtKey = configuration.GetSection("JwtSettings:Secret").Get<string>();
    var jwtAudience = configuration.GetSection("JwtSettings:Audience").Get<string>();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
}

static void ConfigureCloudinaryServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddSingleton(_ =>
    {
        var cloudinaryAccount = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]);

        return new Cloudinary(cloudinaryAccount);
    });
}

static void InjectServicesAndRepositories(IServiceCollection services)
{
    services.AddSingleton<IJwtGenerator, JwtGenerator>();
    services.AddSingleton<IJwtWhitelistService, JwtWhitelistService>();

    services.AddScoped<IImageService, CloudinaryImageService>();

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();

    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAdvertisementService, AdvertisementService>();

    services.AddScoped<IHallRepository, HallRepository>();
    services.AddScoped<IHallService, HallService>();

    services.AddScoped<IBookingRepository, BookingRepository>();
    services.AddScoped<IBookingService, BookingService>();

    services.AddScoped<ICardTempleteRepository, CardTempleteRepository>();
    services.AddScoped<ICardTempleteService, CardTempleteService>();

    services.AddScoped<ICardRepository, CardRepository>();
    services.AddScoped<ICardService, CardService>();

    services.AddScoped<IInviteRepository, InviteRepository>();
    services.AddScoped<IInviteService, InviteService>();
}
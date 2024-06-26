using CloudinaryDotNet;
using ISSHAR.API;
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

builder.Services.AddSwaggerGen(options =>
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

var jwtIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JwtSettings:Secret").Get<string>();
var jwtAudience = builder.Configuration.GetSection("JwtSettings:Audience").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.AddSingleton(_ =>
{
    var cloudinaryAccount = new Account(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:ApiSecret"]);

    return new Cloudinary(cloudinaryAccount);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

InjectServicesAndRepositories(builder);

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


app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void InjectServicesAndRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IJwtGenerator,JwtGenerator>();

    builder.Services.AddScoped<IImageService, CloudinaryImageService>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();

    builder.Services.AddScoped<IHallRepository, HallRepository>();
    builder.Services.AddScoped<IHallService, HallService>();

    builder.Services.AddScoped<IBookingRepository, BookingRepository>();
    builder.Services.AddScoped<IBookingService, BookingService>();

    builder.Services.AddScoped<ICardTempleteRepository, CardTempleteRepository>();
    builder.Services.AddScoped<ICardTempleteService, CardTempleteService>();

    builder.Services.AddScoped<ICardRepository, CardRepository>();
    builder.Services.AddScoped<ICardService, CardService>();

    builder.Services.AddScoped<IInviteRepository, InviteRepository>();
    builder.Services.AddScoped<IInviteService, InviteService>();
}
using CloudinaryDotNet;
using ISSHAR.Application.Profiles;
using ISSHAR.Application.Services;
using ISSHAR.DAL;
using ISSHAR.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

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
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.AddSingleton(_ =>
{
    var cloudinaryAccount = new Account(
        builder.Configuration["Cloudinary:CloudName"],
        builder.Configuration["Cloudinary:ApiKey"],
        builder.Configuration["Cloudinary:ApiSecret"]);

    return new Cloudinary(cloudinaryAccount);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
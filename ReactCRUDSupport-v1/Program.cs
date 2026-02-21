using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ReactCRUDSupport_v1.Database;
using ReactCRUDSupport_v1.Mappings;
using ReactCRUDSupport_v1.Repositories;
using ReactCRUDSupport_v1.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // Add your React dev port
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ReactCRUDSupportAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReactCRUDSupportAuthConnectionString")));

builder.Services.AddDbContext<ValuesDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ValuesDbConnectionString")));

//jwt token conifgurations:
//1)set up identity:
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ReactCRUD")
    .AddEntityFrameworkStores<ReactCRUDSupportAuthDbContext>()
    .AddDefaultTokenProviders();

//2)configure password requirements:
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});

//3)Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IValuesRepository, ValuesRepository>();

var app = builder.Build();
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

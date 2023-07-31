using ACF.Infrastructure.Data;
using ACF.Infrastructure.Interfaces.IRepositories;
using ACF.Infrastructure.Interfaces.IServices;
using ACF.Infrastructure.Repositories;
using ACF.Infrastructure.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IClientServices, ClientServices>();

#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddDbContext<ACFContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"));
    options.EnableSensitiveDataLogging();
});

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

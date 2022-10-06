using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qick.Handler.HandlerInterfaces;
using Qick.Handler.LoginHandler;
using Qick.Models;
using Qick.Repositories;
using Qick.Repositories.Interfaces;
using Qick.Services;
using Qick.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("default", policy => {
    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
}));
builder.Services.AddDbContext<QickDatabaseManangementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Dbcon")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckLoginHandler, CheckLoginHandler>();
builder.Services.AddScoped<ICreateTokenService, CreateTokenService>();
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JWTKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

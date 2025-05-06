using System.Text;
using Bussines.serviceRepository;
using Bussines.Token;
using Data.Repository;
using Entity.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//agregar servico del controller
builder.Services.AddControllers();

//configuracion de swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//agregar los servicios
builder.Services.AddScoped<UserServices>();
    
//agregar data Generica
builder.Services.AddScoped(typeof(DataGeneric<>));
builder.Services.AddScoped<CreateToken>();
builder.Services.AddScoped<UserRepository>();

//configuracion del autoMapper
builder.Services.AddAutoMapper(typeof(Bussines.Map.AutoMapper));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("name=defaultConnection");
});


// JWT
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // ?? Redirección automática de "/" a "/swagger"
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();

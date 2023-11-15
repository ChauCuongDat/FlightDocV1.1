using FlightDocV1._1;
using FlightDocV1._1.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FlightDocContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStr")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfig>();

builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "TokenIssuer",
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("The Super Duper Extreamly Mystery Key")),
        ValidAudience = "TokenUser",
        ValidateAudience = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireClaim("Role", "admin"));
    options.AddPolicy("Office", p => p.RequireClaim("Role", "office"));
    options.AddPolicy("Pilot", p => p.RequireClaim("Role", "pilot"));
    options.AddPolicy("Crew", p => p.RequireClaim("Role", "crew"));

    options.AddPolicy("Admin and Office", p =>
    {
        p.RequireAssertion(context => context.User.HasClaim(c => c.Type == "Role" && c.Value == "admin") ||
                                      context.User.HasClaim(c => c.Type == "Role" && c.Value == "office"));
    });
    options.AddPolicy("Admin, Pilot and Crew", p =>
    {
        p.RequireAssertion(context => context.User.HasClaim(c => c.Type == "Role" && c.Value == "admin") ||
                                      context.User.HasClaim(c => c.Type == "Role" && c.Value == "pilot") ||
                                      context.User.HasClaim(c => c.Type == "Role" && c.Value == "crew"));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

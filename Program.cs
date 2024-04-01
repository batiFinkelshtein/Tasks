using todoList.Services;
using todoList.Interfaces;
using Microsoft.AspNetCore.Builder;
using todoList.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);


builder.Services
     // 
     .AddAuthentication(options =>
     {
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(cfg =>
     {
         cfg.RequireHttpsMetadata = false;
         cfg.TokenValidationParameters = TokenServise.GetTokenValidationParameters();
     });

//auth2
builder.Services.AddAuthorization(cfg =>
     {
         cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
         cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
     });

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks", Version = "v1" });
     //auth3
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         In = ParameterLocation.Header,
         Description = "Please enter JWT with Bearer into field",
         Name = "Authorization",
         Type = SecuritySchemeType.ApiKey
     });
     //auth4
     c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {

                    }
                }
     });
 });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTask();
builder.Services.AddUser();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
app.UseLogMiddleware("file.log");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();
app.Run();


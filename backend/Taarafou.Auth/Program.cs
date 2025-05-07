// backend/Taarafou.Auth/Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1) إضافة CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy
          .WithOrigins("http://localhost:3000")
          .AllowAnyHeader()
          .AllowAnyMethod()
    );
});

// 2) إضافة الكنترولرز
builder.Services.AddControllers();

var app = builder.Build();

// 3) استخدم سياسة CORS
app.UseCors("AllowReactApp");

// 4) ماب الكنترولرز
app.MapControllers();

app.Run();

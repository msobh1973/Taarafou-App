// File: backend/Taarafou.Posts/Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taarafou.Posts;              // <-- لضمان العثور على PostsContext

var builder = WebApplication.CreateBuilder(args);

// 1. تسجيل DbContext لاستخدام SQLite
builder.Services.AddDbContext<PostsContext>(options =>
    options.UseSqlite("Data Source=posts.db")
);

// 2. إضافة إعدادات CORS للسماح للواجهة بالوصول
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
    );
});

// 3. تسجيل Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 4. تفعيل CORS قبل MapControllers
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;       // <-- لاستدعاء OpenApiInfo
using Taarafou.Posts;

var builder = WebApplication.CreateBuilder(args);

// قراءة الإعدادات من ملف appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// تسجيل DbContext مع SQLite
builder.Services.AddDbContext<PostsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// تسجيل الـ Controllers
builder.Services.AddControllers();

// تهيئة Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Taarafou Posts API",
        Version = "v1"
    });
});

var app = builder.Build();

// طبق الهجرات آلياً عند الإقلاع
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostsContext>();
    db.Database.Migrate();
}

// في بيئة التطوير، فعّل Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taarafou Posts API v1");
    });
}

app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Taarafou.Posts; // عدّل الـ namespace إذا اختلف

var builder = WebApplication.CreateBuilder(args);

// إضافة الـ Controllers و Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *** ربط Azure SQL database ***
builder.Services.AddDbContext<PostsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PostsConnection")));

var app = builder.Build();

// تطبيق الهجرات تلقائيًّا عند الإقلاع
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostsContext>();
    db.Database.Migrate();
}

// تهيئة Swagger في التطوير (أو الإنتاج إذا أردت)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

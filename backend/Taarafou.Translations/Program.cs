using System;
using Azure;
using Azure.AI.Translation.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// اقرأ الإعدادات من appsettings.json
var endpoint = builder.Configuration["Translator:Endpoint"]!;
var key      = builder.Configuration["Translator:Key"]!;
var region   = builder.Configuration["Translator:Region"]!;

// سجّل TextTranslationClient بالـ DI Container
builder.Services.AddSingleton(sp =>
    new TextTranslationClient(
        new AzureKeyCredential(key),      // your API key credential :contentReference[oaicite:0]{index=0}
        new Uri(endpoint),                // endpoint URI :contentReference[oaicite:1]{index=1}
        region                            // region (e.g. "global")
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

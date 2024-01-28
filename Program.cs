using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using qodev_authentication_services.auth;
using qodev_authentication_services.utils.jwt;
using qodev_content_management_services.core.constructor;
using qodev_content_management_services.db;

var builder = WebApplication.CreateBuilder(args);
var myappOrigins = "_myAppOrigins";

ConfigurationManager configurationManager = builder.Configuration;

builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlServer(configurationManager["connectionStrings:development"],
        providerOptions => providerOptions.EnableRetryOnFailure())
);

var serviceConfigurator = new ServiceConfigurator();
serviceConfigurator.AddJwtAuthentication(builder.Services, configurationManager);
serviceConfigurator.ApplicationConfiguration(builder.Services, configurationManager);


Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseUrls("http://localhost:5001");
        webBuilder.UseStartup<WebApplication>();
    });
builder.Services.AddScoped<KeyAuthFilter>();
builder.Services.AddScoped<cms_service>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();
app.UseRouting();
app.UseCors(myappOrigins);
app.UseMiddleware<Middleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
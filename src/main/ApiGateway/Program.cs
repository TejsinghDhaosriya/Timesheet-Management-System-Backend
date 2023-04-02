using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Services/TimesheetService"))
//   .AddJsonFile("localsettings.json", optional: true, reloadOnChange: true);

builder.Configuration.AddJsonFile("localsettings-apigateway.json", optional: true, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

//Key Cloak Auth

var publicKey = builder.Configuration.GetSection("KeyCloakAuth:publicKey").Value;
var isStagingValue = builder.Configuration.GetSection("KeyCloakAuth:isStaging").Value;
bool isStaging = bool.Parse(isStagingValue);
var clientName = builder.Configuration.GetSection("KeyCloakAuth:clientName").Value;
var issuer = builder.Configuration.GetSection("KeyCloakAuth:issuer").Value;

builder.Services.AddKeyCloakJwtAAuthExtension(publicKey, issuer, isStaging);
builder.Services.AddTransient<IClaimsTransformation>(_ => new ClaimsTransformer(clientName));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

await app.UseOcelot();

app.UseAuthorization();

app.Run();

using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);

//Key Cloak Auth
const string publicKey =
    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqjpWuE2D3Qp2U6v1CZYDFazKa36uNY8vylAOoiztAUARtf1CVZm8iM497PM+kBVlLkPOjiQtWwQ7zoLiqdn3ERiNfheQg8FL6DVSuz6ud1BciNADhhN+du6l2OBnXOV8m3sDBLg6RFdXW2od4TQy2z3ekB9MhF1zfibF86g6ZVXkQ9gAq8A0qqIVj2T+IaIJVhyHaap5AdoJzydlkvXySmoBQKNp/F/AU87U6sT8iRC/atvKAGKqQUIhJRvmOgW7pdjifu90Mn8iFVtOvXAeEK9vo/H4zlwz+pUXDnY8ZmYAdX1t/SWpQ/IEznaER8mmzXPZ0fzitb9tKLBaUVWnHwIDAQAB";
const bool isStaging = true;
const string clientName = "TMS-Client";
const string issuer = "https://143.110.248.171:8443/realms/Augmento";

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

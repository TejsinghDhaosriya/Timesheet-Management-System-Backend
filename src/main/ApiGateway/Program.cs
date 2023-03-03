using System.Net;
using System.Text;
using ApiGateway;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
builder.Services.AddSingleton(new HttpClient(handler));

// other service registrations
var app = builder.Build();

// app.UseMiddleware<CustomAuthenticationMiddleware>();
// app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
// app.UseRouting();

//app.UseAuthentication();
// app.UseAuthorization();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers().RequireAuthorization("KeycloakPolicy");
// });


// await app.UseOcelot();


using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

var httpClient = serviceProvider.GetRequiredService<HttpClient>();

string url = "https://143.110.248.171:8443/realms/Augmento/protocol/openid-connect/token/introspect";
var configuration = new OcelotPipelineConfiguration
{
    AuthenticationMiddleware = async (ctx, next) =>
    {
        string reqToken = ctx.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(reqToken))
        {
            //Authentication
            ctx.Response.StatusCode = 401;
            await ctx.Response.WriteAsync("Unauthorized");
            return;
        }

        //Authorization
        var keyCloakRequestBody = new List<KeyValuePair<string, string>>();
        keyCloakRequestBody.Add(new KeyValuePair<string, string>("client_id", "TEST"));
        keyCloakRequestBody.Add(new KeyValuePair<string, string>("client_secret", "aOvpJWdZEybsh3THPBHPOVbs7hazW8z8"));
        keyCloakRequestBody.Add(new KeyValuePair<string, string>("token", reqToken));

        // Todo : Inject Via Service
        var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(keyCloakRequestBody));
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        var token = JsonConvert.DeserializeObject<TokenJson>(responseBody);
        if (response.IsSuccessStatusCode && token.Active)
        {
            ctx.Request.Headers["TMS-X-PROJECT-ID"] = token.ProjectId + "";
            ctx.Request.Headers["TMS-X-ORGANIZATION-ID"] = token.OrganizationId + "";
            await next.Invoke();
        }
        else
        {
            ctx.Response.StatusCode = 403;
            await ctx.Response.WriteAsync("Forbidden");
        }
        
    }
};

await app.UseOcelot(configuration);

app.Run();
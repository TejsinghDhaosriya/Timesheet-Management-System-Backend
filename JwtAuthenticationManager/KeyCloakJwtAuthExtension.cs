using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace JwtAuthenticationManager
{
    public static class KeyCloakJwtAuthExtension
    {
        public static void AddKeyCloakJwtAAuthExtension(this IServiceCollection services,string publicKey,string issuer,bool isStaging)
        {
            
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
              
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuers = new[] { issuer },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = BuildRsaKey(publicKey),
                    ValidateLifetime = true
                };

                o.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = c =>
                    {
                        Console.WriteLine("User successfully authenticated");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";

                        return c.Response.WriteAsync(isStaging ? c.Exception.ToString() : "An error occurred during authentication.");
                    }
                };

            });
        }

        private static RsaSecurityKey BuildRsaKey(string publicKeyJwt)
        {
            var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(
                source: Convert.FromBase64String(publicKeyJwt),
                bytesRead: out _
            );

            var issuerSigningKey = new RsaSecurityKey(rsa);
            return issuerSigningKey;
        }
    }

}
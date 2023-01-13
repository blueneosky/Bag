using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alphonse.WebApi.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alphonse.WebApi.Setup;

public static class AuthenticationSetup
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        if (bool.TryParse(builder.Configuration["Alphonse:WithoutAuthorization"], out var withoutAuthorization) && withoutAuthorization)
            return;

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var issuer = builder.Configuration["Alphonse:JwtIssuer"];
            var audience = builder.Configuration["Alphonse:JwtAudience"];
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Alphonse:JwtSecretKey"]!);

            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                // ClockSkew = 

                ValidIssuer = issuer,
                ValidateIssuer = !string.IsNullOrWhiteSpace(issuer),

                ValidAudience = audience,
                ValidateAudience = !string.IsNullOrWhiteSpace(audience),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
        });
    }

    public static void ConfigureAuthentication(this SwaggerGenOptions options, WebApplicationBuilder builder)
    {
        if (bool.TryParse(builder.Configuration["Alphonse:WithoutAuthorization"], out var withoutAuthorization) && withoutAuthorization)
            return;

        options.AddSecurityDefinition("JWT_Auth", new OpenApiSecurityScheme()
        {
            Name = "Bearer",
            BearerFormat = "JWT",
            Scheme = "bearer",
            Description = "Specify the authorization token.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
        });

        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Id = "JWT_Auth",
                Type = ReferenceType.SecurityScheme
            }
        };
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, new string[] { } },
        });
    }

    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
       builder.Services.AddSingleton<IAuthorizationPolicyProvider, MinimumAccessRolePolicyProvider>();
       builder.Services.AddScoped<IAuthorizationHandler, MinimumAccessRoleAuthorizationHandler>();
    }
}

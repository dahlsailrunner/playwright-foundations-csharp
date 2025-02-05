﻿using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace CarvedRock.Api;

[ExcludeFromCodeCoverage]
public class SwaggerOptions(ILogger<SwaggerOptions> logger) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly ILogger<SwaggerOptions> _logger = logger;

    public void Configure(SwaggerGenOptions options)
    {
        try
        {
            var disco = GetDiscoveryDocument();
            var oauthScopes = new Dictionary<string, string>
            {
                { "api", "Resource access: api" },
                { "openid", "OpenID information"},
                { "profile", "User profile information" },
                { "email", "User email address" }
            };
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(disco.AuthorizeEndpoint!),
                        TokenUrl = new Uri(disco.TokenEndpoint!),
                        Scopes = oauthScopes
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    oauthScopes.Keys.ToArray()
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error loading discovery document for Swagger UI");
        }
    }
    
    private static DiscoveryDocumentResponse GetDiscoveryDocument()
    {
        var client = new HttpClient();
        var authority = "https://demo.duendesoftware.com";
        return client.GetDiscoveryDocumentAsync(authority)
            .GetAwaiter()
            .GetResult();
    }
}
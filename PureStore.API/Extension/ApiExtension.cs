using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PureStore.API.Abstractions;
using PureStore.API.Middlewares;
using PureStore.Domain.Common;
using System.Text;

namespace PureStore.API.Extension
{
    public static class ApiExtension
    {
        public static void ConfigureService(this WebApplicationBuilder builder)
        {
            //commentted 'cause we need to use minimap api def
            //builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<GlobaleExceptionHandler>();


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pure Store API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = $@"JWT Authorization header using the bearer scheme.
                        Enter 'Bearer'[Space] and then past your token int the input text below.
                        {Environment.NewLine}Example: 'Bearer [TOKEN]'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
                    ValidAudience = builder.Configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]))
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = a =>
                    {
                        a.Response.StatusCode = 401;
                        a.Response.ContentType = "application/json";
                        return a.Response.WriteAsJsonAsync(new Response<string>("You are not authorized"));
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                        }
                        return context.Response.WriteAsJsonAsync(new Response<string>("You are not authorized"));
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(new Response<string>("You are not authorized"));
                    }
                };

            });
        }

        /// <summary>
        /// Auto register each new module's created endpoint.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureEndpointsAutoRegistration(this WebApplication app)
        {
            var endpointsDefs = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointdef in endpointsDefs)
            {
                endpointdef.RegisterEndpoints(app);
            }
        }
    }
}

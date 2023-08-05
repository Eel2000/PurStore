using PureStore.Persistence;
using PureStore.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PureStore.Domain.Common;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pure Store API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the bearer scheme.\r\n\r\n
                                    Enter 'Bearer'[Space] and then past your token int the input text below.
                                    \r\n\r\nExample: 'Bearer <TOKEN>'",
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
});

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

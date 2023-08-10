using PureStore.API.Extension;
using PureStore.API.Middlewares;
using PureStore.Application;
using PureStore.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureService();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobaleExceptionHandler>();
app.UseHttpsRedirection();

//app.UseCors();to configure later
app.UseAuthentication();
app.UseAuthorization();

app.ConfigureEndpointsAutoRegistration();

app.Run();

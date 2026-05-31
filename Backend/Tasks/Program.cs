using Dapper.FluentMap;
using Microsoft.OpenApi;
using Tasks.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks API", Version = "v1" });
});

// Register application services and repositories
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

//Dapper Fluent Mappers
FluentMapper.Initialize(config =>
{
    config.AddMap(new TaskGridDomainModelMap());
    config.AddMap(new TaskDetailsDomainModelMap());
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.OpenApi;
using System.Reflection;
using Tasks.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// FluentValidation registration
builder.Services.AddFluentValidations();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks API", Version = "v1" });
    // Include XML comments (if generated) for better API documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register application services and repositories
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

DapperFluentMapperExtensions.AddDapperFluentMappers();

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

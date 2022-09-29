using Microsoft.OpenApi.Models;
using ThorAdmin.Services;
using ThorAdmin.Services.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

services.AddScoped<IMySqlService, MySqlService>();
services.AddScoped<IWordPressService, WordPressService>();
services.AddScoped<IFileSystemService, FileSystemService>();

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Thor API",
        Description = "Thor is an administration API for managing your web server.",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "api-docs";
    });
}
else
{
    // serve Angular from 'wwwroot' and handle browser refresh
    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
        {
            context.Request.Path = "/index.html";
            await next();
        }
    });
    app.UseFileServer();

    app.UseHttpsRedirection();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

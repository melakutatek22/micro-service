using MassTransit;
using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using ReportsService.Consumers;
using ReportsService.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Database --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- MassTransit (RabbitMQ) --------------------
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<JobSeekerCreatedConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("jobseeker-created-report-queue", e =>
        {
            e.ConfigureConsumer<JobSeekerCreatedConsumer>(ctx);
        });
    });
});

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Report Service API",
        Version = "v1"
    });

    // JWT Auth for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your JWT token}'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // CORS fix for Aggregator Swagger UI
    var swaggerServerUrl = builder.Configuration["Swagger:ServerUrl"];
    if (!string.IsNullOrWhiteSpace(swaggerServerUrl))
    {
        options.AddServer(new OpenApiServer
        {
            Url = swaggerServerUrl,
            Description = builder.Configuration["Swagger:Description"] ?? "Default Server"
        });
    }
});

// -------------------- CORS --------------------
var allowedOrigins = builder.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI", policy =>
    {
        if (allowedOrigins != null && allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowSwaggerUI");
app.UseAuthorization();
app.MapControllers();

app.Run();

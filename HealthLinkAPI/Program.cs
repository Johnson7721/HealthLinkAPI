using HealthLinkAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IHealthLinkStore, DemoHealthLinkStore>();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("AngularDevelopment", policy =>
    policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularDevelopment");

app.UseAuthorization();

app.MapControllers();

app.Run();

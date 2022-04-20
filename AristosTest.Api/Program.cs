using AristosTest.Api.Calls;
using AristosTest.Api.Data;
using AristosTest.Api.Services.Implementation;
using AristosTest.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DB
builder.Services.AddDbContext<AirportContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("AirportDb"));
});

// Add Services
builder.Services.AddScoped<IAirportService, AirportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Calls
AirportCalls.SetCalls(app);

app.Run();

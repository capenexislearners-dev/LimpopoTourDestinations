using LimpopoTourDestinations.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TourDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("TourConnection")));
// Add this near the top with other builder.Services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7093") // your Blazor app URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
;

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowBlazor");
app.UseAuthorization();

app.MapControllers();

app.Run();

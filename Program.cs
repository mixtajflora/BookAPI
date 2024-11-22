using BookAPI.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<BookContext>(options =>
    options.UseNpgsql(configuration["ConnectionString:DbConnection"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection(); // frontend
app.UseAuthorization(); // frontend
app.UseHttpsRedirection();
app.UseDefaultFiles(); // Kiszolgálja az index.html fájlt automatikusan
app.UseStaticFiles();// 

app.MapControllers();

app.Run();
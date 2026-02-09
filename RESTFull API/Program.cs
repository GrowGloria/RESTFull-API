using Microsoft.EntityFrameworkCore;
using RESTFull_API.Data;
using RESTFull_API.Repositories.Implimentations;
using RESTFull_API.Repositories.Interface;
using RESTFull_API.Services;
using RESTFull_API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IRollRepository, RollRepository>();
builder.Services.AddScoped<IRollService, RollService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	try
	{
		db.Database.Migrate();
	}
	catch (Exception ex)
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "Ошибка при применении миграций БД");
		throw;
	}
}

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using BussinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Mapping;
using DataAccess.Repository;
using Repository.IRepository;
using Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>()
    .AddDbContext<FinalProPrn231Context>(opt =>
    builder.Configuration.GetConnectionString("MyStoreDB"));
builder.Services.AddTransient<ICityRepository, CityRepository>()
	.AddDbContext<FinalProPrn231Context>(opt =>
	builder.Configuration.GetConnectionString("MyStoreDB"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

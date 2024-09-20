using api.Services;
using api.Services.EastCoast;
using api.Services.WestCoast;
using common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new ServiceModelBinderProvider());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register all dependencies as always
builder.Services.AddScoped<SeattleService>();
builder.Services.AddScoped<NewYorkService>();

// register all services for the use cases - they won't be activated until requested
builder.Services.AddScoped<EastWeatherService>();
builder.Services.AddScoped<WestWeatherService>();

// register all services for the use case
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

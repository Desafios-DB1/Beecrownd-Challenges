using _1001___Extremely_Basic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SumService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
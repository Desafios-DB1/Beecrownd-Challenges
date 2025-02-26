using IdentificandoCha.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ContestantService>();

var app = builder.Build();

app.MapControllers();
app.Run();
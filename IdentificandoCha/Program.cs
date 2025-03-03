using FluentValidation;
using FluentValidation.AspNetCore;
using IdentificandoCha.DTOs;
using IdentificandoCha.Repository;
using IdentificandoCha.Services;
using IdentificandoCha.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ContestantService>();
builder.Services.AddScoped<ChallengeService>();
builder.Services.AddScoped<ScoringService>();

builder.Services.AddScoped<ContestantRepository>();
builder.Services.AddScoped<ChallengeRepository>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<IValidator<ContestantData>, ContestantDataValidator>();
builder.Services.AddTransient<IValidator<List<ContestantAnswer>>, ContestantAnswerValidator>();

var app = builder.Build();

app.MapControllers();
app.Run();
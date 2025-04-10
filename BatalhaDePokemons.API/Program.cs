using System.Reflection;
using System.Text.Json.Serialization;
using BatalhaDePokemons.API.Middlewares;
using BatalhaDePokemons.Crosscutting.Enums;
using BatalhaDePokemons.Crosscutting.Interfaces;
using BatalhaDePokemons.Crosscutting.SwaggerExamples;
using BatalhaDePokemons.Crosscutting.Validators;
using BatalhaDePokemons.Domain.Repositories;
using BatalhaDePokemons.Domain.Services;
using BatalhaDePokemons.Infra;
using BatalhaDePokemons.Infra.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IAtaqueService, AtaqueService>();
builder.Services.AddScoped<IBatalhaService, BatalhaService>();

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IAtaqueRepository, AtaqueRepository>();
builder.Services.AddScoped<IPokemonAtaqueRepository, PokemonAtaqueRepository>();
builder.Services.AddScoped<IBatalhaRepository, BatalhaRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

builder.Services.AddSwaggerExamplesFromAssemblyOf<PokemonCreationDtoExample>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AtaqueCreationDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PokemonCreationDtoValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ConfigureSwagger);

builder.Services.AddDbContext<PokemonsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5213";
        options.Audience = "batalhadepokemons";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Batalha de Pokemons v1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
return;


void ConfigureSwagger(SwaggerGenOptions c)
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Batalha de Pokemons",
        Description = "Documentação sobre consumo da api da aplicação de batalha pokemon"
    });

    c.MapType<Tipo>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetValues<Tipo>()
            .Select(v => new OpenApiString(v.ToString()) as IOpenApiAny)
            .ToList()
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o Token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
    c.IncludeXmlComments(xmlPath);
    
    c.ExampleFilters();
}
using Microsoft.AspNetCore.Mvc;
using Q6.Exceptions;
using Q6.Repositories;
using Q6.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState
                .Where(item => item.Value is not null && item.Value.Errors.Count > 0)
                .SelectMany(item => item.Value!.Errors.Select(error => error.ErrorMessage))
                .ToList();

            return new BadRequestObjectResult(new
            {
                mensagem = "Os dados enviados sao invalidos.",
                erros
            });
        };
    });

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<OrcamentoService>();
builder.Services.AddScoped<IOrcamentoCadastroRepository, OrcamentoCadastroRepositoryFake>();

var app = builder.Build();

app.UseExceptionHandler();
app.MapControllers();

app.Run();

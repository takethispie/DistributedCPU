using ISA.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

static bool IsRunningInContainer() =>
    bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
    inContainer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Debug)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x => {
    x.SetKebabCaseEndpointNameFormatter();
    x.SetInMemorySagaRepositoryProvider();
    x.UsingRabbitMq((context, cfg) => {
        if (IsRunningInContainer())
            cfg.Host("rabbitmq");
        cfg.UseDelayedMessageScheduler();
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

var globalApis = app.MapGroup("/global");
globalApis
    .MapGet("/clock/tick", async ([FromServices] IPublishEndpoint endpoint) => {
        await endpoint.Publish(new ClockFired(Guid.NewGuid()));
    })
    .WithName("execute a single clock tick");

var instructionApis = app.MapGroup("/instructions");
instructionApis
    .MapPost("/single/{instruction}",
        async (string instruction, [FromServices] IPublishEndpoint endpoint) => {
            await endpoint.Publish(new InstructionLoaded(Guid.NewGuid(), instruction));
        })
    .WithName("execute a single instruction");

var memoryApis = app.MapGroup("/memory");
memoryApis.MapPost("/storeinstruction/{address}/{instruction}",
    (string address, string instruction) => { return "null"; }).WithName("store instruction in memory");

memoryApis.MapPost("/storedata/{address}/{data}", (string address, string data) => { return "null"; })
    .WithName("store data in memory");

app.Run();
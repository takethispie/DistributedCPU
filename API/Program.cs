using System.Reflection;
using ISA.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

static bool IsRunningInContainer() =>
    bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
    inContainer;

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
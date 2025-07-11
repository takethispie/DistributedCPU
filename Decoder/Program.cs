using System.Reflection;
using Decoder.Services;
using MassTransit;
using Serilog;

bool IsRunningInContainer() =>
    bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer)
    && inContainer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Debug)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

await CreateHostBuilder(args).Build().RunAsync();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) => {
            services.AddSingleton<DecoderService>();
            services.AddSingleton<InstructionTransitionerService>();
            services.AddMassTransit(x => {
                x.AddDelayedMessageScheduler();
                x.SetKebabCaseEndpointNameFormatter();
                x.SetInMemorySagaRepositoryProvider();
                var entryAssembly = Assembly.GetEntryAssembly();
                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);
                x.UsingRabbitMq((context, cfg) => {
                    if (IsRunningInContainer())
                        cfg.Host("rabbitmq");
                    cfg.UseDelayedMessageScheduler();
                    cfg.ConfigureEndpoints(context);
                });
            });
        });
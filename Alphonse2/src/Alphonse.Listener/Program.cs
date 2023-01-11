using Alphonse.Listener;
using NLog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DotNet.RestApi.Client;
using Alphonse.Listener.PhoneNumberHandlers;
using Alphonse.Listener.Mocks;
using Polly.Extensions.Http;
using Polly;

//=== DI/IoC =========================

var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

await Host.CreateDefaultBuilder(args)
    .UseContentRoot(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
    .UseSystemd()
    .ConfigureLogging((hostContext, logging) =>
    {
        // configure Logging with NLog
        // source : https://github.com/NLog/NLog.Extensions.Logging/wiki/NLog-configuration-with-appsettings.json
        logging.ClearProviders();
        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        logging.AddNLog(config);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<HostOptions>(options =>
        {
            // add a delay of 20sec before force shutdown
            options.ShutdownTimeout = TimeSpan.FromSeconds(20);
        });
        services.AddOptions<AlphonseSettings>()
            .Bind(hostContext.Configuration.GetSection("Alphonse"))
            .Validate(s => !string.IsNullOrWhiteSpace(s.WebAppBaseUri), $"Missing value for {nameof(AlphonseSettings.WebAppBaseUri)}");
        services.AddSingleton<PhonebookService>();
        services.AddSingleton<IHistoryPhoneNumberService, HistoryPhoneNumberService>();
        services.AddSingleton<IPhoneNumberHandler, WhitelistHandler>();
        services.AddSingleton<IPhoneNumberHandler, BlacklistHandler>();
        services.AddSingleton<IPhoneNumberHandler, UnkownNumberHandler>();
        services.AddSingleton<IModemDataDispatcher, AlphonseModemDataDispatcher>();
        services.AddSingleton<IModem, Modem>();
        services.AddHttpClient<RestApiClient>()
            .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(new double[]{1, 1, 3, 5, 10, 20, 30}.Select(TimeSpan.FromSeconds))
            );
        
        services.AddHostedService<AlphonseConsoleRunner>();
    })
    .Build()
    .RunAsync();

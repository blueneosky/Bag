﻿using Alphonse.Listener;
using NLog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DotNet.RestApi.Client;
using Alphonse.Listener.PhoneNumberHandlers;

//=== DI/IoC =========================

var config = new ConfigurationBuilder()
    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

await Host.CreateDefaultBuilder(args)
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
        services.AddOptions<AlphonseSettings>().Bind(hostContext.Configuration.GetSection("Alphonse"));
        services.AddSingleton<PhonebookService>();
        services.AddSingleton<IPhoneNumberHandler, HistoryPhoneNumberHandler>();
        services.AddSingleton<IPhoneNumberHandler, WhitelistHandler>();
        services.AddSingleton<IPhoneNumberHandler, BlacklistHandler>();
        services.AddSingleton<IPhoneNumberHandler, UnkownNumberHandler>();
        services.AddSingleton<IModemDataDispatcher, AlphonseModemDataDispatcher>();
        services.AddSingleton<Modem>();
        services.AddHttpClient<RestApiClient>();

        services.AddSingleton<IConsoleRunner, AlphonseConsoleRunner>();
        services.AddHostedService<ConsoleHostedService>();
    })
    .UseConsoleLifetime()
    .RunConsoleAsync();
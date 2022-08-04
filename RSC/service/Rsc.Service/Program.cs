using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Rsc.Service
{
    class Program
    {
        private static readonly LogLevel ConstConsoleMinLevel = LogLevel.Trace;
        private static readonly LogLevel ConstLogFileMinLevel = LogLevel.Debug;
        private static readonly LogLevel ConstEventLogMinLevelLog = LogLevel.Off /*Info*/;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
     
        static void Main(string[] args)
        {
            SetupLog(out var withEventLog);

            RunService(withEventLog);
        }

        private static void SetupLog(out bool withEventLog)
        {
            withEventLog = ConstEventLogMinLevelLog != LogLevel.Off;
            
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logdebugger = new NLog.Targets.DebuggerTarget("logdebugger");
            var logconsole = new NLog.Targets.ColoredConsoleTarget("logconsole");

            var logfile = !withEventLog
                ? new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" }
                : (NLog.Targets.Target)new NLog.Targets.NullTarget();

            var logevent = withEventLog
                ? new NLog.Targets.EventLogTarget("Rsc.Service")
                : (NLog.Targets.Target)new NLog.Targets.NullTarget();

            // Rules for mapping loggers to targets            
            config.AddRuleForAllLevels(logdebugger);
            config.AddRule(ConstConsoleMinLevel, LogLevel.Fatal, logconsole);
            config.AddRule(ConstLogFileMinLevel, LogLevel.Fatal, logfile);
            config.AddRule(ConstEventLogMinLevelLog, LogLevel.Fatal, logevent);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }

        private static void RunService(bool withWindowsEventLogs)
        {
            Logger.Debug($"> {nameof(RunService)}");

            //Configure service host
            var rc = HostFactory.Run(configure =>
            {
                var serviceManager = new ServiceManager();
                //Service actions
                configure.Service<ServiceManager>(s =>
                {
                    s.ConstructUsing(() => serviceManager);
                    s.WhenStarted(i => i.Start());
                    s.WhenStopped(i => i.Stop());
                    s.WhenShutdown(i => i.Stop());
                    s.WhenPaused(i => i.Pause());
                    s.WhenContinued(i => i.Continue());
                });

                //Service settings
                configure.RunAsLocalSystem();
                configure.StartAutomatically();
                configure.DependsOn("Dhcp");
                if (withWindowsEventLogs)
                    configure.DependsOnEventLog();
                configure.EnablePauseAndContinue();
                configure.OnException(e => serviceManager.OnUnhandledException(e));
                //configure.UnhandledExceptionPolicy = 
                configure.UseNLog();

                //Service description
                configure.SetServiceName("Rsc.Service");
                configure.SetDisplayName("Rsc.Service");
                configure.SetDescription("Remote service controle client");
            });

            //Exit code
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());

            Logger.Debug($"> {nameof(RunService)} [ExitCode={exitCode} ({rc})]");

            Environment.ExitCode = exitCode;
        }
    }
}

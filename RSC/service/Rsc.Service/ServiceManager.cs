using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Rsc.Service
{
    public class ServiceManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private CancellationTokenSource _listenerCanceller;

        public void OnUnhandledException(Exception e, object additionnalContext = null)
        {
            var message = "Unexpected error occured";
            if (!string.IsNullOrEmpty(additionnalContext?.ToString()))
                message = $"{message} {additionnalContext}";
            Logger.Fatal(e, message);
        }

        public void Start()
        {
            try
            {
                Logger.Info("Service starting...");

                if(this._listenerCanceller != null)
                {
                    Logger.Warn("Service already started");
                    return;
                }

                this._listenerCanceller = new CancellationTokenSource();
                throw new NotImplementedException();

                //var 
                //Task.Run(this.Listen, this._listenerCanceller.Token);

                Logger.Info("Service started");
            }
            catch (Exception e)
            {
                this.OnUnhandledException(e, $"during '{nameof(Start)}'");

                this._listenerCanceller?.Cancel();
                this._listenerCanceller = null;

                throw;
            }
        }

        public void Stop()
        {
            try
            {
                Logger.Info("Service stoping...");

                if(this._listenerCanceller == null)
                {
                    Logger.Warn("Service already stoped");
                    return;
                }

                this._listenerCanceller?.Cancel();

                // TODO

                Logger.Info("Service stoped");
            }
            catch (Exception e)
            {
                this.OnUnhandledException(e, $"during '{nameof(Stop)}'");

                throw;
            }
            finally
            {
                this._listenerCanceller = null;
            }
        }

        public void Pause()
        {
            // TODO
        }

        public void Continue()
        {
            // TODO
        }
    }
}

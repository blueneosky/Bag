using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Alphonse.Listener.Mocks
{
    public class ModemMock : IModem
    {
        private readonly ILogger<ModemMock> _logger;
        public ModemMock(ILogger<ModemMock> logger)
        {
            this._logger = logger;
        }

        public void Open() => this._logger.LogWarning("[MOCK] MODEM - Open");

        public void Close()  => this._logger.LogWarning("[MOCK] MODEM - Close");

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
        }

        public Task ListenAsync(IModemDataDispatcher listener, CancellationToken token) => Task.Run(async () =>
        {
            this._logger.LogWarning("[MOCK] MODEM - ListenAsync : Simulation");

            // here is the simulation
            await listener.DispatchAsync(Modem.CONST_MODEM_OK, token);
            // await listener.DispatchAsync(Modem.CONST_MODEM_RING, token);
            // await listener.DispatchAsync(Modem.CONST_MODEM_NUMBER_TAG+"P", token);

            await Task.Delay(TimeSpan.FromMinutes(30), token);
        });

        public Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token)
            => Task.Delay(hangupDelay, token);

        public void WriteCommands(IEnumerable<string> commands) { }
    }
}
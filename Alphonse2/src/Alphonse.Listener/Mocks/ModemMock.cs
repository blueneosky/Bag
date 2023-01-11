using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.Listener.Connectors;
using Microsoft.Extensions.Logging;

using static MoreLinq.Extensions.ForEachExtension;

namespace Alphonse.Listener.Mocks
{
    public class ModemMock : IModemConnector
    {
        private readonly ILogger<ModemMock> _logger;
        public ModemMock(ILogger<ModemMock> logger)
        {
            this._logger = logger;
        }

        public Task OpenAsync()
        {
            this._logger.LogWarning("[MOCK] MODEM - Open");
            return Task.CompletedTask;
        }

        public void Close() => this._logger.LogWarning("[MOCK] MODEM - Close");

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
        }

        public Task ListenAsync(IModemDataDispatcher listener, CancellationToken token) => Task.Run(async () =>
        {
            this._logger.LogWarning("[MOCK] MODEM - ListenAsync : Simulation");

            // here is the simulation
            await listener.DispatchAsync(ModemDataType.Ok, null, token);
            // await listener.DispatchAsync(ModemDataType.Ring, null, token);
            // await listener.DispatchAsync(ModemDataType.PhoneNumber, "P", token);

            await Task.Delay(TimeSpan.FromMinutes(30), token);
        });

        public Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token)
            => Task.Delay(hangupDelay, token);

        public void WriteCommands(IEnumerable<string> commands) { }

        public Task WriteCommandsAsync(IEnumerable<string> commands, CancellationToken token)
        {
            commands.ForEach(c => this._logger.LogInformation("[MOCK] Send datagram to modem: {Datagram}", c));
            return Task.CompletedTask;
        }
    }
}
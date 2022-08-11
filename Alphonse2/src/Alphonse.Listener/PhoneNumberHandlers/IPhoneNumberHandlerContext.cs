using Alphonse.Listener.Dto;

namespace Alphonse.Listener.PhoneNumberHandlers;

public interface IPhoneNumberHandlerContext
{
    public DateTimeOffset Timestamp { get; }
    public PhoneNumber Number { get; }
    public PhoneNumberDto? PhoneNumber { get; }
    public bool StopProcessing { get; set; }
}
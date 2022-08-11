namespace Alphonse.Listener.PhoneNumberHandlers;

public interface IPhoneNumberHandler
{
    Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token);
}
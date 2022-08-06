namespace Alphonse.Listener;

public interface IPhoneNumberHandler
{
    Task<bool> ProcessAsync(PhoneNumber number, CancellationToken token);
}
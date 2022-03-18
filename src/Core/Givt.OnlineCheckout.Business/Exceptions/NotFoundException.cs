namespace Givt.OnlineCheckout.API.Exceptions;

public class NotFoundException : GivtException
{
    public NotFoundException(string message): base(message) { }
    public override int ErrorCode => 404;
}
namespace Givt.OnlineCheckout.Business.Exceptions;

public class NotFoundException : GivtException
{
    public NotFoundException(string message): base(message) { }
    public override int ErrorCode => 404;
}
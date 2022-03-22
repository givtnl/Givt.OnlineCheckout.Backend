using System.Runtime.Serialization;

namespace Givt.OnlineCheckout.API.Exceptions;

public class BadRequestException: GivtException
{
    public BadRequestException(string message) : base(message) { }

    public override int ErrorCode => 400;
}
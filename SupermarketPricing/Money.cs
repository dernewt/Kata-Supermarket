using ValueOf;
namespace SupermarketPricing;

public class Money : ValueOf<decimal, Money>
{
    protected override void Validate()
    {
        if (!TryValidate())
            throw new FractionalPennyException(Value.ToString()) { };
    }

    protected override bool TryValidate()
    {
        return Value.Scale <= 2;
    }

}

public class FractionalPennyException(string Message) : Exception(Message) { }

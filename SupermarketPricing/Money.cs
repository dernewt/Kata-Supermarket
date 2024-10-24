using System.Numerics;
using Vogen;
namespace SupermarketPricing;

[ValueObject<Decimal>]
[Instance("Empty", 0)]
public readonly partial struct Money:
    IAdditionOperators<Money, Money, Money>,
    ISubtractionOperators<Money, Money, Money>
{
    public static Validation Validate(decimal value) =>
        value.Scale <= 2 ? Validation.Ok
        : Validation.Invalid("No Fractional Pennies");
    private static decimal NormalizeInput(decimal input)
    {
        return input;
    }

    public static Money operator +(Money left, Money right)
        => From(left.Value + right.Value);

    public static Money operator -(Money left, Money right)
        => From(left.Value - right.Value);
}

public static class MoneyExtentions
{
    public static Money Sum<T>(this IEnumerable<T> collection, Func<T, Money> selector)
        => collection.Aggregate(Money.Empty,
            (a, b) => a + selector(b));
}

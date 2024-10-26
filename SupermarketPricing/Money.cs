using System.Numerics;
using Vogen;
namespace SupermarketPricing;

[ValueObject<decimal>]
[Instance("Empty", 0)]
public readonly partial struct Money :
    IAdditionOperators<Money, Money, Money>,
    ISubtractionOperators<Money, Money, Money>
{
    public static Money FromIgnoringFractionalPenny(decimal value) =>
        From(decimal.Round(value, 2, MidpointRounding.ToNegativeInfinity));

    public static Validation Validate(decimal value) =>
        value.Scale <= 2 ? Validation.Ok
        : Validation.Invalid("No Fractional Pennies");
    private static decimal NormalizeInput(decimal input) => input;

    public static Money operator +(Money left, Money right) =>
        From(left.Value + right.Value);

    public static Money operator -(Money left, Money right) =>
        From(left.Value - right.Value);

    public static MoneyPlus operator /(Money left, int right) =>
        new(left.Value / right);
}

public record MoneyPlus(Money Money, decimal FractionalPenny)
{
    public MoneyPlus(decimal valueWithFrationalPenny)
        : this(
              Money: Money.FromIgnoringFractionalPenny(valueWithFrationalPenny),
              FractionalPenny: Math.Truncate(valueWithFrationalPenny * 100) / 100)
    { }
}

public static class MoneyExtentions
{
    public static Money Sum<T>(this IEnumerable<T> collection, Func<T, Money> selector)
        => collection.Aggregate(Money.Empty,
            (a, b) => a + selector(b));
}

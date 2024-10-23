using Vogen;
namespace SupermarketPricing;

[ValueObject<Decimal>]
public readonly partial struct Money
{
    public static Validation Validate(decimal value) =>
        value.Scale <= 2 ? Validation.Ok
        : Validation.Invalid("No Fractional Pennies");
    private static decimal NormalizeInput(decimal input)
    {
        return input;
    }
}

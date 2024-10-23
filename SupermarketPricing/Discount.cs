using System.Linq;

namespace SupermarketPricing;

public record Discount(string Name, Func<IEnumerable<Item>,Money> Adjustment)
{
    public static Discount HalfOff => new("HalfOff", (i) => Money.From(i.Sum(i=>i.Price.Value) / 2));
}
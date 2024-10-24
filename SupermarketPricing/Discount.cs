using System.Linq;

namespace SupermarketPricing;

public record Discount(string Name, Func<IEnumerable<Item>,Money> Adjustment)
{
    public static Discount HalfOff => new("HalfOff", (items) => Money.FromFloored(items.Sum(item => item.Price).Value / 2));
}
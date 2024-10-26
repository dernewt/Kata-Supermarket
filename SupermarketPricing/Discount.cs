using System.Collections.Immutable;

namespace SupermarketPricing;

public record Discount(string Name, Func<IEnumerable<Item>, Money> Adjustment)
{
    public static Discount HalfOff => new("Half Off Everything",
        (items) => Money.FromFloored(items.Sum(item => item.Price).Value / 2));
    public static Discount BoGoAnyOnce => new("Buy Any Get Any Equal or Lesser Value Free. Limit 1",
        (items) => BoGoStrictItemPair(items, usageLimit: 1));

    protected static Money BoGoStrictItemPair(IEnumerable<Item> items, int? usageLimit = null)
    {
        var itemsPriceDecending = items.OrderByDescending(i => i.Price)
            .ToImmutableArray();

        if (usageLimit != null && usageLimit < itemsPriceDecending.Length/2)
            itemsPriceDecending = itemsPriceDecending[0 .. (usageLimit.Value*2)];

        var evens = itemsPriceDecending.Where((item, index) => index % 2 == 0);
        var odds = itemsPriceDecending.Where((item, index) => index % 2 != 0);
        var pairedItems = evens.Zip(odds, (more, less) => (more, less));

        var discount = Money.Empty;
        foreach (var pair in pairedItems)
        {
            discount += pair.less.Price;
        }

        return discount;
    }
}
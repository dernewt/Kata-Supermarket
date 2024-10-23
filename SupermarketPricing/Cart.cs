namespace SupermarketPricing;

public class Cart()
{
    public List<Item> Items { get; init; } = [];
    public List<Discount> Discounts { get; init; } = [];

    public Money Cost() => Money.From(
        Items.Sum(i => i.Price.Value)
        - Discounts.Sum(d => d.Adjustment.Invoke(Items).Value));

}

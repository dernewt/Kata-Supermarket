namespace SupermarketPricing;

public class Cart()
{
    public List<Item> Items { get; init; } = [];
    public List<Discount> Discounts { get; init; } = [];

    public Money Cost() => Items.Sum(i=>i.Price)
        - Discounts.Sum(d => d.Adjustment.Invoke(Items));
}

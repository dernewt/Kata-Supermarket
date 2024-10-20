namespace SupermarketPricing;

public class Cart()
{
    public List<Item> Items { get; init; } = [];

    public double Cost()
    {
        return Items.Sum(i=>i.Price);
    }

}

namespace SupermarketPricing;

public class Cart(List<Item> Items)
{
    public Cart()
        :this([])
    {
        
    }
    public double Cost()
    {
        return Items.Sum(i=>i.Price);
    }

}

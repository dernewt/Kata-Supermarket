using FluentAssertions;

namespace SupermarketPricing.Tests;

public class TestCart
{
    [Fact]
    public void EmptyCart()
    {
        var emptyCart = new Cart();

        emptyCart.Cost().Should().Be(0);
    }

    [Fact]
    public void CartWithItem()
    {
        var fiveDollarItem = new Item("burger", 5);
        var justFiveDollarItem = new Cart { Items = [fiveDollarItem] };

        justFiveDollarItem.Cost().Should().Be(5);
    }

    [Fact]
    public void CartWithManyItems()
    {
        var fiveDollarItem = new Item("burger", 5);
        var twoFiveDollarItems = new Cart { Items = [fiveDollarItem, fiveDollarItem] };

        twoFiveDollarItems.Cost().Should().Be(10);
    }

    [Fact]
    public void CartItemChange()
    {
        var pennyItem = new Item("candy", .01);
        var tenPennyItems = new Cart { Items = Enumerable.Repeat(pennyItem, 10).ToList() };

        tenPennyItems.Cost().Should().Be(.10);
    }
}

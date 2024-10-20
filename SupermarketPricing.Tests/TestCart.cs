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
        var fiveDollarItem = new Item("whatever", 5);
        var justFiveDollarItem = new Cart([fiveDollarItem]);

        justFiveDollarItem.Cost().Should().Be(5);
    }

    [Fact]
    public void CartWithManyItems()
    {
        var fiveDollarItem = new Item("whatever", 5);
        var threeFiveDollarItems = new Cart([fiveDollarItem, fiveDollarItem, fiveDollarItem]);

        threeFiveDollarItems.Cost().Should().Be(15);
    }

    [Fact]
    public void CartItemChange()
    {
        var pennyItem = new Item("whatever", .01);
        var tenPennyItems = new Cart(Enumerable.Repeat(pennyItem, 10).ToList());

        tenPennyItems.Cost().Should().Be(.10);
    }
}

using FluentAssertions;

namespace SupermarketPricing.Tests;

public class TestCart
{
    [Fact]
    public void EmptyCart()
    {
        var emptyCart = new Cart();

        emptyCart.Cost().Should().Be(Money.Empty);
    }

    [Fact]
    public void CartWithItem()
    {
        var fiveDollarItem = new Item("burger", Money.From(5));
        var justFiveDollarItem = new Cart { Items = [fiveDollarItem] };

        justFiveDollarItem.Cost().Should().Be(Money.From(5));
    }

    [Fact]
    public void CartWithManyItems()
    {
        var fiveDollarItem = new Item("burger", Money.From(5));
        var twoFiveDollarItems = new Cart { Items = [fiveDollarItem, fiveDollarItem] };

        twoFiveDollarItems.Cost().Should().Be(Money.From(10));
    }

    [Fact]
    public void CartItemChange()
    {
        var pennyItem = new Item("candy", Money.From(.01m));
        var tenPennyItems = new Cart { Items = Enumerable.Repeat(pennyItem, 10).ToList() };

        tenPennyItems.Cost().Should().Be(Money.From(.10m));
    }

    [Fact]
    public void CartItemWithHalfOff()
    {
        var tenDollarItem = new Item("burger", Money.From(10));
        var tenDollarItems = new Cart {
            Items = [tenDollarItem],
            Discounts = [Discount.HalfOff] };


        tenDollarItems.Cost().Should().Be(Money.From(5));
    }
}

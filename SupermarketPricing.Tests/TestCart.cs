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

    public static TheoryData<decimal[], decimal> Sums() =>
        new()
        {
            {[5], 5 },
            {[5, 5], 10 },
            {[.01m,.01m,.01m,.01m,.01m,.01m,.01m,.01m,.01m,.01m,], .1m }
        };
    [Theory]
    [MemberData(nameof(Sums))]
    public void CartCost(decimal[] values, decimal expectedResult)
    {
        var cart = new Cart();
        cart.Items.AddRange(values.Select(v => new Item("", Money.From(v))));

        cart.Cost().Should().Be(Money.From(expectedResult));
    }

    public static TheoryData<decimal[], decimal> HalfSums() =>
        new(){
            { [1, 1, 1 ], 1.5m},
            { [10], 5 },
            { [5], 2.5m},
            { [.01m ], .01m} //don't create fractional pennies
        };
    [Theory]
    [MemberData(nameof(HalfSums))]
    public void CartCostHalfOff(decimal[] values, decimal expectedResult)
    {
        var cart = new Cart();
        cart.Items.AddRange(values.Select(v => new Item("", Money.From(v))));
        cart.Discounts.Add(Discount.HalfOff);

        cart.Cost().Should().Be(Money.From(expectedResult));
    }

    public static TheoryData<decimal[], decimal> BoGoOnceSums() =>
    new(){
            { [5, 4, 3, 2], 10},
            { [10], 10},
            { [1, 2, 3], 4},
            { [ ], 0}
    };
    [Theory]
    [MemberData(nameof(BoGoOnceSums))]
    public void CartCostBoGo(decimal[] values, decimal expectedResult)
    {
        var cart = new Cart();
        cart.Items.AddRange(values.Select(v => new Item("", Money.From(v))));
        cart.Discounts.Add(Discount.BoGoAnyOnce);

        cart.Cost().Should().Be(Money.From(expectedResult));
    }
}

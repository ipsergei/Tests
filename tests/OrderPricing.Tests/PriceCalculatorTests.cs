
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPricing;

namespace OrderPricing.Tests;

[TestClass]
public class PriceCalculatorTests
{
    [TestMethod]
    public void RegularOrder_1000WithoutLoyalty_TotalIs1300()
    {
        decimal actual = PriceCalculator.CalculateTotal(1000m, false, false);
        Assert.AreEqual(1300m, actual);
    }

    [TestMethod]
    public void NegativeSubtotal_ThrowsException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            PriceCalculator.CalculateTotal(-1m, false, false));
    }

    [TestMethod]
    public void SubtotalOver100000_ThrowsException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            PriceCalculator.CalculateTotal(100001m, false, false));
    }

    [TestMethod]
    public void Subtotal100000_IsAllowed()
    {
        decimal actual = PriceCalculator.CalculateTotal(100000m, false, false);
        Assert.AreEqual(95000m, actual);
    }

    [TestMethod]
    public void ZeroSubtotal_RegularDeliveryCosts300()
    {
        decimal actual = PriceCalculator.CalculateTotal(0m, false, false);
        Assert.AreEqual(300m, actual);
    }

    [TestMethod]
    public void SubtotalBelow5000_NoDiscount()
    {
        decimal actual = PriceCalculator.CalculateTotal(4999m, false, false);
        Assert.AreEqual(4999m, actual);
    }

    [TestMethod]
    public void Subtotal5000_DiscountIs5Percent()
    {
        decimal actual = PriceCalculator.CalculateTotal(5000m, false, false);
        Assert.AreEqual(4750m, actual);
    }

    [TestMethod]
    public void Subtotal6000_DiscountIs5Percent()
    {
        decimal actual = PriceCalculator.CalculateTotal(6000m, false, false);
        Assert.AreEqual(5700m, actual);
    }

    [TestMethod]
    public void LoyaltyWithSmallSubtotal_DiscountIs10Percent()
    {
        decimal actual = PriceCalculator.CalculateTotal(1000m, true, false);
        Assert.AreEqual(1200m, actual);
    }

    [TestMethod]
    public void LoyaltyAndLargeSubtotal_Only10PercentDiscount()
    {
        decimal actual = PriceCalculator.CalculateTotal(6000m, true, false);
        Assert.AreEqual(5400m, actual);
    }

    [TestMethod]
    public void RegularDeliveryBelow4000_Costs300()
    {
        decimal actual = PriceCalculator.CalculateTotal(3999m, false, false);
        Assert.AreEqual(4299m, actual);
    }

    [TestMethod]
    public void RegularDeliveryAt4000_IsFree()
    {
        decimal actual = PriceCalculator.CalculateTotal(4000m, false, false);
        Assert.AreEqual(4000m, actual);
    }

    [TestMethod]
    public void ExpressDeliveryBelow4000_Costs500()
    {
        decimal actual = PriceCalculator.CalculateTotal(1000m, false, true);
        Assert.AreEqual(1500m, actual);
    }

    [TestMethod]
    public void ExpressDeliveryAbove4000_Costs500()
    {
        decimal actual = PriceCalculator.CalculateTotal(6000m, false, true);
        Assert.AreEqual(6200m, actual);
    }

    [TestMethod]
    public void LoyaltyDiscount_RoundsAwayFromZero()
    {
        decimal actual = PriceCalculator.CalculateTotal(1000.05m, true, false);
        Assert.AreEqual(1200.05m, actual);
    }

    [TestMethod]
    public void LoyaltyDiscount_DoesNotApplyToDelivery()
    {
        decimal actual = PriceCalculator.CalculateTotal(1000m, true, true);
        Assert.AreEqual(1400m, actual);
    }
}

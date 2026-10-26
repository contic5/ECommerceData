namespace EcommerceData.Models;

public class Order
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal GrossSales { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal NetSales { get; set; }
    public decimal ProductCost { get; set; }
    public decimal Profit { get; set; }
}
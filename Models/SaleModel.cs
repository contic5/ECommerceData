

namespace EcommerceData.Models;

public class Sale
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public TimeSpan OrderTime { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public string SalesChannel { get; set; } = string.Empty;

    // Customer Information
    public string CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int CustomerAge { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string CustomerSegment { get; set; } = string.Empty;
    public string CustomerType { get; set; } = string.Empty;
    public string CustomerCity { get; set; } = string.Empty;
    public string CustomerState { get; set; } = string.Empty;
    public string CustomerCountry { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string CustomerPostalCode { get; set; } = string.Empty;

    // Payment Information
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;

    // Shipping and Delivery
    public string ShippingMethod { get; set; } = string.Empty;
    public string Warehouse { get; set; } = string.Empty;
    public decimal DeliveryDays { get; set; }
    public decimal EstimatedDeliveryDays { get; set; }
    public string DeliveryStatus { get; set; } = string.Empty;

    // Returns and Reviews
    public string ReturnStatus { get; set; } = string.Empty;
    public string? ReturnReason { get; set; }
    public decimal? CustomerRating { get; set; }
    public string? ReviewSentiment { get; set; }
    public string? CustomerReview { get; set; }

    // Marketing
    public string MarketingChannel { get; set; } = string.Empty;
    public string? CampaignName { get; set; }
    public string? CouponCode { get; set; }
    public int LoyaltyPointsEarned { get; set; }
    public int LoyaltyPointsRedeemed { get; set; }

    // Sales and Financial Information
    public int Quantity { get; set; }
    public decimal GrossSales { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal NetSales { get; set; }
    public decimal ProductCost { get; set; }
    public decimal Profit { get; set; }
    public decimal ProfitMarginPercentage { get; set; }
    public decimal CustomerLifetimeValue { get; set; }

    // Customer History
    public bool IsRepeatCustomer { get; set; }
    public int CustomerOrderCount { get; set; }
}
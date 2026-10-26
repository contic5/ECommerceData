using System.ComponentModel.DataAnnotations;

namespace EcommerceData.Models;

public class Product
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCategory { get; set; }
    public string ProductSubCategory { get; set; }
    public string Brand { get; set; }

    public string Supplier { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal ProductCost { get; set; }

    public decimal ProductRating { get; set; }

    public Product()
    {
        
    }
}
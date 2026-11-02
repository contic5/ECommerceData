using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;

public class SalesModel : PageModel
{
    public List<SelectListItem> OrderStatusOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string OrderStatus { get; set; } = "";

    public List<SelectListItem> SalesChannelOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string SalesChannel { get; set; } = "";

    public List<SelectListItem> CustomerSegmentOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string CustomerSegment { get; set; } = "";

    public List<SelectListItem> CustomerCountryOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string CustomerCountry { get; set; } = "";

    public List<SelectListItem> PaymentMethodOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string PaymentMethod { get; set; } = "";

    public List<SelectListItem> PaymentStatusOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string PaymentStatus { get; set; } = "";

    public List<SelectListItem> ShippingMethodOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string ShippingMethod { get; set; } = "";

    public List<SelectListItem> ReturnStatusOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string ReturnStatus { get; set; } = "";

    public List<SelectListItem> MarketingChannelOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string MarketingChannel { get; set; } = "";

    public List<Sale> Sales { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string SortBy {get; set;}

    [BindProperty(SupportsGet = true)]
    public bool SortAscending {get; set;}
    public List<SelectListItem> SortColumns {get; set;} = new();

    [BindProperty(SupportsGet = true)]
    public int PageNumber {get; set;}=1;

    public List<SelectListItem> CreateSelectListItems(List<Sale> allSales, string key)
    {
        var property = typeof(Sale).GetProperty(key)
            ?? throw new ArgumentException($"No public Sale property named '{key}'.", nameof(key));
        var allValues = allSales
            .Where(sale => sale is not null)
            .Select(sale => property.GetValue(sale))
            .Where(value => value is not null);
        var uniqueValues = allValues.Distinct().OrderBy(value => value).ToList();

        return uniqueValues.Select(value => new SelectListItem
        {
            Value = value!.ToString(),
            Text = value.ToString(),
        }).ToList();
    }

    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Sales Loading");
        var reader=new StreamReader("Data/ecommerce_sales.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var allSales=csv.GetRecords<Sale>().ToList();
        Console.WriteLine("Sales Loaded");

        Sales = allSales;
        OrderStatusOptions = CreateSelectListItems(allSales, nameof(Sale.OrderStatus));
        SalesChannelOptions = CreateSelectListItems(allSales, nameof(Sale.SalesChannel));
        CustomerSegmentOptions = CreateSelectListItems(allSales, nameof(Sale.CustomerSegment));
        CustomerCountryOptions = CreateSelectListItems(allSales, nameof(Sale.CustomerCountry));
        PaymentMethodOptions = CreateSelectListItems(allSales, nameof(Sale.PaymentMethod));
        PaymentStatusOptions = CreateSelectListItems(allSales, nameof(Sale.PaymentStatus));
        ShippingMethodOptions = CreateSelectListItems(allSales, nameof(Sale.ShippingMethod));
        ReturnStatusOptions = CreateSelectListItems(allSales, nameof(Sale.ReturnStatus));
        MarketingChannelOptions = CreateSelectListItems(allSales, nameof(Sale.MarketingChannel));

        if (!string.IsNullOrWhiteSpace(OrderStatus))
            Sales = Sales.Where(sale => sale.OrderStatus == OrderStatus).ToList();
        if (!string.IsNullOrWhiteSpace(SalesChannel))
            Sales = Sales.Where(sale => sale.SalesChannel == SalesChannel).ToList();
        if (!string.IsNullOrWhiteSpace(CustomerSegment))
            Sales = Sales.Where(sale => sale.CustomerSegment == CustomerSegment).ToList();
        if (!string.IsNullOrWhiteSpace(CustomerCountry))
            Sales = Sales.Where(sale => sale.CustomerCountry == CustomerCountry).ToList();
        if (!string.IsNullOrWhiteSpace(PaymentMethod))
            Sales = Sales.Where(sale => sale.PaymentMethod == PaymentMethod).ToList();
        if (!string.IsNullOrWhiteSpace(PaymentStatus))
            Sales = Sales.Where(sale => sale.PaymentStatus == PaymentStatus).ToList();
        if (!string.IsNullOrWhiteSpace(ShippingMethod))
            Sales = Sales.Where(sale => sale.ShippingMethod == ShippingMethod).ToList();
        if (!string.IsNullOrWhiteSpace(ReturnStatus))
            Sales = Sales.Where(sale => sale.ReturnStatus == ReturnStatus).ToList();
        if (!string.IsNullOrWhiteSpace(MarketingChannel))
            Sales = Sales.Where(sale => sale.MarketingChannel == MarketingChannel).ToList();

        Console.WriteLine(SortBy);

        List<string> allColumns=typeof(Order)
                                     .GetProperties()
                                     .Select(p => p.Name)
                                     .ToList();  

        SortColumns=allColumns.Select(val => new SelectListItem
        {
            Value = val.ToString(),
            Text = val.ToString(),
        }).ToList();
        
        if(!string.IsNullOrEmpty(SortBy))
        {
            Sales=Sales.OrderBy(sale => sale.GetType()
                                        .GetProperty(SortBy)?
                                        .GetValue(sale, null)
                                 ).ToList();
        }
        if(SortAscending==false)
        {
            Sales.Reverse();
        }

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

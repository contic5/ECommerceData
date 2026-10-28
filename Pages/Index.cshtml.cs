using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceData.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Customers Loading");
        var reader = new StreamReader("Data/customer_master.csv"); 
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var customers=csv.GetRecords<Customer>().ToList();
        Console.WriteLine("Customers Loaded");

        Console.WriteLine("Sales Loading");
        reader=new StreamReader("Data/ecommerce_sales.csv");
        csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var sales=csv.GetRecords<Sale>().ToList();
        Console.WriteLine("Sales Loaded");

        Console.WriteLine("Orders Loading");
        reader=new StreamReader("Data/order_items.csv");
        csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var orders=csv.GetRecords<Order>().ToList();
        Console.WriteLine("Orders Loaded");

        Console.WriteLine("Products Loading");
        reader=new StreamReader("Data/product_catalog.csv");
        csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var products=csv.GetRecords<Product>().ToList();
        Console.WriteLine("Products Loaded");

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

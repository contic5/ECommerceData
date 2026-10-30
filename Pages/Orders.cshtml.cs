using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceData.Pages;

public class OrdersModel : PageModel
{
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Orders Loading");
        var reader=new StreamReader("Data/order_items.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var orders=csv.GetRecords<Order>().ToList();
        Console.WriteLine("Orders Loaded");

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

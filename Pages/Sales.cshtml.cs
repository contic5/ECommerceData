using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceData.Pages;

public class SalesModel : PageModel
{
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Sales Loading");
        var reader=new StreamReader("Data/ecommerce_sales.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var sales=csv.GetRecords<Sale>().ToList();
        Console.WriteLine("Sales Loaded");

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

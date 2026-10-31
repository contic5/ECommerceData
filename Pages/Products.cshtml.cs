using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceData.Pages;

public class ProductsModel : PageModel
{
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Products Loading");
        var reader=new StreamReader("Data/product_catalog.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var products=csv.GetRecords<Product>().ToList();
        Console.WriteLine("Products Loaded");

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

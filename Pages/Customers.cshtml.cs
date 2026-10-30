using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceData.Pages;

public class CustomersModel : PageModel
{
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Customers Loading");
        var reader = new StreamReader("Data/customer_master.csv"); 
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var customers=csv.GetRecords<Customer>().ToList();
        Console.WriteLine("Customers Loaded");

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

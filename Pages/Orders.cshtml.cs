using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;

public class OrdersModel : PageModel
{
    public List<Order> Orders {get; set;}

    [BindProperty(SupportsGet = true)]
    public string SortBy {get; set;}

    [BindProperty(SupportsGet = true)]
    public bool SortAscending {get; set;}
    public List<SelectListItem> SortColumns {get; set;} = new();
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Orders Loading");
        var reader=new StreamReader("Data/order_items.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        Orders=csv.GetRecords<Order>().ToList();
        Console.WriteLine("Orders Loaded");
        
        List<string> allColumns=typeof(Order)
                                     .GetProperties()
                                     .Select(p => p.Name)
                                     .ToList();  

        SortColumns=allColumns.Select(val => new SelectListItem
        {
            Value = val.ToString(),
            Text = val.ToString(),
        }).ToList();

        Console.WriteLine(SortBy);
        if(!string.IsNullOrEmpty(SortBy))
        {
            Orders=Orders.OrderBy(order => order.GetType()
                                        .GetProperty(SortBy)?
                                        .GetValue(order, null)
                                 ).ToList();
        }
        if(SortAscending==false)
        {
            Orders.Reverse();
        }
        Orders=Orders.GetRange(0,100);

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

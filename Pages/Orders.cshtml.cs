using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using static SharedFunctions;
namespace ECommerceData.Pages;

public class OrdersModel : PageModel
{
    public List<Order> Orders {get; set;}

    [BindProperty(SupportsGet = true)]
    public string SortBy {get; set;}

    [BindProperty(SupportsGet = true)]
    public bool SortAscending {get; set;}
    public List<SelectListItem> SortByItems {get; set;} = new();

    [BindProperty(SupportsGet = true)]
    public int PageNumber {get; set;}=1;
    
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Orders Loading");
        var reader=new StreamReader("Data/order_items.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        Orders=csv.GetRecords<Order>().ToList();
        Console.WriteLine("Orders Loaded");
        
        SortByItems=MakeSelectListItemsFromColumnNames(Orders[0]);

        Orders=SortValues(Orders,SortBy,SortAscending).Select(item => (Order)item).ToList();
        
        Orders=Slice(Orders,PageNumber).Select(item => (Order)item).ToList();

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

using System.Globalization;
using System.Xml.Serialization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;
using static SharedFunctions;

public class CustomersModel : PageModel
{
    public List<SelectListItem> CustomerCountryOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string CustomerCountry { get; set; }="";

    public List<SelectListItem> CustomerSegmentOptions { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string CustomerSegment { get; set; }="";

    public List<SelectListItem> GenderOptions { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }="";

    public List<Customer> Customers {get; set;}

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

        Console.WriteLine("Customers Loading");
        var reader = new StreamReader("Data/customer_master.csv"); 
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        List<Customer> allCustomers=csv.GetRecords<Customer>().ToList();
        Console.WriteLine("Customers Loaded");
        
        Customers=allCustomers;
        CustomerCountryOptions=MakeSelectListItemsFromUniqueValues(allCustomers,"CustomerCountry");
        CustomerSegmentOptions=MakeSelectListItemsFromUniqueValues(allCustomers,"CustomerSegment");
        GenderOptions=MakeSelectListItemsFromUniqueValues(allCustomers,"Gender");

        if(!string.IsNullOrWhiteSpace(CustomerCountry))
        {
            Console.WriteLine($"Target Country: {CustomerCountry}");
            Customers= Customers.Where(customer => customer.CustomerCountry==CustomerCountry).ToList();
        }
        if(!string.IsNullOrWhiteSpace(CustomerSegment))
        {
            Console.WriteLine($"Target Customer Segment: {CustomerSegment}");
            Customers= Customers.Where(customer => customer.CustomerSegment==CustomerSegment).ToList();
        }
        if(!string.IsNullOrWhiteSpace(Gender))
        {
            Console.WriteLine($"Target Gender: {Gender}");
            Customers= Customers.Where(customer => customer.Gender==Gender).ToList();
        }

        Console.WriteLine(SortBy);

        
        SortByItems=MakeSelectListItemsFromColumnNames(Customers[0]);
        Customers=SortValues(Customers,SortBy,SortAscending).Select(item => (Customer)item).ToList();

        Customers=Slice(Customers,PageNumber).Select(item => (Customer)item).ToList();

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

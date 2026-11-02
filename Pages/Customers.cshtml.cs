using System.Globalization;
using System.Xml.Serialization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;

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
    public List<SelectListItem> SortColumns {get; set;} = new();

    [BindProperty(SupportsGet = true)]
    public int PageNumber {get; set;}=1;

    public List<SelectListItem> CreateSelectListItems(List<Customer> allCustomers,string key)
    {
        var property = typeof(Customer).GetProperty(key) ?? throw new ArgumentException($"No public Customer property named '{key}'.", nameof(key));
        var allValues = allCustomers.Where(c => c is not null).Select(c => property.GetValue(c)).Where(value => value is not null);
        var uniqueValues=allValues.Distinct().OrderBy(val => val).ToList();
        List<SelectListItem>  res= uniqueValues.Select(val => new SelectListItem
        {
            Value = val.ToString(),
            Text = val.ToString(),
        }).ToList();
        return res;
    }
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Customers Loading");
        var reader = new StreamReader("Data/customer_master.csv"); 
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        List<Customer> allCustomers=csv.GetRecords<Customer>().ToList();
        Console.WriteLine("Customers Loaded");
        
        Customers=allCustomers;
        CustomerCountryOptions=CreateSelectListItems(allCustomers,"CustomerCountry");
        CustomerSegmentOptions=CreateSelectListItems(allCustomers,"CustomerSegment");
        GenderOptions=CreateSelectListItems(allCustomers,"Gender");

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

        List<string> allColumns=typeof(Customer)
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
            Customers=Customers.OrderBy(order => order.GetType()
                                        .GetProperty(SortBy)?
                                        .GetValue(order, null)
                                 ).ToList();
        }
        if(SortAscending==false)
        {
            Customers.Reverse();
        }

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

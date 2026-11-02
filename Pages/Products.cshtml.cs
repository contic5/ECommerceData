using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;
using static SharedFunctions;

public class ProductsModel : PageModel
{
    public List<SelectListItem> ProductCategoryOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string ProductCategory { get; set; }="";

    public List<SelectListItem> ProductSubCategoryOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string ProductSubCategory { get; set; }="";

    public List<SelectListItem> BrandOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string Brand { get; set; }="";

    public List<SelectListItem> SupplierOptions { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string Supplier { get; set; }="";

    [BindProperty(SupportsGet = true)]
    public string SortBy {get; set;}

    [BindProperty(SupportsGet = true)]
    public bool SortAscending {get; set;}
    public List<SelectListItem> SortByItems {get; set;} = new();

    public List<Product> Products {get; set;}

    [BindProperty(SupportsGet = true)]
    public int PageNumber {get; set;}=1;
    
    public void OnGet()
    {
        long startMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        Console.WriteLine("Products Loading");
        var reader=new StreamReader("Data/product_catalog.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var allProducts=csv.GetRecords<Product>().ToList();
        Console.WriteLine("Products Loaded");

        Products=allProducts;
        ProductCategoryOptions=SharedFunctions.MakeSelectListItemsFromUniqueValues(allProducts,"ProductCategory");
        ProductSubCategoryOptions=SharedFunctions.MakeSelectListItemsFromUniqueValues(allProducts,"ProductSubCategory");
        BrandOptions=SharedFunctions.MakeSelectListItemsFromUniqueValues(allProducts,"Brand");
        SupplierOptions=SharedFunctions.MakeSelectListItemsFromUniqueValues(allProducts,"Supplier");


        if(!string.IsNullOrWhiteSpace(ProductCategory))
        {
            Console.WriteLine($"Target Category: {ProductCategory}");
            Products= Products.Where(product => product.ProductCategory==ProductCategory).ToList();
        }
        if(!string.IsNullOrWhiteSpace(ProductSubCategory))
        {
            Console.WriteLine($"Target Subcategory: {ProductSubCategory}");
            Products= Products.Where(product => product.ProductSubCategory==ProductSubCategory).ToList();
        }
        if(!string.IsNullOrWhiteSpace(Brand))
        {
            Console.WriteLine($"Target Brand: {Brand}");
            Products= Products.Where(product => product.Brand==Brand).ToList();
        }
        if(!string.IsNullOrWhiteSpace(Supplier))
        {
            Console.WriteLine($"Target Supplier: {Supplier}");
            Products= Products.Where(product => product.Supplier==Supplier).ToList();
        }

        
        SortByItems=MakeSelectListItemsFromColumnNames(Products[0]);

        Products=SortValues(Products,SortBy,SortAscending).Select(item => (Product)item).ToList();
        
        Products=Slice(Products,PageNumber).Select(item => (Product)item).ToList();

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}
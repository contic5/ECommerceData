using System.Globalization;
using CsvHelper;
using EcommerceData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceData.Pages;

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

    public List<Product> Products {get; set;}

    public List<SelectListItem> CreateSelectListItems(List<Product> allProducts,string key)
    {
        var property = typeof(Product).GetProperty(key) ?? throw new ArgumentException($"No public Product property named '{key}'.", nameof(key));
        var allValues = allProducts.Where(p => p is not null).Select(p => property.GetValue(p)).Where(value => value is not null);
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

        Console.WriteLine("Products Loading");
        var reader=new StreamReader("Data/product_catalog.csv");
        var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        var allProducts=csv.GetRecords<Product>().ToList();
        Console.WriteLine("Products Loaded");

        Products=allProducts;
        ProductCategoryOptions=CreateSelectListItems(allProducts,"ProductCategory");
        ProductSubCategoryOptions=CreateSelectListItems(allProducts,"ProductSubCategory");
        BrandOptions=CreateSelectListItems(allProducts,"Brand");
        SupplierOptions=CreateSelectListItems(allProducts,"Supplier");


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

        long endMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        long milliseconds=endMilliseconds-startMilliseconds;
        Console.WriteLine($"Runtime: {milliseconds} ms");
    }
}

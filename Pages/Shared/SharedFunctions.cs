

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class SharedFunctions
{
    public static List<Object> Slice(List<Object> arr,int PageNumber)
    {
        return arr.GetRange(PageNumber*100,100);
    }
    public static List<Object> SortValues(List<Object> arr,string SortBy,bool SortAscending)
    {
        if(!string.IsNullOrEmpty(SortBy))
        {
            arr=arr.OrderBy(product => product.GetType()
                                        .GetProperty(SortBy)?
                                        .GetValue(product, null)
                                 ).ToList();
        }
        if(SortAscending==false)
        {
            arr.Reverse();
        }
        return arr;
    }
    public static List<SelectListItem> CreateSelectListItems(List<Object> arr,string key)
    {
        var property = arr[0].GetType().GetProperty(key) ?? throw new ArgumentException($"No public Customer property named '{key}'.", nameof(key));
        var allValues = arr.Where(c => c is not null).Select(c => property.GetValue(c)).Where(value => value is not null);
        List<Object> uniqueValues=allValues.Distinct().OrderBy(val => val).ToList();
        List<Object> sortedValues=uniqueValues.OrderBy(val=>val).ToList();
        List<SelectListItem>  res= uniqueValues.Select(val => new SelectListItem
        {
            Value = val.ToString(),
            Text = val.ToString(),
        }).ToList();
        return res;
    }
}
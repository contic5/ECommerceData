
using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using MiniExcelLibs;

public class SharedFunctions
{
    public static List<Object> Slice(IEnumerable<Object> arr,int PageNumber)
    {
        return arr.Skip((PageNumber-1)*100).Take(100).ToList();
    }
    public static List<Object> SortValues(IEnumerable<Object> arr,string SortBy,bool SortAscending)
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
        return arr.ToList();
    }
    public static List<SelectListItem> MakeSelectListItemsFromUniqueValues(IEnumerable<Object> arr,string key)
    {
        var property = arr.ElementAt(0).GetType().GetProperty(key) ?? throw new ArgumentException($"No public Customer property named '{key}'.", nameof(key));
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
    public static List<SelectListItem> MakeSelectListItemsFromColumnNames(Object firstItem)
    {
        List<string> allColumns=firstItem.GetType()
                                     .GetProperties()
                                     .Select(p => p.Name)
                                     .ToList();  
        List<SelectListItem> res=allColumns.Select(val => new SelectListItem
        {
            Value = val.ToString(),
            Text = val.ToString(),
        }).ToList();
        return res;
    }
    public static List<string> getColumnNames(Object firstItem)
    {
        List<string> allColumns=firstItem.GetType()
                                     .GetProperties()
                                     .Select(p => p.Name)
                                     .ToList();  
        return allColumns;
    }
    public static List<PropertyInfo> getAllProperties(Object firstItem)
    {
        return firstItem.GetType().GetProperties().ToList();
    }
}
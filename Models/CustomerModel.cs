using System.ComponentModel.DataAnnotations;

namespace EcommerceData.Models;

public class Customer
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }

    public int CustomerAge {get; set;}

    public string Gender {get; set;}

    public string CustomerSegment {get; set;}

    public string CustomerCity {get; set;}

    public string CustomerState {get; set;}

    public string CustomerCountry {get; set;}

    public string Region {get; set;}

    public string CustomerPostalCode {get; set;}

    public decimal CustomerAcquisitionCost {get; set;}
    public Customer()
    {
        
    }
}
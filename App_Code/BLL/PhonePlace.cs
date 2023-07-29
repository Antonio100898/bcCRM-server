using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PhonePlace
/// </summary>
public class PhonePlace
{
    public int id { get; set; }
    public int phoneId { get; set; }
    public string phone { get; set; }
    public int placeId { get; set; }
    public string placeName { get; set; }
    public string bizNumber { get; set; }
    public int warrantyType { get; set; }
    public string placeRemark { get; set; }
    public bool vip { get; set; }
    public string ip { get; set; }
    public string customerName { get; set; }
    
    public PhonePlace()
    {

    }
}
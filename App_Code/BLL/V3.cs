using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class v3Group
{
    public int id { get; set; }
    public string name { get; set; }
    public string database { get; set; }
}


public class v3Branch
{
    //Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher,branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name
    public int id { get; set; }
    public string branchName { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string biCommEmail { get; set; }
    public bool kosher { get; set; }
    public string ip { get; set; }
    public int cityId { get; set; }
    public string cityName { get; set; }
}


public class v3City
{    
    public int id { get; set; }
    public string cityName { get; set; }

}


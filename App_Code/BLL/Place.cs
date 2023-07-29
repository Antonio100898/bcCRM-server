using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Place
/// </summary>
public class Place
{
    public int Id { get; set; }
    public string placeName { get; set; }
    public string ip { get; set; }
    public string biznumber { get; set; }
    public int warrantyType { get; set; }

    public Place()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HardwareType
/// </summary>
public class HardwareType
{
    public int id { get; set; }
    public string hardwareTypeName { get; set; }
    public List<HardwareModel> models { get; set; }

    public HardwareType()
    {
        models = new List<HardwareModel>();
    }
}
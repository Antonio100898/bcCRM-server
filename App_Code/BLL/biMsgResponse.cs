using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for biMsgResponse
/// </summary>
public class biMsgResponse
{
    public int id { get; set; }    
    public bool finish { get; set; }
    public bool failed { get; set; }
    public string responseMsg { get; set; }

    public biMsgResponse()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
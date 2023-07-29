using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for BeeClubSrv
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BeeClubSrv : System.Web.Services.WebService
{

    public BeeClubSrv()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string HelloWorld123(string sss)
    {
        return "Hello World";
    }

    [WebMethod]
    public string Pay(string userName, string password, string card, double sum)
    {
        return "True";
    }

    [WebMethod]
    public string Refund(string userName, string password, string card, double sum)
    {
        return "True";
    }

    [WebMethod]
    public string RegisterNewUser(string userName, string password, string card)
    {
        return "True";
    }
}

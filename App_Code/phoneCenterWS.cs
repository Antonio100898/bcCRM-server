using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for phoneCenterWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class phoneCenterWS : System.Web.Services.WebService
{
    private const string mainKey = "c74fc40e1-1e3b-4d935-a687-b12311ce0e18";
    public phoneCenterWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SendNewCallAnswered(string key, string phone, int shluha)
    {
        //Logger.ErrorLog("phoneCenterWS.SendNewCallAnswered Start");
        try
        {
            if (string.IsNullOrEmpty(key))
            {
                Logger.ErrorLog("phoneCenterWS.SendNewCallAnswered Key Is Null");
                return "false";
            }

            if (mainKey != key)
            {
                Logger.ErrorLog("phoneCenterWS.SendNewCallAnswered Key Is Empty");
                return "false";
            }

            if (string.IsNullOrEmpty(phone))
            {
                Logger.ErrorLog("phoneCenterWS.SendNewCallAnswered Phone Is Empty");
                return "false";
            }

            if (shluha == 0)
            {
                Logger.ErrorLog("phoneCenterWS.SendNewCallAnswered Shluha Is 0");
                return "false";
            }

            WebDal.AppendNewAnsweredCall(phone, shluha);
        }
        catch (Exception e)
        {
            
        }

        return "True";
    }

    [WebMethod]
    public string SendNewIncomeCall(string key, string phone, string shluha)
    {
        if (mainKey != key)
        {
            return "false";
        }

        //WebDal.UpdateTodayProblemsCountSetZero();

        return "True";
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

public partial class HelpPage : System.Web.UI.Page
{

    public static int GetWorkerId
    {
        get
        {
            try
            {
                string s = HttpContext.Current.Session["WorkerId"].ToString();
                int i = 0;
                if (int.TryParse(s, out i))
                {
                    return i;
                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Redirect("Default.aspx");
            }

            return 0;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string AddQuestion(string q)
    {
        if (!string.IsNullOrEmpty(q))
        {
            WebDal.AppendQuestion(GetWorkerId,q);
        }
        return "";
    }

}
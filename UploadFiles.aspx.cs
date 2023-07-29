using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["currentProblemID"] != null)
        {
            string s = HttpContext.Current.Session["currentProblemID"].ToString();

            txtFileProblemId.Text = s;
        }
    }

    public string ProblemID
    {
        get { return this.txtFileProblemId.Text; }
        set { this.txtFileProblemId.Text = value; }
    }
}
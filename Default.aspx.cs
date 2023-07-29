using System;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Worker"] = null;
        Session["WorkerId"] = null;

        //Response.Redirect("https://bccrm-334f4.web.app/");
    }

    protected void btnSighIn_Click(object sender, EventArgs e)
    {
        string userName = Request.Form["txtUserName"];
        string password = Request.Form["txtPassword"];


        Worker w = WebDal.GetWorker(userName, password);
        //CacheHelper.Instance.GetWorker(userName, password);

        if (w == null)
        {
            lblStatus.Text = "שם משתמש או סיסמה לא נכונים";
            lblStatus.Visible = true;
            return;
        }

        if (!w.active)
        {
            lblStatus.Text = "המשתמש אינו פעיל";
            lblStatus.Visible = true;
            return;
        }


        HttpContext.Current.Session["Worker"] = w;
        HttpContext.Current.Session["WorkerId"] = w.Id;
        //HttpContext.Current.Session["Shluha"] = w.shluha;

        Response.Cookies["WorkerId"].Value = w.Id.ToString();
        Response.Cookies["workerName"].Value = w.workerName;
        Response.Cookies["Shluha"].Value = w.shluha.ToString();

        HttpContext.Current.Session["showProblemWhere"] = "toWorker = " + w.Id + " AND statusId<>2 ";
        Response.Redirect("AddProblemNew.aspx");

    }
}
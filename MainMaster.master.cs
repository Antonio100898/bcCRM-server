using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class MainMaster : System.Web.UI.MasterPage
{



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Worker"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        try
        {
            Worker w = (Worker)Session["Worker"];
            //lblWorkerName.Text = w.workerName;
            lblWorkerName.InnerHtml = w.workerName;

            if (!IsPostBack)
            {
                SetSummery(w.Id);
                try
                {
                    imgWorker.Visible = !string.IsNullOrEmpty(w.imgPath);
                    imgWorker.ImageUrl = "~/Pics/workers/" + w.imgPath;
                }
                catch (Exception ex)
                {
                }
                if (grdMsgs.Rows.Count > 0)
                {
                    divMsg.Style["display"] = "block";
                    divMsg.Visible = true;
                    divMsg.Style["Height"] = "500px";
                    //grdMsgs.Height = Unit.Pixel(500);
                }
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void SetSummery(int workerId)
    {
        ProblemsCountSummery p = WebDal.GetProblemsCountsummeryOlder(workerId);
        btnWaitingProblems.Text = p.openProblems.ToString();
        btnHandlingProblems.Text = p.HandlingProblems.ToString();
        btnIOpened.Text = p.Iopened.ToString();
        btnTech.Text = p.tech.ToString();
        btnDejavoo.Text = p.dejavoo.ToString();
        //btnKiosk.Text = p.kiosk.ToString();        
        btnUpgrades.Text = p.upgrades.ToString();
        btnReportToYaron.Text = p.delivery_server.ToString();
        btnResets.Text = p.resets.ToString();
        btnMenus.Text = p.menu.ToString();
        btnSoftware.Text = p.software.ToString();
        btnCounting.Text = p.counting.ToString();
        btnMarketing.Text = p.marketing.ToString();
        btnUsers.Text = p.users.ToString();
        btnReturnToClient.Text = p.returnToClient.ToString();
        btnReturnToClient.Font.Bold = (p.returnToClient > 0);
        btnReturnToClient.ForeColor = System.Drawing.Color.White;        
        if ((p.returnToClient > 0))
        {
            btnReturnToClient.ForeColor = System.Drawing.Color.Red;
            btnReturnToClient.Font.Size = FontUnit.XLarge;
        }
        btnDevelopment.Text = p.developers.ToString();
        //btnReportToYaron.Text = p.reportToYaron.ToString();
        btnAllProblems.Text = p.allProblems.ToString();
        btnTodayProblems.Text = p.todayProblems.ToString();
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        //Session["Worker"] = null;
        //Response.Redirect("Default.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["Worker"] = null;
        Response.Redirect("Default.aspx");

    }

    private void SetShowProblem(string header, string where)
    {
        Session["showProblemHeader"] = header;
        Session["showProblemWhere"] = where;
        //Response.Redirect("ShowProblems.aspx");
        Response.Redirect("AddProblemNew.aspx");

    }

    protected void btnAllProblems_Click(object sender, EventArgs e)
    {
        SetShowProblem("כל התקלות הפתוחות", "statusId<>2");
    }

    protected void btnReportToYaron_Click(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];
        if (w.userTypeId == 1)
        {
            SetShowProblem("ירון", "departmentId=15 AND statusId<>2");
        }
    }
    protected void btnUpgrades_Click(object sender, EventArgs e)
    {
        SetShowProblem("שדרוגים", "departmentId=6 AND statusId<>2");
    }

    protected void btnResets_Click(object sender, EventArgs e)
    {
        SetShowProblem("איפוסים", "departmentId=5 AND statusId<>2");
    }

    protected void btnMenus_Click(object sender, EventArgs e)
    {
        SetShowProblem("תפריטים", "departmentId=4 AND statusId<>2");
    }

    protected void btnSoftware_Click(object sender, EventArgs e)
    {
        SetShowProblem("תוכנה", "departmentId=3 AND statusId<>2");
    }

    protected void btnTech_Click(object sender, EventArgs e)
    {
        SetShowProblem("טכני", "departmentId=2 AND statusId<>2");
    }

    protected void btnGeneral_Click(object sender, EventArgs e)
    {
        SetShowProblem("כללי", "departmentId=1 AND statusId<>2");
    }

    protected void btnHandlingProblems_Click(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];
        SetShowProblem("בטיפול שלך", "toWorker=" + w.Id + " AND statusId=1");
    }

    protected void btnWaitingProblems_Click(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];
        SetShowProblem("ממתינים לך", "toWorker=" + w.Id + " AND statusId=0");
    }

    protected void btnCounting_Click(object sender, EventArgs e)
    {
        SetShowProblem("טכני", "departmentId=7 AND statusId<>2");
    }

    protected void btnMarketing_Click(object sender, EventArgs e)
    {
        SetShowProblem("טכני", "departmentId=8 AND statusId<>2");
    }

    protected void btnTodayProblems_Click(object sender, EventArgs e)
    {
        DateTime start = DateTime.Now;
        if (DateTime.Now.Hour < 6)
        {
            start = start.AddDays(-1);
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }
        else
        {
            start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
        }

        Session["FindStartDate"] = start;

        Response.Redirect("FindProblem.aspx");
    }

    protected void btnReturnToClient_Click(object sender, EventArgs e)
    {
        SetShowProblem("לחזור ללקוח", "departmentId=9 AND statusId<>2");
    }

    protected void btnDevelopment_Click(object sender, EventArgs e)
    {
        SetShowProblem("ציוד", "departmentId=10 AND statusId<>2");
    }

    protected void btnUsers_Click(object sender, EventArgs e)
    {
        SetShowProblem("יוזרים", "departmentId=11 AND statusId<>2");
    }

    protected void btnCloseMsg_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow item in grdMsgs.Rows)
        {
            string s = item.Cells[0].Text;
            int msgId = int.Parse(s);
            WebDal.UpdateMsgStatus(msgId, true);
        }

        divMsg.Visible = false;
    }

    protected void btnIOpened_Click(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];
        SetShowProblem("אני פתחתי", "workerId=" + w.Id + " AND statusId<>2");
    }


    protected void imgWorker_Click(object sender, EventArgs e)
    {
        //זה לא טוב, כי הפוקוס גורם כל הזמן לעבור לטופס הזה
        Response.Redirect("workerPlace.aspx");
    }


    protected void btnDejavoo_Click(object sender, EventArgs e)
    {
        SetShowProblem("dejavoo", "departmentId=13 AND statusId<>2");
    }

    protected void btnKiosk_Click(object sender, EventArgs e)
    {
        SetShowProblem("קיוסק", "departmentId=12 AND statusId<>2");
    }
}

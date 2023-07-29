using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class FindProblem : System.Web.UI.Page
{
    private static List<Phone> phones = new List<Phone>();
    private static List<Place> places = new List<Place>();
    private static List<Worker> workers = new List<Worker>();
    private static Dictionary<string, int> phonesDic = new Dictionary<string, int>();
    private static Dictionary<string, int> placesDic = new Dictionary<string, int>();
    private static DataTable currentTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divProblems.Visible = false;

            var a = SetParams();
            var b = RefreshPage();
            Task.WhenAll(a, b);
        }
    }

    private async Task RefreshPage()
    {
        if (Session["FindStartDate"] == null)
        {
            DateTime start = DateTime.UtcNow.AddHours(4);
            if (DateTime.Now.Hour < 6)
            {
                start = start.AddDays(-1);
                start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
            }
            else
            {
                start = new DateTime(start.Year, start.Month, start.Day, 6, 0, 0);
            }

            var script = "$('#txtFindStartDate').val('" + start.ToString("dd/MM/yy HH:mm:ss") + "');";
            ClientScript.RegisterStartupScript(typeof(string), "txtFindStartDate", script, true);

            DateTime finish = new DateTime(DateTime.Now.Year, DateTime.UtcNow.AddHours(4).Month, DateTime.UtcNow.AddHours(4).Day, DateTime.UtcNow.AddHours(4).Hour, 0, 0);

            script = "$('#txtFindFinishDate').val('" + finish.ToString("dd/MM/yy HH:mm:ss") + "');";
            ClientScript.RegisterStartupScript(typeof(string), "txtFindFinishDate", script, true);
        }
        else
        {
            DateTime start = DateTime.Parse(Session["FindStartDate"].ToString());
            Session["FindStartDate"] = null;
            Find(start, DateTime.Now.ToUniversalTime().AddHours(3));
        }
    }

    private async Task SetParams()
    {
        phones = WebDal.GetPhones(out phonesDic);
        places = CacheHelper.Instance.places;
        //workers = CacheHelper.Instance.workers;
        placesDic = CacheHelper.Instance.placesDic;
        //phonesDic = CacheHelper.Instance.phonesDic;

        cboFindWorker.Items.Clear();
        cboFindToWorker.Items.Clear();
        cboFindWorker.Items.Add(new ListItem { Value = "0", Text = "כולם" });
        cboFindToWorker.Items.Add(new ListItem { Value = "0", Text = "כולם" });
        foreach (Worker item in workers)
        {
            cboFindWorker.Items.Add(new ListItem { Value = item.Id.ToString(), Text = item.workerName });
            cboFindToWorker.Items.Add(new ListItem { Value = item.Id.ToString(), Text = item.workerName });
        }
    }

    [WebMethod]
    public static List<Worker> GetWorkerName(string searchWorkerName)
    {
        List<Worker> result = new List<Worker>();
        if (workers.Count > 0)
        {
            foreach (Worker w in workers)
            {
                if (w.workerName.Contains(searchWorkerName))
                {
                    result.Add(w);
                    if (result.Count > 50)
                    {
                        return result;
                    }
                }
            }
        }
        return result;
    }


    [WebMethod]
    public static List<Phone> GetPhone(string searchPhone)
    {
        List<Phone> result = new List<Phone>();
        if (string.IsNullOrWhiteSpace(searchPhone))
        {
            return result;
        }
        if (searchPhone.Length < 4)
        {
            return result;
        }

        if (phones.Count > 0)
        {
            foreach (Phone p in phones)
            {
                if (p.phone.Contains(searchPhone))
                {
                    result.Add(p);
                    if (result.Count > 50)
                    {
                        return result;
                    }
                }
            }
        }
        return result;
    }

    [WebMethod]
    public static List<Place> GetPlaces(string searchPlaceName)
    {
        List<Place> result = new List<Place>();
        if (string.IsNullOrWhiteSpace(searchPlaceName))
        {
            return result;
        }

        searchPlaceName = searchPlaceName.Replace("@#$%", "'");

        if (places.Count > 0)
        {
            foreach (Place p in places)
            {
                if (p.placeName.Contains(searchPlaceName))
                {
                    result.Add(p);
                    if (result.Count > 50)
                    {
                        return result;
                    }
                }
            }
        }
        return result;
    }

    private int GetPhoneId(string phone)
    {
        int phoneId = 0;
        if (phonesDic.ContainsKey(phone))
        {
            phoneId = phonesDic[phone];
        }

        return phoneId;
    }

    private int GetPlaceId(string placeName, string ip)
    {
        int placeId = 0;
        if (placesDic.ContainsKey(placeName))
        {
            placeId = placesDic[placeName];
        }

        return placeId;
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {

        string start = Request.Form["txtFindStartDate"];
        DateTime startDate;

        if (!DateTime.TryParseExact(start, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
        {
            if (!DateTime.TryParseExact(start, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                //lbl.InnerText = "נכשל להמיר את תאריך ההתחלה " + start;

                var script = "$('#txtFindStartDate').val('" + start + "');";
                ClientScript.RegisterStartupScript(typeof(string), "txtFindStartDate", script, true);

                return;
            }
        }

        string finish = Request.Form["txtFindFinishDate"];
        DateTime finishDate;
        if (!DateTime.TryParseExact(finish, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out finishDate))
        {
            if (!DateTime.TryParseExact(finish, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out finishDate))
            {
                //lbl.InnerText = "נכשל להמיר את תאריך הסיום " + finish;

                var script = "$('#txtFindFinishDate').val('" + finish + "');";
                ClientScript.RegisterStartupScript(typeof(string), "txtFindFinishDate", script, true);

                return;
            }
        }

        Find(startDate, finishDate);
        //lbl.InnerText = "Start: " + startDate.ToString() + Environment.NewLine + " finish: " + finishDate.ToString();
    }

    private void Find(DateTime startDate, DateTime finishDate)
    {
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter { ParameterName = "@startTime", Value = startDate });
        values.Add(new SqlParameter { ParameterName = "@finishTime", Value = finishDate });
                
        string whereClose = "";

        int workerId = int.Parse(cboFindWorker.SelectedValue);
        if (workerId > 0)
        {            
            whereClose += " AND (problemsClose.workerId = @workerId) ";
            values.Add(new SqlParameter { ParameterName = "@workerId", Value = workerId });
        }

        string phone = txtFindPhone.Text;
        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrWhiteSpace(phone))
        {
            int phoneId = GetPhoneId(phone);            
            whereClose += " AND (problemsClose.phoneId = @phoneId) ";
            values.Add(new SqlParameter { ParameterName = "@phoneId", Value = phoneId });
        }

        string placeName = txtFindPlaceName.Text;
        if (!string.IsNullOrEmpty(placeName) && !string.IsNullOrWhiteSpace(placeName))
        {
            int placeId = GetPlaceId(placeName, "");
            if (placeId > 0)
            {                
                whereClose += " AND (problemsClose.placeNameId = @placeNameId) ";
                values.Add(new SqlParameter { ParameterName = "@placeNameId", Value = placeId });
            }
            else
            {
                //מחפש לפי שם המקום שמי
                placeName = placeName.Replace("'", "''");                    
                whereClose += " AND (problemsClose.placeName LIKE N'%" + placeName + "%') ";
            }
        }

        int status = int.Parse(cboFindStatus.SelectedValue);
        if (status > -1)
        {            
            whereClose += " AND (problemsClose.statusId = @statusId) ";
            values.Add(new SqlParameter { ParameterName = "@statusId", Value = status });
        }

        int emergencyType = int.Parse(cboFindEmergencyType.SelectedValue);
        if (emergencyType > -1)
        {         
            whereClose += " AND (problemsClose.emergencyId = @emergencyId) ";
            values.Add(new SqlParameter { ParameterName = "@emergencyId", Value = emergencyType });
        }

        int departmentId = int.Parse(cboFindDepartment.SelectedValue);
        if (departmentId > -1)
        {         
            whereClose += " AND (problemsClose.departmentId = @departmentId) ";
            values.Add(new SqlParameter { ParameterName = "@departmentId", Value = departmentId });
        }

        int toWorkerId = int.Parse(cboFindToWorker.SelectedValue);
        if (toWorkerId > 0)
        {         
            whereClose += " AND (problemsClose.toWorker = @toWorker) ";
            values.Add(new SqlParameter { ParameterName = "@toWorker", Value = toWorkerId });
        }

        if (!string.IsNullOrEmpty(txtFindProblemDesc.Text))
        {         
            whereClose += " AND (problemsClose.problemDesc LIKE N'%" + txtFindProblemDesc.Text + "%') ";
            
        }

        if (!string.IsNullOrEmpty(txtFindProblemSol.Text))
        {         
            whereClose += " AND (problemsClose.problemSolution LIKE N'%" + txtFindProblemSol.Text + "%') ";
            
        }

        string sql =            
            "SELECT problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, problemsClose.HaveLog " +
            "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN departments ON problemsClose.departmentId = departments.id INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
            "WHERE  (startTime >= @startTime) AND (startTime < @finishTime) " + whereClose + " " +
            "Order By id Desc";



        DataSet ds = Dal.GetDataSet(sql, values);
        currentTable = ds.Tables[0];
        grdProblems.DataSource = currentTable;
        grdProblems.DataBind();
        divProblems.Visible = true;
        lblRowCount.Text = "כמות בעיות " + ds.Tables[0].Rows.Count;

        var script = "$('#txtFindStartDate').val('" + startDate.ToString("dd/MM/yyyy HH:mm:ss") + "');";
        ClientScript.RegisterStartupScript(typeof(string), "txtFindStartDate", script, true);

        script = "$('#txtFindFinishDate').val('" + finishDate.ToString("dd/MM/yyyy HH:mm:ss") + "');";
        ClientScript.RegisterStartupScript(typeof(string), "txtFindFinishDate", script, true);

    }

    protected void grdProblems_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (currentTable != null)
        {
            //currentTable.DefaultView.Sort = e.SortExpression;
            currentTable.DefaultView.Sort = e.SortExpression + " " + SortDir(e.SortExpression); // sort direction
            grdProblems.DataSource = currentTable;
            grdProblems.DataBind();
        }

        try
        {
            string start = Request.Form["txtFindStartDate"];
            DateTime startDate;

            if (!DateTime.TryParseExact(start, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                //lbl.InnerText = "נכשל להמיר את תאריך ההתחלה " + start;
                return;
            }

            string finish = Request.Form["txtFindFinishDate"];
            DateTime finishDate;
            if (!DateTime.TryParseExact(finish, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out finishDate))
            {
                //lbl.InnerText = "נכשל להמיר את תאריך הסיום " + finish;
                return;
            }
            var script = "$('#txtFindStartDate').val('" + startDate.ToString("dd/MM/yyyy HH:mm:ss") + "');";
            ClientScript.RegisterStartupScript(typeof(string), "txtFindStartDate", script, true);

            script = "$('#txtFindFinishDate').val('" + finishDate.ToString("dd/MM/yyyy HH:mm:ss") + "');";
            ClientScript.RegisterStartupScript(typeof(string), "txtFindFinishDate", script, true);
        }
        catch (Exception)
        {

        }
    }

    private string SortDir(string sColumn)
    {
        string sDir = "asc"; // ascending by default
        string sPreviousColumnSorted = ViewState["SortColumn"] != null
            ? ViewState["SortColumn"].ToString()
            : "";

        if (sPreviousColumnSorted == sColumn) // same column clicked? revert sort direction
            sDir = ViewState["SortDir"].ToString() == "asc"
                ? "desc"
                : "asc";
        else
            ViewState["SortColumn"] = sColumn; // store current column clicked

        ViewState["SortDir"] = sDir; // store current direction

        return sDir;
    }

    protected void btnShowLog_Click(object sender, EventArgs e)
    {
        ImageButton b = sender as ImageButton;
        long problemId = long.Parse(b.AlternateText);

        Session["showLogproblemId"] = problemId;
        divLog.Visible = true;
        dsLogGroups.DataBind();
        dsLogDetails.DataBind();

        //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", "ShowLog.aspx"));
    }

    protected void grdProblems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowLog")
        {
            long problemId = long.Parse(e.CommandArgument.ToString());
            //if (Session["showLogproblemId"] != null)
            //{
            //    if (Session["showLogproblemId"].ToString() == problemId.ToString() && divLog.Visible)
            //    {
            //        divLog.Visible = false;
            //        return;
            //    }
            //}


            Session["showLogproblemId"] = problemId;
            divLog.Visible = true;
            dsLogGroups.DataBind();
            dsLogDetails.DataBind();
            grdLogGroups.SelectedIndex = 0;
            //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", "ShowLog.aspx"));
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
    }

    protected void btnHideLog_Click(object sender, EventArgs e)
    {
        divLog.Visible = false;
    }
}
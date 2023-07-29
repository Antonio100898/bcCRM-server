using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;

public partial class AddProblemNew : System.Web.UI.Page
{
    //private static int shluhaId = 0;

    public static int currentProblemID
    {
        get
        {
            try
            {
                string s = HttpContext.Current.Session["currentProblemID"].ToString();
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
        set
        {
            HttpContext.Current.Session["currentProblemID"] = value;
        }
    }

    public static int GetWorkerId
    {
        get
        {
            try
            {
                //string s = HttpContext.Current.Session["WorkerId"].ToString();
                string s = "0";
                if (HttpContext.Current.Request.Cookies["WorkerId"] != null)
                {
                    s = HttpContext.Current.Request.Cookies["WorkerId"].Value;
                }


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


    public static int GetWorkerShluha
    {
        get
        {
            try
            {
                //string s = HttpContext.Current.Session["WorkerId"].ToString();
                string s = "0";
                if (HttpContext.Current.Request.Cookies["Shluha"] != null)
                {
                    s = HttpContext.Current.Request.Cookies["Shluha"].Value;
                }


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

    public static string GetCurrentWhere
    {
        get
        {
            try
            {

                string result = "";
                if (HttpContext.Current.Session["showProblemWhere"] != null)
                {
                    result = HttpContext.Current.Session["showProblemWhere"].ToString();
                }

                return result;
            }
            catch (Exception e)
            {

            }

            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["worker"] == null)
        //{
        //    Response.Redirect("Default.aspx");
        //}

        //Worker w = (Worker)Session["worker"];
        //if (w == null)
        //{
        //    Response.Redirect("Default.aspx");
        //}
        
        if (GetWorkerId==0)
        {
            return;
        }

        //shluhaId = GetWorkerShluha;

        //if (!IsPostBack)
        //{
        //    //places = CacheHelper.Instance.places;            
        //    //phonePlaces = CacheHelper.Instance.phonePlaces;
        //}
    }

    [WebMethod]
    public static List<PhonePlace> GetPlacesForPhone(string phone)
    {

        List<PhonePlace> result = new List<PhonePlace>();
        if (string.IsNullOrWhiteSpace(phone))
        {
            return result;
        }

        result = WebDal.GetPhonePlace(phone);

        return result;
    }

    [WebMethod]
    public static string GetWorkers()
    {
        return WebDal.GetWorkersDS().GetXml();
    }


    [WebMethod]
    public static int AddNewPhonePlace(string phone, int placeId, string placeName, string ip, string cusName)
    {
        int result = 0;
        int phoneId = 0;

        phoneId = WebDal.GetPhoneId(phone);
        if (phoneId == 0)
        {
            phoneId = WebDal.AppendPhone(phone);
        }


        if (placeId == 0)
        {
            if (string.IsNullOrEmpty(ip) || string.IsNullOrWhiteSpace(ip))
            {
                placeId = WebDal.GetPlaceId(placeName);
            }
            else
            {
                placeId = WebDal.GetPlaceId(placeName, ip);
            }

            if (placeId == 0)
            {
                placeId = WebDal.AppendPlace(placeName, ip,"");
            }
        }

        List<PhonePlace> phonePlaces = WebDal.GetPhonePlace(phone);

        if (phonePlaces.Count > 0)
        {
            foreach (PhonePlace item in phonePlaces)
            {
                if (item.placeId == placeId && item.ip == ip && item.placeName == placeName)
                {
                    return item.id;
                }
            }
        }

        result = WebDal.AppendPhonePlace(phoneId, placeId, cusName);
        //if (phonePlaces.ContainsKey(phone))
        //{
        //    phonePlaces[phone].Add(new PhonePlace { id = result, customerName = cusName, phone = phone, ip = ip, phoneId = phoneId, placeId = placeId, placeName = placeName });
        //}
        //else
        //{
        //    phonePlaces.Add(phone, new List<PhonePlace> { new PhonePlace { id = result, customerName = cusName, phone = phone, ip = ip, phoneId = phoneId, placeId = placeId, placeName = placeName } });
        //}

        return result;
    }


    [WebMethod]
    public static string AddProblem(string phone, int phonePlaceId)
    {
        int phoneId = 0;
        int placeId = 0;
        string placeName = string.Empty;
        string ip = string.Empty;
        string customerName = string.Empty;
        int toWorkerId = 36;

        int workerId = GetWorkerId;
        //לפתוח תקלה על העובד
        if (phone == "1122334455" && phonePlaceId == -1)
        {
            //Worker w = (Worker)HttpContext.Current.Session["worker"];
            string workerName = HttpContext.Current.Request.Cookies["workerName"].Value;

            phoneId = 22974;
            placeId = 12992;
            placeName = "כללי";
            ip = "";
            customerName = workerName;
        }
        else
        {
            List<PhonePlace> phonePlaces = WebDal.GetPhonePlace(phone);

            if (phonePlaces.Count > 0)
            {
                foreach (var item in phonePlaces)
                {
                    if (item.id == phonePlaceId)
                    {
                        phoneId = item.phoneId;
                        placeId = item.placeId;
                        placeName = item.placeName;
                        ip = item.ip;
                        customerName = item.customerName;
                    }
                }
            }
        }

        if (workerId == 0)
        {
            workerId = toWorkerId;
        }

        int problemId = WebDal.AppendProblemIDs(phone);
        WebDal.UpdateTodayProblemsCountAddOne();
        WebDal.AppendProblem(problemId, workerId, phoneId, phone, ip, placeId, placeName, customerName, "", "", toWorkerId);

        currentProblemID = problemId;

        int wDepart = WebDal.GetWorkerDepartment(GetWorkerId);
        if (wDepart == 9)
        {
            Problem problem = WebDal.GetProblem(problemId);
            WebDal.UpdateNivServer(problem);
        }

        return problemId + ";" + phoneId + ";" + placeId + ";" + placeName + ";" + ip + ";" + customerName + ";" + workerId;

    }

    [WebMethod]
    public static string UpdateProblem(int problemId, string ip, int placeId, string placeName, string customerName, string desc, string solution, int toWorker, int status, int emergency, int department, bool reportToYaron)
    {
        if (placeId == 0)
        {
            placeId = WebDal.GetPlaceId(placeName, ip);
            if (placeId == 0)
            {
                placeId = WebDal.GetPlaceId(placeName);
            }

            if (placeId == 0)
            {
                placeId = WebDal.AppendPlace(placeName, ip,"");
            }
        }

        if (toWorker == 0)
        {
            toWorker = 36;
        }



        DateTime finishTime = CacheHelper.Instance.GetIsraelTime();

        CheckUpdateLog(problemId, ip, placeId, placeName, customerName, desc, solution, toWorker, status, emergency, department, reportToYaron, finishTime);

        WebDal.UpdateProblem(problemId, ip, placeId, placeName, customerName, desc, solution, toWorker, status, emergency, department, reportToYaron, finishTime, GetWorkerId);
                
        int wDepart= WebDal.GetWorkerDepartment(GetWorkerId);
        if (wDepart == 9)
        {
            Problem problem = WebDal.GetProblem(problemId);
            WebDal.UpdateNivServer(problem);
        }

        if (department == 4 && status != 2)
        {
            SendMsgToSlack(problemId, customerName, ip, desc, solution, placeName);
        }

        currentProblemID = 0;
        return "";
    }

    private static void SendMsgToSlack(int problemId, string customerName, string ip, string desc, string solution, string placeName)
    {
        try
        {
            string phone = WebDal.GetProblemPhone(problemId);
            string slackMsg = "צפרא טבא" + Environment.NewLine +
                              "לקוח: " + customerName + Environment.NewLine+
                              "מקום: " + placeName + Environment.NewLine;


            if (!string.IsNullOrEmpty(phone))
            {
                slackMsg += "טלפון: " + phone + Environment.NewLine;
            }
            slackMsg += "IP: " + ip + Environment.NewLine;
            slackMsg += "תקלה: " + desc + Environment.NewLine;
            if (!string.IsNullOrEmpty(solution))
            {
                slackMsg += "פתרון: " + solution;
            }

            SlackClient s = new SlackClient(new Uri("https://hooks.slack.com/services/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"));
            SlackClient.SlackMessage msg = new SlackClient.SlackMessage();
            msg.Text = slackMsg;
            msg.UserName = "CRM";
            s.SendSlackMessage(msg);
        }
        catch (Exception ex)
        {
            Logger.ErrorLog("SendMsgToSlack", ex, "");
        }
    }

    private static void CheckUpdateLog(int problemId, string ip, int placeId, string placeName, string customerName, string desc, string solution, int toWorker, int status, int emergency, int department, bool reportToYaron, DateTime finishTime)
    {
        try
        {

            DataSet ds = WebDal.GetProblemDS(problemId);
            DataTable dt = ds.Tables[0];
            DataRow item = dt.Rows[0];

            string groupKey = Guid.NewGuid().ToString();
            int workerId = GetWorkerId;

            AppendLog(groupKey, problemId, workerId, "ip", item["ip"].ToString(), ip);
            AppendLog(groupKey, problemId, workerId, "placeNameId", item["placeNameId"].ToString(), placeId);
            AppendLog(groupKey, problemId, workerId, "placeName", item["placeName"].ToString(), placeName);
            AppendLog(groupKey, problemId, workerId, "customerName", item["customerName"].ToString(), customerName);
            AppendLog(groupKey, problemId, workerId, "problemDesc", item["problemDesc"].ToString(), desc);
            AppendLog(groupKey, problemId, workerId, "problemSolution", item["problemSolution"].ToString(), solution);
            AppendLog(groupKey, problemId, workerId, "toWorker", item["toWorker"].ToString(), toWorker);
            AppendLog(groupKey, problemId, workerId, "statusId", item["statusId"].ToString(), status);
            AppendLog(groupKey, problemId, workerId, "emergencyId", item["emergencyId"].ToString(), emergency);
            AppendLog(groupKey, problemId, workerId, "departmentId", item["departmentId"].ToString(), department);
            AppendLog(groupKey, problemId, workerId, "reportToYaron", item["reportToYaron"].ToString(), reportToYaron);
        }
        catch (Exception e)
        {
            //Logger.ErrorLog("CheckUpdateLog", e, "");
        }
    }

    private static void AppendLog(string groupKey, int problemId, int workerId, string fieldName, string oldValue, object newValue)
    {
        if (oldValue != newValue.ToString())
        {
            WebDal.AppendLog(groupKey, problemId, workerId, fieldName, oldValue, newValue.ToString());
        }
    }

    [WebMethod]
    public static string GetProblemsForPhone(int problemId, string phone)
    {
        DataSet ds = WebDal.GetProblemsForPhones(problemId, phone);
        return ds.GetXml();
    }

    [WebMethod]
    public static string GetOpenProblemsForWorker()
    {

        DataSet ds = WebDal.GetOpenProblemsForWorker(GetWorkerId);
        return ds.GetXml();
    }

    [WebMethod]
    public static string GetProblems(int problemId, string phone)
    {

        DataSet ds = new DataSet();
        if (problemId == 0)
        {
            ds = WebDal.GetProblems(GetCurrentWhere); // "toWorker = " + GetWorkerId + " ");            
        }
        else
        {
            ds = WebDal.GetProblemsForPhones(problemId, phone);
        }

        return ds.GetXml();
    }

    [WebMethod]
    public static string GetProblem(int problemId)
    {
        currentProblemID = problemId;
        DataSet ds = WebDal.GetProblemDS(problemId);
        return ds.GetXml();
    }

    [WebMethod]
    public static string GetLog(int problemId)
    {
        DataTable dtResult = new DataTable();
        //[id],[groupKey],[problemId],[workerId],firstName + ' ' + lastName as workerName,[fieldName],[originalValue],[newValue],[commitTime]
        DataSet ds = WebDal.GetLogsDs(problemId);
        DataTable dsWorkers = WebDal.GetWorkersDS().Tables[0];

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                dtResult.Columns.Add(new DataColumn("groupKey"));
                dtResult.Columns.Add(new DataColumn("workerId"));

                string groupKey = string.Empty;
                foreach (DataRow item in dt.Rows)
                {
                    string colName = item["fieldName"].ToString();
                    string originalValue = item["originalValue"].ToString();
                    string newValue = item["newValue"].ToString();

                    item["fieldName"] = GetFieldName(colName);

                    switch (colName)
                    {
                        case "toWorker":
                            DataRow[] rows = dsWorkers.Select("id=" + originalValue);
                            if (rows.Length > 0)
                            {
                                item["originalValue"] = rows[0]["firstName"] + " " + rows[0]["lastName"];
                            }

                            rows = dsWorkers.Select("id=" + newValue);
                            if (rows.Length > 0)
                            {
                                item["newValue"] = rows[0]["firstName"] + " " + rows[0]["lastName"];
                            }
                            break;
                        default:
                            break;
                    }

                }
            }


        }

        return ds.GetXml();
    }

    private static string GetFieldName(string fieldName)
    {
        switch (fieldName)
        {
            case "problemDesc":
                fieldName = "בעיה";
                break;
            case "departmentId":
                fieldName = "מחלקה";
                break;
            case "toWorker":
                fieldName = "עובד מטפל";
                break;
            case "statusId":
                fieldName = "סטטוס";
                break;
            case "problemSolution":
                fieldName = "פיתרון";
                break;
            case "placeName":
                fieldName = "placeName";
                break;
            case "customerName":
                fieldName = "לקוח";
                break;
            case "emergencyId":
                fieldName = "דחיפות";
                break;
            case "reportToYaron":
                fieldName = "ירון";
                break;

            default:
                break;
        }

        return fieldName;
    }


    [WebMethod]
    public static string GetFiles(int problemId)
    {
        DataSet ds = WebDal.GetFilesDS(problemId);
        if (ds == null)
        {
            return "";
        }
        return ds.GetXml();
    }

    [WebMethod]
    public static string SearchPlacesName(string placeName)
    {
        DataSet ds = WebDal.GetPlacesByName(placeName);
        if (ds != null)
        {
            return ds.GetXml();
        }
        return "";
    }

    [WebMethod]
    public static string GetPhoneNumberAnswered()
    {
        string result = "";
        try
        {
            int shluha = GetWorkerShluha;
            if (shluha == 0)
            {
                return "-666";
            }

            result = WebDal.GetPhoneCenterAnsweredCall(shluha);
            if (!string.IsNullOrEmpty(result))
            {
                WebDal.DeletePhoneCenterForPhone(shluha);
            }
        }
        catch (Exception e)
        {

        }

        return result;
    }

    [WebMethod]
    public static string CallToThisPhone(string phone)
    {
        string status = "";
        try
        {
            status = "int shluha = GetWorkerShluha;";                        
            int shluha = GetWorkerShluha;
            
            if (shluha == 0)
            {
                return "-666";
            }

            string url = "http://199.203.227.131:6560/dialer/sodialer.php?sExt=" + shluha + "&sNumber=" + phone + "&ClientId=2137";

            status = "WebRequest request = WebRequest.Create(url);";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            status = "using (Stream dataStream = response.GetResponseStream())";
            using (Stream dataStream = response.GetResponseStream())
            {
                status = "StreamReader reader = new StreamReader(dataStream);";
                StreamReader reader = new StreamReader(dataStream);
                status = "string responseFromServer = reader.ReadToEnd();";
                string responseFromServer = reader.ReadToEnd();
            }

            response.Close();

        }
        catch (Exception e)
        {
            //throw e;
            Logger.ErrorLog("CallToThisPhone", e, "phone: " + phone + " Status: " + status);
        }

        return "";
    }

    [WebMethod]
    public static bool UpdateIp(string placeName, string ip)
    {
        WebDal.UpdatePlaceIp(placeName, ip);

        return true;
    }
}

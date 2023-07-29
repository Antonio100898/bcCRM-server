using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestWebMethod : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public static string GetCurrentTime(string name)
    {
        return "Hello " + name + Environment.NewLine + "The Current Time is: "
            + DateTime.Now.ToString();
    }

    [System.Web.Services.WebMethod()]
    public static string Test()
    {
        return "sohel rana";
    }

    [WebMethod]
    public static string AppendCallBackToCustomer(string phone)
    {
        if (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone))
        {
            return "";
        }

        long l = 0;
        if (!long.TryParse(phone, out l))
        {
            return "";
        }


        Worker w = (Worker)HttpContext.Current.Session["worker"];
        int phoneId = GetPhoneId(phone);

        Dictionary<string, List<PhonePlace>> phonePlaces = CacheHelper.Instance.phonePlaces;

        int placeId = 0;
        string placeName = "";
        string ip = "";
        string customerName = "";

        if (phonePlaces.ContainsKey(phone))
        {
            if (phonePlaces[phone].Count > 0)
            {
                int i = phonePlaces[phone].Count - 1;

                placeId = int.Parse(phonePlaces[phone][i].placeId.ToString());
                placeName = phonePlaces[phone][i].placeName;
                ip = phonePlaces[phone][i].ip;
                customerName = phonePlaces[phone][i].customerName;
            }
        }

        if (placeId == 0)
        {
            placeId = 5513; //לחזור ללקוח
        }

        long problemId = WebDal.AppendProblemIDs(phone);
        WebDal.UpdateTodayProblemsCountAddOne();
        WebDal.AppendProblem(problemTableType.problemsOpen, problemId, w.Id, phoneId, phone, ip, placeId, placeName, customerName, "נפתח על ידי " + w.workerName, "", 36, 0, 0, 9, false, CacheHelper.Instance.GetIsraelTime(), CacheHelper.Instance.GetIsraelTime());
        WebDal.AppendProblem(problemTableType.problemsLastForPhone, problemId, w.Id, phoneId, phone, ip, placeId, placeName, customerName, "נפתח על ידי " + w.workerName, "", 36, 0, 0, 9, false, CacheHelper.Instance.GetIsraelTime(), CacheHelper.Instance.GetIsraelTime());
        return "";
    }


    [WebMethod]
    public static string GetMissingIpPlace()
    {
        string result = WebDal.GetPlaceWithNoIp();
        return result;
    }

    [WebMethod]
    public static string UpdatePlaceID(int placeId, string ip)
    {
        WebDal.UpdatePlaceIp(placeId, ip);
        return "";
    }


    private static int GetPhoneId(string phone)
    {
        Dictionary<string, int> phonesDic = CacheHelper.Instance.phonesDic;

        int phoneId = 0;
        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrWhiteSpace(phone))
        {
            if (!phonesDic.ContainsKey(phone))
            {
                phoneId = WebDal.GetPhoneId(phone);
                if (phoneId == 0)
                {
                    phoneId = WebDal.AppendPhone(phone);
                }

                phonesDic.Add(phone, phoneId);
                if (!CacheHelper.Instance.phonesDic.ContainsKey(phone))
                {
                    CacheHelper.Instance.phonesDic.Add(phone, phoneId);
                }
            }
            else
            {
                phoneId = phonesDic[phone];
            }
        }

        return phoneId;
    }


    [WebMethod]
    public static string WorkerBreakTime(int inOrOut, int breakType)
    {
        Worker w = (Worker)HttpContext.Current.Session["worker"];
        if (w == null)
        {
            return "";
        }

        if (inOrOut == 0)
        {
            //מתחיל הפסקה
            if (!WebDal.isWorkerInAbreak(w.Id))
            {
                WebDal.AppendWorkerBreak(w.Id, breakType);
            }
        }
        else
        {
            //מסיים הפסקה
            if (WebDal.isWorkerInAbreak(w.Id))
            {
                WebDal.UpdateWorkerBreak(w.Id);

            }
        }

        //WebDal.UpdatePlaceIp(placeId, ip);
        return "";
    }

    [WebMethod]
    public static string CheckWorkerBreakMsgs()
    {
        string s = string.Empty;
        return s;
        DateTime now = CacheHelper.Instance.GetIsraelTime();
        DateTime startTime = GetLastTimeCheckedWorkersBreak();

        DataSet ds = WebDal.GetWorkersInBreakOrBack(startTime);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    s = ds.GetXml();
                }
            }
        }

        HttpContext.Current.Session["lastTimeChecked"] = now;
        return s;
    }

    private static DateTime GetLastTimeCheckedWorkersBreak()
    {
        DateTime result = CacheHelper.Instance.GetIsraelTime();

        object o = HttpContext.Current.Session["lastTimeChecked"];
        if (o != null)
        {
            if (!string.IsNullOrEmpty(o.ToString()))
            {
                DateTime.TryParse(o.ToString(), out result);
            }
        }

        return result;
    }

    [WebMethod]
    public static bool isWorkerOnBreakTime()
    {
        Worker w = (Worker)HttpContext.Current.Session["worker"];
        if (w == null)
        {
            return false;
        }

        return WebDal.isWorkerInAbreak(w.Id);
    }

    [WebMethod]
    public static string GetWorkerBreaks(int workerId, int breakStatus, string start, string finish)
    {
        //workerId:0,breakTypeId:0, start:"2019-08-14", finish:"2019-08-15"

        DateTime s;
        DateTime f;
        DateTime.TryParse(start, out s);
        DateTime.TryParse(finish, out f);


        DataSet ds = WebDal.GetWorkerBreaks(workerId, breakStatus, s, f);

        return ds.GetXml();
    }
}
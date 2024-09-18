using Newtonsoft.Json;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class CrmWS : WebService
{
    public CrmWS()
    {

    }

    [WebMethod]
    public string Proof()
    {
        return "Functional";
    }

    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =false)]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmLogin Login(string userName, string password)
    {
        Worker w = WebDal.GetWorker(userName, password);
        crmLogin result = new crmLogin();
        if (w == null)
        {
            result.msg = "שם משתמש או סיסמה לא נכונים";
            return result;
        }

        string key = w.token;
        if (string.IsNullOrEmpty(key))
        {
            key = WebDal.CreateWorkerNewGuid(w.Id);
        }

        result.success = true;
        result.key = key;

        result.workerId = w.Id;
        result.workerName = w.workerName;
        result.shluha = w.shluha;
        result.userType = w.userTypeId;
        result.department = w.departmentId;
        result.jobTitle = w.jobTitle;

        if (!string.IsNullOrEmpty(w.imgPath))
        {
            w.imgPath = w.imgPath.Replace('"', ' ').Trim();
        }
        result.imgPath = w.imgPath;
        result.workers = WebDal.GetWorkersBase();
        result.problemTypes = WebDal.GetProblemTypes();
        result.problems = WebDal.GetProblemsList("-1", w.Id);
        result.summery = WebDal.GetProblemsCountsummery(w.Id);

        return result;
    }

    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =false)]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmLogin loginAgain(string workerKey)
    {
        crmLogin result = new crmLogin();
        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "מפתח מזהה לא מוכר";
            return result;
        }

        Worker w = WebDal.GetWorkerByKey(workerKey);
        if (w == null)
        {
            result.msg = "מפתח מזהה לא מוכר";
            return result;
        }

        if (string.IsNullOrEmpty(w.token))
        {
            w.token = WebDal.CreateWorkerNewGuid(w.Id);
        }

        result.success = true;
        result.workerId = w.Id;
        result.key = w.token;
        result.workerName = w.workerName;
        result.shluha = w.shluha;
        result.userType = w.userTypeId;
        result.department = w.departmentId;
        result.jobTitle = w.jobTitle;

        if (!string.IsNullOrEmpty(w.imgPath))
        {
            w.imgPath = w.imgPath.Replace('"', ' ').Trim();
        }

        result.imgPath = w.imgPath;
        result.workers = WebDal.GetWorkersBase();
        result.problemTypes = WebDal.GetProblemTypes();
        result.problems = WebDal.GetProblemsList("-1", w.Id);
        result.summery = WebDal.GetProblemsCountsummery(w.Id);
        result.workerType = w.workerTypeID;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetProblems(string filter, string workerKey)
    {
        crmResponse result = new crmResponse();

        HttpContext ctx = HttpContext.Current;

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            result.logOut = true;
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            result.logOut = true;
            return result;
        }

        result.success = true;

        result.problems = WebDal.GetProblemsList(filter, workerId);

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetProblemSummery(string workerKey)
    {
        crmResponse result = new crmResponse();

        HttpContext ctx = HttpContext.Current;
        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            result.logOut = true;
            return result;
        }

        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            result.logOut = true;
            return result;
        }

        result.success = true;

        result.summery = WebDal.GetProblemsCountsummery(workerId);

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateProblem(Problem problem)
    {
        crmResponse result = new crmResponse();

        var worker = WebDal.GetWorkerByKey(problem.workerKey);
        int workerID = worker.Id;

        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        if (problem.id == 0)
        {
            problem.workerCreateId = workerID;
            problem.toWorker = workerID;
        }

        //problem.workerCreateId = workerID;
        //problem.toWorker = workerID;
        if (string.IsNullOrEmpty(problem.solution))
        {
            problem.solution = "";
        }

        if (string.IsNullOrEmpty(problem.desc))
        {
            problem.desc = "";
        }

        if (string.IsNullOrEmpty(problem.ip))
        {
            problem.ip = "";
        }

        if (problem.phoneId == 0)
        {
            problem.phoneId = WebDal.GetPhoneId(problem.phone);
            if (problem.phoneId == 0)
            {
                problem.phoneId = WebDal.AppendPhone(problem.phone);
            }
        }

        if (problem.placeId == 0)
        {
            problem.placeId = WebDal.GetPlaceId(problem.placeName);
            if (problem.placeId == 0)
            {
                problem.placeId = WebDal.AppendPlace(problem.placeName, "", "");
            }
        }

        if (problem.id == 0)
        {
            int problemId = WebDal.AppendProblemIDs(problem.phone);

            WebDal.UpdateTodayProblemsCountAddOne();
            WebDal.AppendProblemWS(problemId, problem.workerCreateId, problem.phoneId, problem.phone, problem.ip, problem.placeId, problem.placeName, problem.customerName,
                                    problem.desc, problem.solution, problem.toWorker, problem.statusId, problem.emergencyId, problem.departmentId, problem.takingCare, problem.isLocked);


            problem.id = problemId;
            result.problemId = problemId;
            result.workerId = workerID;

            WebDal.DeletePhoneCenterForPhone(worker.shluha, problem.phone);
        }
        else
        {
            
            CheckUpdateLog(problem, workerID);
            WebDal.UpdateProblemWS(problem.id, problem.ip, problem.placeName, problem.customerName, problem.desc, problem.solution, problem.toWorker, problem.statusId, problem.departmentId, problem.emergencyId, CacheHelper.Instance.GetIsraelTime(), workerID, problem.takingCare, problem.isLocked, problem.callCustomerBack);
            
            
        }

        //מעדכן את השרת של ניב
        int departmentId = WebDal.GetWorkerDepartment(workerID);
        if (departmentId == 9)
        {
            WebDal.UpdateNivServer(problem);
        }

        //מעדכן עובדים נוספים לתקלה
        if (problem.toWorkers != null)
        {
            WebDal.UpdateProblemWorkers(problem);
        }

        //מעדכן סוגי תקלה
        WebDal.UpdateProblemTypes(problem.id,problem.problemTypesList);
        

        //מעדכן קבצים אם יש
        if (problem.newFiles != null)
        {
            foreach (var item in problem.newFiles)
            {
                try
                {
                    if (problem.files == null)
                    {
                        problem.files = new List<string>();
                    }

                    string[] a = item.Split(',');
                    if (a.Length > 1)
                    {
                        string fileType = WebDal.GetFileType(a[0]);

                        //item.Replace("data:image/jpeg;base64,", "")
                        byte[] workerImage = Convert.FromBase64String(a[1]);
                        //data:application/x-msdownload;base64

                        string filename = Guid.NewGuid().ToString().Replace("-", "") + fileType;// ".jpg";// + a[a.Length - 1];

                        string path = Server.MapPath("~/") + "Pics\\problems\\" + filename;
                        File.WriteAllBytes(path, workerImage);

                        WebDal.AppendProblemFile(problem.id, filename, "");
                        problem.files.Add(filename);
                    }
                }
                catch (Exception e)
                {
                    WebDal.AppendErrorLog("CrmWS.UpdateProblem", e.Message, "");
                }
            }
        }

        if (problem.crmFiles != null)
        {
            foreach (var item in problem.crmFiles)
            {
                try
                {
                    if (problem.files == null)
                    {
                        problem.files = new List<string>();
                    }

                    //item.Replace("data:image/jpeg;base64,", "")
                    string base64 = item.content.Substring(item.content.IndexOf(",") + 1);
                    byte[] workerImage = Convert.FromBase64String(base64);
                    //data:application/x-msdownload;base64

                    string filename = item.filename.Replace("-", "_").Replace(" ", "_");

                    string path = Server.MapPath("~/") + "Pics\\problems\\" + filename;
                    File.WriteAllBytes(path, workerImage);

                    WebDal.AppendProblemFile(problem.id, filename, "");
                    problem.files.Add(filename);

                }
                catch (Exception e)
                {
                    WebDal.AppendErrorLog("CrmWS.UpdateProblem", e.Message, "");
                }
            }
        }

        result.filesName = problem.files;
        result.problemId = problem.id;
        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateMsgLine(string workerKey, int msgId, string updatedMsg)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }
        try 
        { 
            WebDal.UpdateMsgLine(msgId, updatedMsg);
            result.success = true;
        } 
        catch (Exception e)
        {
            result.msg = e.Message;
        }
       
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse DeleteMsgLine(string workerKey, int msgId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }
        try
        {
            WebDal.DeleteMsgLine(msgId);
            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message;
        }

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UploadProblemFiles(Problem problem)
    {
        crmResponse result = new crmResponse();

        if (problem.crmFiles != null)
        {
            foreach (var item in problem.crmFiles)
            {
                try
                {
                    if (problem.files == null)
                    {
                        problem.files = new List<string>();
                    }

                    //item.Replace("data:image/jpeg;base64,", "")
                    string base64 = item.content.Substring(item.content.IndexOf(",") + 1);
                    byte[] workerImage = Convert.FromBase64String(base64);
                    //data:application/x-msdownload;base64

                    string filename = item.filename.Replace("-", "_").Replace(" ", "_");

                    string path = Server.MapPath("~/") + "Pics\\problems\\" + filename;
                    File.WriteAllBytes(path, workerImage);

                    WebDal.AppendProblemFile(problem.id, filename, "");
                    problem.files.Add(filename);

                }
                catch (Exception e)
                {
                    WebDal.AppendErrorLog("CrmWS.UpdateProblem", e.Message, "");
                }
            }
        }

        result.filesName = problem.files;
        result.success = true;
        return result;
    }


        private static void CheckUpdateLog(Problem problem, int workerId)
    {
        try
        {
            DataSet ds = WebDal.GetProblemDS(problem.id);
            DataTable dt = ds.Tables[0];
            DataRow item = dt.Rows[0];

            string groupKey = Guid.NewGuid().ToString();

            AppendLog(groupKey, problem.id, workerId, "ip", item["ip"].ToString(), problem.ip);
            AppendLog(groupKey, problem.id, workerId, "placeName", item["placeName"].ToString(), problem.placeName);
            AppendLog(groupKey, problem.id, workerId, "customerName", item["customerName"].ToString(), problem.customerName);
            AppendLog(groupKey, problem.id, workerId, "problemDesc", item["problemDesc"].ToString(), problem.desc);
            AppendLog(groupKey, problem.id, workerId, "problemSolution", item["problemSolution"].ToString(), problem.solution);
            AppendLog(groupKey, problem.id, workerId, "toWorker", item["toWorker"].ToString(), problem.toWorker);
            AppendLog(groupKey, problem.id, workerId, "statusId", item["statusId"].ToString(), problem.statusId);
            AppendLog(groupKey, problem.id, workerId, "emergencyId", item["emergencyId"].ToString(), problem.emergencyId);
            AppendLog(groupKey, problem.id, workerId, "departmentId", item["departmentId"].ToString(), problem.departmentId);
            AppendLog(groupKey, problem.id, workerId, "takingCare", item["takingCare"].ToString(), problem.takingCare);
            AppendLog(groupKey, problem.id, workerId, "isLocked", item["isLocked"].ToString(), problem.isLocked);
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

            if (fieldName == "toWorker")
            {
                string msg = "תקלה עברה לטיפולך";
                WebDal.AppendProblemNotafication(problemId, workerId, msg);
            }

            if (fieldName == "problemDesc")
            {
                string msg = "ערכו את תוכן התקלה";
                WebDal.AppendProblemNotafication(problemId, workerId, msg);
            }

            if (fieldName == "placeName")
            {
                string msg = "שם המקום לתקלה שונה מ" + oldValue + " ל" + newValue;
                WebDal.AppendProblemNotafication(problemId, workerId, msg);
            }
        }
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetPlacesForPhone(string phone)
    {
        crmResponse result = new crmResponse();
        if (string.IsNullOrWhiteSpace(phone))
        {
            result.msg = "לא נמצא טלפון " + phone;
            return result;
        }

        if (phone.Length < 5)
        {
            result.places = new List<PhonePlace>();
            result.success = true;
        }

        result.places = WebDal.GetPhonePlaceSearch(phone);
        //GetPhonePlace
        result.success = true;
        return result;
    }

    [WebMethod]
    public crmResponse UploadFiles()
    {
        //Fetch the File.
        HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

        //Fetch the File Name.
        string fileName = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

        //MemoryStream ms = new MemoryStream(byteArrayIn);
        //Image returnImage = Image.FromStream(ms);
        //returnImage.Save("");
        return null;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetProblemHistorySummery(string workerKey, int placeId, int problemId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        string lastSuppoter;
        int trackingId;
        result.msg = WebDal.GetProblemHistorySummeryAndTrackingStatus(problemId, workerID, placeId, out lastSuppoter, out trackingId);
        result.lastSuppoter = lastSuppoter;
        result.trackingId = trackingId;

        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetProblemLogs(string workerKey, int problemId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        result.logs = WebDal.GetProblemLogs(problemId);

        result.success = true;
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateLastTimeSeenChat(string workerKey, int problemId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        //WebDal.Update
        WebDal.UpdateOrAppendProblemChatSeenByWorker(workerID, problemId);

        result.success = true;
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateProblemTracking(string workerKey, int problemId, int trackingId)
    {
        crmResponse result = new crmResponse();

        int workerId = WebDal.GetWorkerId(workerKey);
        if (workerId == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }


        if (trackingId > 0)
        {
            WebDal.DeleteProblemTracking(workerId, problemId);
            trackingId = 0;
        }
        else
        {
            trackingId = WebDal.AppendProblemTracking(workerId, problemId);
        }

        result.trackingId = trackingId;
        result.success = true;
        return result;
    }



    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkers(string workerKey)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה, אנא צא מהמערכת והיכנס שנית.";
            result.logOut = true;
            return result;
        }

        result.workers = WebDal.GetWorkers();
        result.workers.Add(new Worker { Id = 0, firstName = "כולם" });
        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorker(string workerKey)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        Worker w = new Worker();

        DataSet ds = WebDal.GetWorkerDS(workerID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow item = ds.Tables[0].Rows[0];

            w = new Worker();
            w.Id = int.Parse(item["id"].ToString());
            w.firstName = item["firstName"].ToString();
            w.lastName = item["lastName"].ToString();
            w.phone = item["phone"].ToString();
            w.userName = item["userName"].ToString();
            w.password = item["password"].ToString();
            w.userTypeId = int.Parse(item["userTypeId"].ToString());
            w.active = bool.Parse(item["active"].ToString());
            w.imgPath = item["imgPath"].ToString();
            w.shluha = int.Parse(item["shluha"].ToString());
            w.carType = item["carType"].ToString();
            w.carNumber = item["carNumber"].ToString();
            w.departmentId = int.Parse(item["wDepartmentId"].ToString());
            w.jobTitle = item["jobTitle"].ToString();
            w.teudatZehut = item["teudatZehut"].ToString();

        }

        result.worker = w;

        result.success = true;
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkerDepartments(string workerKey, int workerId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        result.workerDepartments = WebDal.GetWorkerDepartments(workerId);

        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkerExpensesValue(string workerKey, int workerId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        result.workExpensesTypes = WebDal.GetWorkerExpensesValue(workerId);
        result.workerExpenses = WebDal.GetWorkerExpensesValuePosibleForWorker(workerId);

        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetExpensesAndShiftForMonth(string workerKey, int year, int month, int departmentId, int workerId)
    {
        crmResponse result = new crmResponse();

        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        result.ExpenseAndShiftsWeeks = WebDal.GetWorkerExpensesAndShiftForMonth(year, month, workerId, departmentId);

        result.success = true;
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse AppendWorkerExpensesValue(string workerKey, int workerId, int workExpensesType, double sum)
    {
        crmResponse result = new crmResponse();

        int iworkerID = WebDal.GetWorkerId(workerKey);
        if (iworkerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        WebDal.AppendWorkerExpensesValue(workerId, workExpensesType, sum);


        result.success = true;
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorker(string workerKey, Worker worker, List<Department> departments = null, List<WorkExpensesType> workerExpensesValue = null)
    {
        crmResponse result = new crmResponse();
        //HttpContext ctx = HttpContext.Current;
        int workerID = WebDal.GetWorkerId(workerKey);
        if (workerID == 0)
        {
            result.msg = "עובד לא מזוהה, אין גישה";
            return result;
        }

        if (worker.Id == 0)
        {
            worker.Id = WebDal.AppendWorker(worker);
        }
        else
        {
            WebDal.UpdateWorkerInfo(worker);

            if (departments != null)
            {
                WebDal.UpdateWorkerDepartments(worker.Id, departments);
            }

            if (workerExpensesValue != null)
            {
                WebDal.UpdateWorkerExpensesValue(workerExpensesValue);
            }

        }

        if (!string.IsNullOrEmpty(worker.imgContent))
        {
            try
            {
                string[] a = worker.imgContent.Split(',');
                if (a.Length > 1)
                {
                    string fileType = WebDal.GetFileType(a[0]);

                    //string[] a = worker.imgContentName.Split('.');
                    byte[] workerImage = Convert.FromBase64String(a[1]);
                    string filename = Guid.NewGuid().ToString().Replace("-", "") + fileType;

                    string path = Server.MapPath("~/") + "Pics\\workers\\" + filename;
                    File.WriteAllBytes(path, workerImage);

                    WebDal.UpdateWorkerImagePath(worker.Id, filename);
                    result.msg = "https://beecomm.azurewebsites.net/Pics/workers/" + filename;
                }
            }
            catch (Exception e)
            {
                WebDal.AppendErrorLog("CrmWS.UpdateWorker", e.Message, "");
            }

        }


        result.success = true;
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse SearchProblems(SearchProblem search)
    {
        crmResponse result = new crmResponse();

        HttpContext ctx = HttpContext.Current;

        if (string.IsNullOrEmpty(search.key))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(search.key);
        if (workerId == 0)
        {
            result.msg = "לא מצליח לזהות את המשתמש שלך";
            return result;
        }

        if (search.daysBack == 0)
        {
            search.daysBack = 3;
        }

        result.problems = WebDal.GetProblemsSearch(search);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse AnsweredCall(string workerKey, int department)
    {
        crmResponse result = new crmResponse();

        HttpContext ctx = HttpContext.Current;

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "עובד לא מזוהה, אנא צא מהמערכת והיכנס שנית";
            return result;
        }

        int shluha = WebDal.GetWorkerShluha(workerId);
        if (shluha == 0)
        {
            result.msg = "לעובד לא מוגדר מספר שלוחה";
            return result;
        }

        string phone = string.Empty;
        if (shluha > 0)
        {
            phone = WebDal.GetPhoneCenterAnsweredCall(shluha);
            if (!string.IsNullOrEmpty(phone))
            {
                result.success = true;
            }
            else
            {
                result.msg = "לא נמצא מספר טלפון למספר שלוחה " + shluha;
            }
        }

        //WebDal.AppendErrorLog("AnsweredCall", "workerKey: " + workerKey, "workerId: " + workerId + " shluha: " + shluha);

        result.phone = phone;

        //result.success = true;
        //result.phone = "0523379468";

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public static crmResponse CallToThisPhone(string workerKey, string phone)
    {
        crmResponse result = new crmResponse();

        try
        {
            HttpContext ctx = HttpContext.Current;

            if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


            int workerId = WebDal.GetWorker(workerKey);
            if (workerId == 0)
            {
                result.msg = "עובד לא מזוהה, אנא צא מהמערכת והיכנס שנית";
                return result;
            }

            int shluha = WebDal.GetWorkerShluha(workerId);
            if (shluha == 0)
            {
                result.msg = "לעובד לא מוגדר מספר שלוחה";
                return result;
            }


            string url = "http://199.203.227.131:6560/dialer/sodialer.php?sExt=" + shluha + "&sNumber=" + phone + "&ClientId=2137";


            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            WebResponse response = request.GetResponse();


            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
            }

            response.Close();
            result.success = true;
        }
        catch (Exception e)
        {
            result.success = false;
            result.msg = e.Message;
            //throw e;
            Logger.ErrorLog("CallToThisPhone", e, "phone: " + phone);
        }


        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse CallThisNumber(string workerKey, string phone)
    {
        crmResponse result = new crmResponse();

        string status = "";
        try
        {
            status = "int shluha = GetWorkerShluha;";


            if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


            int workerId = WebDal.GetWorker(workerKey);
            if (workerId == 0)
            {
                result.msg = "מפתח משתמש לא מזוהה";
                return result;
            }

            int shluha = WebDal.GetWorkerShluha(workerId);

            if (shluha == 0)
            {
                result.msg = "לעובד לא מוגדר מספר שלוחה";
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
            Logger.ErrorLog("CallThisNumber", e, "workerKey: " + workerKey + " phone: " + phone);
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse DeleteFile(string workerKey, int problemId, string fileName)
    {
        crmResponse result = new crmResponse();

        try
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


            int workerId = WebDal.GetWorker(workerKey);
            if (workerId == 0)
            {
                result.msg = "מפתח משתמש לא מזוהה";
                return result;
            }

            string path = Server.MapPath("~/") + "Pics\\problems\\" + fileName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            WebDal.DeleteProblemFile(problemId, fileName);
            result.success = true;
        }
        catch (Exception e)
        {
            //throw e;
            Logger.ErrorLog("DeleteFile", e, "workerKey: " + workerKey + " fileName: " + fileName);
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdatePhonePlace(string workerKey, int placeId, string phone, string placeName, string cusName, bool vip, string remark)
    {
        crmResponse result = new crmResponse();
        bool updatePlace = (placeId > 0);
        try
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


            int workerId = WebDal.GetWorker(workerKey);
            if (workerId == 0)
            {
                result.msg = "מפתח משתמש לא מזוהה";
                return result;
            }


            int phoneId = 0;
            if (!string.IsNullOrEmpty(phone))
            {
                phoneId = WebDal.GetPhoneId(phone);
                if (phoneId == 0)
                {
                    phoneId = WebDal.AppendPhone(phone);
                }
            }

            if (placeId == 0)
            {
                placeId = WebDal.GetPlaceId(placeName);
                if (placeId == 0)
                {
                    placeId = WebDal.AppendPlace(placeName, "", remark, vip);
                }
            }


            if (phoneId > 0 && placeId > 0)
            {
                if (updatePlace)
                {
                    WebDal.UpdatePlace(placeId, placeName, vip, remark);
                    WebDal.UpdatePhonePlace(placeId, phoneId, cusName);
                }
                else
                {
                    WebDal.AppendPhonePlace(phoneId, placeId, cusName);
                }

            }
           
            result.success = true;
        }
        catch (Exception e)
        {
            //throw e;
            Logger.ErrorLog("AppendPhonePlace", e, "workerKey: " + workerKey + " phone: " + phone + " placeName:" + placeName);
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse AddNewChatLine(string workerKey, int problemId, string newLine, int lineType)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.AppendProblemMsg(problemId, workerId, newLine, lineType);

        result.msgLines = WebDal.GetProblemMsgs(problemId);
        WebDal.UpdateProblemSolution(problemId, newLine);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetChatLines(string workerKey, int problemId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
            {
                result.msg = "מפתח משתמש לא מזוהה";
                return result;
            }

        result.msgLines = WebDal.GetProblemMsgs(problemId);
        result.success = true;

        WebDal.UpdateOrAppendProblemChatSeenByWorker(workerId, problemId);

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetNotificationsCount(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.notificationsCount = WebDal.GetNotificationsCount(workerId);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetNotifications(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.notifications = WebDal.GetNotifications(workerId);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse DeleteNotification(string workerKey, int notificationId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.DeleteNotifications(notificationId);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse DeleteNotificationAll(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.DeleteNotificationsAll(workerId);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateNotificationHadSeen(string workerKey, int notificationId, bool hasSeen)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.UpdateNotificationHadSeen(notificationId, hasSeen);
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateNotificationAllHadSeen(string workerKey, bool hasSeen)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.UpdateNotificationHadSeen(workerId, hasSeen);
        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkersCars(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.workers = WebDal.GetWorkersCars();
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse AppendWorkerExpence(string workerKey, DateTime startExpenceDate, DateTime finishExpenceDate,
                                int expenseType, double sum, double expenseTypeUnitValue, bool freePass, string remark)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        startExpenceDate = TimeZoneInfo.ConvertTime(startExpenceDate, remoteTimeZone);
        finishExpenceDate = TimeZoneInfo.ConvertTime(finishExpenceDate, remoteTimeZone);

        WebDal.AppendWorkerExpense(workerId, startExpenceDate, finishExpenceDate, expenseType, sum, expenseTypeUnitValue, freePass, remark);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorkerExpence(string workerKey, WorkerExpenses expense)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }



        WebDal.UpdateWorkerExpense(expense);

        result.success = true;

        return result;
    }



    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkerExpenses(string workerKey, int filterWorkerId, int year, int month)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (filterWorkerId == 0)
        {
            filterWorkerId = workerId;
        }


        result.workerExpenses = WebDal.GetWorkerExpenses(filterWorkerId, year, month);

        double d = 0;

        foreach (var item in result.workerExpenses)
        {
            d += item.expenseValue;
        }

        result.workExpensesSum = d;
        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkExpensesTypes(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        result.workExpensesTypes = WebDal.GetWorkExpensesTypes();

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorkExpensesTypes(string workerKey, List<WorkExpensesType> expensesType)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        WebDal.UpdateWorkExpensesTypes(expensesType);

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkExpensesTypesForWorker(string workerKey, int filterWorkerId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (filterWorkerId == 0)
        {
            filterWorkerId = workerId;
        }


        result.workExpensesTypes = WebDal.GetWorkExpensesTypesForWorker(filterWorkerId);
        double d = 0;
        foreach (var item in result.workExpensesTypes)
        {
            d += item.defValue;
        }

        result.workExpensesSum = d;

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkersExpenses(string workerKey, int year, string months, string filterWorkerId = "0")
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        int intMonth = GetMonths(months);

        int fWorkerId = 0;
        int.TryParse(filterWorkerId, out fWorkerId);

        result.workerExpenses = WebDal.GetWorkersExpenses(year, intMonth, fWorkerId);
        result.workerExpensesSum = WebDal.GetWorkersExpensesSum(year, intMonth);

        double d = 0;

        foreach (var item in result.workerExpenses)
        {
            d += item.expenseValue;
        }

        result.workExpensesSum = d;

        result.success = true;

        return result;
    }

    private int GetMonths(string month)
    {
        List<int> result = new List<int>();

        switch (month)
        {
            case "1":
                result.Add(1);
                break;
            case "2":
                result.Add(2);
                break;
            case "3":
                result.Add(3);
                break;
            case "4":
                result.Add(4);
                break;
            case "5":
                result.Add(5);
                break;
            case "6":
                result.Add(6);
                break;
            case "7":
                result.Add(7);
                break;
            case "8":
                result.Add(8);
                break;
            case "9":
                result.Add(9);
                break;
            case "10":
                result.Add(10);
                break;
            case "11":
                result.Add(11);
                break;
            case "12":
                result.Add(12);
                break;
            default:
                break;

        }

        return result[0];
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse CancelWorkerExpenses(string workerKey, int expenseId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        WebDal.CancelWorkerExpense(expenseId);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorkesExpensesApprove(string workerKey, List<WorkerExpenses> workerExpenses)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        WebDal.UpdateWorkesExpensesApprove(workerExpenses);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetStats(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.stats = WebDal.GetWorkersStats();

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetStatsWorkersHours(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        //result.statsWorkersHours = WebDal.GetWorkersHoursSum();

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftsForWorker(string workerKey, DateTime startTime)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);

        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            startTime = TimeZoneInfo.ConvertTime(startTime, remoteTimeZone);
            startTime = startTime.StartOfWeek();

            result.workerShifts = WebDal.GetShiftsDetailsForWorker(workerId, startTime);


            result.success = true;
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("GetShiftsForWorker", e.Message, "startTime: " + startTime );
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftDetails(string workerKey, DateTime startTime, int shiftGroupID)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        string status = "";
        try
        {
            var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            startTime = TimeZoneInfo.ConvertTime(startTime, remoteTimeZone);
            startTime = startTime.StartOfWeek();

            result.shiftDetails = WebDal.GetShiftsDetails(startTime, shiftGroupID);
            result.shiftsDays = WebDal.GetShiftDaysRemarks(startTime, shiftGroupID);

            result.success = true;
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("GetShiftDetails", e.Message, "startTime: " + startTime + "  status: " + status);
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateShiftDayRemark(string workerKey, dayInfo day, int shiftGroupID)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (day.id == 0)
        {
            int newId = WebDal.AppendShiftDaysRemarks(day.dayValue, day.remark, shiftGroupID);
            result.problemId = newId;
        }
        else
        {
            WebDal.UpdateShiftDaysRemarks(day.id, day.remark);
            result.problemId = day.id;
        }


        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftPlansForWorker(string workerKey, DateTime startTime)
    {
       
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            startTime = TimeZoneInfo.ConvertTime(startTime, remoteTimeZone);
            startTime = startTime.StartOfWeek();

            result.shiftDetails = WebDal.GetShiftPlans(startTime, workerId);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftPlans(string workerKey, DateTime startTime)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
            startTime = TimeZoneInfo.ConvertTime(startTime, remoteTimeZone);
            startTime = startTime.StartOfWeek();

            result.shiftDetails = WebDal.GetShiftPlans(startTime, 0);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftPlansDetailsForWorker(string workerKey, DateTime startTime)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            result.shiftPlanDetails = WebDal.GetShiftPlansDetails(startTime, workerId, workerId);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftPlansDetails(string workerKey, DateTime startTime, int addDays, int shiftTypeId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            result.shiftPlanDetails = WebDal.GetShiftPlansDetails(startTime, 0, addDays, shiftTypeId);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftPlansWeekReport(string workerKey, DateTime startTime)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            result.shiftPlanReport = WebDal.GetShiftPlansWeekReport(startTime);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message + " startTime: " + startTime;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse CancelShiftPlan(string workerKey, int shiftPlanId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.CancelShiftPlan(shiftPlanId);


        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateShiftPlan(string workerKey, List<ShiftPlan> shiftPlans)
    {
        crmResponse result = new crmResponse();
        try
        {
            if (string.IsNullOrEmpty(workerKey))
            {
                result.msg = "חסר פרטי משתמש";
                return result;
            }


            int workerId = WebDal.GetWorker(workerKey);
            if (workerId == 0)
            {
                result.msg = "מפתח משתמש לא מזוהה";
                return result;
            }

            foreach (ShiftPlan shiftPlan in shiftPlans)
            {
                shiftPlan.workerId = workerId;
                if (shiftPlan.id == 0)
                {
                    WebDal.AppendShiftPlan(shiftPlan);
                }
                else
                {
                    WebDal.UpdateShiftPlan(shiftPlan);
                }
            }
        }
        catch (Exception ex) {
            result.msg = ex.Message;
            return result;
        }
       
        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateShiftDetails(string workerKey, ShiftDetail shiftDetail, int shiftGroupId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        //var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        //startTime = TimeZoneInfo.ConvertTime(startTime, remoteTimeZone);
        shiftDetail.shiftGroupId = shiftGroupId;
        if (shiftDetail.id == 0)
        {
            WebDal.AppendShift(shiftDetail);
        }
        else
        {
            WebDal.UpdateShift(shiftDetail);
        }

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse CancelShift(string workerKey, int shiftId)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        WebDal.CancelShift(shiftId);


        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateShiftStartDate(string workerKey, int shiftId, DateTime newDate)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        ShiftDetail s = WebDal.GetShift(shiftId);

        DateTime d = new DateTime(newDate.Year, newDate.Month, newDate.Day, s.startDate.Hour, s.startDate.Minute, s.startDate.Second);

        WebDal.UpdateShiftStartDate(shiftId, d);


        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetShiftWorkerPreferencias(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.shiftWorkerPreferencias = WebDal.GetShiftWorkerPreferencias(workerId);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkersMissingShiftsPlan(string workerKey, DateTime start)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.workerMissingShifts = WebDal.GetWorkerMissingShiftPlans(start);
       
        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse AppendDefultWeekShifts(string workerKey, DateTime startTime, int shiftGroupId)
    {

        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            result.logOut = true;
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            result.logOut = true;

            return result;
        }

        if (startTime.DayOfWeek != DayOfWeek.Sunday) { startTime = startTime.StartOfWeek(DayOfWeek.Sunday); }

        WebDal.AppendDefultWeekShifts(startTime, shiftGroupId);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorkerSickday(string workerKey, WorkerSickDay sickday)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (sickday.id == 0)
        {
            string path = Server.MapPath("~/") + "Pics\\sickDays\\";
            //File.WriteAllBytes(path, workerImage);
            sickday.workerId = workerId;
            WebDal.AppendWorkerSickday(sickday, path);
        }
        else
        {
            if (sickday.cancel)
            {
                WebDal.DeleteWorkerSickday(sickday.id);
            }
            else
            {
                WebDal.UpdateWorkerSickday(sickday);
            }

        }

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkersSickdays(string workerKey, int year, int month, bool justMe)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.workerSickDay = WebDal.GetWorkersSickDays(year, month, justMe ? workerId : 0);

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateWorkerFreeday(string workerKey, WorkerFreeDay freeday)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (freeday.id == 0)
        {
            freeday.workerId = workerId;
            WebDal.AppendWorkerFreeday(freeday);
        }
        else
        {
            if (freeday.cancel)
            {
                WebDal.DeleteWorkerFreeday(freeday.id);
            }
            else
            {
                WebDal.UpdateWorkerFreeday(freeday);
            }

        }

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetWorkersFreedays(string workerKey, int year, int month, bool justMe)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.workerFreeDay = WebDal.GetWorkersFreeDays(year, month, justMe ? workerId : 0);

        result.success = true;

        return result;
    }



    ///////////////////////////////////////////////////////

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetV3Groups(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.v3Groups = biDbDal.GetGroups();
        result.v3Cities = biDbDal.GetCities();

        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetV3Branches(string workerKey, string database)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.v3Branches = biDbDal.GetBranches(database);

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetV3Cities(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.v3Cities = biDbDal.GetCities();

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetHardware(string workerKey, string barcode)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        result.hardwares = WebDal.GetHardware(barcode);


        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetHardwaresCount(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        result.hardwaresCount = WebDal.GetHardwaresCount();

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

    public crmResponse UpdateHardware(string workerKey, Hardware hardware)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        if (hardware.id == 0)
        {
            WebDal.AppendHardware(hardware);
        }
        else
        {
            WebDal.UpdateHardware(hardware);
        }


        result.success = true;

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

    public crmResponse UpdateHardwareTracking(string workerKey, int hardwareId, int statusId, string cusName, string remark)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }


        WebDal.AppendHardwareTracking(hardwareId, statusId, cusName, remark);
        WebDal.UpdateHardwareStatus(hardwareId, statusId, cusName);


        result.success = true;

        return result;
    }


    ///////////////////////////////////////////////////


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdatePlaceBizNumber(string workerKey, int id, string placeName, string bizNumber, int warrantyType)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        //int workerId = WebDal.GetWorker(workerKey);
        //if (workerId == 0)
        //{
        //    result.msg = "מפתח משתמש לא מזוהה";
        //    return result;
        //}

        if (id == 0)
        {
            WebDal.AppendPlaceBizNumber(placeName, bizNumber, warrantyType);
        }
        else
        {
            WebDal.UpdatePlaceBizNumber(id, placeName, bizNumber, warrantyType);
        }

        result.success = true;

        return result;
    }


    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetPlacesBizNumber(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        //int workerId = WebDal.GetWorker(workerKey);
        //if (workerId == 0)
        //{
        //    result.msg = "מפתח משתמש לא מזוהה";
        //    return result;
        //}

        result.places = WebDal.GetPlaceBizNumber();

        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse GetOuterCompanies(string workerKey)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            result.outerCompanies = WebDal.GetOuterCompanies();

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message;
        }
        return result;
    }

    [WebMethod]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    public crmResponse UpdateShiftOuterCompanies(string workerKey, int shiftId, List<OuterCompany> outerCompanies)
    {
        crmResponse result = new crmResponse();

        if (string.IsNullOrEmpty(workerKey))
        {
            result.msg = "חסר פרטי משתמש";
            return result;
        }


        int workerId = WebDal.GetWorker(workerKey);
        if (workerId == 0)
        {
            result.msg = "מפתח משתמש לא מזוהה";
            return result;
        }

        try
        {
            WebDal.UpdateShiftOuterCompanies(shiftId, outerCompanies);

            result.success = true;
        }
        catch (Exception e)
        {
            result.msg = e.Message;
        }
        return result;
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}

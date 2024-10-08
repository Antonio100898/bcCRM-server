using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

public static class WebDal
{
    public static int AppendProblemIDs(string phone)
    {
        int problemId = 0;
        string sql = "INSERT INTO [dbo].[problemsIDs] ([phone]) VALUES (@phone); " +
                     "SELECT SCOPE_IDENTITY()";

        object o = Dal.ExecuteScalar(sql, new List<SqlParameter> { new SqlParameter("@phone", phone) });
        if (o != null)
        {
            problemId = int.Parse(o.ToString());
        }

        return problemId;
    }

    public static void AppendQuestion(int workerId, string question)
    {
        string sql = "INSERT INTO [dbo].[questions]([workerId],[question]) VALUES(@workerId,@question);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@question", question));
        Dal.ExecuteNonQuery(sql, values);
    }

    internal static string CreateWorkerNewGuid(int id)
    {
        string result = Guid.NewGuid().ToString();

        string sql = "UPDATE [dbo].[workers] SET [guidKey] = @guidKey, [keyCreate]= @keyCreate " +
                     "WHERE (id = @id)";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@guidKey", result));
        values.Add(new SqlParameter("@keyCreate", DateTime.Now));

        Dal.ExecuteNonQuery(sql, values);

        return result;
    }

    internal static int GetWorker(string key)

    {
        string sql = "SELECT [id] " +
                     "FROM workers " +
                     "WHERE [guidKey]=@guidKey;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@guidKey", key));

        object o = Dal.GetThisOneValue(sql, values);
        int result = 0;
        if (o != null)
        {
            int.TryParse(o.ToString(), out result);
        }

        return result;
    }

    public static int GetWorkerDepartment(int workerId)

    {
        string sql = "SELECT [wDepartmentId] " +
                     "FROM workers " +
                     "WHERE [id]=@workerId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));

        object o = Dal.GetThisOneValue(sql, values);
        int result = 0;
        if (o != null)
        {
            int.TryParse(o.ToString(), out result);
        }

        return result;
    }

    internal static void AppendErrorLog(string voidName, string errMsg, string extraInfo)
    {
        string sql = "INSERT INTO [dbo].[errLogs]([voidName],[errMsg],[extraInfo]) VALUES(@voidName,@errMsg,@extraInfo);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@voidName", voidName));
        values.Add(new SqlParameter("@errMsg", errMsg));
        values.Add(new SqlParameter("@extraInfo", extraInfo));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static List<ProblemType> GetProblemTypes()
    {
        List<ProblemType> result = new List<ProblemType>();

        string sql = "SELECT [id],[problemTypeName],[color] " +
                     "FROM [dbo].[problemTypes] " +
                     "ORDER BY problemTypeName";

        DataSet ds = Dal.GetDataSet(sql);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        ProblemType p = new ProblemType();

                        p.id =int.Parse(item["id"].ToString());
                        p.problemTypeName = item["problemTypeName"].ToString();
                        p.color = item["color"].ToString();
                        result.Add(p);
                    }
                }
            }
        }

        return result;
    }

    public static void AppendProblemWS(long probID, int workerId, int phoneId, string phone, string ip, int placeNameId, string placeName, string customerName,
        string problemDesc, string problemSolution, int toWorker, int statusId, int emergencyId, int departmentId, bool takingCare, bool isLocked)
    {
        string sql = "INSERT INTO [dbo].[problemsClose] ([id],[workerId],[phoneId],[phone],[ip],[placeNameId],[placeName],[customerName],[problemDesc],[problemSolution],[toWorker],[statusId],[emergencyId],[departmentId],[startTime],[finishTime], [updaterWorkerId], [takingCare], [isLocked]) " +
                 "VALUES (@id,@workerId, @phoneId, @phone, @ip, @placeNameId, @placeName, @customerName, @problemDesc, @problemSolution, @toWorker, @statusId, @emergencyId, @departmentId, @startTime, @finishTime, @updaterWorkerId, @takingCare, @isLocked) " +
                 "SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();

        values.Add(new SqlParameter("@id", probID));
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@phoneId", phoneId));
        values.Add(new SqlParameter("@phone", phone));
        values.Add(new SqlParameter("@ip", ip));
        values.Add(new SqlParameter("@placeNameId", placeNameId));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@customerName", customerName));
        values.Add(new SqlParameter("@problemDesc", problemDesc));
        values.Add(new SqlParameter("@problemSolution", problemSolution));
        values.Add(new SqlParameter("@statusId", statusId));
        values.Add(new SqlParameter("@toWorker", toWorker));
        values.Add(new SqlParameter("@emergencyId", emergencyId));
        values.Add(new SqlParameter("@departmentId", departmentId));
        values.Add(new SqlParameter("@startTime", CacheHelper.Instance.GetIsraelTime()));
        values.Add(new SqlParameter("@finishTime", CacheHelper.Instance.GetIsraelTime()));
        values.Add(new SqlParameter("@updaterWorkerId", workerId));
        values.Add(new SqlParameter("@takingCare", takingCare));
        values.Add(new SqlParameter("@isLocked", isLocked));



        Dal.ExecuteNonQuery(sql, values);
    }

    internal static int GetWorkerId(string workerKey)
    {
        int result = 0;
        if (workerKey == null)
        {
            return result;
        }

        string sql = "SELECT [id] " +
                     "FROM workers " +
                     "WHERE [guidKey]=@workerKey;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerKey", workerKey));

        object o = Dal.ExecuteScalar(sql, values);
        if (o != null)
        {
            if (!string.IsNullOrEmpty(o.ToString()))
            {
                int.TryParse(o.ToString(), out result);
            }
        }

        return result;
    }

    internal static void UpdateProblemWorkers(Problem problem)
    {

        Dal.ExecuteNonQuery("DELETE FROM problemWorkers where problemId=" + problem.id);

        if (problem.toWorkers == null)
        {
            return;
        }

        if (problem.toWorkers.Count == 0)
        {
            return;
        }

        string sql = "";
        foreach (var item in problem.toWorkers)
        {
            if (item != 0)
            {
                sql += "(" + problem.id + "," + item + "),";
            }
        }

        if (sql.Length > 0)
        {
            sql = sql.Substring(0, sql.Length - 1);
            sql = "INSERT INTO [dbo].[problemWorkers] ([problemId], [workerId]) VALUES" + sql;

            Dal.ExecuteNonQuery(sql);
        }


    }

    internal static void UpdateProblemTypes(int problemId, List<ProblemType> problemTypesList)
    {
        Dal.ExecuteNonQuery("DELETE FROM problemTypeDetails where problemId=" + problemId);

        if (problemTypesList == null)
        {
            return;
        }

        if (problemTypesList.Count == 0)
        {
            return;
        }

        string sql = "";
        foreach (var item in problemTypesList)
        {
            sql += "(" + problemId + "," + item.id + "),";
        }

        if (sql.Length > 0)
        {
            sql = sql.Substring(0, sql.Length - 1);
            sql = "INSERT INTO [dbo].[problemTypeDetails] ([problemId], [problemTypeId]) VALUES" + sql;

            Dal.ExecuteNonQuery(sql);
        }

    }

    public static void AppendProblem(long probID, int workerId, int phoneId, string phone, string ip,
                                            int placeNameId, string placeName, string customerName, string problemDesc, string problemSolution, int toWorker)
    {
        string sql = "INSERT INTO [dbo].[problemsClose] ([id],[workerId],[phoneId],[phone],[ip],[placeNameId],[placeName],[customerName],[problemDesc],[problemSolution],[toWorker],[startTime],[finishTime], [updaterWorkerId]) " +
                 "VALUES (@id,@workerId, @phoneId, @phone, @ip, @placeNameId, @placeName, @customerName, @problemDesc, @problemSolution, @toWorker, @startTime, @finishTime, @updaterWorkerId) " +
                 "SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();

        values.Add(new SqlParameter("@id", probID));
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@phoneId", phoneId));
        values.Add(new SqlParameter("@phone", phone));
        values.Add(new SqlParameter("@ip", ip));
        values.Add(new SqlParameter("@placeNameId", placeNameId));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@customerName", customerName));
        values.Add(new SqlParameter("@problemDesc", problemDesc));
        values.Add(new SqlParameter("@problemSolution", problemSolution));
        values.Add(new SqlParameter("@toWorker", workerId));
        values.Add(new SqlParameter("@startTime", CacheHelper.Instance.GetIsraelTime()));
        values.Add(new SqlParameter("@finishTime", CacheHelper.Instance.GetIsraelTime()));
        values.Add(new SqlParameter("@updaterWorkerId", workerId));


        Dal.ExecuteNonQuery(sql, values);
    }

    internal static int GetWorkerShluha(int workerId)
    {
        int result = 0;


        string sql = "SELECT [shluha] " +
                   "FROM workers " +
                    "WHERE [id]=@workerId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));

        object o = Dal.GetThisOneValue(sql, values);
        if (o != null)
        {
            int.TryParse(o.ToString(), out result);
        }

        return result;
    }

    internal static void AppendNewAnsweredCall(string phone, int shluha)
    {
        string sql = "INSERT INTO [dbo].[phoneCenterAnswered]([phone],[shluha],[commitTime]) VALUES(@phone,@shluha, @commitTime);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phone", phone));
        values.Add(new SqlParameter("@shluha", shluha));
        values.Add(new SqlParameter("@commitTime", CacheHelper.Instance.GetIsraelTime()));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static int GetWorkerPlaceInAllWorkers(int workerId)
    {
        DateTime now = CacheHelper.Instance.GetIsraelTime();
        if (now.Hour < 6)
        {
            now = now.AddDays(-1);
        }

        now = new DateTime(now.Year, now.Month, now.Day, 6, 0, 0);

        int result = -1;

        DataSet ds = GetWorkerPlaceInAllWorkersDS(now);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 1;
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        int wid = 0;
                        string s = item["workerId"].ToString();
                        if (int.TryParse(s, out wid))
                        {
                            if (workerId == wid)
                            {
                                return i;
                            }
                        }

                        i++;
                    }
                }
            }
        }

        return result;
    }

    public static string GetPhoneCenterAnsweredCall(int shluha)
    {
        string result = "";
        string sql = "SELECT Top 1 [phone],[shluha] FROM [dbo].[phoneCenterAnswered] WHERE shluha = @shluha Order By [commitTime] Desc;";
        object o = Dal.ExecuteScalar(sql, new List<SqlParameter> { new SqlParameter("@shluha", shluha) });
        if (o != null)
        {
            result = o.ToString();
        }

        return result;

    }

    public static DataSet GetWorkerPlaceInAllWorkersDS(DateTime now)
    {
        string sql = "SELECT ROW_NUMBER() OVER (Order by sum(1) desc) AS RowNumber ,[workerId], sum(1) as c " +
                    "FROM [dbo].[problemsClose] " +
                    "WHERE startTime>=@startTime " +
                    "Group by workerId " +
                    "Order by c desc, workerId;";

        sql = "SELECT [workerId], sum(1) as c " +
                    "FROM [dbo].[problemsClose] " +
                    "WHERE startTime >= @startTime " +
                    "Group by workerId " +
              "Order by c desc, [workerId]";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", now));

        return Dal.GetDataSet(sql, values);
    }

    internal static void AppendProblemNotafication(int problemId, int changingWorkerID, string msg)
    {
        List<int> workerIds = GetWorkersForProblem(problemId);

        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, remoteTimeZone);


        string sql = "INSERT INTO [dbo].[problemNotafications]([problemId], [workerId], [msg]) " +
                     "VALUES(@problemId, @workerId, @msg);";

        foreach (var workerID in workerIds)
        {
            if (workerID != changingWorkerID)
            {


                List<SqlParameter> values = new List<SqlParameter>();
                values.Add(new SqlParameter("@workerID", workerID));
                values.Add(new SqlParameter("@problemId", problemId));
                values.Add(new SqlParameter("@msg", msg));

                Dal.ExecuteNonQuery(sql, values);
            }
        }

    }

    private static List<int> GetWorkersForProblem(int problemId)
    {
        List<int> result = new List<int>();
        string sql = "SELECT [workerId], [toWorker] FROM [dbo].[problemsClose] where id=@problemId;" +
                     "SELECT [workerId]  FROM [dbo].[problemWorkers]  where problemId=@problemId";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        DataSet ds = Dal.GetDataSet(sql, values);
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            int wId = int.Parse(dt.Rows[0]["workerId"].ToString());
            result.Add(wId);
            wId = int.Parse(dt.Rows[0]["toWorker"].ToString());
            if (!result.Contains(wId))
            {
                result.Add(wId);
            }
        }

        dt = ds.Tables[1];
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows)
            {
                int wId = int.Parse(dt.Rows[0]["workerId"].ToString());
                if (!result.Contains(wId))
                {
                    result.Add(wId);
                }
            }
        }

        return result;
    }


    internal static void UpdateWorkerDepartments(int workerID, List<Department> departments)
    {
        if (departments == null)
        {
            return;
        }

        if (departments.Count == 0)
        {
            return;
        }

        string sql = "";

        foreach (var item in departments)
        {
            sql += "Update workerDepartments Set [canSee]=" + item.canSeeInt + " WHERE workerID=@workerID AND departmentId=" + item.id + ";";
        }

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static int GetMsgLinesCount(int problemId)
    {
        int result = 0;
        string sql = "SELECT Sum(1) as c FROM [dbo].[problemMsgs]  where problemId=@problemId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));

        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["c"].ToString()))
            {
                result = int.Parse(ds.Tables[0].Rows[0]["c"].ToString());
            }
        }


        return result;
    }


    internal static int AppendProblemTracking(int workerID, int problemId)
    {
        string sql = "INSERT INTO [dbo].[problemTracking]([workerId],[problemId]) " +
                     "VALUES(@workerId, @problemId) SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));
        values.Add(new SqlParameter("@problemId", problemId));

        object o = Dal.ExecuteScalar(sql, values);

        int trackingId = 0;
        if (o != null)
        {
            trackingId = int.Parse(o.ToString());
        }

        return trackingId;
    }


    internal static void DeleteProblemTracking(int workerId, int problemId)
    {
        string sql = "Delete From problemTracking WHERE workerId=@workerId AND problemId=@problemId";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@problemId", problemId));
        Dal.ExecuteNonQuery(sql, values);
    }

    internal static int GetMsgLinesCountNew(int workerID, int problemId)
    {
        int result = 0;
        string sql = "SELECT [lastTimeSeen] " +
                     "FROM [dbo].[ProblemChatSeenByWorker] " +
                     "WHERE [problemId] = @problemId AND [workerId] = @workerID;";

        sql += "SELECT Sum(1) as c FROM [dbo].[problemMsgs]  where problemId=@problemId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));
        values.Add(new SqlParameter("@problemId", problemId));

        DataSet ds = Dal.GetDataSet(sql, values);
        //העובד מעולם לא הסתכל על הצ'אט, מחזיר את כמות השורות שיש בצ'אט
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["c"].ToString()))
            {
                result = int.Parse(ds.Tables[1].Rows[0]["c"].ToString());
            }

            return result;
        }

        //var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        DateTime now = DateTime.Parse(ds.Tables[0].Rows[0]["lastTimeSeen"].ToString());// TimeZoneInfo.ConvertTime(DateTime.Now, remoteTimeZone);

        sql = "SELECT Sum(1) as c FROM [dbo].[problemMsgs]  where problemId=@problemId and commitTime>@commitTime;";



        values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        values.Add(new SqlParameter("@commitTime", now));

        ds = Dal.GetDataSet(sql, values);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["c"].ToString()))
            {
                result = int.Parse(ds.Tables[0].Rows[0]["c"].ToString());
            }
        }


        return result;
    }


    internal static void UpdateOrAppendProblemChatSeenByWorker(int workerID, int problemId)
    {
        string sql = "SELECT [lastTimeSeen] " +
                     "FROM [dbo].[ProblemChatSeenByWorker] " +
                     "WHERE [problemId] = @problemId AND [workerId] = @workerID;";


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));
        values.Add(new SqlParameter("@problemId", problemId));

        DataSet ds = Dal.GetDataSet(sql, values);
        //העובד מעולם לא הסתכל על הצ'אט, מחזיר את כמות השורות שיש בצ'אט
        if (ds.Tables[0].Rows.Count == 0)
        {
            AppendProblemChatSeenByWorker(workerID, problemId);
        }
        else
        {
            UpdateProblemChatSeenByWorker(workerID, problemId);
        }

    }

    internal static void AppendProblemChatSeenByWorker(int workerID, int problemId)
    {
        string sql = "INSERT INTO [dbo].[ProblemChatSeenByWorker]([problemId], [workerId], [lastTimeSeen]) " +
                     "VALUES(@problemId, @workerId, @lastTimeSeen);";


        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, remoteTimeZone);


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));
        values.Add(new SqlParameter("@problemId", problemId));
        values.Add(new SqlParameter("@lastTimeSeen", now));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateProblemChatSeenByWorker(int workerID, int problemId)
    {
        string sql = "Update [dbo].[ProblemChatSeenByWorker] SET [lastTimeSeen]=@lastTimeSeen " +
                     "WHERE workerID= @workerID AND problemId= @problemId;";


        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, remoteTimeZone);

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerID));
        values.Add(new SqlParameter("@problemId", problemId));
        values.Add(new SqlParameter("@lastTimeSeen", now));

        Dal.ExecuteNonQuery(sql, values);
    }


    public static string GetPlaceWithNoIp()
    {
        string result = "";
        string sql = "SELECT [id],[placeName] " +
                    "FROM [dbo].[Places] " +
                    "where ip = '' or ip is null " +
                    "order by placeName";

        DataSet ds = Dal.GetDataSet(sql);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = ds.Tables[0].Rows.Count;
                    int d = new Random().Next(1, i);
                    DataRow row = ds.Tables[0].Rows[d];
                    string id = row["id"].ToString();
                    string placeName = row["placeName"].ToString();
                    result = id + ";" + placeName;
                }
            }
        }

        return result;
    }

    public static void UpdateWorkerImagePath(int id, string imgPath)
    {
        WebDal.AppendErrorLog("UpdateWorkerImagePath", "id:" + id + " imgPath: " + imgPath, "");

        string sql = "UPDATE [dbo].[workers] SET [imgPath] = @imgPath " +
             "WHERE (id = @id)";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@imgPath", imgPath));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static DataSet GetProblemsForPhones(int problemId, string phone, int rowsCount = 15)
    {
        string sql = "SELECT TOP(" + rowsCount + ") problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, problemsClose.callCustomerBack " +
                     "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN departments ON problemsClose.departmentId = departments.id INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
                     "WHERE (problemsClose.phone = @phone) AND(problemsClose.id <> @id) ORDER BY problemsClose.startTime DESC";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phone", phone));
        values.Add(new SqlParameter("@id", problemId));
        return Dal.GetDataSet(sql, values);
    }

    public static string GetProblemPhone(int problemId)
    {
        string result = "";
        string sql = "SELECT [phone] FROM [dbo].[problemsClose] WHERE id = @id;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", problemId));
        object o = Dal.ExecuteScalar(sql, values);
        if (o != null)
        {
            result = o.ToString();
        }

        return result;
    }

    public static DataSet GetProblemDS(int problemId)
    {
        string sql = "SELECT problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, callCustomerBack " +
                    "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id " +
                            "INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id " +
                            "INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id " +
                            "INNER JOIN departments ON problemsClose.departmentId = departments.id " +
                            "INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
                   "WHERE(problemsClose.id = @id) " +
                   "ORDER BY problemsClose.startTime DESC";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", problemId));
        DataSet ds = Dal.GetDataSet(sql, values);
        return ds;
    }

    public static Problem GetProblem(int problemId)
    {
        Problem result = new Problem();

        DataSet ds = GetProblemDS(problemId);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow item = ds.Tables[0].Rows[0];

                result = new Problem();
                result.id = int.Parse(item["id"].ToString());
                result.workerCreateId = int.Parse(item["workerId"].ToString());
                result.workerCreateName = item["workerName"].ToString();
                result.phone = item["phone"].ToString();
                result.ip = item["ip"].ToString();
                result.placeId = int.Parse(item["placeNameId"].ToString());
                result.placeName = item["placeName"].ToString();
                result.customerName = item["customerName"].ToString();
                result.desc = item["problemDesc"].ToString();
                result.solution = item["problemSolution"].ToString();
                result.statusId = int.Parse(item["statusId"].ToString());
                result.statusName = item["statusName"].ToString();
                result.emergencyId = int.Parse(item["emergencyId"].ToString());
                result.emergencyName = item["emergencyName"].ToString();
                result.departmentId = int.Parse(item["departmentId"].ToString());
                result.departmentName = item["departmentName"].ToString();
                result.toWorker = int.Parse(item["toWorker"].ToString());
                result.toWorkerName = item["toWorkerName"].ToString();

                result.callCustomerBack = bool.Parse(item["callCustomerBack"].ToString());


                result.startTime = DateTime.Parse(item["startTime"].ToString()).ToString("dd/MM/yy HH:mm");
                result.finishTime = item["finishTime"].ToString();
                result.startTimeEN = DateTime.Parse(item["startTime"].ToString()).ToString("MM/dd/yy HH:mm");
            }
        }



        return result;
    }

    public static void DeletePhoneCenterForPhone(int shluha)
    {
        string sql = "Delete From [phoneCenterAnswered] Where shluha=@shluha;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@shluha", shluha));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static void DeletePhoneCenterForPhone(int shluha, string phone)
    {
        string sql = "Delete From [phoneCenterAnswered] Where shluha=@shluha AND phone=@phone;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@shluha", shluha));
        values.Add(new SqlParameter("@phone", phone));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static DataSet GetOpenProblemsForWorker(int workerId)
    {
        string sql = "SELECT problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName " +
                     "FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN departments ON problemsClose.departmentId = departments.id INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
                     "WHERE (problemsClose.toWorker = @toWorker) AND (problemsClose.statusId = 0 OR problemsClose.statusId = 1) " +
                     "ORDER BY problemsClose.startTime DESC";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@toWorker", workerId));
        DataSet ds = Dal.GetDataSet(sql, values);

        return ds;
    }

    public static DataSet GetProblems(string where = "", int rowsCount = 0)
    {
        if (!string.IsNullOrEmpty(where))
        {
            where = "WHERE " + where + " ";
        }

        string top = "";
        if (rowsCount > 0)
        {
            top = "TOP " + rowsCount + " ";
        }

        string sql = "SELECT " + top + " problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, tFiles.[fileCount] " +
                     "FROM problemsClose LEFT JOIN " +
                          "workers ON problemsClose.workerId = workers.id INNER JOIN " +
                          "problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN  " +
                          "emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN  " +
                          "departments ON problemsClose.departmentId = departments.id LEFT JOIN  " +
                          "workers AS workers_1 ON problemsClose.toWorker = workers_1.id " +
                          "LEFT JOIN (SELECT [problemId], Sum(1) as fileCount FROM [dbo].[problemFiles] group by [problemId]) as tFiles on problemsClose.id =tFiles.[problemId] " +
                     where +
                     "ORDER BY problemsClose.startTime DESC";

        //"(SELECT [problemId] FROM [dbo].[problemFiles] Group by problemId) as t on problemsClose.id = t.problemId " +
        //                  "Left Join (SELECT problemId, STRING_AGG(ISNULL([filePath], ' '), ', ') As filesName " +
        //                             "From[problemFiles] " +
        //                             "group by problemId) as tFiles on tFiles.problemId = problemsClose.id " +

        DataSet ds = Dal.GetDataSet(sql);

        return ds;
    }

    public static DataSet GetProblemsForWS(string where = "", int rowsCount = 0, int workerId = 0)
    {
        if (!string.IsNullOrEmpty(where))
        {
            where = "WHERE " + where + " ";
        }

        string top = "";
        if (rowsCount > 0)
        {
            top = "TOP " + rowsCount + " ";
        }

        string trakingWorker = "";
        if (workerId > 0)
        {
            trakingWorker = "LEFT JOIN(SELECT[problemId] FROM [dbo].[problemTracking] WHERE workerId = @workerId GROUP BY [problemId]) tTracking ON problemsClose.id = tTracking.problemId ";
        }

        string workerProblem = "";

        //if (workerInvolved)
        //{
        workerProblem = "Left Join (SELECT problemWorkers.problemId, Sum(IIF(problemWorkers.workerId=@workerId,1,0)) as pCount " +
                        "FROM [dbo].[problemsClose] inner join problemWorkers on problemWorkers.problemId = [problemsClose].id " +
                        "WHERE statusId<>2 AND problemWorkers.workerId = @workerId " +
                        "Group by  problemWorkers.problemId) pt on problemsClose.id = pt.problemId;";
        //}

        string sql = "SELECT " + top + " problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName, workers_1.jobTitle as toWorkerJobTitle, tFiles.[fileCount], tFiles.filesName, problemWorkers.workerId as toWorkersId, updaterWorkerId, workers_2.firstName + N' ' + workers_2.lastName AS updateWorkerName, workers_2.wDepartmentId as updateWorkerDepartment, places.vip, problemsClose.takingCare, problemsClose.isLocked, problemsClose.callCustomerBack, problemTypesInfo, pt.pCount as isInvolved " +
                     "FROM problemsClose LEFT JOIN " +
                          "workers ON problemsClose.workerId = workers.id INNER JOIN " +
                          "problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN " +
                          "departments ON problemsClose.departmentId = departments.id LEFT JOIN " +
                          "workers AS workers_1 ON problemsClose.toWorker = workers_1.id LEFT JOIN " +
                          "workers AS workers_2 ON problemsClose.updaterWorkerId = workers_2.id LEFT JOIN " +
                          "places ON places.id = problemsClose.placeNameId " +
                          "LEFT JOIN (SELECT[problemId], Sum(1) as fileCount, STRING_AGG(ISNULL([filePath], ' '), ', ') As filesName FROM[dbo].[problemFiles] group by[problemId]) as tFiles on problemsClose.id = tFiles.[problemId] LEFT JOIN " +
                          "problemWorkers ON problemsClose.id = problemWorkers.problemId " +
                          "LEFT JOIN(SELECT[problemId], STRING_AGG (CONVERT(NVARCHAR(max),problemTypes.problemTypeName + '-' + color + '-' + CAST(problemTypes.id AS VARCHAR(10))), ',') As problemTypesInfo FROM[dbo].problemTypeDetails  LEFT JOIN problemTypes ON problemTypeDetails.problemTypeId = problemTypes.id group by[problemId]) as tTypes on problemsClose.id = tTypes.[problemId] " +
                          trakingWorker +
                          "Left Join (SELECT problemWorkers.problemId, Sum(IIF(problemWorkers.workerId=@workerId,1,0)) as pCount " +
                            "FROM [dbo].[problemsClose] inner join problemWorkers on problemWorkers.problemId = [problemsClose].id " +
                            "WHERE statusId<>2 AND problemWorkers.workerId = @workerId " +
                            "Group by  problemWorkers.problemId) pt on problemsClose.id = pt.problemId " +
                        where +
                     "ORDER BY problemsClose.startTime DESC";

        List<SqlParameter> values = new List<SqlParameter>();
        //if (workerId > 0)
        //{
        values.Add(new SqlParameter("@workerId", workerId));
        //}
        DataSet ds = Dal.GetDataSet(sql, values);

        return ds;
    }

    
    public static List<Problem> GetProblemsList(string departmentId, int workerId)
    {
        int i;
        if (!int.TryParse(departmentId, out i))
        {
            return new List<Problem>();
        }

        string filter = "";
        switch (departmentId)
        {
            case "-8":
                filter = "statusId<>2 AND pt.pCount>0 ";
                break;
            case "-7":
                filter = "tTracking.problemId>0 ";
                break;
            case "-6":
                int dId = GetWorkerDepartment(workerId);
                filter = "problemsClose.departmentId = " + dId + " AND startTime >=GETDATE()-7 ";
                break;
            case "-5":
                filter = "problemsClose.workerId = " + workerId + " AND finishTime >=GETDATE()-1 and statusId=2 ";
                break;
            case "-4":
                filter = "statusId<>2 AND (problemsClose.startTime>=GETDATE()-1)";
                break;
            case "-3":
                filter = "problemsClose.workerId = " + workerId + " AND problemsClose.statusId<>2";
                break;
            case "-1":
                filter = "problemsClose.toWorker=" + workerId + " AND problemsClose.statusId<>2";
                break;
            default:
                filter = "statusId<>2 AND problemsClose.departmentId=" + departmentId;
                break;
        }

        List<Problem> result = new List<Problem>();
        Dictionary<int, Problem> tempResult = new Dictionary<int, Problem>();
        DataSet ds = GetProblemsForWS(filter, 0, workerId);

        if (ds.Tables.Count == 0)
        {
            return result;
        }

        if (ds.Tables[0].Rows.Count == 0)
        {
            return result;
        }

        foreach (DataRow item in ds.Tables[0].Rows)
        {
            Problem p = new Problem();
            p.id = int.Parse(item["id"].ToString());
            if (tempResult.ContainsKey(p.id))
            {
                //problemWorkers.workerId, workers_2.firstName + N' ' + workers_2.lastName AS toWorkers

                if (int.TryParse(item["toWorkersId"].ToString(), out workerId))
                {
                    if (!tempResult[p.id].toWorkers.Contains(workerId))
                    {
                        tempResult[p.id].toWorkers.Add(workerId);
                    }
                }

                continue;
            }

            p.workerCreateId = int.Parse(item["workerId"].ToString());
            p.workerCreateName = item["workerName"].ToString();
            if (!string.IsNullOrEmpty(item["vip"].ToString()))
            {
                p.vip = bool.Parse(item["vip"].ToString());
            }

            p.phone = item["phone"].ToString();
            p.ip = item["ip"].ToString();
            p.placeId = int.Parse(item["placeNameId"].ToString());
            p.placeName = item["placeName"].ToString();
            p.customerName = item["customerName"].ToString();
            p.desc = item["problemDesc"].ToString();
            p.solution = item["problemSolution"].ToString();

            p.statusId = int.Parse(item["statusId"].ToString());
            p.statusName = item["statusName"].ToString();

            p.emergencyId = int.Parse(item["emergencyId"].ToString());
            //p.emergencyName = item["emergencyName"].ToString();

            p.departmentId = int.Parse(item["departmentId"].ToString());
            p.departmentName = item["departmentName"].ToString();

            p.toWorker = int.Parse(item["toWorker"].ToString());
            p.toWorkerName = item["toWorkerName"].ToString();
            p.toWorkerJobTitle = item["toWorkerJobTitle"].ToString();
            p.takingCare = bool.Parse(item["takingCare"].ToString());
            p.isLocked = bool.Parse(item["isLocked"].ToString());
            p.callCustomerBack = bool.Parse(item["callCustomerBack"].ToString());



            p.yaron = bool.Parse(item["reportToYaron"].ToString());

            p.startTime = DateTime.Parse(item["startTime"].ToString()).ToString("dd/MM/yy HH:mm");
            p.startTimeEN = DateTime.Parse(item["startTime"].ToString()).ToString("MM/dd/yy HH:mm");
            p.finishTime = DateTime.Parse(item["finishTime"].ToString()).ToString("dd/MM/yy HH:mm");// item["finishTime"].ToString();
            p.finishTimeEN = DateTime.Parse(item["finishTime"].ToString()).ToString("MM/dd/yy HH:mm");
            if (item["updaterWorkerId"] != null)
            {
                int updaterwId = 0;
                if (int.TryParse(item["updaterWorkerId"].ToString(), out updaterwId))
                {
                    p.updaterWorkerId = updaterwId;
                    if (item["updateWorkerDepartment"] != null && !string.IsNullOrEmpty(item["updateWorkerDepartment"].ToString()))
                    {
                        p.updaterWorkerDepartmentId = int.Parse(item["updateWorkerDepartment"].ToString());
                        p.updaterWorkerName = item["updateWorkerName"].ToString();
                    }
                    else
                    {
                        p.updaterWorkerDepartmentId = p.departmentId;
                        p.updaterWorkerName = p.workerCreateName;
                    }
                }
            }

            if (int.TryParse(item["toWorkersId"].ToString(), out workerId))
            {
                if (!p.toWorkers.Contains(workerId))
                {
                    p.toWorkers.Add(workerId);
                }
            }

            if (!string.IsNullOrEmpty(item["problemTypesInfo"].ToString()))
            {
                string pTypes = item["problemTypesInfo"].ToString();
                string[] vs = pTypes.Split(new char[] { ',' });

                foreach (var pType in vs)
                {
                    string[] t = pType.Split(new char[] { '-' });
                    ProblemType pTy = new ProblemType();
                    pTy.id = int.Parse(t[2]);
                    pTy.problemTypeName = t[0];
                    pTy.color = t[1];
                    p.problemTypesList.Add(pTy);
                }
            }


            int count = 0;
            int.TryParse(item["fileCount"].ToString(), out count);
            p.fileCount = count;
            p.files = new List<string>();

            p.filesName = item["filesName"].ToString();

            if (p.fileCount > 0)
            {
                string[] a = p.filesName.Split(',');
                if (a.Length > 0)
                {
                    p.files = new List<string>();
                    foreach (var f in a)
                    {
                        p.files.Add(f.Trim());
                    }
                }
            }

            tempResult.Add(p.id, p);
            //result.Add(p);
        }

        foreach (var item in tempResult)
        {
            result.Add(item.Value);
        }

        return result;
    }

    internal static List<Notification> GetNotifications(int workerId)
    {
        List<Notification> result = new List<Notification>();

        string sql = "SELECT problemNotafications.[id],problemNotafications.[problemId], problemsClose.placeName, problemNotafications.[workerId],problemNotafications.[msg],problemNotafications.[hasSeen],problemNotafications.[commitTime] " +
                    "FROM [dbo].[problemNotafications] inner join problemsClose on problemNotafications.problemId = problemsClose.id " +
                    "WHERE problemNotafications.workerId = @workerID " +
                    "order by problemNotafications.[commitTime]";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerId));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Notification m = new Notification();
                    m.id = int.Parse(item["id"].ToString());
                    m.problemId = int.Parse(item["problemId"].ToString());
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.msg = item["msg"].ToString() + ", " + item["placeName"].ToString();
                    m.hasSeen = bool.Parse(item["hasSeen"].ToString());
                    m.commitTime = DateTime.Parse(item["commitTime"].ToString()).ToString("MM/dd/yyyy HH:mm");



                    result.Add(m);
                }
            }
        }

        return result;
    }


    internal static int GetNotificationsCount(int workerId)
    {
        int result = 0;

        string sql = "SELECT Sum(1) as nCount " +
                    "FROM [dbo].[problemNotafications] " +
                    "where workerId = @workerID AND [hasSeen]=0;";


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerId));

        object o = Dal.GetThisOneValue(sql, values);
        if (o != null)
        {
            int.TryParse(o.ToString(), out result);
        }

        return result;
    }

    internal static void DeleteNotifications(int notifictionID)
    {
        List<Notification> result = new List<Notification>();

        string sql = "DELETE FROM [dbo].[problemNotafications] WHERE id=@id;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", notifictionID));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void DeleteNotificationsAll(int workerId)
    {
        List<Notification> result = new List<Notification>();

        string sql = "DELETE FROM [dbo].[problemNotafications] WHERE workerId=@workerId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateNotificationHadSeen(int notifictionID, bool hasSeen)
    {
        List<Notification> result = new List<Notification>();

        string sql = "UPDATE [dbo].[problemNotafications] SET [hasSeen] = @hasSeen WHERE id=@notifictionID;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@notifictionID", notifictionID));
        values.Add(new SqlParameter("@hasSeen", hasSeen));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static void UpdateNotificationAllHadSeen(int workerId, bool hasSeen)
    {
        List<Notification> result = new List<Notification>();

        string sql = "UPDATE [dbo].[problemNotafications] SET [hasSeen] = @hasSeen WHERE workerId=@workerId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@hasSeen", hasSeen));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static List<WorkerExpenses> GetWorkerExpenses(int workerId, int year, int month)
    {

        DateTime startDate = new DateTime(year, month, 1, 0, 0, 0);
        DateTime finishDate = startDate.AddMonths(1);


        List<WorkerExpenses> result = new List<WorkerExpenses>();

        string sql = "SELECT [workerExpenses].[id],[workerExpenses].[workerId],[expenseType],workExpensName,[expenseValue],[startExpenseDate],[finishExpenseDate],[freePass],[remark],[expenseTypeUnitValue],[commitTime] " +
                    "FROM [dbo].[workerExpenses] inner join WorkExpensesTypes on[workerExpenses].expenseType = WorkExpensesTypes.id " +
                    "WHERE [workerExpenses].workerId = @workerID And startExpenseDate>= @startDate AND startExpenseDate<@finishDate AND cancel = 0";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerID", workerId));
        values.Add(new SqlParameter("@startDate", startDate));
        values.Add(new SqlParameter("@finishDate", finishDate));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerExpenses m = new WorkerExpenses();
                    m.id = int.Parse(item["id"].ToString());
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.expenseType = int.Parse(item["expenseType"].ToString());
                    m.workExpensName = item["workExpensName"].ToString();
                    m.expenseValue = double.Parse(item["expenseValue"].ToString());
                    m.expenseTypeUnitValue = double.Parse(item["expenseTypeUnitValue"].ToString());
                    m.startExpenseDate = DateTime.Parse(item["startExpenseDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    m.startExpenseDateEN = DateTime.Parse(item["startExpenseDate"].ToString()).ToString("MM/dd/yyyy HH:mm");
                    m.finishExpenseDate = DateTime.Parse(item["finishExpenseDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    m.freePass = Boolean.Parse(item["freePass"].ToString());
                    m.remark = item["remark"].ToString();

                    result.Add(m);
                }
            }
        }

        return result;
    }

    public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    internal static List<ExpenseAndShiftWeek> GetWorkerExpensesAndShiftForMonth(int year, int month, int workerId = 0, int departmentId = 0)
    {

        DateTime startDate = new DateTime(year, month, 1, 0, 0, 0);
        DateTime finishDate = startDate.AddMonths(1);

        DateTime tempStartDate = StartOfWeek(startDate, DayOfWeek.Sunday);


        Dictionary<DateTime, List<ExpenseAndShift>> temp = new Dictionary<DateTime, List<ExpenseAndShift>>();
        for (DateTime d = tempStartDate; d < finishDate; d = d.AddDays(1))
        {
            temp.Add(d, new List<ExpenseAndShift>());
        }

        string extraWorker = "";
        if (workerId > 0)
        {
            extraWorker = " AND workerExpenses.workerId= @workerId ";
        }

        string extraDepartment = "";
        if (departmentId > 0)
        {
            extraDepartment = " AND workers.wDepartmentId= @wDepartmentId ";
        }

        string sql = "SELECT [workerId], firstName + ' ' + lastName as workerName, ezShiftWorkerCode, Format(startExpenseDate,'yyyy/MM/dd') as eDate, Sum([expenseValue]) as sumExpense,  Sum(IIF([workExpensCategoryId]=1,[expenseValue], 0)) as Category1Sum, Sum(IIF([workExpensCategoryId]=2,[expenseValue], 0)) as Category2Sum, STRING_AGG(workExpensName, ', ') As expensNames " +
                     "FROM [dbo].[workerExpenses] inner join workers on [workerExpenses].workerId = workers.id inner join WorkExpensesTypes on workerExpenses.expenseType = WorkExpensesTypes.id " +
                     "WHERE startExpenseDate>= @startDate AND startExpenseDate< @finishDate and cancel = 0 AND (workerExpenses.expenseType<>17) " + extraWorker + extraDepartment +
                     "Group BY [workerId], firstName +' ' + lastName, ezShiftWorkerCode, Format(startExpenseDate, 'yyyy/MM/dd') " +
                     "ORDER BY workerName, eDate";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startDate", startDate));
        values.Add(new SqlParameter("@finishDate", finishDate));

        if (workerId > 0)
        {
            values.Add(new SqlParameter("@workerId", workerId));
        }

        if (departmentId > 0)
        {
            values.Add(new SqlParameter("@wDepartmentId", departmentId));
        }


        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    ExpenseAndShift m = new ExpenseAndShift();
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.ezShiftWorkerCode = int.Parse(item["ezShiftWorkerCode"].ToString());
                    m.workerName = item["workerName"].ToString();
                    m.dDay = DateTime.Parse(item["eDate"].ToString());
                    m.dDayEN = DateTime.Parse(item["eDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    m.sumExpense = double.Parse(item["sumExpense"].ToString());
                    m.expensNames = item["expensNames"].ToString();
                    m.category1Sum = double.Parse(item["Category1Sum"].ToString());
                    m.category2Sum = double.Parse(item["Category2Sum"].ToString());


                    temp[m.dDay].Add(m);
                }
            }
        }


        DataTable dtWorkres = GetWorkersDS(true).Tables[0];
        List<ezshiftWS.EZShift_Attended> shifts = GetEzShiftWorkers(startDate, finishDate);
        foreach (var item in shifts)
        {
            if (temp.ContainsKey(item.dtAttStart.Date))
            {
                TimeSpan t = item.dtAttEnd - item.dtAttStart;

                ExpenseAndShift expenseAndShift = temp[item.dtAttStart.Date].Find((a) => { return a.ezShiftWorkerCode == item.iEmployeeInternalID; });
                if (expenseAndShift != null)
                {
                    expenseAndShift.totalMinutes += t.TotalMinutes;
                }
                else
                {
                    int wId = 0;
                    DataRow[] dataRow = dtWorkres.Select("ezShiftWorkerCode=" + item.iEmployeeInternalID);

                    if (dataRow != null)
                    {
                        if (dataRow.Length > 0)
                        {


                            //DataRow dataRow = dataRows.AsEnumerable().fi[0];
                            wId = int.Parse(dataRow[0]["id"].ToString());
                            if (workerId > 0 && wId != workerId)
                            {
                                continue;
                            }



                            int departId = int.Parse(dataRow[0]["wDepartmentId"].ToString());
                            if (departmentId > 0 && departId != departmentId)
                            {
                                continue;
                            }
                        }
                    }

                    if (wId == 0)
                    {
                        continue;
                    }

                    ExpenseAndShift m = new ExpenseAndShift();
                    m.workerId = wId;
                    m.ezShiftWorkerCode = item.iEmployeeInternalID;
                    m.workerName = item.strEmployeeFirstName + " " + item.strEmployeeLastName;
                    m.dDay = item.dtAttStart.Date;
                    m.dDayEN = item.dtAttStart.Date.ToString("dd/MM/yyyy HH:mm");
                    m.totalMinutes += t.TotalMinutes;
                    if (string.IsNullOrEmpty(m.remark)) m.remark = "";
                    m.remark += item.dtAttStart.ToString("dd/MM/yyyy HH:mm") + " - " + item.dtAttEnd.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine;
                    m.shifts.Add(new ezShift(item));
                    temp[m.dDay].Add(m);
                }
            }
        }

        List<ExpenseAndShiftWeek> result = new List<ExpenseAndShiftWeek>();
        List<ExpenseAndShiftDay> days = new List<ExpenseAndShiftDay>();

        int i = 0;
        foreach (var item in temp)
        {
            if (i == 7)
            {
                ExpenseAndShiftWeek w = new ExpenseAndShiftWeek();
                w.days = days;
                result.Add(w);
                days = new List<ExpenseAndShiftDay>();
                i = 0;
            }

            ExpenseAndShiftDay e = new ExpenseAndShiftDay();
            e.dDay = item.Key;
            e.dDayEN = item.Key.ToString("dd/MM/yyyy HH:mm");

            e.workers.AddRange(item.Value);
            e.workers.Sort((a, b) => a.workerName.CompareTo(b.workerName));

            days.Add(e);

            i++;
        }

        if (result.Count > 0)
        {
            ExpenseAndShiftWeek w = new ExpenseAndShiftWeek();
            w.days = days;
            result.Add(w);
        }

        return result;
    }

    private static List<ezshiftWS.EZShift_Attended> GetEzShiftWorkers(DateTime start, DateTime finish)
    {

        ezshiftWS.API ap = new ezshiftWS.API();
        ezshiftWS.EZShift_Attended[] shiftsOriginal;
        ezshiftWS.ErrorCode ec = ap.GetAttendedTimes("gfdvgfhdj65409vbfmmjvfxdfkxgvw34ercgsvxvdmg5otch", start, finish, out shiftsOriginal);

        return new List<ezshiftWS.EZShift_Attended>(shiftsOriginal);
    }

    internal static void CancelWorkerExpense(int expenseId)
    {
        string sql = "UPDATE [dbo].[workerExpenses] SET [cancel] = 1 WHERE id=@expenseId;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@expenseId", expenseId));
        Dal.ExecuteNonQuery(sql, values);

    }

    internal static List<WorkExpensesType> GetWorkExpensesTypes()
    {
        List<WorkExpensesType> result = new List<WorkExpensesType>();

        string sql = "SELECT WorkExpensesTypes.[id], [workExpensName], [defValue], [workExpensCategoryId], categoryName, [orderIndex] " +
                     "FROM [dbo].[WorkExpensesTypes] inner join WorkExpensesTypeCategories on WorkExpensesTypeCategories.id = WorkExpensesTypes.workExpensCategoryId " +
                     "order by [workExpensCategoryId], [orderIndex],[workExpensName];";

        DataSet ds = Dal.GetDataSet(sql);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkExpensesType m = new WorkExpensesType();
                    m.id = int.Parse(item["id"].ToString());
                    m.workExpensName = item["workExpensName"].ToString();
                    m.categoryName = item["categoryName"].ToString();
                    m.defValue = double.Parse(item["defValue"].ToString());
                    m.workExpensCategoryId = int.Parse(item["workExpensCategoryId"].ToString());
                    m.orderIndex = int.Parse(item["orderIndex"].ToString());


                    result.Add(m);
                }
            }
        }

        return result;
    }

    internal static void UpdateWorkExpensesTypes(List<WorkExpensesType> expensesType)
    {
        string sql = "";

        foreach (var item in expensesType)
        {
            sql += "UPDATE [dbo].[WorkExpensesTypes] SET [workExpensName]= N'" + item.workExpensName + "', [defValue] = " + item.defValue + ", [orderIndex] = " + item.orderIndex + " WHERE (id=" + item.id + ");";
        }

        Dal.ExecuteNonQuery(sql);
    }

    internal static void UpdateWorkesExpensesApprove(List<WorkerExpenses> workerExpenses)
    {
        string sql = "";

        foreach (var item in workerExpenses)
        {
            sql += "UPDATE [dbo].[workerExpenses] SET [approved]= 1 WHERE (id=" + item.id + ");";
        }

        Dal.ExecuteNonQuery(sql);
    }

    internal static List<WorkExpensesType> GetWorkExpensesTypesForWorker(int workerId)
    {
        List<WorkExpensesType> result = new List<WorkExpensesType>();

        string sql = "SELECT [WorkExpensesTypes].[id] as WorkExpensesTypesId, [workExpensName], [defValue], [workExpensCategoryId], WorkerExpensesValue.id as WorkerExpensesValueId, expensValue " +
                     "FROM [dbo].[WorkExpensesTypes] Left Join WorkerExpensesValue on[WorkExpensesTypes].id = WorkerExpensesValue.workExpensType " +
                     "WHERE (WorkerExpensesValue.workerId = @workerId OR WorkerExpensesValue.workerId is null)";

        sql = "SELECT [WorkExpensesTypes].[id], [workExpensName], [defValue], [workExpensCategoryId], [expensValue], [orderIndex] " +
              "FROM [dbo].[WorkExpensesTypes] Left Join " +
                        "(SELECT[id], [workerId], [workExpensType], [expensValue] FROM [dbo].[WorkerExpensesValue] Where workerid = @workerid) w on [WorkExpensesTypes].id = w.workExpensType " +
              "Order By [orderIndex], workExpensName;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkExpensesType m = new WorkExpensesType();
                    m.id = 0;

                    m.id = int.Parse(item["id"].ToString());
                    m.workExpensType = int.Parse(item["id"].ToString());

                    m.workExpensName = item["workExpensName"].ToString();

                    if (string.IsNullOrEmpty(item["expensValue"].ToString()))
                    {
                        m.defValue = double.Parse(item["defValue"].ToString());
                    }
                    else
                    {
                        m.defValue = double.Parse(item["expensValue"].ToString());
                    }

                    m.workExpensCategoryId = int.Parse(item["workExpensCategoryId"].ToString());


                    result.Add(m);
                }
            }
        }

        return result;
    }

    internal static List<WorkerExpenses> GetWorkersExpenses(int year, int month, int wokrerId)
    {
        List<WorkerExpenses> result = new List<WorkerExpenses>();

        string whereWorker = "";
        if (wokrerId > 0)
        {
            whereWorker = "AND (workerId=@workerId) ";
        }

        string sql = "SELECT [workerExpenses].[id], [workerId], firstName + ' '  + lastName as workerName, categoryName, [expenseType],workExpensName,[expenseValue],[startExpenseDate],[finishExpenseDate],[freePass],[remark],[expenseTypeUnitValue],[cancel],[approved],[workExpensCategoryId],[workerExpenses].[commitTime] " +
                    "FROM [dbo].[workerExpenses] inner join workers on workers.id= workerExpenses.workerId  " +
                    "inner join WorkExpensesTypes on WorkExpensesTypes.id= workerExpenses.[expenseType]   " +
                    "inner join WorkExpensesTypeCategories on WorkExpensesTypeCategories.id= WorkExpensesTypes.[workExpensCategoryId]   " +
                    "WHERE YEAR ([startExpenseDate]) = @year AND (Month([startExpenseDate]) =@month) AND cancel=0 " + whereWorker + " " +
                    "order by categoryName, workExpensName, workerName, startExpenseDate";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@year", year));
        values.Add(new SqlParameter("@month", month));
        if (wokrerId > 0)
        {
            values.Add(new SqlParameter("@workerId", wokrerId));
        }

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerExpenses m = new WorkerExpenses();

                    m.id = int.Parse(item["id"].ToString());

                    m.workerName = item["workerName"].ToString();
                    m.categoryName = item["categoryName"].ToString();
                    m.expenseType = int.Parse(item["expenseType"].ToString());
                    m.startExpenseDate = DateTime.Parse(item["startExpenseDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    m.startExpenseDateEN = DateTime.Parse(item["startExpenseDate"].ToString()).ToString("MM/dd/yyyy HH:mm");
                    m.finishExpenseDate = DateTime.Parse(item["finishExpenseDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    m.workExpensName = item["workExpensName"].ToString();
                    m.workExpensCategoryId = int.Parse(item["workExpensCategoryId"].ToString());

                    //[startExpenseDate],[freePass],[remark],[expenseTypeUnitValue],[cancel],[approved],[workerExpenses].[commitTime] " +
                    m.expenseValue = double.Parse(item["expenseValue"].ToString());
                    m.expenseTypeUnitValue = double.Parse(item["expenseTypeUnitValue"].ToString());
                    m.remark = item["remark"].ToString();
                    m.freePass = Boolean.Parse(item["freePass"].ToString());
                    m.approved = Boolean.Parse(item["approved"].ToString());

                    result.Add(m);
                }
            }
        }

        return result;
    }


    internal static List<WorkerExpensesSum> GetWorkersExpensesSum(int year, int month)
    {
        List<WorkerExpensesSum> result = new List<WorkerExpensesSum>();

        string sql = "SELECT [workerId], firstName + ' '  + lastName as workerName,  Sum([expenseValue]) as totalSum, " +
                            "Sum(IIF(workExpensCategoryId = 1 OR workExpensCategoryId = 4 , [expenseValue], 0)) as workExpense,  " +
                            "Sum(IIF(workExpensCategoryId = 2 OR workExpensCategoryId = 6 OR workExpensCategoryId = 7, [expenseValue], 0)) as bonus, " +
                            "Sum(IIF(workExpensCategoryId = 3, [expenseValue], 0)) as fieldTrip, " +
                            "Sum(IIF(workExpensCategoryId = 5, [expenseValue], 0)) as answerPrecentge, workers.teudatZehut, workers.marselWorkerCode " +
                    "FROM [dbo].[workerExpenses] inner join workers on workers.id = workerExpenses.workerId " +
                            "inner join WorkExpensesTypes on WorkExpensesTypes.id = workerExpenses.[expenseType] " +
                            "inner join WorkExpensesTypeCategories on WorkExpensesTypeCategories.id = WorkExpensesTypes.[workExpensCategoryId] " +
                    "WHERE YEAR ([startExpenseDate]) = @year AND (Month([startExpenseDate]) = @month) AND cancel=0 " +
                    "Group by[workerId], firstName + ' ' + lastName, workers.teudatZehut, workers.marselWorkerCode " +
                    "order by workerName;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@year", year));
        values.Add(new SqlParameter("@month", month));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                WorkerExpensesSum total = new WorkerExpensesSum();
                total.workerId = 0;
                total.workerName = "סהכ";
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerExpensesSum m = new WorkerExpensesSum();

                    m.workerId = int.Parse(item["workerId"].ToString());

                    m.workerName = item["workerName"].ToString();
                    m.totalSum = double.Parse(item["totalSum"].ToString());
                    m.workExpense = double.Parse(item["workExpense"].ToString());
                    m.bonus = double.Parse(item["bonus"].ToString());
                    m.fieldTrip = double.Parse(item["fieldTrip"].ToString());
                    m.answerPrecentge = double.Parse(item["answerPrecentge"].ToString());

                    result.Add(m);

                    total.totalSum += double.Parse(item["totalSum"].ToString());
                    total.workExpense += double.Parse(item["workExpense"].ToString());
                    total.bonus += double.Parse(item["bonus"].ToString());
                    total.fieldTrip += double.Parse(item["fieldTrip"].ToString());
                    total.answerPrecentge += double.Parse(item["answerPrecentge"].ToString());

                    m.teudatZehut = item["teudatZehut"].ToString();
                    m.marselWorkerCode = int.Parse(item["marselWorkerCode"].ToString());


                }
                result.Add(total);
            }
        }

        return result;
    }



    internal static void UpdateWorkerExpensesValue(List<WorkExpensesType> expensesType)
    {
        if (expensesType == null)
        {
            return;
        }
        if (expensesType.Count == 0)
        {
            return;
        }
        string sql = "";

        foreach (var item in expensesType)
        {
            sql += "UPDATE [dbo].[WorkerExpensesValue] SET [expensValue] = " + item.defValue + " WHERE (id=" + item.id + ");";
        }

        Dal.ExecuteNonQuery(sql);
    }

    internal static void UpdateProblemSolution(int problemId, string newLine)
    {
        string sql = "UPDATE [dbo].[problemsClose] " +
                     "SET [problemSolution] = [problemSolution] + ' ' + @newLine " +
                     "WHERE id = @problemId;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        values.Add(new SqlParameter("@newLine", newLine));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void AppendWorkerExpense(int workerId, DateTime startExpenceDate, DateTime finishExpenceDate, int expenseType, double sum,
        double expenseTypeUnitValue, bool freePass, string remark)
    {
        string sql = "INSERT INTO [dbo].[workerExpenses] ([workerId],[expenseType],[expenseValue],[freePass],[startExpenseDate],[finishExpenseDate],[remark],[expenseTypeUnitValue]) VALUES " +
                       "(@workerId,@expenseType,@expenseValue,@freePass,@startExpenseDate,@finishExpenseDate, @remark, @expenseTypeUnitValue);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@expenseType", expenseType));
        values.Add(new SqlParameter("@expenseValue", sum));
        values.Add(new SqlParameter("@expenseTypeUnitValue", expenseTypeUnitValue));

        values.Add(new SqlParameter("@freePass", freePass));
        values.Add(new SqlParameter("@startExpenseDate", startExpenceDate));
        values.Add(new SqlParameter("@finishExpenseDate", finishExpenceDate));

        values.Add(new SqlParameter("@remark", remark));


        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void AppendShift(ShiftDetail shift)
    {
        if (string.IsNullOrEmpty(shift.address))
        {
            shift.address = " ";
        }

        if (string.IsNullOrEmpty(shift.remark))
        {
            shift.remark = " ";
        }

        string sql = "INSERT INTO [dbo].[shiftsDetails] ([shiftTypeId],[jobTypeId],[workerId],[startDate],[finishTime],[remark],[placeName],[contactName],[phone],[address],[shiftGroupId],[isShiftManager]) VALUES " +
                                                    "(@shiftTypeId,@jobTypeId,@workerId,@startDate,@finishTime,@remark,@placeName,@contactName,@phone,@address,@shiftGroupId,@isShiftManager);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@shiftTypeId", shift.shiftTypeId));
        values.Add(new SqlParameter("@jobTypeId", shift.jobTypeId));
        values.Add(new SqlParameter("@workerId", shift.workerId));
        values.Add(new SqlParameter("@startDate", shift.startDate));

        values.Add(new SqlParameter("@finishTime", shift.finishTime));
        values.Add(new SqlParameter("@remark", shift.remark));
        values.Add(new SqlParameter("@placeName", shift.placeName));
        values.Add(new SqlParameter("@address", shift.address));


        values.Add(new SqlParameter("@contactName", shift.contactName));
        values.Add(new SqlParameter("@phone", shift.phone));
        values.Add(new SqlParameter("@shiftGroupId", shift.shiftGroupId));
        values.Add(new SqlParameter("@isShiftManager", shift.isShiftManager));

        Dal.ExecuteNonQuery(sql, values);
        //get shiftId of the created shift
        //then update outer comps for this shift
        sql = "SELECT [id] FROM [dbo].[shiftsDetails] "
            + "WHERE workerId = @workerId AND startDate = @startDate AND shiftGroupId = @shiftGroupId AND shiftTypeId = @shiftTypeId AND jobTypeId = @jobTypeId;";
        DataSet ds = Dal.GetDataSet(sql);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int id = int.Parse(row["id"].ToString());
                    if (shift.outerCompanies != null)
                    {
                        UpdateShiftOuterCompanies(shift.id, shift.outerCompanies);
                    }
                }
            }
        }
    }

    internal static void UpdateShift(ShiftDetail shift)
    {
        //var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        //DateTime startTime = TimeZoneInfo.ConvertTime(shift.startDate, remoteTimeZone);
        //DateTime finishTime = TimeZoneInfo.ConvertTime(shift.finishTime, remoteTimeZone);


        string sql = "UPDATE [dbo].[shiftsDetails] SET " +
                            "[shiftTypeId] = @shiftTypeId, [jobTypeId] = @jobTypeId, [workerId] = @workerId, [startDate] = @startDate, [finishTime] = @finishTime, " +
                            "[remark] = @remark, [placeName] = @placeName, [contactName] = @contactName, [phone] = @phone, [address] = @address " +
                            "WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", shift.id));
        values.Add(new SqlParameter("@shiftTypeId", shift.shiftTypeId));
        values.Add(new SqlParameter("@jobTypeId", shift.jobTypeId));
        values.Add(new SqlParameter("@workerId", shift.workerId));
        values.Add(new SqlParameter("@startDate", shift.startDate));

        values.Add(new SqlParameter("@finishTime", shift.finishTime.ToString("HH:mm:ss")));
        values.Add(new SqlParameter("@remark", shift.remark));
        values.Add(new SqlParameter("@placeName", shift.placeName));

        values.Add(new SqlParameter("@contactName", shift.contactName));
        values.Add(new SqlParameter("@phone", shift.phone));
        values.Add(new SqlParameter("@address", shift.address));

        if (shift.outerCompanies != null) {
            UpdateShiftOuterCompanies(shift.id, shift.outerCompanies);
        }
       
        Dal.ExecuteNonQuery(sql, values);
    }

    internal static List<int> GetWorkerMissingShiftPlans(DateTime start)
    {
        DateTime finish = start.AddDays(7);

        List<int> result = new List<int>();

        string sql = "SELECT workers.[id],[firstName],[lastName] " +
             "FROM [dbo].[workers] Left join(SELECT workerId FROM shiftPlans where ((date>= @startDate) AND(date <= @finishDate)) Group by workerId) as a on[workers].id = a.workerId " +
             "WHERE [wDepartmentId] = 16 AND a.workerId is null AND workers.active = 1" +
              "order by firstName, lastName";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startDate", start));
        values.Add(new SqlParameter("@finishDate", finish));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(int.Parse(row["id"].ToString()));
                }
            }
        }
        return result;
    }

    internal static void AppendDefultWeekShifts(DateTime startOfWeek, int shiftGroupId)
    {
        DateTime d = startOfWeek;
        for (int i = 0; i < 7; i++)
        {
            //8-17 בוקר   - שתיים תומך
            ShiftDetail s = new ShiftDetail();
            s.workerId = 199;
            s.shiftTypeId = 1;
            s.jobTypeId = 2;
            s.startDate = new DateTime(d.Year, d.Month, d.Day, 8, 0, 0);
            s.finishTime = new DateTime(d.Year, d.Month, d.Day, 17, 0, 0);
            s.shiftGroupId = shiftGroupId;
            AppendShift(s);
            AppendShift(s);

            //10-18:30 צהריים אחד תומך            
            s.shiftTypeId = 2;
            s.startDate = new DateTime(d.Year, d.Month, d.Day, 10, 0, 0);
            s.finishTime = new DateTime(d.Year, d.Month, d.Day, 18, 30, 0);
            s.shiftGroupId = shiftGroupId;
            AppendShift(s);

            //11-19 צהריים תומך
            if (d.DayOfWeek != DayOfWeek.Friday && d.DayOfWeek != DayOfWeek.Saturday)
            {
                s.shiftTypeId = 2;
                s.startDate = new DateTime(d.Year, d.Month, d.Day, 11, 0, 0);
                s.finishTime = new DateTime(d.Year, d.Month, d.Day, 19, 0, 0);
                s.shiftGroupId = shiftGroupId;
                AppendShift(s);
            }


            //ערב 17-01 תומך כפול שתיים
            s.shiftTypeId = 3;
            s.startDate = new DateTime(d.Year, d.Month, d.Day, 17, 0, 0);
            DateTime temp = d.AddDays(1);
            s.finishTime = new DateTime(temp.Year, temp.Month, temp.Day, 1, 0, 0);
            s.shiftGroupId = shiftGroupId;
            AppendShift(s);
            AppendShift(s);

            //לילה 1-08 תומך אחד            
            s.shiftTypeId = 4;
            //temp = d.AddDays(1);
            s.startDate = new DateTime(d.Year, d.Month, d.Day, 1, 0, 0);
            s.finishTime = new DateTime(d.Year, d.Month, d.Day, 8, 0, 0);
            s.shiftGroupId = shiftGroupId;
            AppendShift(s);

            // שישי שבת רק את הבוקר ערב ולילה בלי צהריים


            d = d.AddDays(1);
        }

    }

    internal static void CancelShift(int shiftId)
    {
        string sql = "UPDATE [dbo].[shiftsDetails] SET [cancel] = 1 WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", shiftId));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateShiftStartDate(int shiftId, DateTime newDate)
    {
        //var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        //DateTime startTime = TimeZoneInfo.ConvertTime(shift.startDate, remoteTimeZone);
        //DateTime finishTime = TimeZoneInfo.ConvertTime(shift.finishTime, remoteTimeZone);


        string sql = "UPDATE [dbo].[shiftsDetails] SET [startDate] = @startDate WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", shiftId));
        values.Add(new SqlParameter("@startDate", newDate));
        Dal.ExecuteNonQuery(sql, values);
    }


    internal static void AppendShiftPlan(ShiftPlan shiftPlan)
    {
        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        shiftPlan.startDate = TimeZoneInfo.ConvertTime(shiftPlan.startDate, remoteTimeZone);

        if (string.IsNullOrEmpty(shiftPlan.remark))
        {
            shiftPlan.remark = " ";
        }

        string sql = "INSERT INTO [dbo].[shiftPlans] ([workerId],[date],[shiftTypeId],[remark]) VALUES " +
                                                    "(@workerId,@date,@shiftTypeId,@remark);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", shiftPlan.workerId));
        values.Add(new SqlParameter("@date", shiftPlan.startDate));
        values.Add(new SqlParameter("@shiftTypeId", shiftPlan.shiftTypeId));
        values.Add(new SqlParameter("@remark", shiftPlan.remark));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateShiftPlan(ShiftPlan shiftPlan)
    {
        if (string.IsNullOrEmpty(shiftPlan.remark))
        {
            shiftPlan.remark = " ";
        }

        string sql = "UPDATE [dbo].[shiftPlans] SET " +
                            "[workerId] = @workerId, [shiftTypeId] = @shiftTypeId, [remark] = @remark, [date] = @startDate, [cancel] = @cancel " +
                            "WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", shiftPlan.id));
        values.Add(new SqlParameter("@workerId", shiftPlan.workerId));
        values.Add(new SqlParameter("@startDate", shiftPlan.startDate));
        values.Add(new SqlParameter("@shiftTypeId", shiftPlan.shiftTypeId));
        values.Add(new SqlParameter("@remark", shiftPlan.remark));
        values.Add(new SqlParameter("@cancel", shiftPlan.cancel? 1 : 0));

        Dal.ExecuteNonQuery(sql, values);
    }
    
    internal static void CancelShiftPlan(int shiftplanId)
    {
        string sql = "UPDATE [dbo].[shiftPlans] SET [cancel] = 1 WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", shiftplanId));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateHardware(Hardware hardware)
    {
        string sql = "UPDATE [dbo].[[hardwares]] SET " +
                     "[hardwareType] = @hardwareType, [barcode] = @barcode, [remark] = @remark, [statusId] = @statusId, [tokefExpire] = @tokefExpire " +
                     "WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", hardware.id));
        values.Add(new SqlParameter("@hardwareType", hardware.hardwareType));
        values.Add(new SqlParameter("@barcode", hardware.barcode));
        values.Add(new SqlParameter("@remark", hardware.remark));
        values.Add(new SqlParameter("@statusId", hardware.statusId));
        values.Add(new SqlParameter("@tokefExpire", hardware.tokefExpire));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void AppendHardware(Hardware hardware)
    {
        string sql = "INSERT INTO [dbo].[hardwares] ([hardwareType],[barcode],[remark],[statusId],[tokefExpire],[place]) VALUES " +
                        "(@hardwareType,@barcode,@remark,@statusId,@tokefExpire,@place);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@hardwareType", hardware.hardwareType));
        values.Add(new SqlParameter("@barcode", hardware.barcode));
        values.Add(new SqlParameter("@remark", hardware.remark));
        values.Add(new SqlParameter("@statusId", hardware.statusId));
        values.Add(new SqlParameter("@tokefExpire", hardware.tokefExpire));
        values.Add(new SqlParameter("@place", hardware.place));

        Dal.ExecuteNonQuery(sql, values);

    }

    internal static void AppendHardwareTracking(int hardwareId, int statusId, string cusName, string remark)
    {
        string sql = "INSERT INTO [dbo].[hardwareTracking] ([hardwareId],[statusId],[customerName],[remark]) VALUES " +
                        "(@hardwareId,@statusId,@customerName,@remark);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@hardwareId", hardwareId));
        values.Add(new SqlParameter("@statusId", statusId));
        values.Add(new SqlParameter("@customerName", cusName));
        values.Add(new SqlParameter("@remark", remark));

        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateHardwareStatus(int hardwareId, int statusId, string place)
    {
        string sql = "Update hardwares Set statusId = @statusId, place = @place WHERE id=@id;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", hardwareId));
        values.Add(new SqlParameter("@statusId", statusId));
        values.Add(new SqlParameter("@place", place));


        Dal.ExecuteNonQuery(sql, values);

    }

    internal static List<Hardware> GetHardware(string barcode)
    {
        List<Hardware> result = new List<Hardware>();

        string sql = "SELECT [id],[hardwareType],[barcode],[remark],[statusId],[tokefExpire], [place] " +
                    "FROM [dbo].[hardwares] " +
                    "WHERE [barcode] = @barcode;";

        sql += "SELECT [hardwareTracking].[id],[hardwareId],[customerName],[hardwareTracking].[remark],[hardwareTracking].[statusId],hardwareStatusName,[hardwareTracking].[commitTime] " +
               "FROM [dbo].[hardwareTracking] inner join hardwares on[hardwareTracking].hardwareId = hardwares.id inner join hardwareStatus on[hardwareTracking].statusId = hardwareStatus.id " +
               "WHERE (barcode = @barcode);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@barcode", barcode));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTracking = ds.Tables[1];
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Hardware m = new Hardware();
                    m.id = int.Parse(item["id"].ToString());
                    m.hardwareType = int.Parse(item["hardwareType"].ToString());
                    m.barcode = item["barcode"].ToString();
                    m.remark = item["remark"].ToString();
                    m.statusId = int.Parse(item["statusId"].ToString());
                    m.statusName = "במחסן";
                    if (m.statusId == 1)
                    {
                        m.statusName = "לקוח";
                    }
                    if (m.statusId == 2)
                    {
                        m.statusName = "דנגוט";
                    }
                    m.tokefExpire = DateTime.Parse(item["tokefExpire"].ToString());
                    m.tokefExpireEN = DateTime.Parse(item["tokefExpire"].ToString()).ToString("MM/dd/yyyy");
                    m.place = item["place"].ToString();


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataRow[] rows = dtTracking.Select("id=" + m.id);
                        if (rows.Length > 0)
                        {
                            foreach (DataRow track in rows)
                            {
                                HardwareTracking t = new HardwareTracking();
                                t.hardwareId = int.Parse(track["id"].ToString());
                                t.cusName = track["customerName"].ToString();
                                t.remark = track["remark"].ToString();
                                t.statusId = int.Parse(track["statusId"].ToString());
                                t.statusName = track["hardwareStatusName"].ToString();

                                if (m.trackings == null)
                                {
                                    m.trackings = new List<HardwareTracking>();
                                }

                                m.trackings.Add(t);
                            }
                        }
                    }

                    result.Add(m);
                }
            }


        }

        return result;
    }

    internal static List<Hardware> GetHardwaresCount()
    {
        List<Hardware> result = new List<Hardware>();

        string sql = "SELECT [hardwareType], hardwareTypeName,[statusId], sum(1) as c " +
                     "FROM [dbo].[hardwares] inner join hardwareTypes on hardwareTypes.id = hardwares.hardwareType " +
                     "group by[hardwareType], hardwareTypeName, statusId " +
                     "order by hardwareTypeName";

        List<SqlParameter> values = new List<SqlParameter>();
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Hardware m = new Hardware();

                    m.hardwareType = int.Parse(item["hardwareType"].ToString());
                    m.hardwareName = item["hardwareTypeName"].ToString();

                    m.statusId = int.Parse(item["statusId"].ToString());
                    m.id = int.Parse(item["c"].ToString());
                    result.Add(m);
                }
            }
        }

        return result;
    }



    internal static ShiftDetail GetShift(int shiftId)
    {

        string status = "";
        ShiftDetail result = null;
        try
        {


            string sql = "SELECT [shiftsDetails].[id],[shiftTypeId], shiftName,[jobTypeId], jobTypeName,[workerId], firstName + ' ' + lastName as workerName,[startDate],[finishTime],shiftsDetails.[remark],shiftsDetails.[placeName],shiftsDetails.[contactName],shiftsDetails.[phone],address,shiftsDetails.[commitTime] " +
                "FROM [dbo].[shiftsDetails] " +
                "inner join shiftTypes on [shiftsDetails].[shiftTypeId] = shiftTypes.id " +
                "inner join shiftJobTypes on [shiftsDetails].[jobTypeId] = shiftJobTypes.id " +
                "inner join Workers on [shiftsDetails].[workerId] = Workers.id " +
                "WHERE (shiftsDetails.id = @id);";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@id", shiftId));

            DataSet ds = Dal.GetDataSet(sql, values);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ShiftDetail m = new ShiftDetail();
                        m.id = int.Parse(item["id"].ToString());
                        m.shiftTypeId = int.Parse(item["shiftTypeId"].ToString());
                        m.shiftName = item["shiftName"].ToString();
                        m.jobTypeId = int.Parse(item["jobTypeId"].ToString());
                        m.jobTypeName = item["jobTypeName"].ToString();
                        m.workerId = int.Parse(item["workerId"].ToString());
                        m.workerName = item["workerName"].ToString();
                        m.startDate = DateTime.Parse(item["startDate"].ToString());
                        m.startDateEN = DateTime.Parse(item["startDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.finishTime = DateTime.Parse(item["finishTime"].ToString());
                        m.finishTimeEN = DateTime.Parse(item["finishTime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.startHour = DateTime.Parse(item["startDate"].ToString()).ToString("HH:mm");
                        m.finishHour = DateTime.Parse(item["finishTime"].ToString()).ToString("HH:mm");
                        m.remark = item["remark"].ToString();
                        m.placeName = item["placeName"].ToString();
                        m.contactName = item["contactName"].ToString();
                        m.phone = item["phone"].ToString();
                        m.address = item["address"].ToString();

                        result = m;
                    }
                }
            }
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftDetails", e.Message, "status: " + status);
        }


        return result;
    }


    internal static List<ShiftDetail> GetShiftsDetailsForWorker(int workerId, DateTime startTime, int days = 7)
    {

        string status = "";
        List<ShiftDetail> weeks = new List<ShiftDetail>();
        try
        {
            DateTime finishDate = startTime.AddDays(days);

            string sql = "SELECT [shiftsDetails].[id], [shiftTypeId], shiftName, [jobTypeId], jobTypeName, shiftJobTypes.color, [workerId], firstName + ' ' + lastName as workerName,[startDate],[finishTime],shiftsDetails.[remark],shiftsDetails.[placeName],shiftsDetails.[contactName], shiftsDetails.[phone], [address], shiftsDetails.[commitTime] " +
                "FROM [dbo].[shiftsDetails] " +
                "inner join shiftTypes on [shiftsDetails].[shiftTypeId] = shiftTypes.id " +
                "inner join shiftJobTypes on [shiftsDetails].[jobTypeId] = shiftJobTypes.id " +
                "inner join Workers on [shiftsDetails].[workerId] = Workers.id " +
                "WHERE ([startDate] >= @startTime AND [startDate] < @finishTime) AND (workerId=@workerId) AND cancel=0;";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@workerId", workerId));
            values.Add(new SqlParameter("@startTime", startTime));
            values.Add(new SqlParameter("@finishTime", finishDate));

            DataSet ds = Dal.GetDataSet(sql, values);
            string[] daysName = { "א", "ב", "ג", "ד", "ה", "ו", "ש" };
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ShiftDetail m = new ShiftDetail();
                        m.id = int.Parse(item["id"].ToString());
                        m.shiftTypeId = int.Parse(item["shiftTypeId"].ToString());
                        m.shiftName = item["shiftName"].ToString();
                        m.jobTypeId = int.Parse(item["jobTypeId"].ToString());
                        m.jobTypeName = item["jobTypeName"].ToString();
                        m.color = item["color"].ToString();

                        m.workerId = int.Parse(item["workerId"].ToString());
                        m.workerName = item["workerName"].ToString();
                        m.startDate = DateTime.Parse(item["startDate"].ToString());
                        m.startDateEN = DateTime.Parse(item["startDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.finishTime = DateTime.Parse(item["finishTime"].ToString());
                        m.finishTimeEN = DateTime.Parse(item["finishTime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.startHour = DateTime.Parse(item["startDate"].ToString()).ToString("HH:mm");
                        m.finishHour = DateTime.Parse(item["finishTime"].ToString()).ToString("HH:mm");
                        m.remark = item["remark"].ToString();
                        m.placeName = item["placeName"].ToString();
                        m.contactName = item["contactName"].ToString();
                        m.phone = item["phone"].ToString();
                        m.address = item["address"].ToString();

                        m.dayName = daysName[(int)m.startDate.DayOfWeek];


                        weeks.Add(m);
                    }
                }
            }

            return weeks;
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftDetails", e.Message, "status: " + status);
        }


        return weeks;
    }

    internal static List<shiftWeek> GetShiftsDetails(DateTime startTime, int shiftGroupID)
    {

        string status = "";
        List<shiftWeek> weeks = new List<shiftWeek>();
        try
        {
            status = "DateTime finishDate = startTime.AddDays(7);";
            DateTime finishDate = startTime.AddDays(7);
    
            status = "List<shiftWeek> weeks = new List<shiftWeek>();";

            string sql = "SELECT [shiftsDetails].[id], [shiftTypeId], shiftName, [jobTypeId], jobTypeName, shiftJobTypes.color, [workerId], firstName + ' ' + lastName as workerName,[startDate],[finishTime],shiftsDetails.[remark],shiftsDetails.[placeName],shiftsDetails.[contactName], shiftsDetails.[phone], [address], shiftsDetails.[commitTime] " +
                "FROM [dbo].[shiftsDetails] " +
                "inner join shiftTypes on [shiftsDetails].[shiftTypeId] = shiftTypes.id " +
                "inner join shiftJobTypes on [shiftsDetails].[jobTypeId] = shiftJobTypes.id " +
                "inner join Workers on [shiftsDetails].[workerId] = Workers.id " +
                "WHERE ([startDate] >= @startTime AND [startDate] < @finishTime) AND (shiftGroupID=@shiftGroupID) AND cancel=0;";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@startTime", startTime));
            values.Add(new SqlParameter("@finishTime", finishDate));
            values.Add(new SqlParameter("@shiftGroupID", shiftGroupID));


            status = "DataSet ds = Dal.GetDataSet(sql, values);";
            DataSet ds = Dal.GetDataSet(sql, values);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ShiftDetail m = new ShiftDetail();
                        m.id = int.Parse(item["id"].ToString());
                        m.shiftTypeId = int.Parse(item["shiftTypeId"].ToString());
                        m.shiftName = item["shiftName"].ToString();
                        m.jobTypeId = int.Parse(item["jobTypeId"].ToString());
                        m.jobTypeName = item["jobTypeName"].ToString();
                        m.color = item["color"].ToString();

                        m.workerId = int.Parse(item["workerId"].ToString());
                        m.workerName = item["workerName"].ToString();
                        m.startDate = DateTime.Parse(item["startDate"].ToString());
                        m.startDateUTC = GetUnixTimestampInMilliseconds(DateTime.Parse(item["startDate"].ToString()));
                        m.startDateEN = DateTime.Parse(item["startDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.finishTime = DateTime.Parse(item["finishTime"].ToString());
                        m.finishTimeEN = DateTime.Parse(item["finishTime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                        m.startHour = DateTime.Parse(item["startDate"].ToString()).ToString("HH:mm");
                        m.finishHour = DateTime.Parse(item["finishTime"].ToString()).ToString("HH:mm");
                        m.remark = item["remark"].ToString();
                        m.placeName = item["placeName"].ToString();
                        m.contactName = item["contactName"].ToString();
                        m.phone = item["phone"].ToString();
                        m.address = item["address"].ToString();
                        if (m.jobTypeId == 1 || m.jobTypeId == 4) {
                            m.outerCompanies = GetShiftOuterCompanies(m.id);
                        }
                        
                        shiftWeek shiftWeek = weeks.Find(x => x.shiftType == m.shiftTypeId && x.jobType == m.jobTypeId);
                        if (shiftWeek == null)
                        {
                            shiftWeek s = new shiftWeek();
                            s.jobType = m.jobTypeId;
                            s.jobTypeName = m.jobTypeName;
                            s.shiftType = m.shiftTypeId;
                            s.shiftTypeName = m.shiftName;
                            s.color = m.color;


                            s.AddShift(m);

                            weeks.Add(s);
                        }
                        else
                        {
                            shiftWeek.AddShift(m);
                        }
                    }
                }
            }

            return weeks;
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftDetails", e.Message, "status: " + status);
        }


        return weeks;
    }

    internal static long GetUnixTimestampInMilliseconds(DateTime dateTime)
    {
        // Convert DateTime to UTC if it's not already
        DateTime utcDateTime = dateTime.ToUniversalTime();

        // Get the Unix timestamp in milliseconds
        long unixTimestampInMilliseconds = (long)(utcDateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;

        return unixTimestampInMilliseconds;
    }

    internal static List<shiftWeek> GetShiftPlans(DateTime startTime, int workerId, int addDays = 7)
    {
        List<shiftWeek> weeks = new List<shiftWeek>();
        try
        {
            string whereWorker = "";
            if (workerId > 0)
            {
                whereWorker = " AND workerId= @workerId ";
            }

            for (int z = 1; z < 6; z++)
            {
                shiftWeek sw = new shiftWeek();
                sw.shiftType = z;
                weeks.Add(sw);
            }

            DateTime finishDate = startTime.AddDays(addDays);

            string sql = "SELECT shiftPlans.[id],[workerId], workers.firstName + ' ' + workers.lastName as workerName,[date],[shiftTypeId],[remark],[cancel] " +
                         "FROM [dbo].[shiftPlans] Inner join Workers On shiftPlans.workerId = Workers.id " +
                         "WHERE ([date] >= @startTime AND [date] < @finishTime) " + whereWorker + " AND cancel = 0 " +
                         "Order by [date], workerName;";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@startTime", startTime));
            values.Add(new SqlParameter("@finishTime", finishDate));
            if (workerId > 0)
            {
                values.Add(new SqlParameter("@workerId", workerId));
            }

            DataSet ds = Dal.GetDataSet(sql, values);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ShiftDetail m = new ShiftDetail();
                        m.id = int.Parse(item["id"].ToString());
                        m.shiftTypeId = int.Parse(item["shiftTypeId"].ToString());
                        m.workerId = int.Parse(item["workerId"].ToString());
                        m.workerName = item["workerName"].ToString();

                        m.startDate = DateTime.Parse(item["date"].ToString());
                        m.startDateEN = DateTime.Parse(item["date"].ToString()).ToString("yyyy/MM/dd");
                        m.remark = item["remark"].ToString();

                        shiftWeek shiftWeek = weeks.Find(x => x.shiftType == m.shiftTypeId && x.jobType == m.jobTypeId);
                        if (shiftWeek == null)
                        {
                            shiftWeek s = new shiftWeek();
                            s.shiftType = m.shiftTypeId;

                            s.AddShift(m);

                            weeks.Add(s);
                        }
                        else
                        {
                            shiftWeek.AddShift(m);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftPlans", e.Message, "");
        }

        if (weeks.Count == 0)
        {
            for (int z = 1; z < 6; z++)
            {
                shiftWeek sw = new shiftWeek();
                sw.shiftType = z;
                for (int i = 0; i < 7; i++)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = 1;
                    s.startDate = startTime.AddDays(i);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(i).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    sw.AddShift(s);
                }
                weeks.Add(sw);
            }
        }
        else
        {
            foreach (var item in weeks)
            {
                if (item.sunday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(0);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(0).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.monday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(1);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(1).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.tuesday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(2);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(2).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.wendsday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(3);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(3).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.thursday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(4);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(4).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.friday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(5);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(5).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }

                if (item.saturday.Count == 0)
                {
                    ShiftDetail s = new ShiftDetail();
                    s.shiftTypeId = item.shiftType;
                    s.startDate = startTime.AddDays(6);
                    s.startDateEN = DateTime.Parse(startTime.AddDays(6).ToString()).ToString("yyyy/MM/dd");
                    s.id = Guid.NewGuid().GetHashCode();
                    item.AddShift(s);
                }
            }
        }

        return weeks;
    }


    internal static List<ShiftDetail> GetShiftPlansDetails(DateTime startTime, int workerId, int addDays = 1, int shiftTypeId = 0)
    {
        List<ShiftDetail> result = new List<ShiftDetail>();
        try
        {
            string whereWorker = "";
            if (workerId > 0)
            {
                whereWorker = " AND workerId= @workerId ";
            }

            string whereShiftTypeId = "";
            if (shiftTypeId > 0) 
            {
                whereShiftTypeId = " AND shiftTypeId= @shiftTypeId ";
            }

            DateTime start = new DateTime(startTime.Year, startTime.Month, startTime.Day);
            DateTime finish = start.AddDays(addDays);

            string sql = "SELECT shiftPlans.[id],[workerId], workers.firstName + ' ' + workers.lastName as workerName,[date],[shiftTypeId],[shiftName],[remark],[cancel] " +
                         "FROM [dbo].[shiftPlans] Inner join Workers On shiftPlans.workerId = Workers.id Inner join shiftTypes On shiftPlans.shiftTypeId = shiftTypes.id " +
                         "WHERE ([date] >= @startTime AND[date] < @finishTime) " + whereWorker + whereShiftTypeId + " AND cancel = 0;";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@startTime", start));
            values.Add(new SqlParameter("@finishTime", finish));
            values.Add(new SqlParameter("@shiftTypeId", shiftTypeId));

            if (workerId > 0)
            {
                values.Add(new SqlParameter("@workerId", workerId));
            }

            DataSet ds = Dal.GetDataSet(sql, values);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ShiftDetail m = new ShiftDetail();
                        m.id = int.Parse(item["id"].ToString());
                        m.shiftTypeId = int.Parse(item["shiftTypeId"].ToString());
                        m.workerId = int.Parse(item["workerId"].ToString());
                        m.workerName = item["workerName"].ToString();
                        m.shiftName = item["shiftName"].ToString();
                        m.startDate = DateTime.Parse(item["date"].ToString());
                        m.startDateEN = DateTime.Parse(item["date"].ToString()).ToString("yyyy/MM/dd");
                        m.remark = item["remark"].ToString();

                        result.Add(m);

                    }
                }
            }
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftPlans", e.Message, "");
        }




        return result;
    }



    internal static List<shiftWeekReport> GetShiftPlansWeekReport(DateTime startTime)
    {
        List<shiftWeekReport> result = new List<shiftWeekReport>();
        try
        {
            DateTime finishDate = startTime.AddDays(7);

            string colNames = "";
            DateTime d = startTime;
            for (int i = 0; i < 7; i++)
            {
                colNames += "[" + d.ToString("dd-MM-yyyy") + "], ";
                d = d.AddDays(1);
            }
            colNames = colNames.Substring(0, colNames.Length - 2);


            string sql = "SELECT * FROM ( " +
"SELECT [workerId], firstName + ' ' + lastName as workerName, convert(varchar, date, 105) as sDate " +
  "FROM[dbo].shiftPlans inner join workers on shiftPlans.workerId = workers.id " +
  "WHERE date >= @startTime AND date <= @finishTime AND cancel = 0 ) t " +
    "PIVOT( " +
    "COUNT ([workerId]) " +
    "FOR [sDate] IN( " + colNames + ")) AS pivot_table ORDER by workerName; ";

            List<SqlParameter> values = new List<SqlParameter>();
            values.Add(new SqlParameter("@startTime", startTime));
            values.Add(new SqlParameter("@finishTime", finishDate));

            DataSet ds = Dal.GetDataSet(sql, values);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        shiftWeekReport m = new shiftWeekReport();
                        m.workerName = item["workerName"].ToString();
                        m.sunday = int.Parse(item[1].ToString());
                        m.monday = int.Parse(item[2].ToString());
                        m.tuesday = int.Parse(item[3].ToString());
                        m.wendsday = int.Parse(item[4].ToString());
                        m.thursday = int.Parse(item[5].ToString());
                        m.friday = int.Parse(item[6].ToString());
                        m.saturday = int.Parse(item[7].ToString());

                        result.Add(m);

                    }
                }
            }
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("WebDal.GetShiftPlansWeekReport", e.Message, "");
        }




        return result;
    }

    private static List<shiftWeek> GetBaseShiftPlanWeeks(List<shiftWeek> result)
    {
        //List<shiftWeek> result = new List<shiftWeek>();
        if (result.Find((a) => a.shiftType == 1) == null)
        {
            result.Add(new shiftWeek { shiftType = 1 });
        }
        if (result.Find((a) => a.shiftType == 2) == null)
        {
            result.Add(new shiftWeek { shiftType = 2 });
        }
        if (result.Find((a) => a.shiftType == 3) == null)
        {
            result.Add(new shiftWeek { shiftType = 3 });
        }
        if (result.Find((a) => a.shiftType == 4) == null)
        {
            result.Add(new shiftWeek { shiftType = 4 });
        }
        if (result.Find((a) => a.shiftType == 5) == null)
        {
            result.Add(new shiftWeek { shiftType = 5 });
        }

        return result;
    }

    internal static List<shiftWorkerPreferencias> GetShiftWorkerPreferencias(int workerId)
    {


        List<shiftWorkerPreferencias> result = new List<shiftWorkerPreferencias>();

        string sql = "SELECT [shiftTypes].[id] as shiftTypeId, [shiftName], shiftWorkerPreferencias.id as prefId, sunday,monday ,tuesday ,wednesday,thursday,friday,saturday,remark " +
                     "FROM [dbo].[shiftTypes] left join shiftWorkerPreferencias on[shiftTypes].id = shiftWorkerPreferencias.shiftTypeId " +
                     "WHERE(shiftWorkerPreferencias.workerId = @workerId OR shiftWorkerPreferencias.workerId is null)";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    shiftWorkerPreferencias m = new shiftWorkerPreferencias();

                    m.shiftType = int.Parse(item["shiftTypeId"].ToString());
                    m.shiftTypeName = item["shiftName"].ToString();
                    if (!string.IsNullOrEmpty(item["prefId"].ToString()))
                    {
                        m.sunday = int.Parse(item["sunday"].ToString());
                        m.monday = int.Parse(item["monday"].ToString());
                        m.tuesday = int.Parse(item["tuesday"].ToString());
                        m.wendsday = int.Parse(item["wendsday"].ToString());
                        m.thursday = int.Parse(item["thursday"].ToString());
                        m.friday = int.Parse(item["friday"].ToString());
                        m.saturday = int.Parse(item["saturday"].ToString());
                    }
                }
            }
        }

        return result;
    }


    internal static List<bcStats> GetWorkersStats()
    {
        List<bcStats> result = new List<bcStats>();

        string sql = "SELECT[workerId], firstName + ' ' + lastName as workerName, Sum(1) as c, Sum(IIF(statusid = 0, 1, 0)) as cOpen, Sum(IIF(statusid = 2, 1, 0)) as cClose, Min(DATEPART(hour, startTime)) as firstHourOpenProblem, " +
                            "Max(DATEPART(hour, startTime)) as lastHourOpenProblem,Min(DATEPART(hour, finishTime)) as firstHourCloseProblem,Max(DATEPART(hour, finishTime)) as lastHourCloseProblem, Sum(IIF(workerId <> toWorker, 1, 0)) as movedProblems, Sum(IIF(workerId = toWorker, 1, 0)) as openAndOnHim " +
                    "FROM[dbo].[problemsClose] inner join workers on[problemsClose].workerId = workers.id " +
                    "WHERE [startTime] >= @startTime " +
                    "group by [workerId], firstName +' ' + lastName " +
                    "order by workerName";

        DateTime startTime = CacheHelper.Instance.GetIsraelTime();
        startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", startTime));

        DataSet ds = Dal.GetDataSet(sql, values);

        //movedProblems, Sum(IIF(workerId = toWorker, 1, 0)) as openAndOnHim
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bcStats m = new bcStats();
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.workerName = item["workerName"].ToString();
                    m.totalProblems = int.Parse(item["c"].ToString());
                    m.openProblems = int.Parse(item["cOpen"].ToString());
                    m.closeProblems = int.Parse(item["cClose"].ToString());
                    m.firstHourOpenProblem = int.Parse(item["firstHourOpenProblem"].ToString());
                    m.firstHourCloseProblem = int.Parse(item["firstHourCloseProblem"].ToString());

                    m.lastHourOpenProblem = int.Parse(item["lastHourOpenProblem"].ToString());
                    m.lastHourCloseProblem = int.Parse(item["lastHourCloseProblem"].ToString());

                    m.movedProblems = int.Parse(item["movedProblems"].ToString());
                    m.openAndOnHim = int.Parse(item["openAndOnHim"].ToString());

                    result.Add(m);
                }
            }
        }

        return result;
    }

    internal static List<JObject> GetWorkersHoursSum()
    {
        List<bcStats> result = new List<bcStats>();
        Dictionary<int, Dictionary<string, int>> temp = new Dictionary<int, Dictionary<string, int>>();


        string sql = "SELECT [workerId], firstName + ' ' + lastName as workerName, Sum(1) as c, DATEPART(hour, startTime) as h " +
                    "FROM[dbo].[problemsClose] inner join workers on[problemsClose].workerId = workers.id " +
                    "WHERE [startTime] >= @startTime " +
                    "group by [workerId], firstName +' ' + lastName, DATEPART(hour, startTime) " +
                    "order by workerName, h";

        DateTime startTime = CacheHelper.Instance.GetIsraelTime();
        startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@startTime", startTime));

        DataSet ds = Dal.GetDataSet(sql, values);

        List<int> hours = new List<int>(23);
        for (int i = 0; i < 24; i++)
        {
            hours.Add(0);
            temp.Add(i, new Dictionary<string, int>());
        }
        //movedProblems, Sum(IIF(workerId = toWorker, 1, 0)) as openAndOnHim
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bcStats m = new bcStats();
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.workerName = item["workerName"].ToString();
                    int c = int.Parse(item["c"].ToString());
                    int h = int.Parse(item["h"].ToString());

                    WorkerBase w = new WorkerBase { Id = m.workerId, firstName = m.workerName };


                    temp[h].Add(m.workerName, c);
                }
            }
        }

        //hours = new List<int>(23);
        //for (int i = 0; i < 24; i++)
        //{
        //    hours.Add(0);
        //}

        List<JObject> hoursFinal = new List<JObject>();
        foreach (var hour in temp)
        {
            JObject j = new JObject();
            j.Add("name", hour.Key);
            foreach (var item in hour.Value)
            {
                string workerName = item.Key.Replace(" ", "_");
                j.Add(workerName, item.Value);
            }
            hoursFinal.Add(j);
        }

        return hoursFinal;
    }


    internal static void UpdateWorkerExpense(WorkerExpenses expense)
    {
        string sql = "UPDATE [dbo].[workerExpenses] SET " +
                     "[expenseValue] = @expenseValue, [startExpenseDate] = @startExpenseDate, [finishExpenseDate] = @finishExpenseDate, [freePass] = @freePass, [remark] = @remark " +
                     "WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", expense.id));
        values.Add(new SqlParameter("@expenseValue", expense.expenseValue));
        values.Add(new SqlParameter("@startExpenseDate", expense.startExpenseDate));
        values.Add(new SqlParameter("@finishExpenseDate", expense.startExpenseDateEN));
        values.Add(new SqlParameter("@freePass", expense.freePass));
        values.Add(new SqlParameter("@remark", expense.remark));


        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void UpdateMsgLine(int id, string msg)
    {
        string sql = "UPDATE [dbo].[problemMsgs] SET [msg] = @msg WHERE id=@id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@msg", msg));


        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void DeleteMsgLine(int id)
    {
        string sql = "DELETE FROM [dbo].[problemMsgs] WHERE id=@id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static List<msgLine> GetProblemMsgs(int problemId)
    {
        List<msgLine> result = new List<msgLine>();

        string sql = "SELECT [workerId], firstName +  ' ' + lastName as workerName, imgPath,[msg],[lineType],[problemMsgs].[commitTime], [problemMsgs].[id] as msgId " +
                    "FROM [dbo].[problemMsgs] inner join workers on[problemMsgs].workerId = workers.id " +
                    "WHERE [problemId] = @problemId AND problemMsgs.[active]=1 " +
                    "ORDER BY commitTime";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));

        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    msgLine m = new msgLine();
                    m.workerId = int.Parse(item["workerId"].ToString());
                    m.workerName = item["workerName"].ToString();
                    m.msg = item["msg"].ToString();
                    m.msgType = int.Parse(item["lineType"].ToString());
                    m.workerImgPath = item["imgPath"].ToString();
                    m.commitTime = item["commitTime"].ToString();
                    m.commitTimeEN = DateTime.Parse(item["commitTime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                    m.id = int.Parse(item["msgId"].ToString());
                    result.Add(m);
                }
            }
        }

        return result;
    }

    internal static void AppendProblemMsg(int problemId, int workerId, string newLine, int lineType)
    {
        string sql = "INSERT INTO [dbo].[problemMsgs] ([problemId],[workerId],[msg],[lineType],[commitTime]) VALUES " +
                        "(@problemId,@workerId,@msg,@lineType,@commitTime);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@msg", newLine));
        values.Add(new SqlParameter("@lineType", lineType));
        values.Add(new SqlParameter("@commitTime", CacheHelper.Instance.GetIsraelTime()));

        Dal.ExecuteNonQuery(sql, values);
    }

    public static DataSet GetPlacesByName(string placeName)
    {
        string sql = "SELECT [id],[placeName],[ip] " +
                     "FROM [dbo].[Places] " +
                     "WHERE placeName LIKE @placeName " +
                     "Order by placeName;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeName", "%" + placeName + "%"));
        DataSet ds = Dal.GetDataSet(sql, values);

        return ds;
    }

    public static void AppendMsgs(int workerId, string msg, List<int> toWorkers)
    {
        string sql = "INSERT INTO [dbo].[Msgs] ([workerId],[msg],[toWorkerId]) VALUES ";


        string s = "";
        foreach (int item in toWorkers)
        {
            s = s + "(" + workerId + ",N'" + msg + "'," + item + "),";
        }
        s = s.Substring(0, s.Length - 1);

        sql += s;
        Dal.ExecuteNonQuery(sql);
    }

    public static DataSet GetLogsDs(int problemId)
    {
        string sql = "SELECT [problemsLog].[id],[groupKey],[problemsLog].[problemId],[problemsLog].[workerId], firstName + ' ' + lastName as workerName,[fieldName],[originalValue],[newValue],[problemsLog].[commitTime] " +
                     "FROM [dbo].[problemsLog] inner join workers on[problemsLog].[workerId] = workers.id " +
                     "WHERE [problemId] = @problemId " +
                     "Order By id desc;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        DataSet ds = Dal.GetDataSet(sql, values);
        return ds;
    }

    public static List<ProblemLog> GetProblemLogs(int problemId)
    {
        List<ProblemLog> result = new List<ProblemLog>();
        DataSet ds = GetLogsDs(problemId);

        DataTable dsWorkers = WebDal.GetWorkersDS(true).Tables[0];

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                //string groupKey = string.Empty;
                foreach (DataRow item in dt.Rows)
                {
                    string groupKey = item["groupKey"].ToString();
                    string colName = item["fieldName"].ToString();
                    string originalValue = item["originalValue"].ToString();
                    string newValue = item["newValue"].ToString();

                    ProblemLog p = new ProblemLog();
                    p.workerId = int.Parse(item["workerID"].ToString());
                    p.workerName = item["workerName"].ToString();
                    p.oldValue = item["originalValue"].ToString();
                    p.newValue = item["newValue"].ToString();
                    p.commitTime = item["commitTime"].ToString();

                    p.fieldName = GetFieldName(colName);

                    switch (colName)
                    {
                        case "toWorker":
                            DataRow[] rows = dsWorkers.Select("id=" + originalValue);
                            if (rows.Length > 0)
                            {
                                p.oldValue = rows[0]["firstName"] + " " + rows[0]["lastName"];
                            }

                            rows = dsWorkers.Select("id=" + newValue);
                            if (rows.Length > 0)
                            {
                                p.newValue = rows[0]["firstName"] + " " + rows[0]["lastName"];
                            }
                            break;
                        case "departmentId":
                            p.oldValue = GetDeparmentName(p.oldValue);
                            p.newValue = GetDeparmentName(p.newValue);
                            break;
                        case "emergencyId":
                            p.oldValue = GetEmergencyName(p.oldValue);
                            p.newValue = GetEmergencyName(p.newValue);
                            break;
                        default:
                            break;
                    }

                    result.Add(p);

                }
            }


        }

        return result;
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
            case "callCustomerBack":
                fieldName = "לחזור ללקוח";
                break;

            default:
                break;
        }

        return fieldName;
    }


    private static string GetDeparmentName(string departmentId)
    {
        string fieldName = "כללי";
        switch (departmentId)
        {
            case "0":
                fieldName = "כללי";
                break;
            case "1":
                fieldName = "כללי";
                break;
            case "2":
                fieldName = "טכני";
                break;
            case "3":
                fieldName = "תוכנה";
                break;
            case "4":
                fieldName = "תפריטים";
                break;
            case "5":
                fieldName = "איפוסים";
                break;
            case "6":
                fieldName = "שדרוגים";
                break;
            case "7":
                fieldName = "הנהלת חשבונות";
                break;
            case "8":
                fieldName = "שיווק";
                break;
            case "9":
                fieldName = "תמיכה";
                break;
            case "10":
                fieldName = "ציוד";
                break;
            case "11":
                fieldName = "יוזרים";
                break;
            case "12":
                fieldName = "קיוסק";
                break;
            case "13":
                fieldName = "dejavoo";
                break;
            case "14":
                fieldName = "בעיה אצל הלקוח";
                break;
            case "15":
                fieldName = "שרת משלוחים";
                break;
            case "16":
                fieldName = "ענן";
                break;


            default:
                break;
        }

        return fieldName;
    }

    private static string GetEmergencyName(string emergencyId)
    {
        string fieldName = "לא דחוף";
        if (emergencyId == "1")
        {
            fieldName = "דחוף";
        }

        return fieldName;
    }


    public static void AppendProblemFile(int problemID, string filename, string desc)
    {
        string sql = "INSERT INTO [dbo].[problemFiles] ([problemID],[filePath],[fileName],[fileDesc]) VALUES " +
                     "(@problemID,@filePath,@fileName,@fileDesc);";
        //SELECT [id],[filePath],[fileName],[fileDesc] FROM [dbo].[problemFiles]
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemID", problemID));
        values.Add(new SqlParameter("@filePath", filename));
        values.Add(new SqlParameter("@fileName", filename));
        values.Add(new SqlParameter("@fileDesc", desc));

        Dal.ExecuteNonQuery(sql, values);
    }


    public static void DeleteProblemFile(int problemID, string filename)
    {
        string sql = "DELETE FROM problemFiles WHERE problemID=@problemID AND fileName=@fileName;";
        //SELECT [id],[filePath],[fileName],[fileDesc] FROM [dbo].[problemFiles]
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemID", problemID));
        values.Add(new SqlParameter("@fileName", filename));

        Dal.ExecuteNonQuery(sql, values);
    }


    public static DataSet GetFilesDS(int problemId)
    {
        string sql = "SELECT [id],[filePath],[fileName],[fileDesc] " +
                     "FROM [dbo].[problemFiles] " +
                     "WHERE [problemId] = @problemId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@problemId", problemId));
        return Dal.GetDataSet(sql, values);

    }

    public static void UpdateMsgStatus(int msgId, bool opened)
    {
        string sql = "UPDATE [dbo].[Msgs] SET [Opened] = @Opened WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", msgId));
        values.Add(new SqlParameter("@Opened", opened));

        Dal.ExecuteNonQuery(sql, values);
    }

    //public static void DeleteProblem(problemTableType pTableType, long problemId)
    //{
    //    string sql = "DELETE FROM " + pTableType.ToString() + " WHERE(id = @id)";
    //    List<SqlParameter> values = new List<SqlParameter>();
    //    values.Add(new SqlParameter("@id", problemId));
    //    Dal.ExecuteNonQuery(sql, values);
    //}

    public static int AppendPhone(string phone)
    {
        string sql = "INSERT INTO[dbo].[Phones] ([phone]) VALUES (@phone) SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phone", phone));

        object o = Dal.ExecuteScalar(sql, values);

        int phoneId = 0;
        if (o != null)
        {
            phoneId = int.Parse(o.ToString());
        }

        return phoneId;
    }

    public static int AppendPlace(string placeName, string ip, string remark, bool vip = false)
    {
        string sql = "INSERT INTO[dbo].[Places] ([placeName], [Ip], [remark],[vip]) VALUES (@placeName,@ip, @remark,@vip) SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@ip", ip));
        values.Add(new SqlParameter("@remark", remark));
        values.Add(new SqlParameter("@vip", vip));

        object o = Dal.ExecuteScalar(sql, values);

        int placeId = 0;
        if (o != null)
        {
            placeId = int.Parse(o.ToString());
        }

        return placeId;
    }

    public static int AppendWorker(string firstName, string lastName, string phone, int workerTypeID, string userName, string password, int userTypeId, int shluha, bool active)
    {
        string sql = "INSERT INTO [dbo].[workers] ([firstName],[lastName],[phone],[workerTypeID],[userName],[password],[userTypeId],[shluha],[active],[guidKey]) " +
                  "VALUES (@firstName, @lastName, @phone, @workerTypeID, @userName, @password, @userTypeId, @shluha, @active, @guidKey) " +
                  "SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@firstName", firstName));
        values.Add(new SqlParameter("@lastName", lastName));
        values.Add(new SqlParameter("@phone", phone));
        //values.Add(new SqlParameter("@birthDay", birthDay));
        values.Add(new SqlParameter("@workerTypeID", workerTypeID));
        values.Add(new SqlParameter("@userName", userName));
        values.Add(new SqlParameter("@password", password));
        values.Add(new SqlParameter("@userTypeId", userTypeId));
        values.Add(new SqlParameter("@shluha", shluha));
        values.Add(new SqlParameter("@active", active));
        values.Add(new SqlParameter("@guidKey", Guid.NewGuid().ToString()));

        object o = Dal.ExecuteScalar(sql, values);

        int workerId = 0;
        if (o != null)
        {
            workerId = int.Parse(o.ToString());
        }


        if (workerId > 0)
        {
            //מוסיף בסיס
            AppendworkerDepartmentsStater(workerId);
        }


        return workerId;
    }

    public static int AppendWorker(Worker w)
    {
        string sql = "INSERT INTO [dbo].[workers] ([firstName],[lastName],[phone],[workerTypeID],[userName],[password],[userTypeId],[shluha],[wDepartmentId],[jobTitle]) " +
                                          "VALUES (@firstName, @lastName, @phone, @workerTypeID, @userName, @password, @userTypeId, @shluha,@wDepartmentId,@jobTitle) " +
                  "SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@firstName", w.firstName));
        values.Add(new SqlParameter("@lastName", w.lastName));
        values.Add(new SqlParameter("@phone", w.phone));
        //values.Add(new SqlParameter("@birthDay", birthDay));
        values.Add(new SqlParameter("@workerTypeID", w.workerTypeID));
        values.Add(new SqlParameter("@userName", w.userName));
        values.Add(new SqlParameter("@password", w.password));
        values.Add(new SqlParameter("@userTypeId", w.userTypeId));
        values.Add(new SqlParameter("@shluha", w.shluha));
        values.Add(new SqlParameter("@wDepartmentId", w.departmentId));

        if (string.IsNullOrEmpty(w.jobTitle))
        {
            w.jobTitle = "";
        }
        values.Add(new SqlParameter("@jobTitle", w.jobTitle));

        object o = Dal.ExecuteScalar(sql, values);

        int workerId = 0;
        if (o != null)
        {
            workerId = int.Parse(o.ToString());
        }


        if (workerId > 0)
        {
            //מוסיף בסיס
            AppendworkerDepartmentsStater(workerId);
        }


        return workerId;
    }


    public static void AppendworkerDepartmentsStater(int workerId)
    {
        string sql = "INSERT INTO[dbo].[workerDepartments] ([workerId],[departmentId],[canSee]) " +
                     "SELECT " + workerId + ",id,0 FROM[dbo].departments WHERE (id> 1) ORDER BY id";

        Dal.ExecuteNonQuery(sql);

    }

    public static void AppendLog(string groupKey, long problemId, int workerId, string fieldName, string originalValue, string newValue)
    {
        //[problemId],[workerId],[fieldName],[originalValue],[newValue]
        //@problemId, @workerId, @fieldName, @originalValue, @newValue
        string sql = "INSERT INTO[dbo].[problemsLog] ([groupKey],[problemId],[workerId],[fieldName],[originalValue],[newValue]) " +
                  "VALUES (@groupKey, @problemId, @workerId, @fieldName, @originalValue, @newValue)";


        sql = "INSERT INTO [dbo].[problemsLog] ([groupKey],[problemId],[workerId],[fieldName],[originalValue],[newValue]) " +
                  "VALUES (@groupKey, @problemId, @workerId, @fieldName, @originalValue, @newValue)";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter { ParameterName = "@groupKey", Value = groupKey });
        values.Add(new SqlParameter { ParameterName = "@problemId", Value = problemId });
        values.Add(new SqlParameter { ParameterName = "@workerId", Value = workerId });
        values.Add(new SqlParameter { ParameterName = "@fieldName", Value = fieldName });
        values.Add(new SqlParameter { ParameterName = "@originalValue", Value = originalValue });
        values.Add(new SqlParameter { ParameterName = "@newValue", Value = newValue });

        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdatePlaceIp(int placeId, string ip)
    {
        string sql = "UPDATE [dbo].[Places] Set [ip]= @ip WHERE (id = @id);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", placeId));
        values.Add(new SqlParameter("@ip", ip));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdatePlaceIp(string placeName, string ip)
    {
        string sql = "UPDATE [dbo].[Places] Set [ip]= @ip WHERE (placeName = @placeName);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", placeName));
        values.Add(new SqlParameter("@ip", ip));
        Dal.ExecuteNonQuery(sql, values);
    }


    public static void UpdatePlace(int placeId, string placeName, bool vip, string remark)
    {
        string sql = "UPDATE [dbo].[Places] Set [placeName]= @placeName, [vip]= @vip, [remark]= @remark WHERE (id = @id);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", placeId));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@vip", vip));
        values.Add(new SqlParameter("@remark", remark));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdatePhonePlace(int placeId, int phoneId, string customerName)
    {
        string sql = "UPDATE [dbo].[PhonePlace] Set [customerName]= @customerName WHERE (phoneId = @phoneId) AND (placeId = @placeId);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phoneId", phoneId));
        values.Add(new SqlParameter("@placeId", placeId));
        values.Add(new SqlParameter("@customerName", customerName));
        Dal.ExecuteNonQuery(sql, values);
    }



    public static void UpdateWorkerInfo(int workerId, string firstName, string lastName, string phone, string userName, string password, int shluha, bool active, int userTypeId, string jobTitle, string carType = "", string carNumber = "")
    {
        string sql = "UPDATE [dbo].[workers] Set [firstName]= @firstName, [lastName]= @lastName, [phone]= @phone, " +
                            "[userName]= @userName, [password]= @password, [shluha] = @shluha, [active]= @active, [userTypeId]= @userTypeId, [jobTitle]= @jobTitle, " +
                                "[carType] = @carType, [carNumber]= @carNumber " +
                            "WHERE (id = @id);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", workerId));
        values.Add(new SqlParameter("@firstName", firstName));
        values.Add(new SqlParameter("@lastName", lastName));
        values.Add(new SqlParameter("@phone", phone));
        values.Add(new SqlParameter("@userName", userName));
        values.Add(new SqlParameter("@password", password));
        values.Add(new SqlParameter("@shluha", shluha));
        values.Add(new SqlParameter("@active", active));
        values.Add(new SqlParameter("@userTypeId", userTypeId));
        values.Add(new SqlParameter("@jobTitle", jobTitle));

        values.Add(new SqlParameter("@carType", carType));
        values.Add(new SqlParameter("@carNumber", carNumber));

        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdateWorkerInfo(Worker w)
    {
        if (string.IsNullOrEmpty(w.teudatZehut))
        {
            w.teudatZehut = "";
        }

        string sql = "UPDATE [dbo].[workers] Set [firstName]= @firstName, [lastName]= @lastName, [phone]= @phone, " +
                            "[userName]= @userName, [password]= @password, [shluha] = @shluha, [active]= @active, [userTypeId]= @userTypeId, [jobTitle]= @jobTitle, " +
                                "[carType] = @carType, [carNumber]= @carNumber, [workerLevel]= @workerLevel, [teudatZehut]= @teudatZehut, [wDepartmentId]= @wDepartmentId, [marselWorkerCode]= @marselWorkerCode " +
                            "WHERE (id = @id);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", w.Id));
        values.Add(new SqlParameter("@firstName", w.firstName));
        values.Add(new SqlParameter("@lastName", w.lastName));
        values.Add(new SqlParameter("@phone", w.phone));
        values.Add(new SqlParameter("@userName", w.userName));
        values.Add(new SqlParameter("@password", w.password));
        values.Add(new SqlParameter("@shluha", w.shluha));
        values.Add(new SqlParameter("@active", w.active));
        values.Add(new SqlParameter("@userTypeId", w.userTypeId));
        if (string.IsNullOrEmpty(w.jobTitle))
        {
            w.jobTitle = "";
        }
        values.Add(new SqlParameter("@jobTitle", w.jobTitle));
        values.Add(new SqlParameter("@teudatZehut", w.teudatZehut));
        values.Add(new SqlParameter("@workerLevel", w.workerLevel));
        values.Add(new SqlParameter("@wDepartmentId", w.departmentId));
        values.Add(new SqlParameter("@marselWorkerCode", w.marselWorkerCode));

        if (string.IsNullOrEmpty(w.carType))
        {
            w.carType = "";
        }
        values.Add(new SqlParameter("@carType", w.carType));
        if (string.IsNullOrEmpty(w.carNumber))
        {
            w.carNumber = "";
        }
        values.Add(new SqlParameter("@carNumber", w.carNumber));

        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdateTodayProblemsCountAddOne()
    {
        string sql = "UPDATE [dbo].[TodayProblemsCount] SET [id] = [id]+1";
        List<SqlParameter> values = new List<SqlParameter>();
        Dal.ExecuteNonQuery(sql);
    }

    public static void UpdateTodayProblemsCountSetZero()
    {
        string sql = "UPDATE [dbo].[TodayProblemsCount] SET [id] = 0";
        List<SqlParameter> values = new List<SqlParameter>();
        Dal.ExecuteNonQuery(sql);
    }

    public static int AppendPhonePlace(int phoneId, int placeId, string customerName)
    {
        int result = 0;
        string sql = "INSERT INTO[dbo].[PhonePlace] ([phoneId],[placeId],[customerName]) " +
               "VALUES (@phoneId, @placeId, @customerName) " +
               "SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter { ParameterName = "@phoneId", Value = phoneId });
        values.Add(new SqlParameter { ParameterName = "@placeId", Value = placeId });
        values.Add(new SqlParameter { ParameterName = "@customerName", Value = customerName });

        object o = Dal.ExecuteScalar(sql, values);
        if (o != null)
        {
            result = int.Parse(o.ToString());
        }

        return result;
    }

    public static void UpdateProblem(long problemId, string ip, int placeNameId, string placeName, string customerName,
         string problemDesc, string problemSolution, int toWorker, int statusId, int emergencyId, int departmentId, bool reportToYaron, DateTime @finishTime, int updaterWorkerId)
    {
        string sql = "UPDATE [dbo].[problemsClose] Set [ip]= @ip,[placeNameId]= @placeNameId,[placeName]= @placeName,[customerName]= @customerName," +
                            "[problemDesc]= @problemDesc,[problemSolution]= @problemSolution,[toWorker]= @toWorker,[statusId]= @statusId,[emergencyId]= @emergencyId,[departmentId]= @departmentId, " +
                            "[reportToYaron]= @reportToYaron,[finishTime]= @finishTime, [updaterWorkerId]= @updaterWorkerId, [takingCare]= @takingCare " +
                     "WHERE (id = @id);";


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", problemId));
        values.Add(new SqlParameter("@ip", ip));
        values.Add(new SqlParameter("@placeNameId", placeNameId));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@customerName", customerName));
        values.Add(new SqlParameter("@problemDesc", problemDesc));
        values.Add(new SqlParameter("@problemSolution", problemSolution));
        values.Add(new SqlParameter("@statusId", statusId));
        values.Add(new SqlParameter("@toWorker", toWorker));
        values.Add(new SqlParameter("@emergencyId", emergencyId));
        values.Add(new SqlParameter("@departmentId", departmentId));
        values.Add(new SqlParameter("@reportToYaron", reportToYaron));
        values.Add(new SqlParameter("@finishTime", finishTime));
        values.Add(new SqlParameter("@updaterWorkerId", updaterWorkerId));
        values.Add(new SqlParameter("@takingCare", false));


        Dal.ExecuteNonQuery(sql, values);
    }

    public static void UpdateProblemWS(long problemId, string ip, string placeName, string customerName,
     string problemDesc, string problemSolution, int toWorker, int statusId, int departmentId, int emergencyId, DateTime @finishTime, int updaterWorkerId, bool takingCare, bool isLocked, bool callCustomerBack)
    {
        string sql = "UPDATE [dbo].[problemsClose] Set [ip]= @ip,[placeName]= @placeName,[customerName]= @customerName," +
                            "[problemDesc]= @problemDesc,[problemSolution]= @problemSolution,[toWorker]= @toWorker,[statusId]= @statusId,[departmentId]= @departmentId, " +
                            "[emergencyId]= @emergencyId,[finishTime]= @finishTime, [updaterWorkerId]= @updaterWorkerId, [takingCare] = @takingCare, [isLocked] = @isLocked, [callCustomerBack]= @callCustomerBack " +
                     "WHERE (id = @id);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", problemId));
        values.Add(new SqlParameter("@ip", ip));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@customerName", customerName));
        values.Add(new SqlParameter("@problemDesc", problemDesc));
        values.Add(new SqlParameter("@problemSolution", problemSolution));
        values.Add(new SqlParameter("@statusId", statusId));
        values.Add(new SqlParameter("@toWorker", toWorker));
        values.Add(new SqlParameter("@departmentId", departmentId));
        values.Add(new SqlParameter("@emergencyId", emergencyId));
        values.Add(new SqlParameter("@finishTime", finishTime));
        values.Add(new SqlParameter("@updaterWorkerId", updaterWorkerId));
        values.Add(new SqlParameter("@takingCare", takingCare));
        values.Add(new SqlParameter("@isLocked", isLocked));
        values.Add(new SqlParameter("@callCustomerBack", callCustomerBack));


        Dal.ExecuteNonQuery(sql, values);
    }

    //public static void UpdateProblemsCloseTime(long problemId, DateTime finishTime)
    //{
    //    string sql = "UPDATE [dbo].[problemsClose] Set [finishTime]= @finishTime WHERE (id = @id);";
    //    List<SqlParameter> values = new List<SqlParameter>();
    //    values.Add(new SqlParameter("@id", problemId));
    //    values.Add(new SqlParameter("@finishTime", finishTime));
    //    Dal.ExecuteNonQuery(sql, values);
    //}


    public static ProblemsCountSummery GetProblemsCountsummeryOlder(int workerId)
    {
        //כמות תקלות לפי סטטוס
        ProblemsCountSummery result = new ProblemsCountSummery();
        string sql = "SELECT statusId, SUM(1) AS pCount FROM problemsClose WHERE (toWorker = @workerId) AND (statusId=0 OR statusId=1) GROUP BY statusId;";

        DataSet ds = Dal.GetDataSet(sql, new List<SqlParameter> { new SqlParameter { ParameterName = "@workerId", Value = workerId } });
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int statusId = int.Parse(row["statusId"].ToString());
                    int count = int.Parse(row["pCount"].ToString());
                    if (statusId == 0)
                    {
                        result.openProblems = count;
                    }

                    if (statusId == 1)
                    {
                        result.HandlingProblems = count;
                    }
                }
            }
        }

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

        //כמות תקלות שנפתחו היום
        sql = "Select id from [dbo].[TodayProblemsCount]";
        object o = Dal.ExecuteScalar(sql);
        if (o != null)
        {
            try
            {

                result.todayProblems = int.Parse(o.ToString());
            }
            catch (Exception)
            {

            }
        }

        //תקלות שפתחתי
        sql = "SELECT SUM(1) AS pCount FROM problemsClose WHERE (statusId = 0 OR statusId = 1) AND workerId=" + workerId;
        o = Dal.GetThisOneValue(sql, null, 0);
        if (o != null)
        {
            try
            {
                result.Iopened = int.Parse(o.ToString());
            }
            catch (Exception)
            {

            }
        }

        //תקלות לפי מחלקות
        sql = "SELECT departmentId, reportToYaron, SUM(1) AS pCount FROM problemsClose WHERE (statusId = 0 OR statusId = 1) GROUP BY departmentId, reportToYaron";
        ds = Dal.GetDataSet(sql, new List<SqlParameter> { new SqlParameter { ParameterName = "@workerId", Value = workerId } });
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int departmentId = int.Parse(row["departmentId"].ToString());
                    int count = int.Parse(row["pCount"].ToString());
                    bool reportToYaron = bool.Parse(row["reportToYaron"].ToString());

                    if (reportToYaron)
                    {
                        result.reportToYaron += count;
                    }

                    if (departmentId == 0)
                    {
                        result.privateToWorker += count;
                        continue;
                    }

                    if (departmentId == 1)
                    {
                        result.general += count;
                        continue;
                    }
                    if (departmentId == 2)
                    {
                        result.tech += count;
                        continue;
                    }

                    if (departmentId == 3)
                    {
                        result.software += count;
                        continue;
                    }

                    if (departmentId == 4)
                    {
                        result.menu += count;
                        continue;
                    }
                    if (departmentId == 5)
                    {
                        result.resets += count;
                        continue;
                    }

                    if (departmentId == 6)
                    {
                        result.upgrades += count;
                        continue;
                    }

                    if (departmentId == 7)
                    {
                        result.counting += count;
                        continue;
                    }

                    if (departmentId == 8)
                    {
                        result.marketing += count;
                        continue;
                    }

                    if (departmentId == 9)
                    {
                        result.returnToClient += count;
                        continue;
                    }

                    if (departmentId == 10)
                    {
                        result.developers += count;
                        continue;
                    }

                    if (departmentId == 11)
                    {
                        result.users += count;
                        continue;
                    }

                    if (departmentId == 12)
                    {
                        result.kiosk += count;
                        continue;
                    }

                    if (departmentId == 13)
                    {
                        result.dejavoo += count;
                        continue;
                    }

                    if (departmentId == 15)
                    {
                        result.delivery_server += count;
                        continue;
                    }

                }
            }
        }
        return result;
    }


    public static ProblemsCountSummery GetProblemsCountsummeryOld(int workerId)
    {
        //כמות תקלות לפי סטטוס
        ProblemsCountSummery result = new ProblemsCountSummery();
        string sql = "SELECT [departmentId], sum(1) as pCount, Sum(IIF(workerId=" + workerId + ",1,0)) as meCount, Sum(IIF(toWorker=" + workerId + ",1,0)) as onMeCount, Sum(IIF([startTime]>=GETDATE()-1,1,0)) as todayCount " +
                "FROM[dbo].[problemsClose] " +
                "WHERE statusId<>2 " +
                "group by[departmentId];";

        sql += "Select id from [dbo].[TodayProblemsCount]";

        DataSet ds = Dal.GetDataSet(sql, new List<SqlParameter> { new SqlParameter { ParameterName = "@workerId", Value = workerId } });
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int departmentId = int.Parse(row["departmentId"].ToString());
                    int count = int.Parse(row["pCount"].ToString());
                    int meCount = int.Parse(row["meCount"].ToString());
                    int onMeCount = int.Parse(row["onMeCount"].ToString());
                    int todayCount = int.Parse(row["todayCount"].ToString());

                    result.Iopened += meCount;
                    result.openProblems += onMeCount;

                    if (departmentId == 0)
                    {
                        result.privateToWorker += count;
                        continue;
                    }

                    if (departmentId == 1)
                    {
                        result.general += count;
                        continue;
                    }
                    if (departmentId == 2)
                    {
                        result.tech += count;
                        continue;
                    }

                    if (departmentId == 3)
                    {
                        result.software += count;
                        continue;
                    }

                    if (departmentId == 4)
                    {
                        result.menu += count;
                        continue;
                    }
                    if (departmentId == 5)
                    {
                        result.resets += count;
                        continue;
                    }

                    if (departmentId == 6)
                    {
                        result.upgrades += count;
                        continue;
                    }

                    if (departmentId == 7)
                    {
                        result.counting += count;
                        continue;
                    }

                    if (departmentId == 8)
                    {
                        result.marketing += count;
                        continue;
                    }

                    if (departmentId == 9)
                    {
                        result.returnToClient += count;
                        continue;
                    }

                    if (departmentId == 10)
                    {
                        result.developers += count;
                        continue;
                    }

                    if (departmentId == 11)
                    {
                        result.users += count;
                        continue;
                    }

                    if (departmentId == 12)
                    {
                        result.kiosk += count;
                        continue;
                    }

                    if (departmentId == 13)
                    {
                        result.dejavoo += count;
                        continue;
                    }

                    if (departmentId == 15)
                    {
                        result.delivery_server += count;
                        continue;
                    }

                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                result.todayProblems = int.Parse(ds.Tables[1].Rows[0][0].ToString());
            }
        }
        return result;
    }


    public static ProblemsCountSummery GetProblemsCountsummery(int workerId)
    {
        DateTime lastMonth = DateTime.Now.AddDays(-1);
        //כמות תקלות לפי סטטוס
        ProblemsCountSummery result = new ProblemsCountSummery();
        string sql = "SELECT sum(1) as pCount, Sum(IIF(workerId=" + workerId + ",1,0)) as meCount, Sum(IIF(toWorker=" + workerId + ",1,0)) as onMeCount, Sum(IIF([startTime]>=GETDATE()-1,1,0)) as todayCount " +
                "FROM [dbo].[problemsClose] " +
                "WHERE statusId<>2;";

        sql += "Select id from [dbo].[TodayProblemsCount]";

        //sql += "SELECT [problemsClose].[departmentId],departmentName,sum(1) as pCount " +
        //       "FROM [dbo].[problemsClose] " +
        //            "inner join [workerDepartments] on [problemsClose].[departmentId] = [workerDepartments].[departmentId] " +
        //            "inner join departments on [workerDepartments].[departmentId]= departments.id " +
        //        "WHERE statusId<>2 AND (workerDepartments.workerId=@workerId) " +
        //        "Group by [problemsClose].[departmentId],departmentName";

        sql += "SELECT [problemsClose].[departmentId],sum(1) as pCount " +
               "FROM [dbo].[problemsClose] " +
                    "inner join [workerDepartments] on [problemsClose].[departmentId] = [workerDepartments].[departmentId] " +
                "WHERE statusId<>2 AND (workerDepartments.workerId=@workerId) AND (workerDepartments.canSee=1) " +
                "Group by [problemsClose].[departmentId]";


        //יותר מהיר לבקש את המחלקות לעובד בנפרד, מאשר בשאילתה
        sql += "SELECT [workerDepartments].[departmentId],departmentName " +
               "FROM [workerDepartments] inner join departments on [workerDepartments].[departmentId]= departments.id " +
               "WHERE (workerDepartments.workerId = @workerId) AND (workerDepartments.canSee=1);";

        sql += "SELECT Sum(1) as c " +
               "FROM [dbo].[problemsClose] " +
               "WHERE workerId = @workerId and finishTime >= @lastMonth and statusId = 2;";

        //סיכום תקלות לשבוע האחרון למחלקה שלי
        sql += "SELECT[problemsClose].[departmentId],sum(1) as pCount " +
                "FROM [dbo].[problemsClose] inner join[workers] on[problemsClose].[departmentId] = [workers].[wdepartmentId] " +
                "WHERE ([problemsClose].startTime >= GETDATE() - 7) AND ([workers].id = @workerId) " +
                "Group by[problemsClose].[departmentId];";

        //סיכום תקלות לשבוע האחרון למחלקה שלי
        sql += "SELECT [problemId] " +
                "FROM [dbo].[problemTracking] " +
                "WHERE workerId = @workerId " +
                "GROUP BY [problemId]";

        //להשיג כמות תקלות שהעובד מעורב בהן
        sql += "SELECT sum(1) as pCount " +
                "FROM [dbo].[problemsClose] left join problemWorkers on problemWorkers.problemId = [problemsClose].id " +
                "WHERE statusId<>2 AND problemWorkers.workerId = @workerId;";

        ProblemSummery psOpen = new ProblemSummery();

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@lastMonth", lastMonth));

        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    //int departmentId = int.Parse(dr["departmentId"].ToString());
                    int count = int.Parse(dr["pCount"].ToString());
                    int meCount = int.Parse(dr["meCount"].ToString());
                    int onMeCount = int.Parse(dr["onMeCount"].ToString());
                    int todayCount = int.Parse(dr["todayCount"].ToString());

                    result.Iopened += meCount;
                    result.openProblems += onMeCount;


                    ProblemSummery ps = new ProblemSummery();
                    ps.departmentId = -1;
                    ps.departmentName = "שלי";
                    ps.count = result.openProblems;
                    ps.departmentValue = "-1";
                    result.departments.Add(ps);

                    ps = new ProblemSummery();
                    ps.departmentId = -3;
                    ps.departmentName = "פתחתי";
                    ps.count = result.Iopened;
                    ps.departmentValue = "-3";
                    result.departments.Add(ps);


                    psOpen = new ProblemSummery();
                    psOpen.departmentId = 0;
                    psOpen.departmentName = "פתוח";
                    psOpen.count = count;
                    psOpen.departmentValue = "0";

                }
            }

            if (ds.Tables[7].Rows.Count >= 0)
            {
                int z = 0;
                int.TryParse(ds.Tables[7].Rows[0]["pCount"].ToString(), out z);

                result.imInvolved = z;

                ProblemSummery ps = new ProblemSummery();
                ps.departmentId = -8;
                ps.departmentName = "מעורב";
                ps.count = result.imInvolved;
                ps.departmentValue = "-8";
                result.departments.Add(ps);
            }

            //מוסיף את "אני סגרתי" בסוף
            if (ds.Tables[4].Rows.Count > 0)
            {
                int i = 0;
                int.TryParse(ds.Tables[4].Rows[0][0].ToString(), out i);
                //result.todayProblems = i;
                ProblemSummery ps = new ProblemSummery();
                ps.departmentId = -5;
                ps.departmentName = "סגרתי";
                ps.count = i;
                ps.departmentValue = "-5";
                result.departments.Add(ps);
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[3].Rows)
                {
                    ProblemSummery ps = new ProblemSummery();
                    ps.departmentId = int.Parse(item["departmentId"].ToString());
                    if (ps.departmentId != 1)
                    {
                        ps.departmentName = (item["departmentName"].ToString());
                        ps.departmentValue = ps.departmentId.ToString();
                        result.departments.Add(ps);
                    }
                }
            }


            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[2].Rows)
                {
                    //ProblemSummery ps = new ProblemSummery();
                    int departmentId = int.Parse(item["departmentId"].ToString());
                    foreach (var dep in result.departments)
                    {
                        if (dep.departmentId == departmentId)
                        {

                            dep.count = int.Parse(item["pCount"].ToString());
                            dep.departmentValue = (item["departmentId"].ToString());
                        }
                    }
                }
            }

            //מוסיף את "פתוח" בסוף
            result.departments.Add(psOpen);

            //מוסיף את "היום" בסוף
            if (ds.Tables[1].Rows.Count > 0)
            {
                result.todayProblems = int.Parse(ds.Tables[1].Rows[0][0].ToString());

                ProblemSummery ps = new ProblemSummery();
                ps.departmentId = -4;
                ps.departmentName = "היום";
                ps.count = result.todayProblems;
                ps.departmentValue = "-4";
                result.departments.Add(ps);
            }

            //מוסיף את "השבוע במחלקה" בסוף
            if (ds.Tables[5].Rows.Count > 0)
            {
                result.todayProblems = int.Parse(ds.Tables[5].Rows[0][1].ToString());

                ProblemSummery ps = new ProblemSummery();
                ps.departmentId = -6;
                ps.departmentName = "השבוע";
                ps.count = result.todayProblems;
                ps.departmentValue = "-6";
                result.departments.Add(ps);
            }

            //מוסיף את "השבוע במחלקה" בסוף
            if (ds.Tables[6].Rows.Count >= 0)
            {
                result.trackingProblems = ds.Tables[6].Rows.Count;

                ProblemSummery ps = new ProblemSummery();
                ps.departmentId = -7;
                ps.departmentName = "מעקב";
                ps.count = result.trackingProblems;
                ps.departmentValue = "-7";
                result.departments.Add(ps);
            }
        }
        return result;
    }


    public static List<Phone> GetPhones(out Dictionary<string, int> phonesDic)
    {
        List<Phone> result = new List<Phone>();
        phonesDic = new Dictionary<string, int>();
        string sql = "SELECT [id], [phone] FROM [dbo].[Phones] Order By phone";

        DataSet ds = Dal.GetDataSet(sql);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Phone p = new Phone();
                    p.Id = int.Parse(item["id"].ToString());
                    p.phone = item["phone"].ToString();
                    result.Add(p);

                    if (!phonesDic.ContainsKey(p.phone))
                    {
                        phonesDic.Add(p.phone, p.Id);
                    }
                }
            }
        }
        return result;
    }

    public static int GetPhoneId(string phone)
    {
        int result = 0;

        string sql = "SELECT TOP 1 [id] FROM [dbo].[Phones] WHERE phone = @phone";

        object o = Dal.ExecuteScalar(sql, new List<SqlParameter> { new SqlParameter("@phone", phone) });
        if (o != null)
        {
            string s = o.ToString();
            if (!int.TryParse(s, out result))
            {
                result = 0;
            }
        }
        return result;
    }

    public static DataSet GetPlaces()
    {
        string sql = "SELECT [id], [placeName], [ip], vip, workerId " +
                     "FROM [dbo].[Places] " +
                     "Order By placeName";

        DataSet ds = Dal.GetDataSet(sql);
        return ds;
    }

    public static List<Place> GetPlaces(out Dictionary<string, int> placesDic)
    {
        List<Place> result = new List<Place>();
        placesDic = new Dictionary<string, int>();
        //string sql = "SELECT [id], [placeName], [ip] FROM [dbo].[Places] Order By placeName";
        DataSet ds = GetPlaces();// Dal.GetDataSet(sql);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Place p = new Place();
                    p.Id = int.Parse(item["id"].ToString());
                    p.placeName = item["placeName"].ToString();
                    p.ip = item["ip"].ToString();
                    result.Add(p);

                    if (!placesDic.ContainsKey(p.placeName))
                    {
                        placesDic.Add(p.placeName, p.Id);
                    }
                }
            }
        }
        return result;
    }

    public static int GetPlaceId(string placeName)
    {
        int result = 0;

        string sql = "SELECT TOP 1 [id] FROM [dbo].[Places] WHERE placeName = @placeName";

        object o = Dal.ExecuteScalar(sql, new List<SqlParameter> { new SqlParameter("@placeName", placeName) });
        if (o != null)
        {
            string s = o.ToString();
            if (!int.TryParse(s, out result))
            {
                result = 0;
            }
        }
        return result;
    }

    public static int GetPlaceId(string placeName, string ip)
    {
        int result = 0;

        string sql = "SELECT TOP 1 [id] FROM [dbo].[Places] WHERE placeName = @placeName AND ip = @ip";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@ip", ip));
        object o = Dal.ExecuteScalar(sql, values);
        if (o != null)
        {
            string s = o.ToString();
            if (!int.TryParse(s, out result))
            {
                result = 0;
            }
        }
        return result;
    }

    public static List<Worker> GetWorkers(bool onlyActive)
    {
        List<Worker> result = new List<Worker>();

        DataSet ds = GetWorkersDS(onlyActive);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Worker p = new Worker();
                    p.Id = int.Parse(item["id"].ToString());
                    p.firstName = item["firstName"].ToString();
                    p.lastName = item["lastName"].ToString();
                    p.phone = item["phone"].ToString();
                    //p.birthDay = DateTime.Parse(item["birthDay"].ToString());
                    //p.workerTypeID = int.Parse(item["workerTypeID"].ToString());
                    p.userName = item["userName"].ToString();
                    p.password = item["password"].ToString();
                    p.userTypeId = int.Parse(item["userTypeId"].ToString());
                    p.active = bool.Parse(item["active"].ToString());
                    p.imgPath = item["imgPath"].ToString();
                    p.shluha = int.Parse(item["shluha"].ToString());
                    p.departmentId = int.Parse(item["wDepartmentId"].ToString());
                    p.departmentName = item["departmentName"].ToString();
                    p.workerLevel = int.Parse(item["workerLevel"].ToString());
                    p.jobTitle = item["jobTitle"].ToString();
                    p.carType = item["carType"].ToString();
                    p.carNumber = item["carNumber"].ToString();

                    p.teudatZehut = (item["teudatZehut"].ToString());
                    p.marselWorkerCode = int.Parse(item["marselWorkerCode"].ToString());


                    result.Add(p);
                }
            }
        }
        return result;
    }

    public static List<Worker> GetWorkersCars()
    {
        List<Worker> result = new List<Worker>();

        DataSet ds = GetWorkersDS(true);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Worker p = new Worker();
                    p.Id = int.Parse(item["id"].ToString());
                    p.firstName = item["firstName"].ToString();
                    p.lastName = item["lastName"].ToString();
                    p.carType = item["carType"].ToString();
                    p.carNumber = item["carNumber"].ToString();
                    if (!string.IsNullOrEmpty(p.carNumber) ||
                        !string.IsNullOrEmpty(p.carType))
                    {
                        result.Add(p);
                    }

                }
            }
        }
        return result;
    }

    public static List<Department> GetWorkerDepartments(int workerID)
    {
        List<Department> result = new List<Department>();

        string sql =
                "SELECT workerDepartments.departmentId, workerDepartments.canSee, departmentName " +
                "FROM workerDepartments Left join departments on workerDepartments.departmentId = departments.id " +
                "WHERE [workerId] = @workerId " +
                "ORDER BY departmentName;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerID));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    int departmentId = int.Parse(item["departmentId"].ToString());
                    bool cSee = bool.Parse(item["canSee"].ToString());
                    string departmentName = item["departmentName"].ToString();

                    Department d = new Department { id = departmentId, canSee = cSee, departmentName = departmentName };

                    result.Add(d);
                }
            }
        }
        return result;
    }

    public static List<WorkExpensesType> GetWorkerExpensesValue(int workerID)
    {
        List<WorkExpensesType> result = new List<WorkExpensesType>();

        string sql =
                "SELECT [WorkerExpensesValue].[id], [WorkerExpensesValue].[workExpensType], WorkExpensesTypes.workExpensName, [WorkerExpensesValue].[expensValue] " +
                "FROM [dbo].[WorkerExpensesValue] inner join WorkExpensesTypes on[WorkerExpensesValue].workExpensType = WorkExpensesTypes.id " +
                "WHERE workerId = @workerId " +
                "order by workExpensName;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerID));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    int id = int.Parse(item["id"].ToString());
                    int workExpensType = int.Parse(item["workExpensType"].ToString());
                    string workExpensName = item["workExpensName"].ToString();
                    double expensValue = int.Parse(item["expensValue"].ToString());
                    WorkExpensesType d = new WorkExpensesType { id = id, workExpensType = workExpensType, workExpensName = workExpensName, defValue = expensValue };

                    result.Add(d);
                }
            }
        }
        return result;
    }


    public static List<WorkerExpenses> GetWorkerExpensesValuePosibleForWorker(int workerID)
    {
        List<WorkerExpenses> result = new List<WorkerExpenses>();

        string sql =
                "SELECT c.[id],c.[workExpensName] " +
                "FROM [dbo].[WorkExpensesTypes] as c " +
                "WHERE c.[id] NOT IN(SELECT workExpensType FROM WorkerExpensesValue WHERE workerId = @workerId) AND c.[workExpensCategoryId]<> 1 " +
                "order by c.[workExpensName]";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerID));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    int workExpensType = int.Parse(item["id"].ToString());
                    string workExpensName = item["workExpensName"].ToString();

                    WorkerExpenses d = new WorkerExpenses { id = workExpensType, workExpensName = workExpensName };

                    result.Add(d);
                }
            }
        }
        return result;
    }


    public static void AppendWorkerExpensesValue(int workerId, int workExpensType, double sum)
    {
        string sql = "INSERT INTO [dbo].[WorkerExpensesValue]([workerId], [workExpensType], [expensValue]) " +
                                                    "VALUES (@workerId, @workExpensType, @expensValue);";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@workExpensType", workExpensType));
        values.Add(new SqlParameter("@expensValue", sum));

        Dal.ExecuteNonQuery(sql, values);
    }


    public static List<WorkerBase> GetWorkersBase()
    {
        List<WorkerBase> result = new List<WorkerBase>();

        DataSet ds = GetWorkersDS(true);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerBase p = new WorkerBase();
                    p.Id = int.Parse(item["id"].ToString());
                    p.firstName = item["firstName"].ToString();
                    p.lastName = item["lastName"].ToString();
                    p.departmentId = int.Parse(item["wDepartmentId"].ToString());
                    p.departmentName = item["departmentName"].ToString();
                    p.workerLevel = int.Parse(item["workerLevel"].ToString());
                    p.jobTitle = item["jobTitle"].ToString();
                    p.imgPath = item["imgPath"].ToString();

                    result.Add(p);
                }
            }
        }
        return result;
    }

    public static Worker GetWorker(string userName, string password)
    {
        Worker result = null;

        string sql = "SELECT [id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId],[shluha], [active], [imgPath], [wDepartmentId], [jobTitle], [teudatZehut], [guidKey] " +
                    "FROM workers " +
                     "WHERE [userName]=@userName AND [password]=@password;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@userName", userName));
        values.Add(new SqlParameter("@password", password));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Worker p = new Worker();
                    p.Id = int.Parse(item["id"].ToString());
                    p.firstName = item["firstName"].ToString();
                    p.lastName = item["lastName"].ToString();
                    p.phone = item["phone"].ToString();
                    //p.birthDay = DateTime.Parse(item["birthDay"].ToString());
                    //p.workerTypeID = int.Parse(item["workerTypeID"].ToString());
                    p.userName = item["userName"].ToString();
                    p.password = item["password"].ToString();
                    p.userTypeId = int.Parse(item["userTypeId"].ToString());
                    p.active = bool.Parse(item["active"].ToString());
                    p.imgPath = item["imgPath"].ToString();
                    p.shluha = int.Parse(item["shluha"].ToString());
                    p.departmentId = int.Parse(item["wDepartmentId"].ToString());
                    p.jobTitle = item["jobTitle"].ToString();
                    p.teudatZehut = item["teudatZehut"].ToString();
                    p.token = item["guidKey"].ToString();


                    result = p;
                }
            }
        }
        return result;
    }

    public static Worker GetWorkerByKey(string workerKey)
    {
        Worker result = null;

        if (string.IsNullOrEmpty(workerKey))
        {
            return result;
        }

        workerKey = workerKey.Replace("\"", "");

        string sql = "SELECT [id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId],[shluha], [active], [imgPath], [wDepartmentId], [jobTitle], [teudatZehut], [guidKey] " +
                    "FROM workers " +
                     "WHERE [guidKey]=@workerKey;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerKey", workerKey));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Worker p = new Worker();
                    p.Id = int.Parse(item["id"].ToString());
                    p.firstName = item["firstName"].ToString();
                    p.lastName = item["lastName"].ToString();
                    p.phone = item["phone"].ToString();
                    //p.birthDay = DateTime.Parse(item["birthDay"].ToString());
                    p.workerTypeID = int.Parse(item["workerTypeID"].ToString());
                    p.userName = item["userName"].ToString();
                    p.password = item["password"].ToString();
                    p.userTypeId = int.Parse(item["userTypeId"].ToString());
                    p.active = bool.Parse(item["active"].ToString());
                    p.imgPath = item["imgPath"].ToString();
                    p.shluha = int.Parse(item["shluha"].ToString());
                    p.departmentId = int.Parse(item["wDepartmentId"].ToString());
                    p.jobTitle = item["jobTitle"].ToString();
                    p.teudatZehut = item["teudatZehut"].ToString();
                    p.token = item["guidKey"].ToString();

                    result = p;
                }
            }
        }
        return result;
    }


    public static DataSet GetWorkersDS(bool onlyActive)
    {
        List<Worker> result = new List<Worker>();

        string activeFilter = "";
        if (onlyActive) {
            activeFilter = "WHERE [active]=1 ";
        }

        string sql = "SELECT workers.[id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId], [active], [imgPath], [shluha], [wDepartmentId], [departmentName], jobTitle, workerLevel, carType, carNumber, teudatZehut, marselWorkerCode, ezShiftWorkerCode " +
                    "FROM workers inner join departments on workers.wDepartmentId= departments.id " +
                    activeFilter +
                    "order by firstname, lastname";

        DataSet ds = Dal.GetDataSet(sql);

        return ds;
    }

    public static DataSet GetAllWorkersDS()
    {
        List<Worker> result = new List<Worker>();

        string sql = "SELECT workers.[id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId], [active], [imgPath], [shluha], [wDepartmentId], [departmentName], jobTitle, workerLevel, carType, carNumber, teudatZehut, marselWorkerCode, ezShiftWorkerCode " +
                    "FROM workers inner join departments on workers.wDepartmentId= departments.id " +
                    "order by firstname, lastname";

        DataSet ds = Dal.GetDataSet(sql);

        return ds;
    }

    public static DataSet GetWorkerDS(int workerId)
    {
        List<Worker> result = new List<Worker>();

        string sql = "SELECT [id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId], [active], [imgPath], [shluha], jobTitle, carType, carNumber, [wDepartmentId], [teudatZehut] " +
                    "FROM workers " +
                     "WHERE [id]=@id;";

        DataSet ds = Dal.GetDataSet(sql, new List<SqlParameter> { new SqlParameter("@id", workerId) });

        return ds;
    }

    public static Dictionary<string, List<PhonePlace>> GetPhonePlace()
    {
        Dictionary<string, List<PhonePlace>> result = new Dictionary<string, List<PhonePlace>>();

        string sql = "SELECT PhonePlace.id, PhonePlace.phoneId, Phones.phone, PhonePlace.placeId, Places.placeName, Places.ip, PhonePlace.customerName " +
                     "FROM PhonePlace INNER JOIN Phones ON PhonePlace.phoneId = Phones.id INNER JOIN Places ON PhonePlace.placeId = Places.id " +
                     "Order By Phones.phone";

        DataSet ds = Dal.GetDataSet(sql);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    PhonePlace p = new PhonePlace();
                    p.id = int.Parse(item["id"].ToString());
                    p.phoneId = int.Parse(item["phoneId"].ToString());
                    p.phone = item["phone"].ToString();
                    p.placeId = int.Parse(item["placeId"].ToString());
                    p.placeName = item["placeName"].ToString();
                    p.ip = item["ip"].ToString();
                    p.customerName = item["customerName"].ToString();

                    if (!result.ContainsKey(p.phone))
                    {
                        result.Add(p.phone, new List<PhonePlace>());
                    }
                    result[p.phone].Add(p);
                }
            }
        }
        return result;
    }

    public static List<PhonePlace> GetPhonePlace(string phone)
    {
        List<PhonePlace> result = new List<PhonePlace>();

        string sql = "SELECT PhonePlace.id, PhonePlace.phoneId, Phones.phone, PhonePlace.placeId, Places.placeName, Places.ip, PhonePlace.customerName " +
                     "FROM PhonePlace INNER JOIN Phones ON PhonePlace.phoneId = Phones.id INNER JOIN Places ON PhonePlace.placeId = Places.id " +
                     "WHERE Phones.phone=@phone " +
                     "Order By Phones.phone";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phone", phone));
        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    PhonePlace p = new PhonePlace();
                    p.id = int.Parse(item["id"].ToString());
                    p.phoneId = int.Parse(item["phoneId"].ToString());
                    p.phone = item["phone"].ToString();
                    p.placeId = int.Parse(item["placeId"].ToString());
                    p.placeName = item["placeName"].ToString();
                    p.ip = item["ip"].ToString();
                    p.customerName = item["customerName"].ToString();

                    result.Add(p);
                }
            }
        }
        return result;
    }


    public static List<PhonePlace> GetPhonePlaceSearch(string phone)
    {
        List<PhonePlace> result = new List<PhonePlace>();

        string sql = "SELECT Top 30 PhonePlace.id, PhonePlace.phoneId, Phones.phone, PhonePlace.placeId, Places.placeName, Places.ip, PhonePlace.customerName, Places.remark, Places.vip " +
                     "FROM PhonePlace INNER JOIN Phones ON PhonePlace.phoneId = Phones.id INNER JOIN Places ON PhonePlace.placeId = Places.id " +
                     "WHERE Phones.phone LIKE N'%" + phone + "%'" +
                     "Order By placeName, customerName, Phones.phone";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@phone", phone));
        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    PhonePlace p = new PhonePlace();
                    p.id = int.Parse(item["id"].ToString());
                    p.phoneId = int.Parse(item["phoneId"].ToString());
                    p.phone = item["phone"].ToString();
                    p.placeId = int.Parse(item["placeId"].ToString());
                    p.placeName = item["placeName"].ToString();
                    p.ip = item["ip"].ToString();
                    p.placeRemark = item["remark"].ToString();
                    if (!string.IsNullOrEmpty(item["vip"].ToString()))
                    {
                        p.vip = bool.Parse(item["vip"].ToString());
                    }

                    p.customerName = item["customerName"].ToString();

                    result.Add(p);
                }
            }
        }
        return result;
    }


    public static int GetPosIdFromBiBeecomm(string userName, string password)
    {
        string biConnString = "Server=tcp:bibeecommserver.database.windows.net,1433;Initial Catalog = biBeecomm; Persist Security Info=False;User ID = bcBiUser; Password=0cf23e5f-2051-47b2-a051-eec97eb48c34; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        int result = 0;
        string sql = "SELECT [id] " +
                     "FROM [dbo].[pos] " +
                     "WHERE (userName = @userName) AND ([password] = @password) AND ([active]=1)";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@userName", userName));
        values.Add(new SqlParameter("@password", password));
        object o = Dal.ExecuteScalar(sql, values, biConnString);

        if (o != null)
        {
            string s = o.ToString();
            int.TryParse(s, out result);
        }

        return result;
    }

    public static string GetProblemHistorySummeryAndTrackingStatus(int problemId, int workerId, int placeNameId, out string lastSuppoter, out int trackingId)
    {
        trackingId = 0;
        lastSuppoter = "";
        string result = "";

        //סיכום כמות הפעמים שפנה היום, השבוע, החודש
        string sql = "SELECT Sum(1) as c,Sum( IIF(startTime>=GETDATE()-1,1,0)) as cToday, Sum(IIF(startTime>=GETDATE()-7,1,0)) as cWeek , Sum(IIF(startTime>=GETDATE()-30,1,0)) as cMonth " +
                     "FROM [dbo].[problemsClose] " +
                     "WHERE placeNameId = @placeNameId;";

        sql += "SELECT top 2 placeNameId,firstName + ' ' + lastName as workerName, [finishTime] " +
               "FROM [dbo].[problemsClose] inner join workers on workers.id = problemsClose.toWorker " +
               "WHERE placeNameId = @placeNameId " +
               "ORDER BY [finishTime] desc;";

        //האם התקלה תחת מעקב על ידי העובד
        sql += "SELECT [id] " +
                "FROM[dbo].[problemTracking] " +
                "WHERE [workerId]=@workerId AND problemId = @problemId;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeNameId", placeNameId));
        values.Add(new SqlParameter("@workerId", workerId));
        values.Add(new SqlParameter("@problemId", problemId));
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            //סיכום כמות לחודש האחרון
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    if (item["cToday"] != null)
                    {
                        int today = int.Parse(item["cToday"].ToString());
                        int week = int.Parse(item["cWeek"].ToString());
                        int month = int.Parse(item["cMonth"].ToString());

                        if (today > 0)
                        {
                            result += today + " תקלות היום, ";
                        }

                        if (week > 0)
                        {
                            result += week + " תקלות השבוע, ";
                        }

                        if (month > 0)
                        {
                            result += month + " תקלות החודש ";
                        }
                    }
                }
            }



            //מי ומתי טיפלו בו פעם אחרונה
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow item = ds.Tables[1].Rows[0];

                if (ds.Tables[1].Rows.Count > 1)
                {
                    item = ds.Tables[1].Rows[1];
                }

                string workerName = item["workerName"].ToString();
                DateTime lastD = DateTime.Parse(item["finishTime"].ToString());
                lastSuppoter =
                        "תומך אחרון שדיבר עם הלקוח: " + workerName + " בתאריך: " + lastD.ToString("dd.MM.yyyy HH:mm");
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataRow item = ds.Tables[2].Rows[0];
                trackingId = int.Parse(item["id"].ToString());
            }

        }

        return result;
    }

    public static List<Problem> GetProblemsSearch(SearchProblem search)
    {

        List<Problem> result = new List<Problem>();


        List<string> wheres = new List<string>();
        if (!search.place && !search.phone && !search.desc && !search.department && !search.workerName)
        {
            double d = 0;
            if (double.TryParse(search.searchValue, out d))
            {
                search.phone = true;
            }
            else
            {
                search.place = true;
            }

            search.workerName = true;
        }

        if (search.place)
        {
            wheres.Add("(problemsClose.PlaceName LIKE N'%" + search.searchValue.Replace("'", "''") + "%')");
        }

        if (search.phone)
        {
            wheres.Add("(problemsClose.phone LIKE N'%" + search.searchValue + "%')");
        }

        if (search.desc)
        {
            wheres.Add("((problemsClose.problemDesc LIKE N'%" + search.searchValue + "%') OR (problemsClose.problemSolution LIKE N'%" + search.searchValue + "%'))");
        }

        //if (search.solution)
        //{
        //    wheres.Add("(problemsClose.problemSolution LIKE N'%" + search.searchValue + "%')");
        //}

        if (search.department)
        {
            wheres.Add("(departmentName LIKE N'%" + search.searchValue + "%')");
        }

        if (search.workerName)
        {
            //workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.
            wheres.Add("(((workers.firstName + N' ' + workers.lastName) LIKE N'%" + search.searchValue + "%') OR ((workers_1.firstName + N' ' + workers_1.lastName) LIKE N'%" + search.toWorkerName + "%'))");
        }

        string filter = "";
        foreach (var item in wheres)
        {
            filter += item + " OR ";
        }
        filter = filter.Substring(0, filter.Length - 4);
        filter += " AND (problemsClose.startTime > GETDATE() - " + search.daysBack + ")";

        DataSet ds = GetProblemsForWS(filter);

        foreach (DataRow item in ds.Tables[0].Rows)
        {
            Problem p = new Problem();
            p.id = int.Parse(item["id"].ToString());
            p.workerCreateId = int.Parse(item["workerId"].ToString());
            p.workerCreateName = item["workerName"].ToString();

            p.phone = item["phone"].ToString();
            p.ip = item["ip"].ToString();
            p.placeId = int.Parse(item["placeNameId"].ToString());
            p.placeName = item["placeName"].ToString();
            p.customerName = item["customerName"].ToString();
            p.desc = item["problemDesc"].ToString();
            p.solution = item["problemSolution"].ToString();

            p.statusId = int.Parse(item["statusId"].ToString());
            p.statusName = item["statusName"].ToString();

            p.emergencyId = int.Parse(item["emergencyId"].ToString());
            //p.emergencyName = item["emergencyName"].ToString();

            p.departmentId = int.Parse(item["departmentId"].ToString());
            p.departmentName = item["departmentName"].ToString();

            p.toWorker = int.Parse(item["toWorker"].ToString());
            p.toWorkerName = item["toWorkerName"].ToString();

            p.updaterWorkerId = int.Parse(item["updaterWorkerId"].ToString());
            p.updaterWorkerName = item["updateWorkerName"].ToString();


            p.yaron = bool.Parse(item["reportToYaron"].ToString());

            p.startTime = DateTime.Parse(item["startTime"].ToString()).ToString("dd/MM/yy HH:mm");
            p.finishTime = DateTime.Parse(item["finishTime"].ToString()).ToString("dd/MM/yy HH:mm");
            p.startTimeEN = DateTime.Parse(item["startTime"].ToString()).ToString("MM/dd/yy HH:mm");
            p.finishTimeEN = DateTime.Parse(item["finishTime"].ToString()).ToString("MM/dd/yy HH:mm");

            p.takingCare = bool.Parse(item["takingCare"].ToString());
            p.isLocked = bool.Parse(item["isLocked"].ToString());

            p.callCustomerBack = bool.Parse(item["callCustomerBack"].ToString());


            //p.filesName = item["filesName"].ToString();

            //if (!string.IsNullOrEmpty(p.filesName))
            //{
            //    string[] a = p.filesName.Split(',');
            //    if (a.Length > 0)
            //    {
            //        p.files = new List<string>();
            //        foreach (var f in a)
            //        {
            //            p.files.Add("https://beecomm.azurewebsites.net/Pics/problems/" + f);
            //        }
            //    }
            //}
            result.Add(p);
        }


        return result;
    }


    public static int AppendPlaceBizNumber(string placeName, string bizNumber, int warrantyType)
    {
        string sql = "INSERT INTO [dbo].[PlacesBizNumber] ([placeName], [bizNumber], [warrantyType]) VALUES (@placeName,@bizNumber, @warrantyType) SELECT SCOPE_IDENTITY()";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@bizNumber", bizNumber));
        values.Add(new SqlParameter("@warrantyType", warrantyType));

        object o = Dal.ExecuteScalar(sql, values);

        int placeId = 0;
        if (o != null)
        {
            placeId = int.Parse(o.ToString());
        }

        return placeId;
    }

    public static void UpdatePlaceBizNumber(int id, string placeName, string bizNumber, int warrantyType)
    {
        string sql = "UPDATE [dbo].[PlacesBizNumber] Set [placeName]= @placeName, [bizNumber]= @bizNumber, [warrantyType]= @warrantyType WHERE (id = @id);";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@placeName", placeName));
        values.Add(new SqlParameter("@bizNumber", bizNumber));
        values.Add(new SqlParameter("@warrantyType", warrantyType));
        Dal.ExecuteNonQuery(sql, values);
    }

    public static List<PhonePlace> GetPlaceBizNumberByName(string placeName)
    {
        List<PhonePlace> result = new List<PhonePlace>();
        string sql = "SELECT [id],[placeName],[bizNumber], warrantyType " +
                     "FROM [dbo].[PlacesBizNumber] " +
                     "WHERE placeName LIKE @placeName " +
                     "Order by placeName;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@placeName", "%" + placeName + "%"));
        DataSet ds = Dal.GetDataSet(sql, values);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    PhonePlace p = new PhonePlace();
                    p.id = int.Parse(item["id"].ToString());
                    p.placeName = (item["placeName"].ToString());
                    p.bizNumber = (item["bizNumber"].ToString());
                    p.warrantyType = int.Parse(item["warrantyType"].ToString());

                    result.Add(p);
                }
            }
        }

        return result;
    }

    public static List<PhonePlace> GetPlaceBizNumber()
    {
        List<PhonePlace> result = new List<PhonePlace>();
        string sql = "SELECT [id],[placeName],[bizNumber], warrantyType " +
                     "FROM [dbo].[PlacesBizNumber] " +
                     "Order by placeName;";

        List<SqlParameter> values = new List<SqlParameter>();
        DataSet ds = Dal.GetDataSet(sql, values);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    PhonePlace p = new PhonePlace();
                    p.id = int.Parse(item["id"].ToString());
                    p.placeName = (item["placeName"].ToString());
                    p.bizNumber = (item["bizNumber"].ToString());
                    p.warrantyType = int.Parse(item["warrantyType"].ToString());

                    result.Add(p);
                }
            }
        }

        return result;
    }


    public static List<WorkerSickDay> GetWorkersSickDays(int year, int month, int workerId = 0)
    {
        string filterWorker = "";
        if (workerId > 0)
        {
            filterWorker = " AND [workersSickday].workerid=@workerid ";
        }


        string sql = "SELECT workersSickday.[id]      ,[workerId], firstName + ' ' + lastName as workerName, [startDate],[finishDate],[fileName] " +
                    "FROM [dbo].[workersSickday] inner join workers on[workersSickday].workerid = workers.id " +
                    "WHERE (Year(startdate)=@year) AND (Month(startdate)= @month) " + filterWorker + " " +
                    "Order by startDate, workerName";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@year", year));
        values.Add(new SqlParameter("@month", month));

        if (workerId > 0)
        {
            values.Add(new SqlParameter("@workerid", workerId));
        }

        DataSet ds = Dal.GetDataSet(sql, values);

        List<WorkerSickDay> result = new List<WorkerSickDay>();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerSickDay p = new WorkerSickDay();
                    p.id = int.Parse(item["id"].ToString());
                    p.workerId = int.Parse(item["workerId"].ToString());
                    p.workerName = (item["workerName"].ToString());
                    p.startDate = DateTime.Parse(item["startDate"].ToString());
                    p.startDateEN = DateTime.Parse(item["startDate"].ToString()).ToString("MM/dd/yyyy");
                    p.finishDate = DateTime.Parse(item["finishDate"].ToString());
                    p.finishDateEN = DateTime.Parse(item["finishDate"].ToString()).ToString("MM/dd/yyyy");
                    p.fileName = (item["fileName"].ToString());

                    result.Add(p);
                }
            }
        }

        return result;
    }

    internal static void AppendWorkerSickday(WorkerSickDay sickDay, string serverMapPath)
    {

        if (!string.IsNullOrEmpty(sickDay.imgContent))
        {
            sickDay.fileName = SaveSickDayFile(sickDay, serverMapPath);
        }
        else {
            sickDay.fileName = "";
        }

        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        sickDay.startDate = TimeZoneInfo.ConvertTime(sickDay.startDate, remoteTimeZone);
        sickDay.finishDate = TimeZoneInfo.ConvertTime(sickDay.finishDate, remoteTimeZone);


        string sql = "INSERT INTO [dbo].[workersSickday] ([workerId],[startDate],[finishDate],[fileName]) VALUES " +
                     "(@workerId,@startDate,@finishDate,@fileName);";
                    
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", sickDay.workerId));
        values.Add(new SqlParameter("@startDate", sickDay.startDate));
        values.Add(new SqlParameter("@finishDate", sickDay.finishDate));
        values.Add(new SqlParameter("@fileName", sickDay.fileName));

        Dal.ExecuteScalar(sql, values);
    }

    internal static void UpdateWorkerSickday(WorkerSickDay sickDay , string serverMapPath)
    {
        string sql = "";
        if (!string.IsNullOrEmpty(sickDay.imgContent))
        {
            sickDay.fileName = SaveSickDayFile(sickDay, serverMapPath);
            sql = "UPDATE [dbo].[workersSickday] SET " +
                            "[workerId] = @workerId, [startDate] = @startDate, [finishDate] = @finishDate, [fileName] = @fileName " +
                            "WHERE id = @id";
        }
        else
        {
            sql = "UPDATE [dbo].[workersSickday] SET " +
                           "[workerId] = @workerId, [startDate] = @startDate, [finishDate] = @finishDate " +
                           "WHERE id = @id";
        }
       

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", sickDay.id));
        values.Add(new SqlParameter("@workerId", sickDay.workerId));
        values.Add(new SqlParameter("@startDate", sickDay.startDate));
        values.Add(new SqlParameter("@finishDate", sickDay.finishDate));
        values.Add(new SqlParameter("@fileName", sickDay.fileName));

        Dal.ExecuteNonQuery(sql, values);
    }

    private static string SaveSickDayFile(WorkerSickDay sickDay, string serverMapPath)
    {
        string filename = "";
        try
        {
            string[] a = sickDay.imgContent.Split(',');
            if (a.Length > 1)
            {
                string fileType = GetFileType(a[0]);

                //string[] a = worker.imgContentName.Split('.');
                byte[] workerImage = Convert.FromBase64String(a[1]);
                filename = Guid.NewGuid().ToString().Replace("-", "") + fileType;

                // Server.MapPath("~/") 
                string path = serverMapPath + filename;
                File.WriteAllBytes(path, workerImage);
            }
        }
        catch (Exception e)
        {
            WebDal.AppendErrorLog("CrmWS.SaveSickDayFile", e.Message, "");
        }
        return filename;
    }

    public static string GetFileType(string s)
    {

        if (s.Contains("data:text/plain"))
        {
            return ".txt";
        }

        if (s.Contains("data:application/msaccess"))
        {
            return ".mdb";
        }

        if (s.Contains("data:application/vnd.ms-excel"))
        {
            return ".xlsx";
        }

        if (s.Contains("data:application/msword"))
        {
            return ".doc";
        }

        if (s.Contains("data:application/pdf"))
        {
            return ".pdf";
        }





        if (s.Contains("jpeg"))
        {
            return ".jpg";
        }

        if (s.Contains("png"))
        {
            return ".png";
        }


        //if (s.Contains("data:application"))
        //{
        //    return ".exe";
        //}

        return ".jpg";
    }


    public static List<WorkerFreeDay> GetWorkersFreeDays(int year, int month, int workerId = 0)
    {
        string filterWorker = "";
        if (workerId > 0)
        {
            filterWorker = " AND [workersFreeDay].workerid=@workerid ";
        }


        string sql = "SELECT workersFreeDay.[id], workersFreeDay.[remark], workersFreeDay.[workerId], firstName + ' ' + lastName as workerName, [startDate],[finishDate],workersFreeDay.[statusId] " +
                    "FROM [dbo].[workersFreeDay] inner join workers on[workersFreeDay].workerid = workers.id " +
                    "WHERE (Year(startdate)=@year) AND (Month(startdate)= @month) " + filterWorker + " " +
                    "Order by startDate, workerName";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@year", year));
        values.Add(new SqlParameter("@month", month));

        if (workerId > 0)
        {
            values.Add(new SqlParameter("@workerid", workerId));
        }

        DataSet ds = Dal.GetDataSet(sql, values);

        List<WorkerFreeDay> result = new List<WorkerFreeDay>();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WorkerFreeDay p = new WorkerFreeDay();
                    p.id = int.Parse(item["id"].ToString());
                    p.workerId = int.Parse(item["workerId"].ToString());
                    p.workerName = (item["workerName"].ToString());
                    p.remark = (item["remark"].ToString());
                    p.startDate = DateTime.Parse(item["startDate"].ToString());
                    p.startDateEN = DateTime.Parse(item["startDate"].ToString()).ToString("MM/dd/yyyy");
                    p.finishDate = DateTime.Parse(item["finishDate"].ToString());
                    p.finishDateEN = DateTime.Parse(item["finishDate"].ToString()).ToString("MM/dd/yyyy");
                    p.statusId = int.Parse(item["statusId"].ToString());
                    
                    result.Add(p);
                }
            }
        }

        return result;
    }

    internal static void AppendWorkerFreeday(WorkerFreeDay freeDay)
    {
        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        freeDay.startDate = TimeZoneInfo.ConvertTime(freeDay.startDate, remoteTimeZone);
        freeDay.finishDate = TimeZoneInfo.ConvertTime(freeDay.finishDate, remoteTimeZone);

        string sql = "INSERT INTO [dbo].[workersFreeDay] ([workerId],[startDate],[finishDate],[remark]) VALUES " +
                     "(@workerId,@startDate,@finishDate,@remark);";


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@workerId", freeDay.workerId));
        values.Add(new SqlParameter("@startDate", freeDay.startDate));
        values.Add(new SqlParameter("@finishDate", freeDay.finishDate));
        values.Add(new SqlParameter("@remark", freeDay.remark));
        //values.Add(new SqlParameter("@statusId", freeDay.statusId));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static void UpdateWorkerFreeday(WorkerFreeDay freeDay)
    {
        if (string.IsNullOrEmpty(freeDay.remark))
        {
            freeDay.remark = "";
        }
        string sql = "UPDATE [dbo].[workersFreeDay] SET " +
                            "[workerId] = @workerId, [startDate] = @startDate, [finishDate] = @finishDate, [remark] = @remark, [statusId] = @statusId " +
                            "WHERE id = @id";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", freeDay.id));
        values.Add(new SqlParameter("@workerId", freeDay.workerId));
        values.Add(new SqlParameter("@startDate", freeDay.startDate));
        values.Add(new SqlParameter("@finishDate", freeDay.finishDate));

        values.Add(new SqlParameter("@remark", freeDay.remark));
        values.Add(new SqlParameter("@statusId", freeDay.statusId));

        Dal.ExecuteNonQuery(sql, values);
    }


    internal static void DeleteWorkerFreeday(int sickDayId)
    {
        string sql = "DELETE FROM [dbo].[workersFreeDay] WHERE id = @id ";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", sickDayId));
        Dal.ExecuteNonQuery(sql, values);
    }

    internal static void DeleteWorkerSickday(int sickDayId)
    {
        string sql = "DELETE FROM [dbo].[workersSickday] WHERE id = @id ";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", sickDayId));
        Dal.ExecuteNonQuery(sql, values);
    }

    internal static int AppendShiftDaysRemarks(DateTime dateValue, string remark, int shiftGroupId)
    {

        string sql = "INSERT INTO [dbo].[shiftDaysRemarks] ([dateValue],[remark],[shiftGroupId]) VALUES " +
                     "(@dateValue,@remark,@shiftGroupId);" +
                     "SELECT SCOPE_IDENTITY()";


        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@dateValue", dateValue));
        values.Add(new SqlParameter("@remark", remark));
        values.Add(new SqlParameter("@shiftGroupId", shiftGroupId));


        int newId = 0;
        object o = Dal.ExecuteScalar(sql, values);
        if (o != null)
        {
            newId = int.Parse(o.ToString());
        }


        return newId;
    }

    internal static void UpdateShiftDaysRemarks(int id, string remark)
    {
        if (string.IsNullOrEmpty(remark))
        {
            remark = "";
        }
        string sql = "UPDATE [dbo].[shiftDaysRemarks] SET [remark] = @remark WHERE id = @id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@remark", remark));

        Dal.ExecuteNonQuery(sql, values);
    }


    public static List<dayInfo> GetShiftDaysRemarks(DateTime day, int shiftGroupId)
    {
        Dictionary<DateTime, dayInfo> days = new Dictionary<DateTime, dayInfo>();
        string[] daysName = { "א", "ב", "ג", "ד", "ה", "ו", "ש" };
        for (int i = 0; i < 7; i++)
        {
            days.Add(day.AddDays(i), new dayInfo { dayValue = day.AddDays(i), dayValueEN = day.AddDays(i).ToString("dd/MM/yyyy HH:mm"), dayName = daysName[i] });
        }

        DateTime finish = day.AddDays(7);
        string sql = "SELECT [dateValue],[holydayName] " +
                     "FROM[dbo].[shiftHolydays] " +
                     "WHERE [dateValue]>=@start AND [dateValue]<@finish ";

        sql += "SELECT [id],[dateValue],[remark] " +
              "FROM [dbo].[shiftDaysRemarks] " +
              "WHERE [dateValue]>=@start AND [dateValue]<@finish AND shiftGroupId=@shiftGroupId ";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@start", day));
        values.Add(new SqlParameter("@finish", finish));
        values.Add(new SqlParameter("@shiftGroupId", shiftGroupId));


        DataSet ds = Dal.GetDataSet(sql, values);

        List<dayInfo> result = new List<dayInfo>();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    dayInfo p = new dayInfo();
                    p.dayValue = DateTime.Parse(item["dateValue"].ToString());
                    p.dayValueEN = DateTime.Parse(item["dateValue"].ToString()).ToString("dd/MM/yyyy");
                    p.dayName = daysName[(int)p.dayValue.DayOfWeek];

                    //p.remark = (item["remark"].ToString());
                    p.holydayName = (item["holydayName"].ToString());
                    p.isToday = (p.dayValue.Date == DateTime.Now.Date);
                    if (days.ContainsKey(p.dayValue))
                    {
                        days[p.dayValue] = p;
                    }
                    //result.Add(p);
                }
            }
        }

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    dayInfo p = new dayInfo();
                    p.dayValue = DateTime.Parse(item["dateValue"].ToString());
                    if (days.ContainsKey(p.dayValue))
                    {
                        p = days[p.dayValue];
                    }
                    p.id = int.Parse(item["id"].ToString());

                    p.dayValueEN = DateTime.Parse(item["dateValue"].ToString()).ToString("dd/MM/yyyy");
                    p.dayName = daysName[(int)p.dayValue.DayOfWeek];

                    //p.remark = (item["remark"].ToString());
                    p.remark = (item["remark"].ToString());
                    p.isToday = (p.dayValue.Date == DateTime.Now.Date);
                    if (days.ContainsKey(p.dayValue))
                    {
                        days[p.dayValue] = p;
                    }
                    //result.Add(p);
                }
            }
        }

        foreach (var item in days)
        {
            result.Add(item.Value);
        }

        return result;
    }

    public static List<OuterCompany> GetOuterCompanies()
    {
        List<OuterCompany> result = new List<OuterCompany>();
       
        string sql = "SELECT [id],[name],[color] " +
            "FROM [dbo].[OuterCompanies];";

        DataSet ds = Dal.GetDataSet(sql);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    OuterCompany obj = new OuterCompany();
                    obj.id = int.Parse(item["id"].ToString());
                    obj.name = item["name"].ToString();
                    obj.color = item["color"].ToString();
                    result.Add(obj);
                }
            }
        }
     
        return result;
    }

    public static List<OuterCompany> GetShiftOuterCompanies(int shiftId)
    {
        List<OuterCompany> result = new List<OuterCompany>();

        string sql = "SELECT [outerCompanyId], [color], [name]" +
            "FROM [dbo].[ShiftOuterCompanies]" +
            "INNER JOIN [dbo].[OuterCompanies] ON  [ShiftOuterCompanies].[outerCompanyId] = [OuterCompanies].[id]" +
            "WHERE [ShiftOuterCompanies].[shiftId] = " + shiftId + ";";

        DataSet ds = Dal.GetDataSet(sql);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    OuterCompany obj = new OuterCompany();
                    obj.id = int.Parse(item["outerCompanyId"].ToString());
                    obj.name = item["name"].ToString();
                    obj.color = item["color"].ToString();
                    result.Add(obj);
                }
            }
        }

        return result;
    }

    public static void UpdateShiftOuterCompanies(int shiftId, List<OuterCompany> outerCompanies)
    {
        try {
            Dal.ExecuteNonQuery("DELETE FROM [dbo].[ShiftOuterCompanies] WHERE [ShiftOuterCompanies].[shiftId] = " + shiftId + ";");

            string sql = "INSERT INTO [dbo].[ShiftOuterCompanies]([shiftId],[outerCompanyId]) VALUES(@shiftId,@outerCompanyId);";

            foreach (OuterCompany comp in outerCompanies)
            {
                List<SqlParameter> values = new List<SqlParameter>();
                values.Add(new SqlParameter("@shiftId", shiftId));
                values.Add(new SqlParameter("@outerCompanyId", comp.id));
                Dal.ExecuteNonQuery(sql, values);
            }
        }
        catch (Exception ex) {
        }
      
    }

    public static void UpdateNivServer(Problem problem)
    {
        var json = JsonConvert.SerializeObject(problem);
        string url = "https://us-central1-telegram-bot-53724.cloudfunctions.net/api/crm/newProblem";
        var request = WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";

        //byte[] byteArray = Encoding.UTF8.GetBytes(json);

        try
        {


            //reqStream.Write(byteArray, 0, byteArray.Length);
            using (var s = new StreamWriter(request.GetRequestStream()))
            {
                s.Write(json);
            }

            var response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            var respStream = response.GetResponseStream();

            var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
        }
        catch (WebException e)
        {
            var s = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
            //throw;
        }
        //Console.WriteLine(data);

        //record User(string Name, string Occupation);


    }


}

//משיכת חגים ומועדים
//https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&start=2023-04-01&end=2023-04-13&geo=none
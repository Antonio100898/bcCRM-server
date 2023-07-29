using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services;

public partial class SettingsBiV3 : System.Web.UI.Page
{
    static string connString = "Data Source=bidb.beecomm.co.il;Initial Catalog=BI_masterDB;Persist Security Info=True;User ID=shahaf;Password=#JxMj8wQDbJnmDVsR";
    private static Dictionary<int, bcGroup> groupsId = new Dictionary<int, bcGroup>();
    private static int currentGroupId = 0;
    private static List<biDbCity> cities;
    private static Dictionary<int, string> citiesDic = new Dictionary<int, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetCities();
        }
    }


    private static string GetConnString(string dataBaseName)
    {
        return "Server=bidb.beecomm.co.il;Database=" + dataBaseName + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
    }

    private void SetCities()
    {
        cities = new List<biDbCity>();
        citiesDic = new Dictionary<int, string>();

        string sql = "SELECT [Id],[name] FROM [dbo].[cities] order by [name];";
        DataSet ds = Dal.GetDataSetOld(sql, null, connString);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    biDbCity city = new biDbCity();
                    city.id = int.Parse(item["id"].ToString());
                    city.cityName = item["name"].ToString();

                    cities.Add(city);

                    citiesDic.Add(city.id, city.cityName);
                }
            }
        }
    }

    [WebMethod]
    public static List<biDbCity> GetCities()
    {
        return cities;
    }

    //////////////////Groups
    private static DataSet GetGroupsDS()
    {
        string sql = "SELECT [id], [groupName], [databaseName] FROM [dbo].[Groups] ORDER BY [groupName];";
        DataSet ds = Dal.GetDataSetOld(sql, null, connString);
        return ds;
    }

    [WebMethod]
    public static List<bcGroup> GetGroups()
    {
        List<bcGroup> groups = new List<bcGroup>();
        DataSet ds = GetGroupsDS();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bcGroup g = new bcGroup();
                    g.id = int.Parse(item["id"].ToString());
                    g.name = item["groupName"].ToString();
                    g.database = item["databaseName"].ToString();
                    groups.Add(g);

                    if (!groupsId.ContainsKey(g.id))
                    {
                        groupsId.Add(g.id, g);
                    }
                }
            }
        }

        return groups;
    }

    [WebMethod]
    public static string GetGroupDatabaseName(int groupId)
    {
        if (groupsId != null)
        {
            if (groupsId.ContainsKey(groupId))
            {
                return groupsId[groupId].database;
            }
        }
        return "";
    }


    [WebMethod]
    public static void UpdateGroup(int groupId, string groupName)
    {
        if (groupId == 0)
        {
            string dbName = "group" + Guid.NewGuid().ToString();
            AppendGroup(groupName, dbName);
            CreateDataBase(dbName);
        }
        else
        {

        }
    }

    public static int GetMaxGroupID()
    {
        string sql = "SELECT MAX(id) AS maxID FROM Groups";
        int i = int.Parse(Dal.ExecuteScalar(sql, new List<SqlParameter>(), GetConnString("BI_masterDB")).ToString());
        return i;
    }

    public static void AppendGroup(string groupName, string databaseName)
    {
        int groupID = GetMaxGroupID() + 1;

        string sql = "INSERT INTO [dbo].[Groups] ([id],[groupName],[databaseName],[timeZone]) VALUES (@id,@groupName,@databaseName,@timeZone)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", groupID));
        values.Add(new SqlParameter("@groupName", groupName));
        values.Add(new SqlParameter("@databaseName", databaseName));
        values.Add(new SqlParameter("@timeZone", "Asia/Jerusalem"));

        Dal.ExecuteScalar(sql, values, connString);
    }

    public static void UpdateGroupDB(int groupId, string groupName)
    {
        string sql = "UPDATE [dbo].[Groups] SET[groupName] = @groupName WHERE id = @id;";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", groupId));
        values.Add(new SqlParameter("@groupName", groupName));

        Dal.ExecuteScalar(sql, values, connString);
    }

    public static void CreateDataBase(string newDataBaseName)
    {
        string sqlConnectionString = @"Server=bidb.beecomm.co.il;User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
        string script = File.ReadAllText(@"D:\home\site\wwwroot\SqlScripts\DataV3\CreateDatabase.sql");
        //string script = File.ReadAllText(@"C:\Users\NEZEK7\Documents\Visual Studio 2015\WebSites\bcCRM\SqlScripts\DataV3\CreateDatabase.sql");
        script = script.Replace("@NewDatabaseName", newDataBaseName);
        Dal.ExecuteNonQuery(script, new List<SqlParameter>(), sqlConnectionString);
        //RunSqlScript(script);

        CreateDataBaseTables(newDataBaseName);
        CreateDatabaseStoredFunctions(newDataBaseName);

        DateTime now = DateTime.Now;
        TimeSpan tp = DateTime.Now - now;

        while (tp.TotalSeconds < 3)
        {
            tp = DateTime.Now - now;
        }

        InsertBasicTablesInfo(newDataBaseName);

    }
    public static void CreateDatabaseStoredFunctions(string newDataBaseName)
    {
        string connString = GetConnString(newDataBaseName);

        string sql =
                   "CREATE FUNCTION[dbo].[GetAllDaysInBetween](@FirstDay DATETIME, @LastDay DATETIME) " + Environment.NewLine +
                   "RETURNS @retDays TABLE (DayInBetween DATETIME) AS BEGIN " + Environment.NewLine +
                   "DECLARE @currentDay DATETIME " + Environment.NewLine +
                   "SELECT @currentDay = @FirstDay " + Environment.NewLine +
                   "WHILE @currentDay <= @LastDay " + Environment.NewLine +
                   "BEGIN " + Environment.NewLine +
                       "INSERT @retDays(DayInBetween) " + Environment.NewLine +
                           "SELECT @currentDay " + Environment.NewLine +
                       "SELECT @currentDay = DATEADD(DAY, 1, @currentDay) " + Environment.NewLine +
                   "END " + Environment.NewLine +
                    "RETURN " + Environment.NewLine +
                   "END";

        Dal.ExecuteNonQuery(sql, new List<SqlParameter>(), connString);



        sql =
            "CREATE FUNCTION [dbo].[getMissingZs](@FirstZ int, @LastZ int) " + Environment.NewLine +
            "RETURNS @retZs TABLE(ZInBetween int) AS BEGIN " + Environment.NewLine +
            "DECLARE @minZ int " + Environment.NewLine +
            "DECLARE @maxZ int " + Environment.NewLine +
            "DECLARE @currentZ int " + Environment.NewLine +
            "SELECT @currentZ = @FirstZ " + Environment.NewLine +
            "WHILE @currentZ <= @LastZ " + Environment.NewLine +
            "BEGIN " + Environment.NewLine +
                "INSERT @retZs(ZInBetween) " + Environment.NewLine +
                    "SELECT @currentZ " + Environment.NewLine +
                    "SELECT @currentZ = @currentZ + 1 " + Environment.NewLine +
            "END " + Environment.NewLine +
                "RETURN " + Environment.NewLine +
            "END";

        Dal.ExecuteNonQuery(sql, new List<SqlParameter>(), connString);
    }

    public static void CreateDataBaseTables(string newDataBaseName)
    {
        string script = File.ReadAllText(@"D:\home\site\wwwroot\SqlScripts\DataV3\CreateTables.sql", Encoding.UTF8);
        script = script.Replace("@NewDatabaseName", newDataBaseName);
        Dal.ExecuteNonQuery(script, new List<SqlParameter>(), GetConnString(newDataBaseName));
    }

    public static void InsertBasicTablesInfo(string newDataBaseName)
    {
        string script = File.ReadAllText(@"D:\home\site\wwwroot\SqlScripts\DataV3\InsertAllBaseData.sql", Encoding.UTF8);
        script = script.Replace("@NewDatabaseName", newDataBaseName);
        Dal.ExecuteNonQuery(script, new List<SqlParameter>(), GetConnString(newDataBaseName));
    }



    /////////////////////Branches    

    [WebMethod]
    public static List<biDbBranch> GetBranches(int groupId)
    {
        List<biDbBranch> result = new List<biDbBranch>();

        if (groupsId.Count==0)
        {
            GetGroups();
        }

        if (!groupsId.ContainsKey(groupId))
        {
            
            return result;
        }

        currentGroupId = groupId;

        bcGroup group = groupsId[groupId];

        string conn = GetConnString(group.database);

        string sql = "SELECT branches.Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher, branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name " +
                    "FROM branches INNER JOIN [BI_masterDB].[dbo].[cities] ON branches.cityId = [BI_masterDB].[dbo].[cities].Id " +
                    "ORDER BY branches.branchName";
        DataSet ds = Dal.GetDataSetOld(sql, new List<SqlParameter>(), conn);

        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            foreach (DataRow item in t.Rows)
            {
                biDbBranch branch = new biDbBranch();
                branch.id = int.Parse(item["Id"].ToString());
                branch.branchName = item["branchName"].ToString();
                branch.address = item["address"].ToString();
                branch.city = item["city"].ToString();
                branch.biCommEmail = item["biCommEmail"].ToString();
                branch.kosher = bool.Parse(item["kosher"].ToString());
                branch.ip = item["ip"].ToString();
                branch.cityId = int.Parse(item["cityId"].ToString());
                branch.cityName = item["name"].ToString();

                result.Add(branch);
            }

        }

        return result;
    }


    [WebMethod]
    public static biDbBranch GetBranch(int branchId)
    {
        biDbBranch result = new biDbBranch();

        if (currentGroupId == 0)
        {
            return result;
        }

        bcGroup group = groupsId[currentGroupId];
        string conn = GetConnString(group.database);

        string sql = "SELECT Id, branchName, address, city, biCommEmail, kosher, ip, cityId " +
                    "FROM branches " +
                    "WHERE id=@id;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", branchId));
        DataSet ds = Dal.GetDataSetOld(sql, values, conn);

        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            if (t.Rows.Count > 0)
            {
                DataRow item = t.Rows[0];
                result = new biDbBranch();
                result.id = int.Parse(item["Id"].ToString());
                result.branchName = item["branchName"].ToString();
                result.address = item["address"].ToString();
                result.city = item["city"].ToString();
                result.biCommEmail = item["biCommEmail"].ToString();
                result.kosher = bool.Parse(item["kosher"].ToString());
                result.ip = item["ip"].ToString();
                result.cityId = int.Parse(item["cityId"].ToString());
                //result.cityName = item["name"].ToString();
            }
        }

        return result;
    }


    [WebMethod]
    public static void UpdateBranch(int branchId, string branchName, string ip, bool kosher, int cityId, string address, string email)
    {
        if (currentGroupId == 0)
        {
            return;
        }

        if (!citiesDic.ContainsKey(cityId))
        {
            return;
        }

        string cityName = citiesDic[cityId];

        bcGroup g = groupsId[currentGroupId];
        string conn = GetConnString(g.database);

        if (branchId == 0)
        {
            branchId = AppendBranch(branchName, address, cityName, email, kosher, cityId, ip, conn);
            AppendShifts_branch_overHours(branchId, 8, 1.25, conn);
            AppendShifts_branch_overHours(branchId, 12, 1.50, conn);

            //לנדוור
            if (conn.Contains("groupc682af17-2a1e-4298-86b7-6d29a4474c85"))
            {
                AppendbranchBenchmarkConfig(branchId, conn);
                UpdateBranchSubChain(branchId, conn);
            }
        }
        else
        {
            UpdateBranchToDB(branchId, branchName, address, cityId, cityName, email, ip, kosher);
        }
    }


    public static int AppendBranch(string branchName, string address, string cityName, string email, bool kosher, int cityId, string ip, string conn)
    {
        string sql = "INSERT INTO [dbo].[branches] ([branchName],[address],[city],[biCommEmail],[kosher],[cityId],[ip]) " +
                      "VALUES (@branchName,@address,@city,@biCommEmail,@kosher,@cityId,@ip); SELECT SCOPE_IDENTITY()";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchName", branchName));
        values.Add(new SqlParameter("@address", address));
        values.Add(new SqlParameter("@city", cityName));
        values.Add(new SqlParameter("@biCommEmail", email));
        values.Add(new SqlParameter("@kosher", kosher));
        values.Add(new SqlParameter("@cityId", cityId));
        values.Add(new SqlParameter("@ip", ip));


        int branchId = int.Parse(Dal.ExecuteScalar(sql, values, conn).ToString());
        return branchId;
    }

    public static void AppendShifts_branch_overHours(int branchId, int maxHours, double AdditionalPayment, string conn)
    {
        string sql = "INSERT INTO [dbo].[shifts_branch_overHours] ([branchId],[maxHours],[AdditionalPayment]) " +
                      "VALUES (@branchId,@maxHours,@AdditionalPayment)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));
        values.Add(new SqlParameter("@maxHours", maxHours));
        values.Add(new SqlParameter("@AdditionalPayment", AdditionalPayment));

        Dal.ExecuteNonQuery(sql, values, conn);
    }

    public static void AppendbranchBenchmarkConfig(int branchId, string conn)
    {
        //[branchId],[maxHours],[AdditionalPayment]
        //@branchId,@maxHours,@AdditionalPayment
        string sql = "INSERT INTO [dbo].[branchBenchmarkConfig] ([branchId]) " +
                      "VALUES (@branchId)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));

        Dal.ExecuteNonQuery(sql, values, conn);
    }

    public static void UpdateBranchSubChain(int branchId, string conn)
    {
        string sql = "UPDATE [dbo].[branches] SET [subChain] = @subChain WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@subChain", 1));
        values.Add(new SqlParameter("@id", branchId));
        Dal.ExecuteNonQuery(sql, values, conn);
    }

    private static void UpdateBranchToDB(int id, string branchName, string address, int cityId, string city, string email, string ip, bool kosher)
    {
        if (currentGroupId == 0)
        {
            return;
        }

        bcGroup g = groupsId[currentGroupId];
        string conn = GetConnString(g.database);


        string sql = "UPDATE       branches SET branchName = @branchName, address = @address, city = @city, biCommEmail = @biCommEmail, kosher = @kosher, cityId = @cityId, ip = @ip " +
                     "WHERE(Id = @Id)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@Id", id));
        values.Add(new SqlParameter("@branchName", branchName));
        values.Add(new SqlParameter("@address", address));
        values.Add(new SqlParameter("@city", city));
        values.Add(new SqlParameter("@biCommEmail", email));
        values.Add(new SqlParameter("@kosher", kosher));
        values.Add(new SqlParameter("@cityId", cityId));
        values.Add(new SqlParameter("@ip", ip));
        Dal.ExecuteNonQuery(sql, values, conn);
    }


    ///////////Poses
    [WebMethod]
    public static List<biDbPos> GetPoses(int branchId)
    {
        List<biDbPos> result = new List<biDbPos>();

        if (currentGroupId == 0)
        {
            return result;
        }

        bcGroup group = groupsId[currentGroupId];

        string conn = GetConnString(group.database);

        string sql = "SELECT pos.[Id],[udid],[dbVersion],[posName],[branchId],deductServiceFromTotal,deductServiceFromCash,deductServiceFromCredit,deductCouponComissionFromTotal,deductCouponComissionFromCouponTotal " +
                     "FROM [dbo].[pos] Inner Join posSumConfig ON pos.Id = posSumConfig.posId " +
                     "WHERE pos.branchId= @branchId " +
                     "Order by pos.Id;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));

        DataSet ds = Dal.GetDataSetOld(sql, values, conn);

        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            foreach (DataRow item in t.Rows)
            {
                biDbPos branch = new biDbPos();
                branch.id = int.Parse(item["Id"].ToString());
                branch.udid = int.Parse(item["udid"].ToString());
                branch.dbVersion = int.Parse(item["dbVersion"].ToString());
                branch.posName = item["posName"].ToString();
                branch.deductServiceFromTotal = bool.Parse(item["deductServiceFromTotal"].ToString());
                branch.deductServiceFromCash = bool.Parse(item["deductServiceFromCash"].ToString());
                branch.deductServiceFromCredit = bool.Parse(item["deductServiceFromCredit"].ToString());
                branch.deductCouponComissionFromTotal = bool.Parse(item["deductCouponComissionFromTotal"].ToString());
                branch.deductCouponComissionFromCouponTotal = bool.Parse(item["deductCouponComissionFromCouponTotal"].ToString());

                result.Add(branch);
            }

        }

        return result;
    }

    [WebMethod]
    public static biDbPos GetPos(int posId)
    {
        biDbPos result = new biDbPos();

        if (currentGroupId == 0)
        {
            return result;
        }

        bcGroup group = groupsId[currentGroupId];

        string conn = GetConnString(group.database);

        string sql = "SELECT pos.[Id],[udid],[dbVersion],[posName],[branchId],deductServiceFromTotal,deductServiceFromCash,deductServiceFromCredit,deductCouponComissionFromTotal,deductCouponComissionFromCouponTotal " +
                     "FROM [dbo].[pos] Left Join posSumConfig ON pos.Id = posSumConfig.posId " +
                     "WHERE pos.id= @posId " +
                     "Order by pos.Id;";

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));

        DataSet ds = Dal.GetDataSetOld(sql, values, conn);

        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            foreach (DataRow item in t.Rows)
            {
                result = new biDbPos();
                result.id = int.Parse(item["Id"].ToString());
                result.udid = int.Parse(item["udid"].ToString());
                result.dbVersion = int.Parse(item["dbVersion"].ToString());
                result.posName = item["posName"].ToString();
                result.deductServiceFromTotal = bool.Parse(item["deductServiceFromTotal"].ToString());
                result.deductServiceFromCash = bool.Parse(item["deductServiceFromCash"].ToString());
                result.deductServiceFromCredit = bool.Parse(item["deductServiceFromCredit"].ToString());
                result.deductCouponComissionFromTotal = bool.Parse(item["deductCouponComissionFromTotal"].ToString());
                result.deductCouponComissionFromCouponTotal = bool.Parse(item["deductCouponComissionFromCouponTotal"].ToString());


            }

        }

        return result;
    }

    [WebMethod]
    public static void UpdatePos(int posId, int branchId, int udid, int dbVersion, string posName,
                            bool deductServiceFromTotal, bool deductServiceFromCash, bool deductServiceFromCredit, bool deductCouponComissionFromTotal, bool deductCouponComissionFromCouponTotal)
    {
        if (currentGroupId == 0)
        {
            return;
        }


        bcGroup group = groupsId[currentGroupId];

        string conn = GetConnString(group.database);


        if (posId == 0)
        {
            udid = GetMaxUdid(conn) + 1;
            posId = AppendPos(posName, branchId, udid, conn);
            biDbBranch b = GetBranch(branchId);

            AppendX(posId, b.branchName, conn);

            AppendposSumConfig(posId, deductServiceFromTotal, deductServiceFromCash, deductServiceFromCredit, deductCouponComissionFromTotal, deductCouponComissionFromCouponTotal, conn);


        }
        else
        {
            UpdatePosDB(posId, udid, dbVersion, posName, conn);
            UpdateposSumConfig(posId, deductServiceFromTotal, deductServiceFromCash, deductServiceFromCredit, deductCouponComissionFromTotal, deductCouponComissionFromCouponTotal, conn);
        }
    }



    public static int GetMaxUdid(string conn)
    {
        int reuslt = 0;
        string sql = "SELECT MAX(udid) AS maxID FROM [dbo].[pos]";
        object o = Dal.ExecuteScalar(sql, new List<SqlParameter>(), conn);
        if (o != null)
        {
            if (int.TryParse(o.ToString(), out reuslt))
            {

            }
        }

        return reuslt;
    }


    public static void AppendposSumConfig(int posId, bool deductServiceFromTotal, bool deductServiceFromCash, bool deductServiceFromCredit, bool deductCouponComissionFromTotal,
        bool deductCouponComissionFromCouponTotal, string conn)
    {
        //@posId,@deductServiceFromTotal,@deductServiceFromCash,@deductServiceFromCredit,@deductCouponComissionFromTotal,@deductCouponComissionFromCouponTotal
        string sql = "INSERT INTO [dbo].[posSumConfig] ([posId],[deductServiceFromTotal],[deductServiceFromCash],[deductServiceFromCredit],[deductCouponComissionFromTotal],[deductCouponComissionFromCouponTotal]) " +
                      "VALUES (@posId,@deductServiceFromTotal,@deductServiceFromCash,@deductServiceFromCredit,@deductCouponComissionFromTotal,@deductCouponComissionFromCouponTotal)";

        //@,@,@,@,@,@
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@deductServiceFromTotal", deductServiceFromTotal));
        values.Add(new SqlParameter("@deductServiceFromCash", deductServiceFromCash));
        values.Add(new SqlParameter("@deductServiceFromCredit", deductServiceFromCredit));
        values.Add(new SqlParameter("@deductCouponComissionFromTotal", deductCouponComissionFromTotal));
        values.Add(new SqlParameter("@deductCouponComissionFromCouponTotal", deductCouponComissionFromCouponTotal));


        Dal.ExecuteNonQuery(sql, values, conn);
    }


    public static int AppendPos(string posName, int branchId, int udid, string conn)
    {
        string sql = "INSERT INTO [dbo].[pos] ([udid],[dbVersion],[posName],[branchId]) " +
                      "VALUES (@udid, @zigi, @posName, @branchId); SELECT SCOPE_IDENTITY()";

        sql = "INSERT INTO [dbo].[pos] ([udid],[dbVersion],[posName],[branchId]) " +
                      "VALUES (" + udid + ", 0, N'" + posName + "', " + branchId + "); SELECT SCOPE_IDENTITY()";
        List<SqlParameter> values = new List<SqlParameter>();

        int posID = int.Parse(Dal.ExecuteScalar(sql, values, conn).ToString());
        return posID;
    }

    public static void AppendX(int posId, string CusName, string conn)
    {
        //[udid],[dbVersion],[posName],[branchId]
        //@udid,@dbVersion,@posName,@branchId
        string sql = "INSERT INTO [dbo].[X] ([posId],[CusName]) " +
                      "VALUES (@posId,@CusName)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@CusName", CusName));

        Dal.ExecuteNonQuery(sql, values, conn);
    }



    private static void UpdatePosDB(int id, int udid, int dbVersion, string posName, string conn)
    {
        string sql = "UPDATE pos SET udid = @udid, dbVersion = @dbVersion, posName = @posName " +
                     "WHERE(Id = @Id)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@Id", id));
        values.Add(new SqlParameter("@udid", udid));
        values.Add(new SqlParameter("@dbVersion", dbVersion));
        values.Add(new SqlParameter("@posName", posName));
        //values.Add(new SqlParameter("@branchId", branchId));
        Dal.ExecuteNonQuery(sql, values, conn);

    }

    private static void UpdateposSumConfig(int posId, bool deductServiceFromTotal, bool deductServiceFromCash,
            bool deductServiceFromCredit, bool deductCouponComissionFromTotal, bool deductCouponComissionFromCouponTotal, string conn)
    {
        string sql = "UPDATE posSumConfig SET deductServiceFromTotal = @deductServiceFromTotal, deductServiceFromCash = @deductServiceFromCash, deductServiceFromCredit = @deductServiceFromCredit, deductCouponComissionFromTotal = @deductCouponComissionFromTotal, deductCouponComissionFromCouponTotal = @deductCouponComissionFromCouponTotal " +
                         "WHERE (posId = @posId)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@deductServiceFromTotal", deductServiceFromTotal));
        values.Add(new SqlParameter("@deductServiceFromCash", deductServiceFromCash));
        values.Add(new SqlParameter("@deductServiceFromCredit", deductServiceFromCredit));
        values.Add(new SqlParameter("@deductCouponComissionFromTotal", deductCouponComissionFromTotal));
        values.Add(new SqlParameter("@deductCouponComissionFromCouponTotal", deductCouponComissionFromCouponTotal));
        Dal.ExecuteNonQuery(sql, values, conn);

    }



    public class bcGroup
    {
        public int id { get; set; }
        public string name { get; set; }
        public string database { get; set; }
    }


    public class biDbBranch
    {
        //Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher,branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name
        public int id { get; set; }
        public string branchName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string biCommEmail { get; set; }
        public bool kosher { get; set; }
        public string ip { get; set; }
        public int cityId { get; set; }
        public string cityName { get; set; }
    }


    public class biDbPos
    {
        public int id { get; set; }
        public int udid { get; set; }
        public int dbVersion { get; set; }
        public string posName { get; set; }

        public bool deductServiceFromTotal;
        public bool deductServiceFromCash;
        public bool deductServiceFromCredit;
        public bool deductCouponComissionFromTotal;
        public bool deductCouponComissionFromCouponTotal;
    }


    public class biDbCity
    {
        //Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher,branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name
        public int id { get; set; }
        public string cityName { get; set; }

    }

}
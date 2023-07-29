using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class SettingsV3 : System.Web.UI.Page
{
    private static Dictionary<int, bcGroup> groupsId = new Dictionary<int, bcGroup>();
    private static List<bcGroup> groups = new List<bcGroup>();
    private static DataSet groupsDS;

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

    static string connString = "Data Source=bidb.beecomm.co.il;Initial Catalog=BI_masterDB;Persist Security Info=True;User ID=shahaf;Password=#JxMj8wQDbJnmDVsR";
    //"Server=bidb.beecomm.co.il;Database=BI_masterDB;User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
    //Data Source=bidb.beecomm.co.il;Initial Catalog=BI_masterDB;Persist Security Info=True;User ID=shahaf;Password=#JxMj8wQDbJnmDVsR

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Worker"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        Worker w = (Worker)Session["Worker"];
        if (w.userTypeId != 1)
        {
            Response.Redirect("AddProblemNew.aspx");
        }

        SetGroupList();

    }

    private void SetGroupList()
    {

        groups = new List<bcGroup>();
        //string sql = "SELECT [id], [groupName], [databaseName] FROM [Groups] ORDER BY [groupName];";

        //DataSet ds = Dal.GetDataSetOld(sql, new List<SqlParameter>(), connString);
        SetGroupsDS();
        DataSet ds = groupsDS;
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
    }

    private static void SetGroupsDS()
    {
        string sql = "SELECT [id], [groupName], [databaseName] FROM [dbo].[Groups] ORDER BY [groupName];";
        groupsDS = Dal.GetDataSetOld(sql, null, connString);
    }

    [WebMethod]
    public static List<bcGroup> GetAllGroups()
    {
        return groups;
    }

    [WebMethod]
    public static List<biDbBranch> GetBranches(int groupId)
    {
        List<biDbBranch> result = new List<global::SettingsV3.biDbBranch>();

        if (!groupsId.ContainsKey(groupId))
        {
            return result;
        }

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


    //////////////////////////////////////


    [WebMethod]
    public static List<bcGroup> GetGroups(string groupName)
    {
        List<bcGroup> result = new List<bcGroup>();
        if (string.IsNullOrEmpty(groupName))
        {
            return result;
        }

        if (groups.Count > 0)
        {
            foreach (bcGroup s in groups)
            {
                if (s.name.Contains(groupName))
                {
                    result.Add(s);
                }
            }
        }
        return result;
    }


    private static string GetConnString(string dataBaseName)
    {
        return "Server=bidb.beecomm.co.il;Database=" + dataBaseName + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
    }

    //public DataSet GetDataSetOld1(string queryString, List<SqlParameter> values, string connS)
    //{
    //    DataSet result = new DataSet();
    //    try
    //    {
    //        using (SqlConnection conn = new SqlConnection(connS))
    //        {
    //            SqlCommand cmd = new SqlCommand(queryString, conn);

    //            if (values != null)
    //            {
    //                cmd.Parameters.AddRange(values.ToArray());
    //            }

    //            SqlDataAdapter myDataAdapter = new SqlDataAdapter(cmd);

    //            myDataAdapter.Fill(result, queryString);
    //        }
    //        //conn.Close();
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //        //lbl.Text = "GetDataSetOld1: Error Msg:" + e.Message + Environment.NewLine + "Inner Msg: " + e.InnerException;
    //    }
    //    return result;
    //}

    //public object ExecuteScalar(string sql, List<SqlParameter> values, string connS)
    //{
    //    object result = null;
    //    try
    //    {
    //        using (SqlConnection conn = new SqlConnection(connS))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand(sql, conn))
    //            {
    //                if (values != null)
    //                {
    //                    cmd.Parameters.AddRange(values.ToArray());
    //                }

    //                result = cmd.ExecuteScalar();
    //            }
    //        }
    //        //conn.Close();
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //    return result;
    //}

    //public object ExecuteNonQuery(string sql, List<SqlParameter> values, string connS)
    //{
    //    object result = null;
    //    try
    //    {
    //        using (SqlConnection conn = new SqlConnection(connS))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand(sql, conn))
    //            {
    //                if (values != null)
    //                {
    //                    cmd.Parameters.AddRange(values.ToArray());
    //                }

    //                result = cmd.ExecuteNonQuery();
    //            }
    //        }
    //        //conn.Close();
    //    }
    //    catch (SqlException se)
    //    {
    //        throw se;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //    return result;
    //}

    private void SetBranches()
    {
        string conn = Session["connString"].ToString();
        string sql = "SELECT branches.Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher,branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name " +
                     "FROM branches INNER JOIN [BI_masterDB].[dbo].[cities] ON branches.cityId = [BI_masterDB].[dbo].[cities].Id " +
                     "ORDER BY branches.branchName";
        DataSet ds = Dal.GetDataSetOld(sql, new List<SqlParameter>(), conn);
        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            grdBranches.DataSource = t;
            grdBranches.DataBind();
            //cboUpdateBranch.DataSource = t;
            //cboUpdateBranch.DataBind();
            cboUpdatePosAnotherBranch.DataSource = t;
            cboUpdatePosAnotherBranch.DataBind();
            cboAddPosBranches.DataSource = t;
            cboAddPosBranches.DataBind();
        }

    }

    protected void grdBranches_SelectedIndexChanged(object sender, EventArgs e)
    {
        string branchId = grdBranches.Rows[grdBranches.SelectedIndex].Cells[2].Text;
        //Session["connString"] = "Server=bidb.beecomm.co.il;Database=" + db + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";// WebDal.GetConn(db);

        SetPoses(branchId);



    }

    public void SetPoses(string branchId)
    {
        string sql = "SELECT        pos.Id, pos.udid, pos.dbVersion, pos.posName, pos.branchId, branches.branchName, posSumConfig.deductServiceFromTotal, posSumConfig.deductServiceFromCash, posSumConfig.deductServiceFromCredit, posSumConfig.deductCouponComissionFromTotal, posSumConfig.deductCouponComissionFromCouponTotal FROM            pos INNER JOIN branches ON pos.branchId = branches.Id INNER JOIN posSumConfig ON pos.Id = posSumConfig.Id WHERE        (pos.branchId = @branchId)";
        string conn = Session["connString"].ToString();

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));
        DataSet ds = Dal.GetDataSetOld(sql, values, conn);
        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            grdPos.DataSource = t;
            grdPos.DataBind();
            //cboUpdatePos.DataSource = t;
            //cboUpdatePos.DataBind();
        }
    }

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAddGrpopName.Text))
        {
            return;
        }

        string groupName = txtAddGrpopName.Text;

        if (IsGroupExists(groupName))
        {
            ShowMsg("קיימת כבר קבוצה עם השם " + groupName);
            return;
        }

        string dbName = "group" + Guid.NewGuid().ToString();

        int groupID = GetMaxGroupID() + 1;
        AppendGroup(groupID, groupName, dbName);
        CreateDataBase(dbName);
        //CreateDataBase("aaa");

        txtAddGrpopName.Text = "";
        //SetConns();
        //Response.Redirect(Request.RawUrl);
    }

    public bool IsGroupExists(string groupName)
    {
        string sql = "SELECT id  FROM [BI_masterDB].[dbo].[Groups]    where groupName=@groupName";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@groupName", groupName));

        object o = Dal.ExecuteScalar(sql, values, GetConnString("BI_masterDB"));
        if (o != null)
        {
            int i = 0;
            if (int.TryParse(o.ToString(), out i))
            {
                if (i > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetMaxGroupID()
    {
        string sql = "SELECT MAX(id) AS maxID FROM Groups";
        int i = int.Parse(Dal.ExecuteScalar(sql, new List<SqlParameter>(), GetConnString("BI_masterDB")).ToString());
        return i;
    }

    public void AppendGroup(int id, string groupName, string databaseName)
    {
        string sql = "INSERT INTO [dbo].[Groups] ([id],[groupName],[databaseName],[timeZone]) VALUES (@id,@groupName,@databaseName,@timeZone)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@id", id));
        values.Add(new SqlParameter("@groupName", groupName));
        values.Add(new SqlParameter("@databaseName", databaseName));
        values.Add(new SqlParameter("@timeZone", "Asia/Jerusalem"));

        Dal.ExecuteScalar(sql, values, GetConnString("BI_masterDB"));
    }

    public void CreateDataBase(string newDataBaseName)
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
    public void CreateDatabaseStoredFunctions(string newDataBaseName)
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

    public void CreateDataBaseTables(string newDataBaseName)
    {
        string script = File.ReadAllText(@"D:\home\site\wwwroot\SqlScripts\DataV3\CreateTables.sql", Encoding.UTF8);
        script = script.Replace("@NewDatabaseName", newDataBaseName);
        Dal.ExecuteNonQuery(script, new List<SqlParameter>(), GetConnString(newDataBaseName));
    }

    public void InsertBasicTablesInfo(string newDataBaseName)
    {
        string script = File.ReadAllText(@"D:\home\site\wwwroot\SqlScripts\DataV3\InsertAllBaseData.sql", Encoding.UTF8);
        //string script = File.ReadAllText(@"C:\Users\NEZEK7\Documents\Visual Studio 2015\WebSites\bcCRM\SqlScripts\DataV3\InsertAllBaseData.sql", Encoding.UTF8);
        script = script.Replace("@NewDatabaseName", newDataBaseName);
        Dal.ExecuteNonQuery(script, new List<SqlParameter>(), GetConnString(newDataBaseName));
        //INSERT INTO[dbo].[Dishes] ([Id],[Name],[dishCategoryId]) VALUES(0, N'לא מקוטלג',0)
    }

    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAddBramchName.Text))
        {
            return;
        }

        if (string.IsNullOrEmpty(txtAddBranchAddress.Text))
        {
            return;
        }

        if (string.IsNullOrEmpty(txtAddBranchIP.Text))
        {
            return;
        }

        if (Session["connString"] == null)
        {
            return;
        }

        string conn = Session["connString"].ToString();

        if (string.IsNullOrEmpty(conn))
        {
            return;
        }

        string branchName = txtAddBramchName.Text;

        if (!branchName.Contains("סניף"))
        {
            branchName = "סניף " + branchName;
        }
        string address = txtAddBranchAddress.Text;
        string ip = txtAddBranchIP.Text;
        string cityName = cboAddBranchCity.SelectedItem.Text;
        int cityId = int.Parse(cboAddBranchCity.SelectedItem.Value);
        string email = txtEmail.Text;
        bool kosher = chkAddBranchKosher.Checked;

        int branchid = AppendBranch(branchName, address, cityName, email, kosher, cityId, ip, conn);

        if (branchid == 0)
        {
            return;
        }

        AppendShifts_branch_overHours(branchid, 8, 1.25, conn);
        AppendShifts_branch_overHours(branchid, 12, 1.50, conn);

        if (conn.Contains("groupc682af17-2a1e-4298-86b7-6d29a4474c85"))
        {
            AppendbranchBenchmarkConfig(branchid, conn);
            UpdateBranchSubChain(branchid, conn);
        }


        txtAddBramchName.Text = "";
        txtAddBranchIP.Text = "";
        txtAddBranchAddress.Text = "";

        SetBranches();
    }

    public int AppendBranch(string branchName, string address, string cityName, string email, bool kosher, int cityId, string ip, string conn)
    {
        //[branchName],[address],[city],[biCommEmail],[kosher],[cityId],[ip]
        //@branchName,@address,@city,@biCommEmail,@kosher,@cityId,@ip
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

    public void AppendShifts_branch_overHours(int branchId, int maxHours, double AdditionalPayment, string conn)
    {
        //[branchId],[maxHours],[AdditionalPayment]
        //@branchId,@maxHours,@AdditionalPayment
        string sql = "INSERT INTO [dbo].[shifts_branch_overHours] ([branchId],[maxHours],[AdditionalPayment]) " +
                      "VALUES (@branchId,@maxHours,@AdditionalPayment)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));
        values.Add(new SqlParameter("@maxHours", maxHours));
        values.Add(new SqlParameter("@AdditionalPayment", AdditionalPayment));

        Dal.ExecuteNonQuery(sql, values, conn);
    }


    public void AppendbranchBenchmarkConfig(int branchId, string conn)
    {
        //[branchId],[maxHours],[AdditionalPayment]
        //@branchId,@maxHours,@AdditionalPayment
        string sql = "INSERT INTO [dbo].[branchBenchmarkConfig] ([branchId]) " +
                      "VALUES (@branchId)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));

        Dal.ExecuteNonQuery(sql, values, conn);
    }

    public void UpdateBranchSubChain(int branchId, string conn)
    {
        string sql = "UPDATE [dbo].[branches] SET [subChain] = @subChain WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@subChain", 1));
        values.Add(new SqlParameter("@id", branchId));
        Dal.ExecuteNonQuery(sql, values, Session["connString"].ToString());
    }

    protected void btnAddPos_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAddPosName.Text))
        {
            return;
        }

        if (Session["connString"] == null)
        {
            return;
        }

        string conn = Session["connString"].ToString();

        if (string.IsNullOrEmpty(conn))
        {
            return;
        }

        string posName = txtAddPosName.Text;
        int branchIs = int.Parse(cboAddPosBranches.SelectedItem.Value);
        string branchName = cboAddPosBranches.SelectedItem.Text;

        if (branchIs == 0)
        {
            return;
        }

        int udid = GetMaxUdid(conn) + 1;
        int posID = AppendPos(posName, branchIs, udid, conn);

        AppendX(posID, branchName, conn);

        AppendposSumConfig(posID, chkConfigZSumTotalWithoutService.Checked, chkConfigZSumCashWithoutService.Checked, chkConfigZSumCreditWithoutService.Checked,
                             chkConfigZSumTotalWithoutCuponCommision.Checked, chkConfigZSumCuponWithoutCuponCommision.Checked, conn);

        txtAddPosName.Text = "";
        string branchId = grdBranches.Rows[grdBranches.SelectedIndex].Cells[1].Text;
        SetPoses(branchId);

    }

    public int GetMaxUdid(string conn)
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


    public void AppendposSumConfig(int posId, bool deductServiceFromTotal, bool deductServiceFromCash, bool deductServiceFromCredit, bool deductCouponComissionFromTotal,
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


    public int AppendPos(string posName, int branchId, int udid, string conn)
    {
        string sql = "INSERT INTO [dbo].[pos] ([udid],[dbVersion],[posName],[branchId]) " +
                      "VALUES (@udid, @zigi, @posName, @branchId); SELECT SCOPE_IDENTITY()";

        sql = "INSERT INTO [dbo].[pos] ([udid],[dbVersion],[posName],[branchId]) " +
                      "VALUES (" + udid + ", 0, N'" + posName + "', " + branchId + "); SELECT SCOPE_IDENTITY()";
        List<SqlParameter> values = new List<SqlParameter>();

        int posID = int.Parse(Dal.ExecuteScalar(sql, values, conn).ToString());
        return posID;
    }

    public void AppendX(int posId, string CusName, string conn)
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


    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtAddCity.Text))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(txtLongitude.Text))
        {
            return;

        }

        if (string.IsNullOrWhiteSpace(txtLatitude.Text))
        {
            return;
        }



        string cityName = txtAddCity.Text;
        string Longitude = txtLongitude.Text;
        string Latitude = txtLatitude.Text;

        if (GetCityID(cityName) > 0)
        {
            return;
        }


        AppendCity(cityName, Longitude, Latitude);

        txtAddCity.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
    }

    private int GetCityID(string cityName)
    {
        int result = 0;

        string sql = "SELECT id FROM [dbo].[cities] WHERE [name]=@name";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@name", cityName));
        object o = Dal.ExecuteScalar(sql, values, GetConnString("BI_masterDB"));

        if (o != null)
        {
            int.TryParse(o.ToString(), out result);
        }

        return result;
    }

    public void AppendCity(string cityName, string Longitude, string Latitude)
    {
        string sql = "INSERT INTO [dbo].[cities] ([name],[lat],[lng]) VALUES (@name,@lat,@lng)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@name", cityName));
        values.Add(new SqlParameter("@lat", Latitude));
        values.Add(new SqlParameter("@lng", Longitude));

        Dal.ExecuteNonQuery(sql, values, GetConnString("BI_masterDB"));
        cboAddBranchCity.DataBind();
    }


    protected void btnUpdateBranchName_Click(object sender, EventArgs e)
    {
        string branchID = "";// cboUpdateBranch.SelectedValue;
        if (string.IsNullOrEmpty(branchID))
        {
            return;
        }

        string branchName = txtUpdateBranchName.Text;

        if (string.IsNullOrWhiteSpace(txtUpdateBranchName.Text))
        {
            return;
        }

        if (!branchName.Contains("סניף"))
        {
            branchName = "סניף " + branchName;
        }

        //string sql = "UPDATE [dbo].[X] SET [CusName] = '" + branchName + "' WHERE posId=" + posID;
        string sql = "UPDATE [dbo].[branches] SET [branchName] = @branchName WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchName", branchName));
        values.Add(new SqlParameter("@id", branchID));
        Dal.ExecuteNonQuery(sql, values, Session["connString"].ToString());

        txtUpdateBranchName.Text = "";

        SetBranches();
    }

    protected void btnUpdateCity_Click(object sender, EventArgs e)
    {
        string branchID = "";// cboUpdateBranch.SelectedValue;
        if (string.IsNullOrEmpty(branchID))
        {
            return;
        }

        string cityId = cboUpdateBranchCity.SelectedValue;
        string cityName = cboUpdateBranchCity.SelectedItem.Text;

        string sql = "UPDATE [dbo].[branches] SET city= @city, cityId=@cityId WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@city", cityName));
        values.Add(new SqlParameter("@cityId", cityId));
        values.Add(new SqlParameter("@id", branchID));

        Dal.ExecuteScalar(sql, values, Session["connString"].ToString());


    }

    protected void btnUpdateBranchIP_Click(object sender, EventArgs e)
    {
        string branchID = "";// cboUpdateBranch.SelectedValue;
        if (string.IsNullOrEmpty(branchID))
        {
            return;
        }

        string Ip = txtUpdateBranchIP.Text;

        if (string.IsNullOrWhiteSpace(Ip))
        {
            return;
        }

        //string sql = "UPDATE [dbo].[X] SET [CusName] = '" + branchName + "' WHERE posId=" + posID;
        string sql = "UPDATE [dbo].[branches] SET [ip] = @Ip WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@Ip", Ip));
        values.Add(new SqlParameter("@id", branchID));
        Dal.ExecuteNonQuery(sql, values, Session["connString"].ToString());

        txtUpdateBranchIP.Text = "";

        SetBranches();
    }


    protected void btnUpdateBranchAddress_Click(object sender, EventArgs e)
    {
        string branchID = "";// cboUpdateBranch.SelectedValue;
        if (string.IsNullOrEmpty(branchID))
        {
            return;
        }

        string address = txtUpdateBranchAddress.Text;

        if (string.IsNullOrWhiteSpace(address))
        {
            return;
        }

        //string sql = "UPDATE [dbo].[X] SET [CusName] = '" + branchName + "' WHERE posId=" + posID;
        string sql = "UPDATE [dbo].[branches] SET [address] = @address WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@address", address));
        values.Add(new SqlParameter("@id", branchID));
        Dal.ExecuteNonQuery(sql, values, Session["connString"].ToString());

        txtUpdateBranchIP.Text = "";

        SetBranches();
    }


    protected void btnUpdateBranchKosher_Click(object sender, EventArgs e)
    {
        string branchID = "";// cboUpdateBranch.SelectedValue;
        if (string.IsNullOrEmpty(branchID))
        {
            return;
        }

        bool kosher = chkUpdateBranchKosher.Checked;

        //string sql = "UPDATE [dbo].[X] SET [CusName] = '" + branchName + "' WHERE posId=" + posID;
        string sql = "UPDATE [dbo].[branches] SET [kosher] = @kosher WHERE id=@id";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@kosher", kosher));
        values.Add(new SqlParameter("@id", branchID));
        Dal.ExecuteNonQuery(sql, values, Session["connString"].ToString());

        txtUpdateBranchIP.Text = "";

        SetBranches();
    }

    protected void btnSelectGroup_Click(object sender, EventArgs e)
    {
        if (lstGroups.SelectedIndex == -1)
        {
            return;
        }

        int i = int.Parse(lstGroups.SelectedValue);
        txtGroupId.Value = i.ToString();
        txtSearchGroup.Text = lstGroups.SelectedItem.Text;
        btnShowGroup_Click(null, null);
    }

    protected void btnShowGroup_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchGroup.Text) || string.IsNullOrWhiteSpace(txtSearchGroup.Text))
        {
            ShowMsg("אנא הזן את שם המקום");
            return;
        }

        string groupName = txtSearchGroup.Text;
        foreach (bcGroup group in groups)
        {
            if (group.name == groupName)
            {
                txtGroupId.Value = group.id.ToString();
                lblGroupDatabaseName.Text = group.database;
                lblGroupName.Text = group.name;
                txtSearchGroup.Text = "";
                Session["connString"] = "Server=bidb.beecomm.co.il;Database=" + group.database + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
                SetBranches();
                return;
            }
        }
    }

    private void ShowMsg(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Beecomm", "alert('" + msg + "');", true);
    }


    protected void btn_EditBranch_Click(object sender, EventArgs e)
    {
        Button b = sender as Button;
        DataControlFieldCell f = b.Parent as DataControlFieldCell;
        GridViewRow row = f.Parent as GridViewRow;

        string id = row.Cells[2].Text;
        string branchName = row.Cells[3].Text;
        string address = row.Cells[4].Text;
        string city = row.Cells[5].Text;
        string email = row.Cells[6].Text;
        CheckBox c = row.Cells[7].Controls[0] as CheckBox;
        bool kosher = c.Checked;
        string ip = row.Cells[8].Text;

        txtUpdateBranchId.Value = id;
        txtUpdateBranchName.Text = branchName;
        txtUpdateBranchAddress.Text = address;
        txtUpdateBranchEmail.Text = email;
        txtUpdateBranchIP.Text = ip;
        chkUpdateBranchKosher.Checked = kosher;

        if (cboAddBranchCity.Items.Count > 0)
        {
            for (int i = 0; i < cboAddBranchCity.Items.Count; i++)
            {
                if (cboAddBranchCity.Items[i].Text == city)
                {

                    cboUpdateBranchCity.SelectedValue = cboAddBranchCity.Items[i].Value;
                }
            }
        }

        divOverlayserver.Style["display"] = "block";
        divOverlayserver.Visible = true;

        divEditBranch.Style["display"] = "block";
        divEditBranch.Visible = true;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowDiv('divEditBranch')", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowDiv(divEditBranch)", true);


    }

    protected void btnUpdateBranch_Click(object sender, EventArgs e)
    {
        string id = txtUpdateBranchId.Value;
        string branchName = txtUpdateBranchName.Text;
        string address = txtUpdateBranchAddress.Text;
        string city = cboUpdateBranchCity.SelectedItem.Text;
        string cityId = cboUpdateBranchCity.SelectedItem.Value;
        string email = txtUpdateBranchEmail.Text;
        bool kosher = chkUpdateBranchKosher.Checked;
        string ip = txtUpdateBranchIP.Text;
        int branchId = int.Parse(id);
        UpdateBranch(id, branchName, address, cityId, city, email, ip, kosher);
    }

    private void UpdateBranch(string id, string branchName, string address, string cityId, string city, string email, string ip, bool kosher)
    {
        string conn = GetConnString(lblGroupDatabaseName.Text);

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

        grdBranches.DataBind();
    }

    protected void btnCancelUpdateBranch_Click(object sender, EventArgs e)
    {
        divOverlayserver.Style["display"] = "none";
        divOverlayserver.Visible = false;

        divEditBranch.Style["display"] = "none";
        divEditBranch.Visible = false;
    }

    protected void btnShowEditPos_Click(object sender, EventArgs e)
    {
        Button b = sender as Button;
        DataControlFieldCell f = b.Parent as DataControlFieldCell;
        GridViewRow row = f.Parent as GridViewRow;

        string id = row.Cells[1].Text;
        string udid = row.Cells[2].Text;
        string dbVersion = row.Cells[3].Text;
        string posName = row.Cells[4].Text;
        string branchName = row.Cells[5].Text;
        string totalSinService = row.Cells[6].Text;
        string cashSinService = row.Cells[7].Text;
        string creditSinService = row.Cells[8].Text;
        string totalSinCuponCommision = row.Cells[9].Text;
        string cuponSinCuponCommision = row.Cells[10].Text;

        txtUpdatePosID.Value = id;
        txtUpdatePosUdid.Text = udid;
        txtUpdatePosName.Text = posName;
        txtUpdatePosDbVersion.Text = dbVersion;

        chkUpdatePosTotalNoService.Checked = bool.Parse(totalSinService);
        chkUpdatePosCashNoService.Checked = bool.Parse(cashSinService);
        chkUpdatePosCreditNoService.Checked = bool.Parse(creditSinService);
        chkUpdatePosTotalNoCommision.Checked = bool.Parse(totalSinCuponCommision);
        chkUpdatePosCuponNoCommision.Checked = bool.Parse(cuponSinCuponCommision);

        divOverlayserver.Style["display"] = "block";
        divOverlayserver.Visible = true;

        divEditPos.Style["display"] = "block";
        divEditPos.Visible = true;
    }

    protected void btnCancelUpdatePos_Click(object sender, EventArgs e)
    {
        divOverlayserver.Style["display"] = "none";
        divOverlayserver.Visible = false;

        divEditPos.Style["display"] = "none";
        divEditPos.Visible = false;
    }

    protected void btnUpdatePos_Click(object sender, EventArgs e)
    {
        string id = txtUpdatePosID.Value;
        string udid = txtUpdatePosUdid.Text;
        string dbVersion = txtUpdatePosDbVersion.Text;
        string posName = txtUpdatePosName.Text;

        int branchId = int.Parse(cboUpdatePosAnotherBranch.SelectedValue);



        bool totalSinService = chkUpdatePosTotalNoService.Checked;
        bool cashSinService = chkUpdatePosCashNoService.Checked;
        bool creditSinService = chkUpdatePosCreditNoService.Checked;
        bool totalSinCuponCommision = chkUpdatePosTotalNoCommision.Checked;
        bool cuponSinCuponCommision = chkUpdatePosCuponNoCommision.Checked;

        int posId = int.Parse(id);

        UpdatePos(posId, udid, dbVersion, posName, branchId);
        UpdateposSumConfig(posId, totalSinService, cashSinService, creditSinService, totalSinCuponCommision, cuponSinCuponCommision);
        grdPos.DataBind();
    }

    private void UpdatePos(int id, string udid, string dbVersion, string posName, int branchId)
    {
        string conn = GetConnString(lblGroupDatabaseName.Text);

        string sql = "UPDATE pos SET udid = @udid, dbVersion = @dbVersion, posName = @posName, branchId = @branchId " +
                     "WHERE(Id = @Id)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@Id", id));
        values.Add(new SqlParameter("@udid", udid));
        values.Add(new SqlParameter("@dbVersion", dbVersion));
        values.Add(new SqlParameter("@posName", posName));
        values.Add(new SqlParameter("@branchId", branchId));
        Dal.ExecuteNonQuery(sql, values, conn);

        grdBranches.DataBind();
    }

    private void UpdateposSumConfig(int posId, bool deductServiceFromTotal, bool deductServiceFromCash, bool deductServiceFromCredit, bool deductCouponComissionFromTotal, bool deductCouponComissionFromCouponTotal)
    {
        string conn = GetConnString(lblGroupDatabaseName.Text);

        string sql = "UPDATE posSumConfig SET deductServiceFromTotal = @deductServiceFromTotal, deductServiceFromCash = @deductServiceFromCash, deductServiceFromCredit = @deductServiceFromCredit, deductCouponComissionFromTotal = @deductCouponComissionFromTotal, deductCouponComissionFromCouponTotal = @deductCouponComissionFromCouponTotal " +
                         "WHERE(posId = @posId)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@deductServiceFromTotal", deductServiceFromTotal));
        values.Add(new SqlParameter("@deductServiceFromCash", deductServiceFromCash));
        values.Add(new SqlParameter("@deductServiceFromCredit", deductServiceFromCredit));
        values.Add(new SqlParameter("@deductCouponComissionFromTotal", deductCouponComissionFromTotal));
        values.Add(new SqlParameter("@deductCouponComissionFromCouponTotal", deductCouponComissionFromCouponTotal));
        Dal.ExecuteNonQuery(sql, values, conn);

        grdBranches.DataBind();
    }


    protected void btnShowBranches_Click(object sender, EventArgs e)
    {
        return;
        string sql = "SELECT branches.Id, branches.branchName FROM branches ORDER BY branches.branchName";
        List<BranchesGroups> result = new List<global::SettingsV3.BranchesGroups>();
        //Session["connString"] = 
        foreach (var group in groups)
        {
            string conn = "Server=bidb.beecomm.co.il;Database=" + group.database + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";

            DataSet ds = Dal.GetDataSetOld(sql, new List<SqlParameter>(), conn);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    BranchesGroups b = new BranchesGroups();
                    b.groupId = group.id;
                    b.groupName = group.name;
                    b.branchId = int.Parse(row["Id"].ToString());
                    b.branchName = row["branchName"].ToString();

                    result.Add(b);
                }
            }
        }
    }

    public class BranchesGroups
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
    }
}
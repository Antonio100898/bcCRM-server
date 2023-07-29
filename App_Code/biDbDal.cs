using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for biDbDal
/// </summary>
public class biDbDal
{
    static string connString = "Data Source=bidb.beecomm.co.il;Initial Catalog=BI_masterDB;Persist Security Info=True;User ID=shahaf;Password=#JxMj8wQDbJnmDVsR";


    private static string GetConnString(string dataBaseName)
    {
        return "Server=bidb.beecomm.co.il;Database=" + dataBaseName + ";User Id=shahaf;Password=#JxMj8wQDbJnmDVsR;";
    }


    public static List<v3Group> GetGroups()
    {
        List<v3Group> result = new List<v3Group>();
        string sql = "SELECT [id], [groupName], [databaseName] FROM [dbo].[Groups] ORDER BY [groupName];";
        DataSet ds = Dal.GetDataSetOld(sql, null, connString);
        //DataSet ds = Dal.GetDataSet(sql, null, connString);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    v3Group g = new v3Group();
                    g.id = int.Parse(item["id"].ToString());
                    g.name = item["groupName"].ToString();
                    g.database = item["databaseName"].ToString();
                    result.Add(g);
                }
            }
        }

        return result;
    }

    public static List<v3Branch> GetBranches(string database)
    {
        List<v3Branch> result = new List<v3Branch>();

        string conn = GetConnString(database);

        string sql = "SELECT branches.Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher, branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name " +
                    "FROM branches INNER JOIN [BI_masterDB].[dbo].[cities] ON branches.cityId = [BI_masterDB].[dbo].[cities].Id " +
                    "ORDER BY branches.branchName";
        DataSet ds = Dal.GetDataSetOld(sql, new List<SqlParameter>(), conn);

        if (ds.Tables.Count > 0)
        {
            DataTable t = ds.Tables[0];
            foreach (DataRow item in t.Rows)
            {
                v3Branch branch = new v3Branch();
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


    public static List<v3City> GetCities()
    {
        List<v3City> result = new List<v3City>();

        string sql = "SELECT [Id],[name] FROM [dbo].[cities] order by [name];";
        DataSet ds = Dal.GetDataSetOld(sql, null, connString);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    v3City city = new v3City();
                    city.id = int.Parse(item["id"].ToString());
                    city.cityName = item["name"].ToString();

                    result.Add(city);
                }
            }
        }

        return result;
    }
}
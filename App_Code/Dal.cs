using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Dal
{
    private const int timeOut = 300;

    private const string connString = "Server=tcp:beecommserver.database.windows.net,1433;Initial Catalog = BeecommDB; Persist Security Info=False;User ID = bcCrmUser; Password=5af9302e-fe64-4178-a5cb-018850a49e4e; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

    public static object GetThisOneValue(string sql, List<SqlParameter> values = null,object defultValue=null)
    {
        object result = defultValue;
        try
        {
            result = ExecuteScalar(sql,values);
            if (result == DBNull.Value)
            {
                result = defultValue;
            }
            if (result == null)
            {
                result = defultValue;
            }
        }
        catch (Exception e)
        {
            Logger.ErrorLog("GetThisOneValue", e, "sql: " + sql);
        }
        return result;
    }

    public static object ExecuteScalar(string sql, List<SqlParameter> values = null, string connS = connString)
    {
        object result = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connS))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (values != null)
                    {
                        cmd.Parameters.AddRange(values.ToArray());
                    }

                    result = cmd.ExecuteScalar();
                }
            }
            //conn.Close();
        }
        catch (Exception e)
        {
            Logger.ErrorLog("ExecuteScalar", e, "sql: " + sql);
        }
        return result;
    }
    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]

    public static void ExecuteNonQuery(string sql, List<SqlParameter> values = null, string connS = connString)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connS))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (values != null)
                    {
                        cmd.Parameters.AddRange(values.ToArray());
                    }
                    cmd.ExecuteNonQuery();
                }
            }            
        }
        catch (Exception e)
        {
            Logger.ErrorLog("ExecuteNonQuery", e, "sql: " + sql);
        }
    }

    public static DataSet GetDataSet(string queryString, List<SqlParameter> values = null, string connS = connString)
    {
        DataSet result = new DataSet();
        try
        {
            //בצורה הזאת הXML של הDATASET יוצא נקי     
            SqlCommand cmd = new SqlCommand(queryString);
            if (values != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(values.ToArray());
            }
            cmd.CommandTimeout = timeOut;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    sda.Fill(result);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.ErrorLog("GetDataSet", ex, "sql: " + queryString);
            //throw ex;
        }
        return result;
    }


    /// <summary>
    /// For V3 Moshik
    /// </summary>
    /// <param name="queryString"></param>
    /// <param name="values"></param>
    /// <param name="connS"></param>
    /// <returns></returns>
    public static DataSet GetDataSetOld(string queryString, List<SqlParameter> values = null, string connS = connString)
    {
        DataSet result = new DataSet();
        try
        {
            //SqlCommand cmd = new SqlCommand(queryString);
            //if (values != null)
            //{
            //    cmd.Parameters.AddRange(values.ToArray());
            //}

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlDataAdapter sda = new SqlDataAdapter())
            //    {
            //        cmd.Connection = conn;
            //        sda.SelectCommand = cmd;

            //        sda.Fill(result);
            //    }
            //}

            //בצורה הזאת הXML של הDATASET יוצא נקי     
            //SqlCommand cmd = new SqlCommand(queryString);
            //if (values != null)
            //{
            //    cmd.Parameters.Clear();
            //    cmd.Parameters.AddRange(values.ToArray());
            //}
            //cmd.CommandTimeout = timeOut;

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlDataAdapter sda = new SqlDataAdapter())
            //    {
            //        cmd.Connection = conn;
            //        sda.SelectCommand = cmd;

            //        sda.Fill(result);
            //    }
            //}

            using (SqlConnection conn = new SqlConnection(connS))
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);

                if (values != null)
                {
                    cmd.Parameters.AddRange(values.ToArray());
                }

                SqlDataAdapter myDataAdapter = new SqlDataAdapter(cmd);

                myDataAdapter.Fill(result, queryString);
            }
            //conn.Close();
        }
        catch (Exception ex)
        {
            Logger.ErrorLog("GetDataSet", ex, "sql: " + queryString);
            //throw ex;
        }
        return result;
    }

    public static int GetRecordsCount(string queryString)
    {
        int result = 0;
        try
        {
            DataSet reader = GetDataSet(queryString);
            if (reader.Tables.Count > 0)
            {
                result = reader.Tables[0].Rows.Count;
            }
        }
        catch (Exception)
        {
            throw;
        }


        return result;
    }
}

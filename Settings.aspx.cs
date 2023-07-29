using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Worker"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        Worker w = (Worker)Session["Worker"];
        if (w.userTypeId != 1)
        {
            Response.Redirect("AddProblem.aspx");
        }
    }
    [WebMethod]
    public static string GetWorkers()
    {
        string sql = "SELECT workers.[id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [workerTypeName], [userName], [password], [userTypeId],[userType], [shluha], [active], [imgPath], carType, carNumber " +
                     "FROM workers Left Join workerTypes ON workers.workerTypeID= workerTypes.id " +
                            "Left Join userTypes ON workers.userTypeId= userTypes.id " +
                     "ORDER BY firstname, lastname";

        DataSet ds = Dal.GetDataSet(sql);

        return ds.GetXml();
    }


    [WebMethod]
    public static string GetWorker(int workerId)
    {
        DataSet ds = WebDal.GetWorkerDS(workerId);
        return ds.GetXml();
    }

    [WebMethod]
    public static string UpdateWorker(int workerId, string firstName, string lastName, string phone, int workerTypeID,
                                                    string userName, string password, int userTypeId, int shluha, bool active, string carType, string carNumber)
    {
        
        //[id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [workerTypeName], [userName], [password], [userTypeId],[userType], [active]
        if (workerId == 0)
        {
            WebDal.AppendWorker(firstName, lastName, phone, workerTypeID, userName, password, userTypeId, shluha, true);
        }
        else
        {
            WebDal.UpdateWorkerInfo(workerId, firstName, lastName, phone, userName, password, shluha, active, userTypeId ,carType, carNumber);
        }

        //CacheHelper.Instance.SetWorkers();
        return "";
    }


    [WebMethod]
    public static string GetPlaces()
    {
        DataSet ds = WebDal.GetPlaces();
        return ds.GetXml();
    }




    //מציג את הסניפים של מושיק לקבוצה
    [WebMethod]
    public static string GetMoshikBranchesForGroup(int groupId)
    {
        // = 1;
        string postData = GetuserPass(groupId);
        string str = SendAndRecive("https://biapp.beecomm.co.il:8094/v2/oauth/token", postData, true);
        moshikTokenAccess moshikTokenAccess = JsonConvert.DeserializeObject<moshikTokenAccess>(str);
        if (!moshikTokenAccess.result)
        {
            return "";
            //Console.WriteLine("Result Is False. " + str);
            //Console.ReadKey();
        }

        string contents = SendAndRecive("https://biapp.beecomm.co.il:8094/api/v2/services/orderCenter/customers", postData, false, true, moshikTokenAccess.access_token);

        return contents;
    }

    private static string GetuserPass(int i)
    {
        string str = string.Empty;
        if (i == 1)
            str = "{\"client_id\": \"v05vfWZgaFB0GX6Mlg1t0h3FxQHn3mmYliyA4GisRy2Rt4EeBk0P608d907stK7D\",\"client_secret\": \"7LNtvoJ5uid01KT4Dqn2O5gu5zy8ir9UKzlHGkcCNSySGqbP10DhM0Ka7CY4PA1K\"}";
        if (i == 2)
            str = "{\"client_id\": \"Gp8aYfplZuAG8iFyvKgaMIwRQMUdTOGvQommyYQL5q0coq01hlmEVhCxsudgI0Lc\",\"client_secret\": \"mGkMcJDl9JyCtNb1LDRlu7QvCAFDD7afTsnCsWZbRcZ6W2nDhJ6P0dCl0C36grZE\"}";
        if (i == 3)
            str = "{\"client_id\": \"qOJZ1wDRoDfWV5eTRaxG357SqW0w0jfFeBSKZbHQP4Ek5czKasYAsREbJRqkPl2O\",\"client_secret\": \"coCk7yLc0CwinHpjZBUbyabKYbh0wDXXIoyvG0GzLKwuTtbUmXnKiXHI0yWEJ0Ch\"}";
        if (i == 4)
            str = "{\"client_id\": \"dyl0j7cHME5omW0KwoW5jrOWHKnOCtqTqy0oEQdswczTmKTJnL8AfbgOqDm4VUKl\",\"client_secret\": \"AhZMOESMMwxRafTzlRmhjMAPJRNqETj2h9odPe9gocGshwGcLz0DHphMVyZmVuC6\"}";
        if (i == 5)
            str = "{\"client_id\": \"ZDzHmEDsCCClXXYK8Di5xhOpLYLXoKPqYd0bmcMJOyUCED0fErj0QgpFWhTfMk6z\",\"client_secret\": \"ZsEUTwuHEfqE4CmkTzG8tDiPc5PabLCiyQRsvWHP9DnbCWTJdowtsaXOm0B6XQDW\"}";
        return str;
    }

    public static string SendAndRecive(string urlCommand, string postData, bool post, bool addHeader = false, string headerValue = "")
    {
        string str = string.Empty;
        try
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlCommand);
            if (post)
                httpWebRequest.Method = "POST";
            else
                httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            if (addHeader)
                httpWebRequest.Headers.Add("access_token", headerValue);
            if (post)
            {
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(requestStream))
                        streamWriter.Write(postData);
                }
            }
            using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                    str = streamReader.ReadToEnd();
            }
            if (!(str == string.Empty))
                ;
        }
        catch (WebException ex)
        {
            //if (!ex.ToString().Contains("404"));
        }
        catch (Exception ex)
        {
        }
        return str;
    }

    private class moshikTokenAccess
    {
        public bool result { get; set; }

        public string message { get; set; }

        public string access_token { get; set; }
    }

    private class moshikBranches
    {
        public bool result { get; set; }

        public moshikBranch[] customers { get; set; }

    }

    private class moshikBranch
    {
        public string customerName { get; set; }

        public string branchName { get; set; }

        public string branchId { get; set; }
    }
}
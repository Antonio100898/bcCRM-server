using System;
using System.IO;

public static class Logger
{
    public static void ErrorLog(string voidName, Exception e, string extraInfo)
    {
        WebDal.AppendErrorLog(voidName, e.Message, extraInfo);
        LogException(System.Web.HttpContext.Current.Server.MapPath("~/Logs"), voidName, e, extraInfo);
        
    }

    private static void LogException(string fileName, string voidName, Exception e, string extraInfo)
    {
        var message = e.Message;

        var innerMsg = "";
        if (e.InnerException != null)
        {
            innerMsg = e.InnerException.Message;

        }
        string msg = voidName + " Exception Message: " + message + Environment.NewLine;
        if (innerMsg != string.Empty)
        {
            msg += ". InnerException: " + innerMsg + Environment.NewLine;
        }

        if (extraInfo != string.Empty)
        {
            msg += ". extraInfo: " + extraInfo + Environment.NewLine;
        }

        Writefile(fileName, msg);
    }


    public static void ErrorLog(string msg)
    {

        Writefile(System.Web.HttpContext.Current.Server.MapPath("~/Logs"), msg);
    }

    public static void GeneralLog(string msg)
    {
        Writefile(System.Web.HttpContext.Current.Server.MapPath("~/Logs"), msg);
    }


    private static void Writefile(string path, string msg)
    {
        WebDal.AppendErrorLog("Log", msg, "");
        try
        {
            string time = CacheHelper.Instance.GetIsraelTime().ToString("dd/MM/yy HH:mm") + "  ";
            StreamWriter sw = new StreamWriter(path + "\\" + GetFileName(), true);
            sw.WriteLine(time + msg);
            sw.Flush();
            sw.Close();
        }
        catch (Exception e)
        {

        }
    }

    private static string GetFileName()
    {
        return CacheHelper.Instance.GetIsraelTime().ToString("yyMMdd") + ".txt";
    }
}
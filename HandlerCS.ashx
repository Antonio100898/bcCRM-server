<%@ WebHandler Language="C#" Class="HandlerCS" %>
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class HandlerCS : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files == null)
        {
            return;
        }

        //Check if Request is to Upload the File.
        if (context.Request.Files.Count == 0)
        {
            return;
        }

        try
        {
            //Fetch the Uploaded File.
            HttpPostedFile postedFile = context.Request.Files[0];
            string desc = context.Request["txtDesc"];

            string s = context.Request["txtProblemId"];

            //string s = HttpContext.Current.Session["currentProblemID"].ToString();

            int currentProblemID = 0;
            int.TryParse(s, out currentProblemID);
            if (currentProblemID == 0)
            {
                return;
            }

            //Set the Folder Path.
            string folderPath = context.Server.MapPath("~/");
            folderPath += "Pics\\problems\\";

            //Set the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);
            string[] a = fileName.Split('.');
            string endFileName = a[a.Length - 1];

            fileName = Guid.NewGuid().ToString().Replace(".", "").Replace("-", "") + "." + endFileName;

            //Save the File in Folder.
            postedFile.SaveAs(folderPath + fileName);

            WebDal.AppendProblemFile(currentProblemID, fileName, desc);

            //Send File details in a JSON Response.
            string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName
                });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }
        catch (Exception e)
        {
            //throw;
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
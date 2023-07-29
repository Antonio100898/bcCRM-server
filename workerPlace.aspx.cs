using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class workerPlace : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Worker"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            SetWorkerInfo();
        }



    }

    private void SetWorkerInfo()
    {
        Worker w = (Worker)Session["Worker"];
        txtFirstName.Text = w.firstName;
        txtLastName.Text = w.lastName;
        txtPhone.Text = w.phone;
        txtUserName.Text = w.userName;
        txtPassword.Text = w.password;
        txtShluha.Text = w.shluha.ToString();

        try
        {
            DataSet ds = WebDal.GetWorkerDS(w.Id);
            txtCarType.Text = ds.Tables[0].Rows[0]["carType"].ToString();
            txtCarNumber.Text = ds.Tables[0].Rows[0]["carNumber"].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
        }
        catch (Exception e)
        {

        }

        try
        {
            imgWorker.ImageUrl = "~/Pics/workers/" + w.imgPath;
        }
        catch (Exception e)
        {
        }

        int placeInAllWorkers = WebDal.GetWorkerPlaceInAllWorkers(w.Id);
        if (placeInAllWorkers == -1)
        {
            lblOrderInMostProblems.Text = "";
        }
        else
        {
            lblOrderInMostProblems.Text = "מקומך " + placeInAllWorkers;
        }


        //var script = "$('#txtbirthDay').val('" + w.birthDay.ToString("dd/MM/yyyy") + "');";
        //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);


    }


    private void showMsg(string msg)
    {
        Response.Write("<script>alert('" + msg + "');</script>");
    }


    protected void btnUpdateWorkerInfo_Click(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];

        //string start = Request.Form["txtbirthDay"];
        //DateTime birthDay;

        //if (!DateTime.TryParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
        //{
        //    if (!DateTime.TryParseExact(start, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
        //    {
        //        var scripst = "$('#txtbirthDay').val('" + start + "');";
        //        ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", scripst, true);

        //        return;
        //    }
        //}

        string firstName = txtFirstName.Text;
        string lastName = txtLastName.Text;
        string phone = txtPhone.Text;
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        string asdasd = txtShluha.Text;
        int shluha = 0;
        int.TryParse(asdasd, out shluha);

        var script = "";
        if (string.IsNullOrEmpty(userName))
        {
            showMsg("אנא הזן שם משתמש");
            //script = "$('#txtbirthDay').val('" + birthDay.ToString("dd/MM/yyyy") + "');";
            //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            showMsg("אנא הזן סיסמה");
            //script = "$('#txtbirthDay').val('" + birthDay.ToString("dd/MM/yyyy") + "');";
            //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);
            return;
        }

        if (userName.Length < 2)
        {
            showMsg("שם משתמש חייב להיות לפחות באורך 6 תוים");
            //script = "$('#txtbirthDay').val('" + birthDay.ToString("dd/MM/yyyy") + "');";
            //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);
            return;
        }

        if (password.Length < 2)
        {
            showMsg("סיסמה חייבת להיות לפחות באורך 6 תוים");
            //script = "$('#txtbirthDay').val('" + birthDay.ToString("dd/MM/yyyy") + "');";
            //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);
            return;
        }

        WebDal.UpdateWorkerInfo(w.Id, firstName, lastName, phone, userName, password, shluha, true,w.userTypeId , txtCarType.Text, txtCarNumber.Text);

        w.firstName = firstName;
        w.lastName = lastName;
        w.phone = phone;
        //w.birthDay = birthDay;
        w.userName = userName;
        w.password = password;
        w.shluha = shluha;

        Session["Worker"] = w;
        //CacheHelper.Instance.SetWorkers();

        //script = "$('#txtbirthDay').val('" + birthDay.ToString("dd/MM/yyyy") + "');";
        //ClientScript.RegisterStartupScript(typeof(string), "txtbirthDay", script, true);
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                Worker w = Session["Worker"] as Worker;
                if (w == null)
                {
                    StatusLabel.Text = "Upload status: No Worker!";
                    return;
                }

                if (isValidFileType(FileUploadControl.PostedFile.ContentType))
                {
                    if (FileUploadControl.PostedFile.ContentLength < 1024000)
                    {
                        string filename = Path.GetFileName(FileUploadControl.FileName);
                        string[] a = filename.Split('.');

                        filename = Guid.NewGuid().ToString().Replace("-", "") + "." + a[1];

                        string path = Server.MapPath("~/") + "Pics\\workers\\" + filename;
                        FileUploadControl.SaveAs(path);

                        WebDal.UpdateWorkerImagePath(w.Id, filename);
                        imgWorker.ImageUrl = "~/Pics/workers/" + filename;
                        w.imgPath = filename;
                        Session["Worker"] = w;
                    }
                    else
                        StatusLabel.Text = "Upload status: The file has to be less than 1000 kb!";
                }
                else
                    StatusLabel.Text = "Upload status: Only Image files are accepted!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }

    private bool isValidFileType(string s)
    {
        if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
        {
            return true;
        }
        if (FileUploadControl.PostedFile.ContentType == "image/png")
        {
            return true;
        }

        return false;
    }
}
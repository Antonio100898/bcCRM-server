using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MsgCenter : System.Web.UI.Page
{
    private static string connString = "Server=tcp:bibeecommserver.database.windows.net,1433;Initial Catalog = biBeecomm; Persist Security Info=False;User ID = bcBiUser; Password=0cf23e5f-2051-47b2-a051-eec97eb48c34; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

    protected void Page_Load(object sender, EventArgs e)
    {
        Worker w = (Worker)Session["Worker"];

        btnUpload.Visible = (w.Id == 1);

        if (!IsPostBack)
        {
            SetBranches();
        }
    }

    private void SetBranches()
    {

        string sql = "SELECT [id], [branchName] FROM [branches] order by branchName";

        DataSet ds = Dal.GetDataSet(sql, null, connString);
        cboBranches.DataSource = ds.Tables[0];
        cboBranches.DataBind();

        RefreshCboPos();
    }

    private void RefreshCboPos()
    {
        int branchId = int.Parse(cboBranches.SelectedValue);

        string sql = "SELECT id, posName FROM pos WHERE (branchId = @branchId) AND (Active=1)";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@branchId", branchId));
        DataSet ds = Dal.GetDataSet(sql, values, connString);
        cboPos.Items.Clear();

        cboPos.DataSource = ds.Tables[0];
        cboPos.DataBind();
    }

    protected void cboBranches_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshCboPos();
    }

    protected void btnAddMsg_Click(object sender, EventArgs e)
    {
        if (cboMsgType.SelectedValue == "2" && string.IsNullOrEmpty(txtMsgInfo.Text))
        {
            ShowMsg("שכחת לרשום איזה זדים");
            return;
        }

        if (cboMsgType.SelectedValue == "2" && chkAllPos.Checked)
        {
            chkAllPos.Checked = false;
            ShowMsg("אין צורך לשלוח לכל העמדות בקשה לשלוח זד");
            return;
        }

        if (cboMsgType.SelectedValue == "2")
        {
            string s = txtMsgInfo.Text;
            string[] a = s.Split(';');
            foreach (var item in a)
            {
                int i;
                if (!int.TryParse(item, out i))
                {
                    ShowMsg("לא הצלחתי להמיר את אחד מספרי הזד שאתה מנסה לשלוח, אנא בדוק את המספרים");
                    return;
                }
            }
        }
        int branchId = int.Parse(cboBranches.SelectedValue);
        bool allPos = chkAllPos.Checked;
        string branchName = cboBranches.SelectedItem.Text;
        int msgType = int.Parse(cboMsgType.SelectedValue);

        if (allPos)
        {
            foreach (ListItem item in cboPos.Items)
            {
                int posId = int.Parse(item.Value);
                string posName = item.Text;
                if (!isMsgExists(posId, msgType))
                {
                    AppendPosMsg(posId, branchName, posName, msgType, txtMsgInfo.Text);
                }
            }
        }
        else
        {
            int posId = int.Parse(cboPos.SelectedValue);
            string posName = cboPos.SelectedItem.Text;
            if (!isMsgExists(posId, msgType))
            {
                AppendPosMsg(posId, branchName, posName, msgType, txtMsgInfo.Text);
            }
        }

        grdMsgs.DataBind();
        txtMsgInfo.Text = "";
    }

    private bool isMsgExists(int posId, int msgType)
    {
        string sql = "SELECT [id] FROM [dbo].[posMsgs] where posId=@posId And msgType=@msgType And posRecived=0";
        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@msgType", msgType));

        object o = Dal.ExecuteScalar(sql, values);
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

    private void AppendPosMsg(int posId, string branchName, string posName, int msgType, string extraParams)
    {
        //[posId],[branchName],[posName],[msgType],[extraParams],[posRecived],[recivedTime],[finished],[finishTime],[faild],[responseMsg],[commitTime]
        string sql = "INSERT INTO [dbo].[posMsgs] ([posId],[branchName],[posName],[msgType],[extraParams],[commitTime]) " +
                                  "VALUES (@posId,@branchName,@posName,@msgType,@extraParams,@commitTime);";
        DateTime commitTime = DateTime.Now.AddHours(3);

        List<SqlParameter> values = new List<SqlParameter>();
        values.Add(new SqlParameter("@posId", posId));
        values.Add(new SqlParameter("@branchName", branchName));
        values.Add(new SqlParameter("@posName", posName));
        values.Add(new SqlParameter("@msgType", msgType));
        values.Add(new SqlParameter("@extraParams", extraParams));
        values.Add(new SqlParameter("@commitTime", commitTime));
        Dal.ExecuteNonQuery(sql, values);
    }

    private void ShowMsg(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Beecomm", "alert('" + msg + "')", true);
    }


    protected void Upload(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            //if (FileUpload1.PostedFile.ContentLength > 315500)
            //{
            //    ShowMsg("גודל תמונה מקסימלי עד 300 קילובייט");
            //    return;
            //}
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //string[] a = fileName.Split('.');

            //Guid g = Guid.NewGuid();
            //fileName = g.ToString() + "." + a[1];

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Downloads/Assistant/") + fileName);

            //WebDal.UpdateContactInfoImagePath(currentStudent.contactInfo.id, "~/pics/contacts/" + fileName);

            //imgStudent.ImageUrl = "~/pics/contacts/" + fileName;

            //Response.Redirect(Request.Url.AbsoluteUri);
            ShowMsg("הקובץ עלה בהצלחה");
        }
    }

}
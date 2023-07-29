using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowProblems : System.Web.UI.Page
{
    private static DataTable currentTable;
    //private static List<Phone> phones = new List<Phone>();
    private static Dictionary<string, int> phonesDic = new Dictionary<string, int>();
    private static List<Place> places = new List<Place>();
    private static Dictionary<string, int> placesDic = new Dictionary<string, int>();
    //private static List<Worker> workers = new List<Worker>();
    private static Dictionary<string, List<PhonePlace>> phonePlaces = new Dictionary<string, List<PhonePlace>>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("https://bccrm-334f4.web.app/");
        return;

        if (!IsPostBack)
        {
            //phones = CacheHelper.Instance.phones;
            places = CacheHelper.Instance.places;
            //workers = CacheHelper.Instance.workers;
            //phonesDic = CacheHelper.Instance.phonesDic;
            placesDic = CacheHelper.Instance.placesDic;
            phonePlaces = CacheHelper.Instance.phonePlaces;
        }

        string header = GetString(Session["showProblemHeader"]);
        string where = GetString(Session["showProblemWhere"]);

        lblHeader.Text = header;
        if (!string.IsNullOrEmpty(where) && !string.IsNullOrWhiteSpace(where))
        {
            //where = "WHERE " + where + " ";
            dsOpenProblems.FilterExpression = where;
            //grdProblems.DataBind();
            divProblems.Visible = true;
        }
    }


    private static string GetString(object o)
    {
        string result = "";
        if (o != null)
        {
            result = o.ToString();
        }
        return result;
    }


    private int GetPhoneId(string phone)
    {
        int phoneId = 0;
        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrWhiteSpace(phone))
        {
            if (!phonesDic.ContainsKey(phone))
            {
                phoneId = WebDal.GetPhoneId(phone);
                if (phoneId == 0)
                {
                    phoneId = WebDal.AppendPhone(phone);
                }

                phonesDic.Add(phone, phoneId);
                //if (!CacheHelper.Instance.phonesDic.ContainsKey(phone))
                //{
                //    CacheHelper.Instance.phonesDic.Add(phone, phoneId);
                //}
            }
            else
            {
                phoneId = phonesDic[phone];
            }
        }

        return phoneId;
    }

    private void CheckPhonePlaceExists(int phoneID, string Phone, int placeNameId, string placename, string cusName, string Ip)
    {
        int pId = 0;
        PhonePlace p;
        //i dont have it
        if (!phonePlaces.ContainsKey(Phone))
        {
            //is Phone Dont Exists In Cache- Refresh Me
            if (!CacheHelper.Instance.phonePlaces.ContainsKey(Phone))
            {
                pId = WebDal.AppendPhonePlace(phoneID, placeNameId, cusName);
                p = new PhonePlace { id = pId, customerName = cusName, phoneId = phoneID, phone = Phone, placeId = placeNameId, placeName = placename, ip = Ip };

                phonePlaces.Add(Phone, new List<PhonePlace> { p });
                //CacheHelper.Instance.phonePlaces.Add(Phone, new List<PhonePlace> { p });
                return;
            }

            //Loop the Cache For
            foreach (var item in CacheHelper.Instance.phonePlaces[Phone])
            {
                if (item.phoneId == phoneID && item.placeId == placeNameId && item.customerName == cusName)
                {
                    if (string.IsNullOrEmpty(Ip) && !string.IsNullOrEmpty(item.ip) && placeNameId > 2)
                    {
                        item.ip = Ip;
                        WebDal.UpdatePlaceIp(placeNameId, item.ip);
                    }
                    return;
                }
            }

            ////Add Data Refresh Stuff
            pId = WebDal.AppendPhonePlace(phoneID, placeNameId, cusName);
            p = new PhonePlace { id = pId, customerName = cusName, phoneId = phoneID, phone = Phone, placeId = placeNameId, placeName = placename, };

            phonePlaces.Add(Phone, new List<PhonePlace> { p });
            //CacheHelper.Instance.phonePlaces[Phone].Add(p);
            //phonePlaces.Add(Phone, new List<PhonePlace> { p });
            return;
        }


        //Checks Me
        foreach (PhonePlace item in phonePlaces[Phone])
        {
            if (item.phoneId == phoneID && item.placeId == placeNameId && item.customerName == cusName)
            {
                if (!string.IsNullOrEmpty(Ip) && string.IsNullOrEmpty(item.ip) && placeNameId > 2)
                {
                    item.ip = Ip;

                    WebDal.UpdatePlaceIp(placeNameId, item.ip);
                }
                return;
            }
        }

        //is Phone Dont Exists In Cache- Refresh Me
        if (!CacheHelper.Instance.phonePlaces.ContainsKey(Phone))
        {
            pId = WebDal.AppendPhonePlace(phoneID, placeNameId, cusName);
            p = new PhonePlace { id = pId, customerName = cusName, phoneId = phoneID, phone = Phone, placeId = placeNameId, placeName = placename, };

            phonePlaces.Add(Phone, new List<PhonePlace> { p });
            //CacheHelper.Instance.phonePlaces.Add(Phone, new List<PhonePlace> { p });
            return;
        }

        //Loop the Cache For
        foreach (var item in CacheHelper.Instance.phonePlaces[Phone])
        {
            if (item.phoneId == phoneID && item.placeId == placeNameId && item.customerName == cusName)
            {
                item.ip = Ip;
                return;
            }
        }

        ////Add Data Refresh Stuff
        pId = WebDal.AppendPhonePlace(phoneID, placeNameId, cusName);
        p = new PhonePlace { id = pId, customerName = cusName, phoneId = phoneID, phone = Phone, placeId = placeNameId, placeName = placename, ip = Ip };

        phonePlaces[Phone].Add(p);
        //CacheHelper.Instance.phonePlaces[Phone].Add(p);       
    }

    protected void grdProblems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string stateS = e.Row.RowState.ToString();
            if (!stateS.Contains(DataControlRowState.Edit.ToString()))
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                string state = rowView["statusName"].ToString();

                //format color of the as below 
                if (state == "ממתין")
                    (e.Row.FindControl("lblInGridStatusName") as Label).BackColor = Color.Red;
                (e.Row.FindControl("lblInGridStatusName") as Label).ForeColor = Color.White;

                if (state == "בטיפול")
                    (e.Row.FindControl("lblInGridStatusName") as Label).BackColor = Color.Green;
                (e.Row.FindControl("lblInGridStatusName") as Label).ForeColor = Color.White;

                if (state == "סגור")
                    (e.Row.FindControl("lblInGridStatusName") as Label).BackColor = Color.Blue;
                (e.Row.FindControl("lblInGridStatusName") as Label).ForeColor = Color.White;

            }
        }

    }

    protected void grdProblems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string placeName = GetString(e.NewValues["placeName"]);
        int placeNameId = 0;

        if (placesDic.ContainsKey(placeName))
        {
            placeNameId = placesDic[placeName];
        }
        e.NewValues.Add("placeNameId", placeNameId);
    }
    protected void grdProblems_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        long problemId = long.Parse(e.Keys[0].ToString());
        
        Worker w = (Worker)Session["worker"];

        CheckChanges(problemId, w.Id, e.NewValues, e.OldValues);

        if (e.OldValues["statusId"].ToString() != "2" && e.NewValues["statusId"].ToString() == "2")
        {
            DateTime finishTime = CacheHelper.Instance.GetIsraelTime();
        }
    }

    private void CheckChanges(long problemId, int wId, IOrderedDictionary newValues, IOrderedDictionary oldValues)
    {
        List<ProblemLog> logs = new List<ProblemLog>();
        foreach (var key in newValues.Keys)
        {
            if (oldValues.Contains(key))
            {
                if (GetString(newValues[key]) != GetString(oldValues[key]))
                {
                    ProblemLog p = new ProblemLog();
                    p.problemId =(int) problemId;
                    p.workerId = wId;
                    p.fieldName = key.ToString();
                    p.oldValue = GetString(oldValues[key]);
                    p.newValue = GetString(newValues[key]);

                    logs.Add(p);
                }
            }
        }

        if (logs.Count > 0)
        {
            string groupKey = Guid.NewGuid().ToString();            
            foreach (var item in logs)
            {
                WebDal.AppendLog(groupKey, item.problemId, item.workerId, item.fieldName, item.oldValue, item.newValue);
            }
        }
    }

    protected void btnShowLog_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton b = sender as ImageButton;
        long problemId = long.Parse(b.AlternateText);

        Session["showLogproblemId"] = problemId;

        //divOverlayServer.Style["display"] = "block";
        //divOverlayServer.Visible = true;
        divLog.Style["display"] = "block";
        divLog.Visible = true;
        dsLogGroups.DataBind();
        dsLogDetails.DataBind();
    }

    protected void btnHideLog_Click(object sender, EventArgs e)
    {
        divLog.Visible = false;

    }


}
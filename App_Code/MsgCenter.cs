using System.Web.Services;

[WebService(Namespace = "http://beecomm.co.il/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MsgCenter : System.Web.Services.WebService
{

    public MsgCenter()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string CheckMsgs(string userName, string password)
    {
        return "";
        //if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //{
        //    return null;
        //}

        //int posId = WebDal.GetPosIdFromBiBeecomm(userName, password);
        //if (posId == 0)
        //{
        //    return null;
        //}

        //List<biMsg> result = WebDal.GetMsgs(posId);
        //if (result.Count > 0)
        //{
        //    WebDal.UpdatePosMsgRecived(posId);
        //}
        //return JsonConvert.SerializeObject(result);
    }

    [WebMethod]
    public void AnswerMsg(string userName, string password, string msgResponse)
    {
        return;
        //if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //{
        //    return;
        //}

        //int posId = WebDal.GetPosIdFromBiBeecomm(userName, password);
        //if (posId == 0)
        //{            
        //    return;
        //}

        //try
        //{
        //    biMsgResponse msg = JsonConvert.DeserializeObject<biMsgResponse>(msgResponse);
        //    WebDal.UpdatePosMsgResponse(posId, msg);
        //}
        //catch (Exception e)
        //{
        //    return;
        //}
    }


    [WebMethod]
    public string SendMsg(string placeName, string phone, string msg)
    {
        int problemId = WebDal.AppendProblemIDs(phone);
        WebDal.UpdateTodayProblemsCountAddOne();
        //WebDal.AppendProblem(problemId, 36, 0, phone, "", 0, placeName, "אוטומטי", "", "", 36, 0, 0, 13, false);

        return "";
        //if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //{
        //    return null;
        //}

        //int posId = WebDal.GetPosIdFromBiBeecomm(userName, password);
        //if (posId == 0)
        //{
        //    return null;
        //}

        //List<biMsg> result = WebDal.GetMsgs(posId);
        //if (result.Count > 0)
        //{
        //    WebDal.UpdatePosMsgRecived(posId);
        //}
        //return JsonConvert.SerializeObject(result);
    }

}

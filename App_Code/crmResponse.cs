using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class crmResponse
{
    public int problemId { get; set; }
    public string key { get; set; }
    public int workerId { get; set; }
    public string phone { get; set; }
    public List<Problem> problems { get; set; }
    public string msg { get; set; }
    public bool logOut { get; set; }
    public string lastSuppoter { get; set; }
    public int trackingId { get; set; }

    public List<ProblemLog> logs { get; set; }
    public int msgLinesCount { get; set; }

    public List<msgLine> msgLines { get; set; }
    public List<Notification> notifications { get; set; }
    public int notificationsCount { get; set; }
    public bool success { get; set; }
    public ProblemsCountSummery summery { get; set; }
    public List<Worker> workers { get; set; }
    public Worker worker { get; set; }
    public List<PhonePlace> places { get; set; }
    public List<Department> workerDepartments { get; set; }


    public List<WorkExpensesType> workExpensesTypes { get; set; }
    public List<WorkerExpenses> workerExpenses { get; set; }
    public List<WorkerExpensesSum> workerExpensesSum { get; set; }
    public List<bcStats> stats { get; set; }
    //public List<JObject> statsWorkersHours { get; set; }
    public List<ShiftDetail> workerShifts { get; set; }
    public List<shiftWeek> shiftDetails { get; set; }
    public List<ShiftDetail> shiftPlanDetails { get; set; }
    public List<shiftWeekReport> shiftPlanReport { get; set; }
    public List<dayInfo> shiftsDays { get; set; }
    public List<WorkerSickDay> workerSickDay { get; set; }
    public List<WorkerFreeDay> workerFreeDay { get; set; }

    public List<shiftWorkerPreferencias> shiftWorkerPreferencias { get; set; }

    public List<v3Group> v3Groups { get; set; }
    public List<v3Branch> v3Branches { get; set; }
    public List<v3City> v3Cities { get; set; }



    public List<Hardware> hardwares { get; set; }
    public List<Hardware> hardwaresCount { get; set; }
    public List<string> filesName { get; set; }
    public double workExpensesSum { get; set; }
    public List<ExpenseAndShiftWeek> ExpenseAndShiftsWeeks { get; set; }
}

public class crmLogin
{
    public string key { get; set; }
    public int workerId { get; set; }
    public string workerName { get; set; }
    public int workerType { get; set; }
    public int userType { get; set; }
    public int department { get; set; }
    public int shluha { get; set; }
    public string imgPath { get; set; }
    public string jobTitle { get; set; }

    public string msg { get; set; }
    public bool success { get; set; }
    public List<WorkerBase> workers { get; set; }
    public List<ProblemType> problemTypes { get; internal set; }
    public List<Problem> problems { get; set; }
    public ProblemsCountSummery summery { get; set; }
}

public class SearchProblem
{
    public int daysBack { get; set; }
    public string key { get; set; }
    public string searchValue { get; set; }
    public bool place { get; set; }
    public bool phone { get; set; }
    public bool desc { get; set; }
    //public bool solution { get; set; }
    public bool department { get; set; }
    public bool workerName { get; set; }
    public bool toWorkerName { get; set; }
}

public class msgLine
{
    public int id { get; set; }
    public int workerId { get; set; }
    public string workerName { get; set; }
    public string msg { get; set; }
    public string workerImgPath { get; set; }
    public int msgType { get; set; }
    public string commitTime { get; set; }
    public string commitTimeEN { get; set; }
}

public class WorkExpensesType
{
    public int id { get; set; }
    public string workExpensName { get; set; }
    public string categoryName { get; set; }

    public double defValue { get; set; }
    public int workExpensCategoryId { get; set; }
    public int orderIndex { get; set; }

    public int workExpensType { get; set; }
    public int WorkerExpensesValueId { get; set; }
    public double expenseTypeUnitValue { get; set; }
}

public class WorkerExpenses
{
    public int id { get; set; }
    public int workerId { get; set; }
    public int expenseType { get; set; }
    public string workExpensName { get; set; }
    public int workExpensCategoryId { get; set; }

    public double expenseValue { get; set; }
    public double expenseTypeUnitValue { get; set; }
    public string startExpenseDate { get; set; }
    public string startExpenseDateEN { get; set; }
    public string finishExpenseDate { get; set; }
    public bool freePass { get; set; }
    public bool approved { get; set; }
    public string remark { get; set; }
    public string workerName { get; set; }
    public string categoryName { get; set; }

}


public class WorkerExpensesSum
{
    public int workerId { get; set; }
    public string workerName { get; set; }
    public double totalSum { get; set; }
    public double workExpense { get; set; }

    public double bonus { get; set; }
    public double fieldTrip { get; set; }

    public double answerPrecentge { get; set; }
    public string teudatZehut { get; set; }
    public int marselWorkerCode { get; set; }

}



public class ProblemLog
{
    public int problemId { get; set; }
    public int workerId { get; set; }
    public string workerName { get; set; }
    public string fieldName { get; set; }
    public string oldValue { get; set; }
    public string newValue { get; set; }
    public string commitTime { get; set; }
}

public class Notification
{
    public int id { get; set; }
    public int problemId { get; set; }
    public int workerId { get; set; }
    public string msg { get; set; }
    public bool hasSeen { get; set; }

    public string commitTime { get; set; }
}

public class bcStats
{
    public int workerId { get; set; }
    public string workerName { get; set; }
    public int totalProblems { get; set; }
    public int closeProblems { get; set; }
    public int openProblems { get; set; }

    public int firstHourOpenProblem { get; set; }
    public int lastHourOpenProblem { get; set; }

    public int firstHourCloseProblem { get; set; }
    public int lastHourCloseProblem { get; set; }


    public int movedProblems { get; set; }
    public int openAndOnHim { get; set; }


}



public class shifsForShiftType
{
    public int shiftType { get; set; }
    public string shiftName { get; set; }
    public List<ShifsForJobType> jobTypes { get; set; }

    public shifsForShiftType()
    {
        jobTypes = new List<ShifsForJobType>();
    }
}
public class ShifsForJobType
{
    public int jobType { get; set; }
    public string jobTypeName { get; set; }
    public shiftWeek week { get; set; }

    public ShifsForJobType()
    {
        week = new shiftWeek();
    }

    internal void AddShift(ShiftDetail shift)
    {
        week.AddShift(shift);
    }
}

public class shiftWeek
{
    public int jobType { get; set; }
    public string jobTypeName { get; set; }
    public int shiftType { get; set; }
    public string shiftTypeName { get; set; }
    public string color { get; set; }


    public List<ShiftDetail> sunday { get; set; }
    public List<ShiftDetail> monday { get; set; }
    public List<ShiftDetail> tuesday { get; set; }
    public List<ShiftDetail> wendsday { get; set; }
    public List<ShiftDetail> thursday { get; set; }
    public List<ShiftDetail> friday { get; set; }
    public List<ShiftDetail> saturday { get; set; }

    public shiftWeek()
    {
        sunday = new List<ShiftDetail>();
        monday = new List<ShiftDetail>();
        tuesday = new List<ShiftDetail>();
        wendsday = new List<ShiftDetail>();
        thursday = new List<ShiftDetail>();
        friday = new List<ShiftDetail>();
        saturday = new List<ShiftDetail>();
    }

    internal void AddShift(ShiftDetail m)
    {
        try
        {
            //DateTime t;
            //if (!DateTime.TryParse(m.startDate +" AM", out t))
            //{
            //    WebDal.AppendErrorLog("AddShift", "Failed To Parse startDate", m.startDate);
            //}
            int dayOfWeek = (int)m.startDate.DayOfWeek + 1;
            switch (dayOfWeek)
            {
                case 1:
                    sunday.Add(m);
                    break;
                case 2:
                    monday.Add(m);
                    break;
                case 3:
                    tuesday.Add(m);
                    break;
                case 4:
                    wendsday.Add(m);
                    break;
                case 5:
                    thursday.Add(m);
                    break;
                case 6:
                    friday.Add(m);
                    break;
                case 7:
                    saturday.Add(m);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {

            WebDal.AppendErrorLog("AddShift", "startDate" + m.startDate, "");
        }
    }
}

public class shiftWeekReport
{

    public string workerName { get; set; }

    public int sunday { get; set; }
    public int monday { get; set; }
    public int tuesday { get; set; }
    public int wendsday { get; set; }
    public int thursday { get; set; }
    public int friday { get; set; }
    public int saturday { get; set; }
}

public class ShiftDetail : ShiftPlan
{
    public string shiftName { get; set; }

    public int jobTypeId { get; set; }
    public string jobTypeName { get; set; }
    public string color { get; set; }

    public string workerName { get; set; }


    public DateTime finishTime { get; set; }
    public string finishTimeEN { get; set; }


    public string placeName { get; set; }
    public string address { get; set; }
    public string dayName { get; set; }
    

    public string contactName { get; set; }
    public string phone { get; set; }

    public string startHour { get; set; }
    public string finishHour { get; set; }
    public int shiftGroupId { get; set; }
    public bool isShiftManager { get; set; }

    public ShiftDetail()
    {
        shiftName = "";
        jobTypeName = "";
        workerName = "";
        finishTime = DateTime.Now;
        finishTimeEN = finishTime.ToString("yyyy/MM/dd");
        placeName = "";
        address = "";
        contactName = "";
        phone = "";
        startHour = "0";
        finishHour = "0";
        isShiftManager = false;
    }
}


public class ShiftPlan
{
    public int id { get; set; }
    public int shiftTypeId { get; set; }
    public int workerId { get; set; }
    public DateTime startDate { get; set; }
    public string startDateEN { get; set; }
    public string remark { get; set; }
    public bool cancel { get; set; }
}

public class WorkerSickDay
{
    public int id { get; set; }
    public int workerId { get; set; }
    public string workerName { get; set; }
    public DateTime startDate { get; set; }
    public string startDateEN { get; set; }
    public DateTime finishDate { get; set; }
    public string finishDateEN { get; set; }
    public string fileName { get; set; }
    public bool cancel { get; set; }
    public string imgContent { get; set; }
    public int daysLen
    {
        get
        {
            TimeSpan span = finishDate.Subtract(startDate);
            return (int)span.TotalDays;
        }
    }
}


public class WorkerFreeDay
{
    public int id { get; set; }
    public int workerId { get; set; }
    public string workerName { get; set; }
    public DateTime startDate { get; set; }
    public string startDateEN { get; set; }
    public DateTime finishDate { get; set; }
    public string finishDateEN { get; set; }
    public string remark { get; set; }
    public int statusId { get; set; }
    public bool cancel { get; set; }

    public int daysLen
    {
        get
        {
            TimeSpan span = finishDate.Subtract(startDate);
            return (int)span.TotalDays;
        }
    }
}


public class shiftWorkerPreferencias
{
    public int shiftType { get; set; }
    public string shiftTypeName { get; set; }

    public int sunday { get; set; }
    public int monday { get; set; }
    public int tuesday { get; set; }
    public int wendsday { get; set; }
    public int thursday { get; set; }
    public int friday { get; set; }
    public int saturday { get; set; }

}

public class Hardware
{
    public int id { get; set; }
    public int hardwareType { get; set; }
    public string hardwareName { get; set; }
    public string barcode { get; set; }
    public string remark { get; set; }
    public string customerName { get; set; }

    public int statusId { get; set; }
    public string statusName { get; set; }
    public DateTime tokefExpire { get; set; }
    public string tokefExpireEN { get; set; }
    public string place { get; set; }

    public List<HardwareTracking> trackings { get; set; }
}

public class HardwareTracking
{
    public int hardwareId { get; set; }
    public string cusName { get; set; }
    public string remark { get; set; }
    public int statusId { get; set; }
    public string statusName { get; set; }
}

public class dayInfo
{
    public int id { get; set; }
    public DateTime dayValue { get; set; }
    public string dayValueEN { get; set; }
    public string dayName { get; set; }
    public string remark { get; set; }
    public string holydayName { get; set; }

    public bool isToday { get; set; }
}


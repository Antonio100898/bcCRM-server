using ezshiftWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ExpenseAndShiftWeek
{
    public List<ExpenseAndShiftDay> days { get; set; }
    public ExpenseAndShiftWeek()
    {
        days = new List<ExpenseAndShiftDay>();
    }
}

public class ExpenseAndShiftDay
{
    public DateTime dDay { get; set; }
    public string dDayEN { get; set; }
    public int dayInMonth
    {
        get
        {
            return dDay.Day;
        }
    }


    public double totalSum
    {
        get
        {
            if (workers!=null)
            {
                double sum = 0;            
                foreach (var item in workers)
                {
                    sum += item.sumExpense;
                }
                return sum;
            }
            return 0;
            
        }
    }

    public double totalMinutes
    {
        get
        {
            if (workers != null)
            {
                double sum = 0;
                foreach (var item in workers)
                {
                    sum += item.totalMinutes;
                }
                return sum;
            }
            return 0;

        }
    }

    public List<ExpenseAndShift> workers { get; set; }
    public ExpenseAndShiftDay()
    {
        workers = new List<ExpenseAndShift>();
    }
}

public class ExpenseAndShift
{
    public int workerId { get; set; }
    public int ezShiftWorkerCode { get; set; }
    
    public string workerName { get; set; }
    public DateTime dDay { get; set; }
    public string dDayEN { get; set; }
    public double sumExpense { get; set; }
    public double category1Sum { get; set; }
    public double category2Sum { get; set; }
    
    public double totalMinutes{ get; set; }
    public string expensNames { get; set; }
    public string remark { get; set; }
    public List<ezShift> shifts { get; set; }

    public ExpenseAndShift()
    {
        shifts = new List<ezShift>();
    }
}

public class ezShift
{
    public ezShift(EZShift_Attended item)
    {
        startTime = item.dtAttStart;
        startTimeEN = item.dtAttStart.ToString("dd/MM/yyyy HH:mm"); 
        finishTime = item.dtAttEnd;
        finishTimeEN= item.dtAttEnd.ToString("dd/MM/yyyy HH:mm");
    }

    public DateTime startTime { get; set; }
    public string startTimeEN { get; set; }
    public DateTime finishTime { get; set; }
    public string finishTimeEN { get; set; }
    public double totalMinutes
    {
        get
        {
            TimeSpan t = finishTime - startTime;          
            return t.TotalMinutes;

        }
    }
}
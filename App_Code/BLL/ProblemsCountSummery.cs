using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProblemsCountSummery
/// </summary>
public class ProblemsCountSummery
{

    public List<ProblemSummery> departments { get; set; }
    public int todayProblems { get; set; }
    public int allProblems
    {
        get
        {
            int result = privateToWorker + general + menu + software + tech + resets + upgrades + returnToClient + developers;
            return result;
        }
    }
    public int trackingProblems { get; set; }
    public int imInvolved { get; set; }
    public int privateToWorker { get; set; }
    public int reportToYaron { get; set; }
    public int general { get; set; }
    public int Iopened { get; set; }
    public int menu { get; set; }
    public int software { get; set; }
    public int tech { get; set; }
    public int resets { get; set; }
    public int upgrades { get; set; }

    public int openProblems { get; set; }
    public int HandlingProblems { get; set; }

    public int counting { get; set; }
    public int marketing { get; set; }
    public int users { get; set; }
    public int kiosk { get; set; }
    public int returnToClient { get; set; }
    public int developers { get; set; }
    public int dejavoo { get; set; }
    public int delivery_server{ get; set; }

    public ProblemsCountSummery()
    {
        departments = new List<ProblemSummery>();
        //
        // TODO: Add constructor logic here
        //
    }
}

public class ProblemSummery
{
    public int departmentId { get; set; }
    public string departmentName{ get; set; }
    public string departmentValue { get; set; }
    public int count { get; set; }
}
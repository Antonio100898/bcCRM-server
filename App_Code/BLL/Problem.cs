using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

public class RequestBase
{
    public string workerKey { get; set; }
    public int workerId { get; set; }
}

public class CrmFile
{
    public string filename { get; set; }
    public string content{ get; set; }
}

public class Problem: RequestBase
{
    
    public string startTime { get; set; }
    public string finishTime { get; set; }

    public string startTimeEN { get; set; }
    public string finishTimeEN { get; set; }

    public int id { get; set; }    
    public int workerCreateId { get; set; }    
    public string workerCreateName { get; set; }
    public string customerName { get; set; }
    public int placeId { get; set; }
    public string placeName { get; set; }
    public bool vip{ get; set; }
    public string ip { get; set; }
    public string phone { get; set; }
    public int phoneId { get; set; }
    public string desc { get; set; }
    public string solution { get; set; }
    public int statusId { get; set; }
    public string statusName { get; set; }
    public int emergencyId { get; set; }
    public string emergencyName{ get; set; }
    public int departmentId { get; set; }
    public string departmentName { get; set; }
    public int toWorker { get; set; }
    public string toWorkerName { get; set; }
    public string toWorkerJobTitle { get; set; }
    public bool takingCare { get; set; }
    public bool isLocked { get; set; }
    public bool callCustomerBack { get; set; }
    

    public bool yaron { get; set; }
    public bool containFiles { get; set; }
    public string filesName { get; set; }
    public List<string> files { get; set; }
    public List<string> newFiles { get; set; }
    public List<CrmFile> crmFiles { get; set; }
    
    public int fileCount{ get; set; }

    
    public int updaterWorkerId { get; set; }
    public string updaterWorkerName { get; set; }
    public int updaterWorkerDepartmentId { get; set; }
    public List<int> toWorkers { get; set; }
    public List<ProblemType> problemTypesList { get; set; }

    public Problem()
    {
        toWorkers = new List<int>();
        newFiles = new List<string>();
        problemTypesList = new List<ProblemType>();
    }

    public Problem CloneMe()
    {
        Problem result = new Problem();                
        result.id = this.id;

        result.startTime = this.startTime;
        result.finishTime = this.finishTime;
        
        result.workerCreateId = this.workerCreateId;
        result.workerCreateName = this.workerCreateName;
        result.customerName = this.customerName;

        result.placeId = this.placeId;
        result.placeName = this.placeName;
        result.ip = this.ip;
        result.phone = this.phone;
        result.phoneId = this.phoneId;
        result.desc = this.desc;
        result.solution = this.solution;
        result.statusId = this.statusId;
        result.statusName = this.statusName;
        result.emergencyId = this.emergencyId;
        result.emergencyName = this.emergencyName;
        result.departmentId = this.departmentId;
        
        result.departmentName = this.departmentName;
        result.toWorker = this.toWorker;
        result.toWorkerName = this.toWorkerName;
        result.yaron = this.yaron;
        result.containFiles = this.containFiles;
        result.filesName = this.filesName;
        result.files = this.files;
        result.newFiles = this.newFiles;
        result.fileCount = this.fileCount;
        result.updaterWorkerId = this.updaterWorkerId;
        result.updaterWorkerName = this.updaterWorkerName;
        result.updaterWorkerDepartmentId = this.updaterWorkerDepartmentId;
        result.toWorkers = this.toWorkers;

        return result;
    }
}

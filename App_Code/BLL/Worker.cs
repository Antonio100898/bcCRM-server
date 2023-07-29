using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;


[DataContract]
public class WorkerBase
{
    public int Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string workerName { get { return firstName + " " + lastName; } }
    public int workerLevel { get; set; }
    public string jobTitle { get; set; }
    public string teudatZehut { get; set; }
    public string departmentName { get; set; }
    public int departmentId { get; set; }
    public string imgPath { get; set; }
    public string imgContent { get; set; }
    public string imgContentName { get; set; }    

}

[DataContract]
public class Worker : WorkerBase
{
    

    //public int Id { get; set; }
    //public string firstName { get; set; }
    //public string lastName { get; set; }
    public string phone { get; set; }
    //public DateTime birthDay { get; set; }
    //public int workerTypeID { get; set; }
    public string userName { get; set; }
    public string password { get; set; }
    public string carType { get; set; }
    public string carNumber { get; set; }
    
    public int shluha { get; set; }
    public int userTypeId { get; set; }
    public bool active { get; set; }    
    public List<Department> departments { get; set; }
    public int workerTypeID { get; set; }
    public int marselWorkerCode { get; set; }
    public Worker()
    {
        departments = new List<Department>();
    }
}
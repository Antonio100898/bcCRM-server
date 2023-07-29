using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HardwareProblem
/// </summary>
public class HardwareProblem
{
    public int id { get; set; }
    public int placeId { get; set; }
    public string placeName { get; set; }
    public int workerId { get; set; }
    public int toWorkerId { get; set; }
    public int workerTechSupport { get; set; }
    public string remarks { get; set; }
    public double costPrice { get; set; }
    public double recommendedPrice { get; set; }
    public int status { get; set; }
    public List<HardwareProblemType> hardwareTypes { get; set; }

    public HardwareProblem()
    {
        hardwareTypes = new List<HardwareProblemType>();
    }
}

public class HardwareProblemType
{
    public int id { get; set; }
    public int problemHardwareId { get; set; }
    public int hardwareTypeid { get; set; }
    public string hardwareTypeName { get; set; }
    public int modelId { get; set; }
    public string modelName { get; set; }
    public string remarks { get; set; }
    public double cost { get; set; }
    public double price { get; set; }
    public bool sentToLab { get; set; }
    public bool fixedAtBeecomm { get; set; }
    public bool dead { get; set; }


    public List<HardwareProblemTypePart> parts { get; set; }

    public HardwareProblemType()
    {
        parts = new List<HardwareProblemTypePart>();
    }
}

public class HardwareProblemTypePart
{
    public int id { get; set; }
    public int partId { get; set; }
    public int damageType { get; set; }
    public string Name { get; set; }
}
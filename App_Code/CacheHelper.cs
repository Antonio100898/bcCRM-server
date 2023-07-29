using System;
using System.Collections.Generic;

public sealed class CacheHelper
{
    private static CacheHelper instance = null;
    //public List<Phone> phones = new List<Phone>();
    //public Dictionary<string, int> phonesDic = new Dictionary<string, int>();
    public List<Place> places = new List<Place>();
    public Dictionary<string, int> placesDic = new Dictionary<string, int>();
    
    public Dictionary<string, List<PhonePlace>> phonePlaces = new Dictionary<string, List<PhonePlace>>();
    
    CacheHelper()
    {
    }

    public static CacheHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CacheHelper();
            }
            return instance;

        }
    }

    public void Setup()
    {
        //רשימת טלפונים
        //phones = WebDal.GetPhones(out phonesDic);
        //רשימת מקומות
        places = WebDal.GetPlaces(out placesDic);
        //לאתחל רשימת טלפונים ושמות מקומות
        phonePlaces = WebDal.GetPhonePlace();
        //לאתחל רשימת עובדים
        //SetWorkers();
                
        //חומרה
        //hardwares = WebDal.GetHardware();
        //hardwareTypes = WebDal.GetHardwareTypeAndModel();
        //hardwareTypePart = WebDal.GetHardwareTypePart();
    }

    public DateTime GetIsraelTime()
    {
        var remoteTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time");
        var remoteTime = TimeZoneInfo.ConvertTime(DateTime.Now, remoteTimeZone);
        //Console.WriteLine("Time in {0} is {1}", remoteTimeZone.Id, remoteTime.TimeOfDay.ToString());

        return remoteTime; //DateTime.Now.ToUniversalTime().AddHours(3);
    }  
}

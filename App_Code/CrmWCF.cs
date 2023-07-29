using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class CrmWCF : ICrmWCF
{
    public Worker Login(string userName, string password)
    {
        Worker w = WebDal.GetWorker(userName, password);
        return w;
    }

    
}

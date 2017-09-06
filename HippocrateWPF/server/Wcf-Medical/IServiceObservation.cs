using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dbo;

namespace Wcf_Medical
{
    // NOTE: If you change the interface name "IServiceObservation" here, you must also update the reference to "IServiceObservation" in Web.config.
    [ServiceContract]
    public interface IServiceObservation
    {
        [OperationContract]
        bool AddObservation(int idPatient, Observation obs);
    }
}

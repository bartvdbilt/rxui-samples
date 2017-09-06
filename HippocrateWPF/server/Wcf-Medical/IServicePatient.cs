using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dbo;

namespace Wcf_Medical
{
    // NOTE: If you change the interface name "IServicePatient" here, you must also update the reference to "IServicePatient" in Web.config.
    [ServiceContract]
    public interface IServicePatient
    {
        [OperationContract]
        List<Patient> GetListPatient();

        [OperationContract]
        Patient GetPatient(int id);

        [OperationContract]
        bool AddPatient(Patient user);

        [OperationContract]
        bool DeletePatient(int id);
    }
}

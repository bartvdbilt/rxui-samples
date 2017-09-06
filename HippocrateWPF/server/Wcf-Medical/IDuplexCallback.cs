using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace Wcf_Medical
{
    public interface IDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void PushDataHeart(double requestData);

        [OperationContract(IsOneWay = true)]
        void PushDataTemp(double requestData);
    }
}
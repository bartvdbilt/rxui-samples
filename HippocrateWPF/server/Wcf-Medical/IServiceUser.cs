using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dbo;

namespace Wcf_Medical
{
    // REMARQUE : si vous modifiez le nom d’interface « IServiceUser » ici, vous devez également mettre à jour la référence à « IServiceUser » dans Web.config.
    [ServiceContract]
    public interface IServiceUser
    {
        [OperationContract]
        List<User> GetListUser();

        [OperationContract]
        User GetUser(string login);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool DeleteUser(string login);

        [OperationContract]
        bool Connect(string login, string pwd);


        [OperationContract]
        void Disconnect(string login);

        [OperationContract]
        string GetRole(string login);
    }
}

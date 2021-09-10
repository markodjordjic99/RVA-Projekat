using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.ServiceInterface
{
    [ServiceContract]
    public interface IAddUser
    {
        [OperationContract]
        void AddUser(String username, string password);
    }
}

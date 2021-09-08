using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server.DatabaseModels;

namespace Server.ServiceInterface
{
    [ServiceContract]
    interface IAddUser
    {
        [OperationContract]
        void AddUser(User user);
    }
}

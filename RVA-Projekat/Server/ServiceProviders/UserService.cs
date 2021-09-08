using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.DatabaseModels;
using Server.ServiceInterface;

namespace Server.ServiceProviders
{
    class UserService : IAddUser
    {
        public void AddUser(User user)
        {
            using (var dbContext = new UsersModelContainer())
            {
                dbContext.Users.Add(user);
            }
        }
    }
}

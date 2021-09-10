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
        public void AddUser(string username, string password)
        {
            using (var dbContext = new UsersModelContainer())
            {
                Console.WriteLine("Operations successfully completed:)");
                dbContext.Users.Add(new User(){Username = username, Password = password});
            }
        }
    }
}

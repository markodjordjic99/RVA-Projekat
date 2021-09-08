using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using Server.DatabaseModels;

namespace Server.Identity
{
    class IdentityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            using (var db = new UsersModelContainer())
            {
                var user = db.Users.Find(userName) as User;
                
                if (user == null || user?.Password != password)
                {
                    string msg = "";
                    throw new FaultException(msg);
                }
            }
        }
    }
}

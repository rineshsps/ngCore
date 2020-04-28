using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngCore.Model
{
    public class UserService : IUserService
    {
        public void Change(string Name, string age)
        {
            throw new NotImplementedException();
        }

        public void Add()
        {
            var user = new User();

        }

    }

    interface IUserService
    {
        void Add();
        void Change(string Name, string age);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace post
{
    internal class ActiveUser
    {
        private ActiveUser() { }

        static ActiveUser instance;
        public static ActiveUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new ActiveUser();
                return instance;
            }
        }
        private User user;
        public User GetUser()
        {
            return user;
        }

        public void SetUser(User value)
        {
            user = value;
        }
    }

    //internal class NewActiveUser 
    //{
    //  private NewActiveUser() { }
    //  static NewActiveUser instance;
    //public static NewActiveUser Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new NewActiveUser();
    //            return instance;
    //        }
    //    }
    //    private User user;
    //    public User GetUser()
    //    {
    //        return user;
    //    }

    //    public void SetUser(User value)
    //    {
    //        user = value;
    //    }

    //}

}

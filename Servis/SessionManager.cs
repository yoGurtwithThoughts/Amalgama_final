using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amalgama.Servis.DBConnection;

namespace Amalgama.Servis
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsAdmin => CurrentUser?.IsAdmin == true;

        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}

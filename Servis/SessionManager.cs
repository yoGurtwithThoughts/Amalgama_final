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
        // Текущий пользователь (может быть User, Administrator или SuperAdmin)
        public static object? CurrentUser { get; private set; }

        // Проверка, является ли текущий пользователь администратором
        public static bool IsAdministrator => CurrentUser is Administrator;

        // Проверка, является ли текущий пользователь суперадмином
        public static bool IsSuperAdmin => CurrentUser is SuperAdmin;

        // Установка текущего пользователя
        public static void SetUser(object user)
        {
            if (user is User || user is Administrator || user is SuperAdmin)
            {
                CurrentUser = user;
            }
            else
            {
                throw new ArgumentException("Invalid user type. Only User, Administrator, or SuperAdmin are allowed.");
            }
        }

        // Выход из системы
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;

namespace BeverageStore.Config
{
    public static class MySession
    {
        public static Employee CurrentEmployee { get; set; } = null;
        public static bool IsLoggedIn { get; set; } = false;

        public static void ClearSession()
        {
            CurrentEmployee = null;
            IsLoggedIn = false;
        }
    }
}

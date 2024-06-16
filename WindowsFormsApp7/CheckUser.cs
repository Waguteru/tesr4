using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7
{
    public class CheckUser
    {
        
        public string Login {  get; set; }

        public bool IsAdmin { get; }

        public string Status => IsAdmin ? "Роль Админ" : "Роль Менеджер";

        public CheckUser(string login,bool isadmin) 
        {
            Login = login.Trim();

            IsAdmin = isadmin;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Antuh
{
    class Helper
    {
        private static Model.Entities s_entities;
        public static Model.Entities getContext()
        {
            if (s_entities == null)
            {
                s_entities = new Model.Entities();
            }
            return s_entities;
        }

        public static void Create(Model.User user, Model.Role role)
        {
            s_entities.User.Add(user);
            s_entities.Role.Add(role);
            s_entities.SaveChanges();
        }

        public static int GetLastIDStaff()
        {
            int id = s_entities.User.OrderByDescending(user => user.UserID).First().UserID;

            return id + 1;
        }

        public static int GetLastIDAuth()
        {
            int id = s_entities.User.OrderByDescending(authorizations => authorizations.UserID).First().UserID;
            return id + 1;
        }
    }
}




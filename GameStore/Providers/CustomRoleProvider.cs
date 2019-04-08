using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GameStore.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (DBContext db = new DBContext())
            {
                string roleName = db.Users.Where(u => u.Login == username).Select(u => u.Role.Name).FirstOrDefault();
                if (roleName != "")
                {
                    // получаем роль
                    roles = new string[] { roleName };
                }
                return roles;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (DBContext db = new DBContext())
            {
                // Получаем пользователя
                string roleName1 = db.Users.Where(u => u.Login == username).Select(u => u.Role.Name).FirstOrDefault();
                if (roleName1 == roleName)
                    return true;
                else
                    return false;
            }
        }

        #region NotImplemented

        public override string ApplicationName
        {
            set
            {
                throw new NotImplementedException();
            }
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
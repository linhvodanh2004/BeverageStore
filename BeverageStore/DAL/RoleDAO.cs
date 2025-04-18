using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;

namespace BeverageStore.Repository
{
    public class RoleDAO
    {
        private BeverageStoreContext _context;
        public RoleDAO()
        {
            _context = new BeverageStoreContext();
        }
        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
        public Role getRoleByName(String name)
        {
            Role role = _context.Roles.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
            if (role != null)
            {
                return role;
            }
            return null;
        }
    }
}

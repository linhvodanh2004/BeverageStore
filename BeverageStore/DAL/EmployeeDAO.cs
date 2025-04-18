using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageStore.Repository
{
    public class EmployeeDAO
    {
        private BeverageStoreContext _context;
        public EmployeeDAO()
        {
            _context = new BeverageStoreContext();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.Include(e => e.Role)
                .ToList();
        }

        public List<Employee> GetEmployeesByRoleName(String role)
        {
            var roleId = _context.Roles
                .Where(r => r.Name == role)
                .Select(r => r.Id)
                .FirstOrDefault();
            var employees = _context.Employees
                .Where(e => e.RoleId == roleId)
                .Include(e => e.Role)
                .ToList();
            return employees;
        }
        public List<Employee> GetEmployeesByFullnameOrUsernameOrPhone(string searchString)
        {
            return _context.Employees
                .Where(e => e.Fullname.Contains(searchString)
                || e.Username.Contains(searchString)
                || e.Phone.Contains(searchString))
                .Include(e => e.Role)
                .ToList();
        }
        public bool checkPhoneExists(string phone)
        {
            return _context.Employees.Any(e => e.Phone == phone);
        }
        public bool checkEmailExists(string email)
        {
            return _context.Employees.Any(e => e.Email == email);
        }
        public bool checkUsernameExists(string username)
        {
            return _context.Employees.Any(e => e.Username == username);
        }
        public bool checkValidUsernameAndPassword(string username, string password)
        {
            return _context.Employees.Any(e => e.Username == username && e.Password == password);
        }
        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Email == email);
        }   
        public Employee GetEmployeeByUsername(String username)
        {
            return _context.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Username == username);
        }
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Id == id);
        }

        public bool AddEmployee(Employee employee)
        {
            if (GetEmployeeById(employee.Id) == null)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            if (GetEmployeeById(employee.Id) != null)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

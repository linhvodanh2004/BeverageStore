using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BeverageStore.Models;
using BeverageStore.Repository;
using BeverageStore.WPF.Layout;

namespace BeverageStore.WPF.Auth
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly RoleDAO _roleDAO;
        private readonly EmployeeDAO _employeeDAO;
        public Register()
        {
            InitializeComponent();
            _roleDAO = new RoleDAO();
            _employeeDAO = new EmployeeDAO();
        }

        private void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void RegisterHandler(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string confirmPassword = ConfirmPassword.Password;
            string fullname = Fullname.Text;
            DateOnly birthdate = DateOnly.FromDateTime(Dob.SelectedDate.Value);
            string email = Email.Text;
            string phone = Phone.Text;
            bool gender = Male.IsChecked == true;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) 
                || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(fullname) 
                || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning","Please fill all information to register");
                customMessageBoxOk.ShowDialog();
                return;
            }

            if (password != confirmPassword)
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Password and Confirm Password must be the same");
                customMessageBoxOk.ShowDialog();
                return;
            }

            if(_employeeDAO.checkUsernameExists(username))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Username already exists");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (_employeeDAO.checkEmailExists(email))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Email already exists");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (_employeeDAO.checkPhoneExists(phone))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Phone already exists");
                customMessageBoxOk.ShowDialog();
                return;
            }

            Employee employee = new Employee()
            {
                Username = username,
                Password = password,
                Fullname = fullname,
                Dob = birthdate,
                Email = email,
                Phone = phone,
                Gender = gender,
                IsActivated = true,
                RoleId = _roleDAO.getRoleByName("WAITER").Id
            };
            if(_employeeDAO.AddEmployee(employee))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Success", "Register successfully");
                customMessageBoxOk.ShowDialog();
                Login login = new Login();
                login.Show();
                this.Close();
            }
            else
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Error", "Register failed");
                customMessageBoxOk.ShowDialog();
            }
        }
    }
}

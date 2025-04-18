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
using BeverageStore.Config;
using BeverageStore.Models;
using BeverageStore.Repository;
using BeverageStore.WPF.Layout;
using BeverageStore.WPF.Manager;
using MaterialDesignThemes.Wpf;

namespace BeverageStore.WPF.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly EmployeeDAO _employeeDAO;
        private readonly RoleDAO _roleDAO;
        public Login()
        {
            InitializeComponent();
            _employeeDAO = new EmployeeDAO();
            _roleDAO = new RoleDAO();
        }

        private void NavigateToRegister(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void LoginHandler(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            if (_employeeDAO.checkValidUsernameAndPassword(username, password))
            {
                Employee employee = _employeeDAO.GetEmployeeByUsername(username);
                MySession.CurrentEmployee = employee;
                MySession.IsLoggedIn = true;
                // 1 for MANAGER, 2 for BARISTA, 3 for WAITER
                switch (employee.RoleId)
                {
                    case 1:
                        ManagerDashboard managerDashboard = new ManagerDashboard();
                        managerDashboard.Show();
                        break;
                    case 2:
                        CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Login", "Login successfully as BARISTA");
                        customMessageBoxOk.ShowDialog();
                        break;
                    case 3:
                        customMessageBoxOk = new CustomMessageBoxOk("Login", "Login successfully as WAITER");
                        customMessageBoxOk.ShowDialog();
                        break;

                }
                this.Close();
            }
            else
            {

                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Error", "Invalid username or password");
                customMessageBoxOk.ShowDialog();
            }

        }

        private void ForgotPasswordNavigate(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Close();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeverageStore.Config;
using BeverageStore.WPF.Auth;
using BeverageStore.WPF.Manager;

namespace BeverageStore.WPF.Layout
{
    /// <summary>
    /// Interaction logic for ManagerNavigation.xaml
    /// </summary>
    public partial class ManagerNavigation : UserControl
    {
        public ManagerNavigation()
        {
            InitializeComponent();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            // Show confirmation message box
            CustomMessageBoxYesNo messageBox = new CustomMessageBoxYesNo("Logout", "Are you sure you want to logout?");
            bool? result = messageBox.ShowDialog();

            if (messageBox.Confirmation == true && result == true)
            {
                MySession.ClearSession();
                Login login = new Login();
                login.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void Employee(object sender, RoutedEventArgs e)
        {
            EmployeeManagement employeeManagement = new EmployeeManagement();
            employeeManagement.Show();
            Window.GetWindow(this).Close();
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            Window.GetWindow(this).Close();
        }

        private void MyProfile(object sender, RoutedEventArgs e)
        {
            ViewProfile viewProfile = new ViewProfile();
            viewProfile.Show();
            Window.GetWindow(this).Close();
        }
    }
}

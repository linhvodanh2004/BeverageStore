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

namespace BeverageStore.WPF.Manager
{
    /// <summary>
    /// Interaction logic for ViewProfile.xaml
    /// </summary>
    public partial class ViewProfile : Window
    {
        private readonly EmployeeDAO _employeeDAO;
        private Employee employee;
        public ViewProfile()
        {
            InitializeComponent();
            _employeeDAO = new EmployeeDAO();
            employee = MySession.CurrentEmployee;
            LoadEmployeeData();
        }

        public void LoadEmployeeData()
        {
            if (employee != null)
            {
                Username.Text = employee.Username;
                Fullname.Text = employee.Fullname;
                Email.Text = employee.Email;
                Phone.Text = employee.Phone;
                Dob.SelectedDate = employee.Dob.ToDateTime(TimeOnly.MinValue);
                if (employee.Gender)
                {
                    Male.IsChecked = true;
                    Female.IsChecked = false;
                }
                else
                {
                    Male.IsChecked = false;
                    Female.IsChecked = true;
                }
            }
        }

        private void SaveChange(object sender, RoutedEventArgs e)
        {
            String fullname = Fullname.Text;
            String email = Email.Text;
            String phone = Phone.Text;
            DateOnly birthdate = DateOnly.FromDateTime(Dob.SelectedDate.Value);

            bool gender = Male.IsChecked == true ? true : false;
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Please fill in all fields.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (_employeeDAO.checkEmailExists(email))
            {
                if (employee.Email != email)
                {
                    CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Email already exists");
                    customMessageBoxOk.ShowDialog();
                    return;
                }
            }
            if (_employeeDAO.checkPhoneExists(phone))
            {
                if (employee.Phone != phone)
                {
                    CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Phone already exists");
                    customMessageBoxOk.ShowDialog();
                    return;
                }
            }


            employee.Fullname = fullname;
            employee.Email = email;
            employee.Phone = phone;
            employee.Gender = gender;
            employee.Dob = birthdate;

            if (_employeeDAO.UpdateEmployee(employee))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Success", "Update information of " + employee.Username + " successfully.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            CustomMessageBoxOk customMessageBoxOkSuccess = new CustomMessageBoxOk("Error", "Update information of " + employee.Username + " failed.");
            customMessageBoxOkSuccess.ShowDialog();
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            String oldPassword = OldPassword.Password;
            String newPassword = NewPassword.Password;
            String confirmPassword = ConfirmPassword.Password;
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Please fill in all fields.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (oldPassword != employee.Password)
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "Old password is incorrect.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (newPassword != confirmPassword)
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "New password and Confirm password must be the same.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            if (newPassword == oldPassword)
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Warning", "New password must be different from old password.");
                customMessageBoxOk.ShowDialog();
                return;
            }
            employee.Password = newPassword;
            if (_employeeDAO.UpdateEmployee(employee))
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Success", "Change password successfully.");
                customMessageBoxOk.ShowDialog();
                ClearPassword();
                return;
            }
            else
            {
                CustomMessageBoxOk customMessageBoxOk = new CustomMessageBoxOk("Error", "Change password failed.");
                customMessageBoxOk.ShowDialog();
                ClearPassword();
            }
            void ClearPassword()
            {
                OldPassword.Clear();
                NewPassword.Clear();
                ConfirmPassword.Clear();
            }
        }
    }
}

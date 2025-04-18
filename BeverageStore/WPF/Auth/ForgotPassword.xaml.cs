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

namespace BeverageStore.WPF.Auth
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        private Dictionary<string, (string Otp, DateTime Expiry)> otpStore =
            new Dictionary<string, (string, DateTime)>();
        private readonly EmployeeDAO _employeeDAO;
        private CustomMessageBoxOk _customMessageBoxOk;
        public ForgotPassword()
        {
            InitializeComponent();
            _employeeDAO = new EmployeeDAO();
        }

        private void SendOtpButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            if (string.IsNullOrEmpty(email) || !_employeeDAO.checkEmailExists(email))
            {
                CustomMessageBoxOk _customMessageBoxOk =
                    new CustomMessageBoxOk("Invalid email", "Email is not valid or does not exist.");
                _customMessageBoxOk.ShowDialog();
                return;
            }

            // Generate 6-digit OTP
            string otp = new Random().Next(100000, 999999).ToString();
            DateTime expiry = DateTime.Now.AddMinutes(5);
            otpStore[email] = (otp, expiry);

            EmailService emailService = new EmailService();
            if (emailService.SendOtpEmail(email, otp))
            {
                CustomMessageBoxOk customMessageBoxOk =
                new CustomMessageBoxOk("Send OTP", $"OTP sent to your email. Expires at {expiry}.");
                customMessageBoxOk.ShowDialog();
                // Show OTP input UI
                SendOtpButton.Visibility = Visibility.Collapsed;
                EmailTextBox.IsEnabled = false;
                //OtpLabel.Visibility = Visibility.Visible;
                OtpTextBox.Visibility = Visibility.Visible;
                VerifyOtpButton.Visibility = Visibility.Visible;
            }
            else
            {
                CustomMessageBoxOk customMessageBoxOk =
                new CustomMessageBoxOk("Send OTP", "Failed to send OTP. Please try again.");
                customMessageBoxOk.ShowDialog();
            }

        }
        private void VerifyOtpButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string enteredOtp = OtpTextBox.Text.Trim();

            if (!otpStore.ContainsKey(email))
            {
                _customMessageBoxOk =
                    new CustomMessageBoxOk("Invalid OTP", "No OTP request found for this email.");
                _customMessageBoxOk.ShowDialog();
                return;
            }

            var (storedOtp, expiry) = otpStore[email];
            if (DateTime.Now > expiry)
            {
                _customMessageBoxOk =
                    new CustomMessageBoxOk("Expired OTP", "OTP has expired. Please request a new one.");
                _customMessageBoxOk.ShowDialog();
                ResetToEmailEntry();
                return;
            }

            if (enteredOtp != storedOtp)
            {
                _customMessageBoxOk =
                    new CustomMessageBoxOk("Invalid OTP", "Entered OTP is incorrect. Please try again.");
                _customMessageBoxOk.ShowDialog();
                return;
            }

            // OTP verified, show password update UI
            _customMessageBoxOk =
                new CustomMessageBoxOk("OTP Verified", "OTP verified successfully. Please enter a new password.");
            _customMessageBoxOk.ShowDialog();
            //OtpLabel.Visibility = Visibility.Collapsed;
            OtpTextBox.Visibility = Visibility.Collapsed;
            VerifyOtpButton.Visibility = Visibility.Collapsed;

            //PasswordLabel.Visibility = Visibility.Visible;
            NewPasswordBox.Visibility = Visibility.Visible;
            UpdatePasswordButton.Visibility = Visibility.Visible;
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string newPassword = NewPasswordBox.Password;

            if (string.IsNullOrEmpty(newPassword))
            {
                _customMessageBoxOk =
                    new CustomMessageBoxOk("Invalid Password", "Password cannot be empty.");
                _customMessageBoxOk.ShowDialog();
                return;
            }

            // Update password in mock store
            Employee employee = _employeeDAO.GetEmployeeByEmail(email);
            employee.Password = newPassword;
            _employeeDAO.UpdateEmployee(employee);
            otpStore.Remove(email); // Clean up OTP

            _customMessageBoxOk =
                new CustomMessageBoxOk("Password Updated", "Password updated successfully.");
            _customMessageBoxOk.ShowDialog();
            ResetToEmailEntry();
        }
        private void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void ResetToEmailEntry()
        {
            EmailTextBox.Text = "";
            EmailTextBox.IsEnabled = true;
            OtpTextBox.Text = "";
            NewPasswordBox.Password = "";
            SendOtpButton.Visibility = Visibility.Visible;
            OtpTextBox.Visibility = Visibility.Collapsed;
            VerifyOtpButton.Visibility = Visibility.Collapsed;
            NewPasswordBox.Visibility = Visibility.Collapsed;
            UpdatePasswordButton.Visibility = Visibility.Collapsed;
        }
    }
}
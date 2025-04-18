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

namespace BeverageStore.WPF.Layout
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxYesNo.xaml
    /// </summary>
    public partial class CustomMessageBoxYesNo : Window
    {
        public bool Confirmation { get; private set; } = false;
        public CustomMessageBoxYesNo(String title, String message)
        {
            InitializeComponent();
            this.Title = title;
            MessageText.Text = message;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Confirmation = false;
            this.DialogResult = false;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Confirmation = true;
            this.DialogResult = true;
        }
    }
}

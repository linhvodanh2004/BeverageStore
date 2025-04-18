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
    /// Interaction logic for CustomMessageBoxOk.xaml
    /// </summary>
    public partial class CustomMessageBoxOk : Window
    {
        public CustomMessageBoxOk(String title, String message)
        {
            InitializeComponent();
            this.Title = title;
            MessageText.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

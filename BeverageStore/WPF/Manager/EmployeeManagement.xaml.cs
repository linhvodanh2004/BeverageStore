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
using BeverageStore.Repository;

namespace BeverageStore.WPF.Manager
{
    /// <summary>
    /// Interaction logic for EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : Window
    {
        private EmployeeDAO _employeeDAO;
        public EmployeeManagement()
        {
            InitializeComponent();
            _employeeDAO = new EmployeeDAO();
            EmployeeDataGrid.ItemsSource = _employeeDAO.GetAllEmployees();
        }
    }
}

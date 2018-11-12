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
using CRMv2.Models;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for SelectCustomer.xaml
    /// </summary>
    public partial class SelectCustomer : Window
    {
        CRMEntities db = new CRMEntities();
        public SelectCustomer()
        {
            InitializeComponent();
            FillCustomersCmb();
        }

        private void FillCustomersCmb()
        {
            foreach (Customer cm in db.Customers.ToList())
            {
                CmbCustomers.Items.Add(cm);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CmbCustomers.SelectedIndex==-1)
            {
                MessageBox.Show("Müstərini seçməmisiniz!","Səhv",MessageBoxButton.OK,MessageBoxImage.Asterisk);
            }
            else
            {
                Customer updated = CmbCustomers.SelectedItem as Customer;
                CreateCompany cr = new CreateCompany();
                cr.updCustomer = updated;
                cr.isUpdated = true;
                cr.Show();
                this.Close();

            }
        }
    }
}

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
    /// Interaction logic for DeleteCustomer.xaml
    /// </summary>
    public partial class DeleteCustomer : Window
    {
        CRMEntities db = new CRMEntities();
        public DeleteCustomer()
        {
            InitializeComponent();
            FillCmbCustomers();


        }

        private void FillCmbCustomers()
        {
            foreach (Customer c in db.Customers.ToList())
            {
                if (c.IsActive == true)
                {
                    cmbCustomers.Items.Add(c);
                }
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer deletedCustomer = cmbCustomers.SelectedItem as Customer;
            deletedCustomer.IsActive = false;
            db.SaveChanges();
            MessageBox.Show("Uğur");
            this.Close();

        }
    }
}

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
    /// Interaction logic for AddComment.xaml
    /// </summary>
    public partial class AddComment : Window
    {
        public User currentUser;
        CRMEntities db = new CRMEntities();
        public AddComment()
        {
            InitializeComponent();
            FillCustomersCmb();
        }

        private void txtComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtComment.Text.Length==501)
            {
                txtComment.Text = txtComment.Text.Substring(0, 500);
                txtComment.SelectionStart = txtComment.Text.Length;
                txtComment.SelectionLength = 0;
            }
            else
            {
                int charCount = 500 - txtComment.Text.Length;
                lblCounter.Content = "Qalan simvol sayı : " + charCount.ToString();
            }
            
            
        }

        private void FillCustomersCmb()
        {
            foreach (Customer cm in db.Customers.ToList())
            {
                cmbCustomerList.Items.Add(cm);
            }


        }

        private void btnAddComment_Click(object sender, RoutedEventArgs e)
        {
            Comment cmnt = new Comment();
           Customer cstmSelected = cmbCustomerList.SelectedItem as Customer;
            cmnt.CustomerID= cstmSelected.CustomerId;
            cmnt.UserID = currentUser.UserId;
            cmnt.Text = txtComment.Text;
            db.Comments.Add(cmnt);
            db.SaveChanges();
            MessageBox.Show("Rəy uğurla əlavə edildi", "Status:OK", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}

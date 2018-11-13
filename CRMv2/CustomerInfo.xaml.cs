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
    /// Interaction logic for CustomerInfo.xaml
    /// </summary>
    public partial class CustomerInfo : Window
    {  
        CRMEntities db = new CRMEntities();
        public CustomerInfo()
        {
            InitializeComponent();
            FillCustomerCombo();
            ClearCommentUI();
        }

        private void FillCustomerCombo()
        {
            foreach (Customer cst in db.Customers.ToList())
            {
                cmbCustomers.Items.Add(cst);
            }
        }

        private void BtnSearchByText_Click(object sender, RoutedEventArgs e)
        {
            DgvCustomer.Items.Clear();
            string search = txtSearch.Text;
            foreach (Customer c in db.Customers.ToList())
            {
                if (c.CustomerName.ToLower().Contains(search.ToLower()))
                {
                   
                    
                    DgvCustomer.Items.Add(c);

                }
            }
        }

        private void BtnSearchByList_Click(object sender, RoutedEventArgs e)
        {
            DgvCustomer.Items.Clear();
            DgvCustomer.Items.Add(cmbCustomers.SelectedItem);

        }

        private void DgvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearCommentUI();

            if (DgvCustomer.SelectedItems.Count == 1)
            {
               
                    Customer selection = DgvCustomer.SelectedItem as Customer;
                    int commentCount = 1;
                    foreach (Comment cm in db.Comments.ToList())
                    {
                     if (cm.CustomerID == selection.CustomerId)
                         {
                            Label l1 = ((Label)FindName("lblComment_1" + commentCount));
                        l1.Visibility = Visibility.Visible;
                        l1.Content = cm.User.Name + " " + cm.User.Surname;
                        Label l2 = ((Label)FindName("lblComment_2" + commentCount));
                        l2.Visibility = Visibility.Visible;
                        l2.Content = cm.CreationDate.Value.ToShortDateString();
                        RichTextBox r = ((RichTextBox)FindName("rtb" + commentCount));
                        r.Visibility = Visibility.Visible;
                        r.SelectAll();
                        r.Selection.Text = "";
                        r.AppendText(cm.Text);
                        commentCount++;


                         }
                    }
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void ClearCommentUI()
        {
            for (int i = 1; i < 16; i++)
            {
                RichTextBox r = ((RichTextBox)FindName("rtb" + i));
                r.Visibility = Visibility.Hidden;
                
                r.IsReadOnly = true;
                Label l1 = ((Label)FindName("lblComment_1" + i));
                l1.Visibility = Visibility.Hidden;
                Label l2 = ((Label)FindName("lblComment_2" + i));
                l2.Visibility = Visibility.Hidden;
            }
        }
    }
}

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
            int left = 300, top = 0, right = 0, bottom = 650;
            if (DgvCustomer.SelectedItems.Count==1)
            { Customer selection = DgvCustomer.SelectedItem as Customer;
                int commentCount = 1;
                foreach (Comment cm in db.Comments.ToList())
                {
                    if (cm.CustomerID==selection.CustomerId)
                    {
                        if (commentCount%3==1&&commentCount!=1)
                        {
                            left += 450; top = 0; right = 0; bottom = 650;
                        }
                        commentCount++;
                        Label l = new Label();
                        mainGrid.Children.Add(l);
                        l.Margin = new Thickness(left, top,right, bottom);

                        l.Content = cm.User.Name+" "+cm.User.Surname;
                        l.Height = 50;
                        l.Width = 100;
                        bottom -= 200;
                        left += 100;
                        RichTextBox t = new RichTextBox();
                        mainGrid.Children.Add(t);
                        t.Margin= new Thickness(left, top, right, bottom);
                        t.Height =200;
                        t.Width = 200;
                        t.AppendText(cm.Text);
                        t.IsReadOnly = true;
                        bottom -= 250;
                        left -= 100;


                    }
                }
            }

           
        }
    }
}

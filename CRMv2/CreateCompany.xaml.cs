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
using System.ComponentModel.DataAnnotations;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for CreateCompany.xaml
    /// </summary>
    public partial class CreateCompany : Window
    {
        public Customer updCustomer = new Customer();
        public bool isUpdated = false;
        
        public CreateCompany()
        {
            InitializeComponent();
           
        }
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        private void FillUpdatedCustomerInfo()
        {
            txtCustomerName.Text = updCustomer.CustomerName;
            txtContactPerson.Text = updCustomer.ContactPerson;
            txtCustomerAddress.Text = updCustomer.Address;
            txtOfficePhoneNumber.Text = updCustomer.OfficePhoneNumber;
            txtCustomerMobile.Text = updCustomer.MobilePhone;
            txtCustomerEmail.Text = updCustomer.Email;
            this.Title = "Müştəri məlumatların yenilənməsi";
            btn_create.Content = "Yenilə";

        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CRMEntities db = new CRMEntities();
            if (!isUpdated)
            {
                foreach (Customer item in db.Customers.ToList())
                {
                    if (item.CustomerName == txtCustomerName.Text)
                    {
                        MessageBox.Show("Qeyd olunan müştəri adı mövcuddur!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Şirkət adı boş ola bilməz!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                MessageBox.Show("Əlaqəli şəxs adı boş ola bilməz!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txtCustomerAddress.Text))
            {
                MessageBox.Show("Ünvanı qeyd edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txtOfficePhoneNumber.Text))
            {
                MessageBox.Show("Ofis nömrəsini daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txtCustomerMobile.Text))
            {
                MessageBox.Show("Mobil nömrəsini daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txtCustomerEmail.Text))
            {
                MessageBox.Show("E-poct daxil edin", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!IsValidEmail(txtCustomerEmail.Text))
            {
                MessageBox.Show("E-poct düzgün formatda daxil edilməyib!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Customer nc = new Customer();
                nc.CustomerName = txtCustomerName.Text;
                nc.ContactPerson = txtContactPerson.Text;
                nc.Address = txtCustomerAddress.Text;
                nc.OfficePhoneNumber = txtOfficePhoneNumber.Text;
                nc.MobilePhone = txtCustomerMobile.Text;
                nc.Email = txtCustomerEmail.Text;
                nc.IsActive = true;
                
                
                if (isUpdated)
                {
                    Customer forupdate = db.Customers.Find(updCustomer.CustomerId);
                    forupdate.CustomerName = txtCustomerName.Text;
                    forupdate.ContactPerson = txtContactPerson.Text;
                    forupdate.Address = txtCustomerAddress.Text;
                    forupdate.OfficePhoneNumber = txtOfficePhoneNumber.Text;
                    forupdate.MobilePhone = txtCustomerMobile.Text;
                    forupdate.Email = txtCustomerEmail.Text;
                    db.SaveChanges();
                    MessageBox.Show("Müştəri məlumatı uğurla yeniləndi!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    isUpdated = false;
                }
                else {

                    db.Customers.Add(nc);
                    db.SaveChanges();
                    MessageBox.Show("Müştəri uğurla yaradıldı!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                this.Close();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isUpdated)
            {
                FillUpdatedCustomerInfo();
            }
        }
    }
}

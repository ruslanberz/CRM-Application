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
using System.IO;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for CreateCompany.xaml
    /// </summary>
    public partial class CreateCompany : Window
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CRMlogs.log";
        public Customer updCustomer = new Customer();
        public User currentUser = new User();
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
           

            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Şirkət adı boş ola bilməz!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Company name can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                MessageBox.Show("Əlaqəli şəxs adı boş ola bilməz!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Contact person can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtCustomerAddress.Text))
            {
                MessageBox.Show("Ünvanı qeyd edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Company address can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtOfficePhoneNumber.Text))
            {
                MessageBox.Show("Ofis nömrəsini daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Office phone number can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtCustomerMobile.Text))
            {
                MessageBox.Show("Mobil nömrəsini daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Company mobile number can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtCustomerEmail.Text))
            {
                MessageBox.Show("E-poct daxil edin", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Company email address can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }
            }
            else if (!IsValidEmail(txtCustomerEmail.Text))
            {
                MessageBox.Show("E-poct düzgün formatda daxil edilməyib!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error); using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new/update user : Wrong company email format", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username);
                }

            }
            else if (db.Customers.FirstOrDefault(cst=>cst.CustomerName.ToLower()==txtCustomerName.Text.ToLower())!=null)
            {
                Customer cst = db.Customers.FirstOrDefault(cstm => cstm.CustomerName.ToLower() == txtCustomerName.Text.ToLower());

                if (cst.IsActive==false)
                {
                    cst.CustomerName = txtCustomerName.Text;
                    cst.ContactPerson = txtContactPerson.Text;
                    cst.Address = txtCustomerAddress.Text;
                    cst.OfficePhoneNumber = txtOfficePhoneNumber.Text;
                    cst.MobilePhone = txtCustomerMobile.Text;
                    cst.Email = txtCustomerEmail.Text;
                    cst.IsActive = true;
                    db.SaveChanges();
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} User {2} activated old Company entry for {3} and updated company info", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(), currentUser.Username,cst.CustomerName);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Müştəri artiq müvcuddur!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} User {2} failed to create new user : Company already exist", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(), currentUser.Username);
                    }
                }

                
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
                nc.CreationDate = DateTime.Now;
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
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1}Success:  User {2} successfully updated {3} company info", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(), currentUser.Username, forupdate.CustomerName);
                    }
                    isUpdated = false;
                }
                else {

                    db.Customers.Add(nc);
                    db.SaveChanges();
                    MessageBox.Show("Müştəri uğurla yaradıldı!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} Success: User {2} successfully created new company: {3}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(), currentUser.Username , nc.CustomerName);
                    }

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

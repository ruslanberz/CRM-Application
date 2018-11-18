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
using System.ComponentModel.DataAnnotations;
using CRMv2.Models;
using System.IO;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\CRMlogs.log";
        
      public  User CurrentUser = new User();
        public AddUser()
        {
            InitializeComponent();

        }
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CRMEntities db = new CRMEntities();
            foreach (User item in db.Users.ToList())
            {
                if (item.Username == txtUsername.Text)
                {
                    MessageBox.Show("İstifadəçi adı movcuddur,başqasını seçin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} User {2} failed to create new user : specified username already exist", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(),CurrentUser.Username);
                    }
                    return;
                }
            }


            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Istifadəçinin adı boş ola bilməz","Səhv",MessageBoxButton.OK,MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : User Name can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtSurname.Text))
            {
                MessageBox.Show("Istifadəçinin Soyadı boş ola bilməz", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : User Surname can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username-ni qeyd edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : User`s username can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }

            } else if (string.IsNullOrEmpty(txtPassword.Text))
               {
                MessageBox.Show("Şifrəni daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : Password can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (txtPassword.Text!=txtPasswordConfirm.Text)
            {
                MessageBox.Show("Şifrə və şifrənin təsdiqi eyni olmalıdır!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : Passwords did not match", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Emaili daxil edin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : Email can not be empty", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Emaili düzgün formatda daxil edilməyib", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);

                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : Email is in wrong format", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }
            }
            else if (cmbRole.SelectedIndex==-1)
            {
                MessageBox.Show("İstifadəçinin rolunu seçin!", "Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} failed to create new user : New user`s role was not specified", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username);
                }

            }
            else
            {
                string role = "";
                User newUser = new User();
                newUser.Name = txtName.Text;
                newUser.Surname = txtSurname.Text;
                newUser.Username = txtUsername.Text;
                newUser.Password = txtPassword.Text;
                newUser.Email = txtEmail.Text;
                newUser.CreationDate = DateTime.Now;
                newUser.CreatedBy = CurrentUser.Username;
                switch (cmbRole.SelectedIndex) {
                    case 0:
                        newUser.RoleID = 3;
                        role = "Administrator";
                        break;
                    case 1:
                        newUser.RoleID = 2;
                        role = "User";
                        break;
                    case 2:
                        newUser.RoleID = 1;
                        role = "Viewer";
                        break;
                    default:
                        break;
  
                }
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Yeni istifadəçi yaradıldı!", "Status:OK", MessageBoxButton.OK, MessageBoxImage.Information);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} User {2} successfully created new user {3} with {4} privelegies ", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), CurrentUser.Username,newUser.Username, role);
                }
                this.Close();
                


            }
            
            
            
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRMv2.Models;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        CRMEntities db = new CRMEntities();
        MainPage main = new MainPage();
        public LoginWindow()
        {
            InitializeComponent();
            
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
         
            if (string.IsNullOrEmpty(txtLogin.Text))
            {
                MessageBox.Show("İstifadəçi adı mütləq qeyd olunmalıdır", "Səhv", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Şifrəni daxil edin", "Səhv", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {

                bool isUser = false;
                foreach (User u in db.Users)
                {
                    if (u.Username == txtLogin.Text && u.Password == txtPassword.Password)
                    {
                        main.LoggedUserId = u.UserId;
                        main.Show();
                        isUser = true;

                        this.Close();



                    }
                }
                if (!isUser)
                {
                    MessageBox.Show("İstifadəçi adl/şifrə yanlışdır", "Səhv", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }
      
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo ci = new CustomerInfo();
            ci.Show();
        }

        
    }
}

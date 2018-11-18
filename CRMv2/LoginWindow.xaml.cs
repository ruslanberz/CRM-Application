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
using System.IO;
using CRMv2.Models;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        CRMEntities db = new CRMEntities();
       
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public LoginWindow()
        {
            InitializeComponent();
            LogInit();
            
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
                        MainPage main = new MainPage(u);
                        main.LoggedUserId = u.UserId;
                        main.Show();
                        isUser = true;
                        using (TextWriter tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine("{0} {1} User {2} successfully logged in", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToShortDateString(), u.Username);
                        }
                        this.Close();



                    }
                  
                }
                if (!isUser)
                {
                    MessageBox.Show("İstifadəçi adl/şifrə yanlışdır", "Səhv", MessageBoxButton.OK, MessageBoxImage.Information);
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} Unsuccessfull attempt to logon", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString());
                    }
                }

            }
        }
      
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            using (TextWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine("{0} {1} Application Close", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString());
            }
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

     

        private void LogInit()
        {
            path = path + @"\CRMlogs.log";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            else
            {
                using (TextWriter tw = new StreamWriter(path,true))
                {
                    tw.WriteLine("{0} {1} Application Open", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString());
                }
            }
        }
    }
}

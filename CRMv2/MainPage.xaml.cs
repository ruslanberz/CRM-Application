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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public int LoggedUserId = 0;
        User loggedUser;
        CRMEntities db = new CRMEntities();
        public MainPage()
        {
            InitializeComponent();

        }

        public void FillUserInfo()
        {
           loggedUser= db.Users.First(x => x.UserId == LoggedUserId);
            lblUserName.Content = "Siz " + loggedUser.Name + " " + loggedUser.Surname;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillUserInfo();
            CheckUserControls();
            fillTasks();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }
        private void fillTasks()
        {
            exp_1.Visibility = Visibility.Hidden;
            exp_2.Visibility = Visibility.Hidden;
            exp_3.Visibility = Visibility.Hidden;
            exp_4.Visibility = Visibility.Hidden;
            exp_5.Visibility = Visibility.Hidden;
            exp_6.Visibility = Visibility.Hidden;
            exp_7.Visibility = Visibility.Hidden;
            exp_8.Visibility = Visibility.Hidden;
            exp_9.Visibility = Visibility.Hidden;
            int count = 0;
            foreach (CRMv2.Models.Task t in db.Tasks.ToList())
            { 
                if (t.User.UserId==LoggedUserId&&t.isFinised==false)
                {
                    count++;
                  Expander expander=  ((Expander)FindName("exp_" + count));
                    expander.Visibility= Visibility.Visible;
                    expander.Header ="Müştəri :  "+ t.Customer.CustomerName;
                    TextBox txt = ((TextBox)FindName("txtExp_" + count));
                    txt.Text = t.Description;
                    Label lbl1 = ((Label)FindName("lblExp_1"+count));
                    Label lbl2 = ((Label)FindName("lblExp_2" + count));
                    lbl1.Content = "Yaranma tarixi : " + t.CreationTime.ToShortDateString();
                    lbl2.Content = "Bitmə tarixi : " + t.DeadlineTime.ToShortDateString();

                    
                    
                }
            }
        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
            add.Show();

        }

        private void CheckUserControls()
        {
            if (loggedUser.RoleID==2)
            {
                btnAddUser.IsEnabled = false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            CreateCompany cc = new CreateCompany();
            cc.ShowDialog();
        }

        private void btnCompanyDelete_Click(object sender, RoutedEventArgs e)
        {
            AddComment ac = new AddComment();
            ac.currentUser = loggedUser;
            ac.Show();
        }

        private void btnCompanyUpdate_Click(object sender, RoutedEventArgs e)
        {
            SelectCustomer sc = new SelectCustomer();
            sc.Show();
        }

        private void btnCompanyInfo_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo ci = new CustomerInfo();
            ci.Show();
        }
    }
}

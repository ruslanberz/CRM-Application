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
using System.IO;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        User loggedUser;
        public int LoggedUserId = 0;
        CRMEntities db = new CRMEntities();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CRMlogs.log";
        public MainPage( User lu)
        {
            InitializeComponent();
            this.loggedUser = lu;
            FillUserInfo();
            CheckUserControls();
            runningText.Text = "";
            FillRunningTask();

        }

        public void FillUserInfo()
        {
           //loggedUser= db.Users.First(x => x.UserId == LoggedUserId);
            lblUserName.Content = "Siz " + loggedUser.Name + " " + loggedUser.Surname;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //FillUserInfo();
            //CheckUserControls();
            //fillTasks();
            //runningText.Text = "";
            //FillRunningTask();
            fillTasks();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            using (TextWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine("{0} {1} User {2} logged out", DateTime.Now.ToLongTimeString(),
        DateTime.Now.ToShortDateString(),loggedUser.Username);
            }
            lw.Show();
            this.Close();
        }
        public void fillTasks()
        {
            CRMEntities dbss = new CRMEntities();
          
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
            foreach (CRMv2.Models.Task t in dbss.Tasks.ToList())
            { 
                if (t.User.UserId==LoggedUserId&&t.isFinised==false)
                {
                    count++;
                  Expander expander=  ((Expander)FindName("exp_" + count));
                    expander.Visibility= Visibility.Visible;
                    expander.IsExpanded = true;
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
            add.CurrentUser = loggedUser;
            add.Show();

        }

        private void CheckUserControls()
        {
            if (loggedUser.RoleID==2)
            {
                btnAddUser.IsEnabled = false;
            }

            if (loggedUser.RoleID==1)
            {
                btnAddUser.IsEnabled = false;
                btnAddCompany.IsEnabled = false;
                btnCompanyUpdate.IsEnabled = false;
                btnCompanyDelete.IsEnabled = false;
                btnAddComment.IsEnabled = false;
                btnAddTask.IsEnabled = false;
                btnTaskFinish.IsEnabled = false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            using (TextWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine("{0} {1} User {2} logged out", DateTime.Now.ToLongTimeString(),
        DateTime.Now.ToShortDateString(), loggedUser.Username);

                tw.WriteLine("Application Close", DateTime.Now.ToLongTimeString(),
        DateTime.Now.ToShortDateString());
            }
            Environment.Exit(0);
        }

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            CreateCompany cc = new CreateCompany();
            cc.currentUser = loggedUser;
            cc.ShowDialog();
        }


        private void btnCompanyUpdate_Click(object sender, RoutedEventArgs e)
        {
            SelectCustomer sc = new SelectCustomer();
            sc.Show();
        }
        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            DeleteCustomer dc = new DeleteCustomer();
            dc.currentUser= loggedUser;
            dc.Show();
        }

        
        private void btnCompanyInfo_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo ci = new CustomerInfo();
            ci.Show();
        }

        private void btnAddComment_Click(object sender, RoutedEventArgs e)
        {
            AddComment ac = new AddComment();
            ac.currentUser = loggedUser;
            ac.Show();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            CreateTask ct = new CreateTask(this);
            ct.currentUSer = loggedUser;
            ct.Show();
        }

        public void FillRunningTask()
        {
            CRMEntities dbs = new CRMEntities();
            runningText.Text = "";
            foreach (Notification nt in dbs.Notifications.ToList())
            {
                if (nt.Task.User.UserId == loggedUser.UserId&&nt.IsActive==true)
                {
                   switch (nt.NotificationType)
                    {
                        case 1:
                            
                            if (nt.Task.DeadlineTime.Subtract(DateTime.Now).TotalHours<=24)
                            {
                                runningText.Text += nt.Task.Customer.CustomerName + " : " + nt.Task.Description+"      "; 
                            }
                        break;
                        case 2:

                            if (nt.Task.DeadlineTime.Subtract(DateTime.Now).TotalHours>24&& nt.Task.DeadlineTime.Subtract(DateTime.Now).TotalHours<72)
                            {
                                runningText.Text += nt.Task.Customer.CustomerName + " : " + nt.Task.Description + "      ";
                            }
                            break;
                        case 3:

                            if (nt.Task.DeadlineTime.Subtract(DateTime.Now).TotalHours <= 168)
                            {
                                runningText.Text += nt.Task.Customer.CustomerName + " : " + nt.Task.Description + "      ";
                            }
                            break;
                        case 4:
                            runningText.Text += nt.Task.Customer.CustomerName + " : " + nt.Task.Description + "      ";


                            break;

                    }
                }
            }
        }

        private void btnTaskFinish_Click(object sender, RoutedEventArgs e)
        {
            CompleteTask ct = new CompleteTask(this);
            ct.currentUser = loggedUser;
            ct.Show(); 
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Report rp = new Report(loggedUser);
            rp.Show();
        }
    }
}

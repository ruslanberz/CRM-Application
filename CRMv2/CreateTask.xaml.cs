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
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        CRMEntities db = new CRMEntities();
        MainPage mp;
     public   User currentUSer = new User();
        public CreateTask(MainPage main)
        {
            InitializeComponent();
            FillCustomerCmb();
          FillNotificationsCmb();
            this.mp = main;

        }

        private void FillCustomerCmb()
        {
            foreach (Customer cm in db.Customers.ToList())
            {
                if (cm.IsActive==true)
                {
                    cmbCustomers.Items.Add(cm);
                }
            }
        }

        private void FillNotificationsCmb()
        {
            cmbNotifications.Items.Add("Yox");
            cmbNotifications.Items.Add("1 gün qalmış");
            cmbNotifications.Items.Add("3 gün qalmış");
            cmbNotifications.Items.Add("1 həftə qalmış");
            cmbNotifications.Items.Add("Həmişə");
            cmbNotifications.SelectedIndex = 0;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (dtpDeadline.SelectedDate == null)
            {
                MessageBox.Show("Son icra tarixi seçin!","Status : Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }  else
            {
                Models.Task t = new Models.Task();
                t.CreationTime = DateTime.Now;
                t.DeadlineTime = dtpDeadline.SelectedDate.Value.Date;
                Customer cstm = cmbCustomers.SelectedItem as Customer;
                t.CustomerID = cstm.CustomerId;
                t.UserID = currentUSer.UserId;
                t.isFinised = false;
                t.Description = txtDescription.Text;
                db.Tasks.Add(t);
                db.SaveChanges();
                if (cmbNotifications.SelectedIndex == 0)
                {
                    
                    MessageBox.Show("Uğur!", "Status : OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                    
                }
                else
                {
                    
                    Notification nt = new Notification();
                    nt.TaskID = t.TaskId;
                    nt.NotificationType = Convert.ToByte(cmbNotifications.SelectedIndex);
                    
                    db.Notifications.Add(nt);
                    db.SaveChanges();
                    MessageBox.Show("Uğur!", "Status : OK", MessageBoxButton.OK, MessageBoxImage.Information);
                    mp.FillRunningTask();
                    this.Close();
                }
            }
           
            
        }
    }
}

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
    /// Interaction logic for CompleteTask.xaml
    /// </summary>
    public partial class CompleteTask : Window
    {
        MainPage mp;
       public User currentUser = new User();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CRMlogs.log";
        CRMEntities db = new CRMEntities();
        public CompleteTask(MainPage main)
        {
            InitializeComponent();
            FillCustomerCombo();
            this.mp = main;
        }
        
        public void FillCustomerCombo()
        {
            foreach (Customer ct in db.Customers.ToList())
            {
                if (ct.IsActive==true)
                {
                    CmbCustomers.Items.Add(ct);
                }
            }
        }

        private void CmbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgvTasks.Items.Clear();
            Models.Customer selection = CmbCustomers.SelectedItem as Models.Customer;
            if (currentUser.RoleID==3)
            {
                foreach (Models.Task tsk in db.Tasks.ToList())
                {
                    if (tsk.isFinised==false&&selection.CustomerId==tsk.CustomerID)
                    {
                        vwTask dgv = new vwTask();
                        dgv.CreatedBy = tsk.User.Name + " " + tsk.User.Surname;
                        dgv.StartTime = tsk.CreationTime.Date;
                        dgv.FinishTime = tsk.DeadlineTime.Date;
                        dgv.Description = tsk.Description;
                        dgv.id = tsk.TaskId;
                        dgvTasks.Items.Add(dgv);

                    }
                }
            }
            else if (currentUser.RoleID==2)
            {
                foreach (Models.Task tsk in db.Tasks.ToList())
                {
                    if (tsk.isFinised == false&&currentUser.UserId==tsk.UserID && selection.CustomerId == tsk.CustomerID)
                    {
                        vwTask dgv = new vwTask();
                        dgv.id = tsk.TaskId;
                        dgv.CreatedBy = tsk.User.Name + " " + tsk.User.Surname;
                        dgv.StartTime = tsk.CreationTime.Date;
                        dgv.FinishTime = tsk.DeadlineTime.Date;
                        dgv.Description = tsk.Description;
                        dgvTasks.Items.Add(dgv);

                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Search = txtSearch.Text;
            dgvTasks.Items.Clear();
            if (currentUser.RoleID==3)
            {
                foreach (Models.Task tsk in db.Tasks.ToList())
                {
                    if (tsk.isFinised==false&&(tsk.User.Name.ToLower().Contains(Search.ToLower())||tsk.User.Surname.ToLower().Contains(Search.ToLower())||tsk.Customer.CustomerName.ToLower().Contains(Search.ToLower())))
                    {
                        vwTask dgv = new vwTask();
                        dgv.CreatedBy = tsk.User.Name + " " + tsk.User.Surname;
                        dgv.StartTime = tsk.CreationTime.Date;
                        dgv.FinishTime = tsk.DeadlineTime.Date;
                        dgv.Description = tsk.Description;
                        dgv.id = tsk.TaskId;
                        dgvTasks.Items.Add(dgv);
                    }
                }

            }
            else if (currentUser.RoleID==2)
            {
                foreach (Models.Task ts in db.Tasks.ToList())
                {
                    if (ts.isFinised==false&&ts.UserID==currentUser.UserId&&ts.Customer.CustomerName.ToLower().Contains(Search.ToLower()))
                    {
                        vwTask dgv = new vwTask();
                        dgv.CreatedBy = ts.User.Name + " " + ts.User.Surname;
                        dgv.StartTime = ts.CreationTime.Date;
                        dgv.FinishTime = ts.DeadlineTime.Date;
                        dgv.Description = ts.Description;
                        dgv.id = ts.TaskId;
                        dgvTasks.Items.Add(dgv);
                    }
                }
            }
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
           if (MessageBox.Show(currentUser.Name+ " , siz taskın bitirməyinə əminsiniz? =)","Status : Qoz kimi",MessageBoxButton.OKCancel,MessageBoxImage.Question)==MessageBoxResult.OK)
            {
                if (dgvTasks.SelectedItems.Count == 1)
                {
                    vwTask complete = dgvTasks.SelectedItem as vwTask;
                    Models.Task forComplete = new Models.Task();
                    forComplete = db.Tasks.FirstOrDefault(t => t.TaskId == complete.id);
                    forComplete.FinishTime = DateTime.Now.Date;
                    forComplete.isFinised = true;
                    using (TextWriter tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("{0} {1} Success: User {2} completed the task for company: {3}, created  by user: {4}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToShortDateString(), currentUser.Username,forComplete.Customer.CustomerName,forComplete.User.Username);
                    }
                    Notification nt = new Notification();
                    nt = db.Notifications.FirstOrDefault(n => n.TaskID == forComplete.TaskId);
                    if (nt!=null)
                    {
                        nt.IsActive = false;
                        using (TextWriter tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine("{0} {1} Success: User {2} deleted the notification of task for company: {3}, created  by user: {4}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToShortDateString(), currentUser.Username, forComplete.Customer.CustomerName, forComplete.User.Username);
                        }

                    } 
                    db.SaveChanges();
                    mp.FillRunningTask();
                    mp.fillTasks();
                    dgvTasks.Items.Clear();
                    MessageBox.Show("OK");


                }
            } 
        }
    }
}

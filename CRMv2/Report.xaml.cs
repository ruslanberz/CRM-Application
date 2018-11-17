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
    /// Interaction logic for Report.xaml
    /// </summary>
    public static class DateTimeExtensions
    {
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
    public partial class Report : Window
    {   
        CRMEntities db = new CRMEntities();

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CRMlogs.log";
        User currentUser;
        public Report(User loggedUser)
        {
            InitializeComponent();
            this.currentUser = loggedUser;
          
           
        }

        private void fillChart()
        {
            DateTime currentDate = DateTime.Now;
            int TaskCountAll=0;
            int TaskCountFinished = 0;
            int TaskCountIncomplete = 0;
            int notificationCount = 0;
            int newUsersCount = 0;
            int newCustomersCount = 0;
            int deletedCustomersCount = 0;
            int newCommentCount = 0;
            //FILL Task aid statistics
            foreach (Models.Task t in db.Tasks.ToList())
            {
                if (t.CreationTime.Month == currentDate.Month&&t.CreationTime.Year==currentDate.Year)
                {
                    TaskCountAll++;

                }
                if (t.CreationTime.Month == currentDate.Month && t.CreationTime.Year == currentDate.Year&& t.isFinised==true)
                {
                    TaskCountFinished++;
                }

                if (t.CreationTime.Month == currentDate.Month&&currentDate.Month==t.DeadlineTime.Month && currentDate.Day-t.DeadlineTime.Day>0&&t.isFinised==false)
                {
                    TaskCountIncomplete++;
                }
               
            }
            chrtAllTasks.Value = TaskCountAll;
            lblCreatedTaskCount.Content = TaskCountAll;
            chrtCompletedTasks.Value = TaskCountFinished;
            lblCompletedTaskCount.Content = TaskCountFinished;
            chrtIncompleteTAskCount.Value = TaskCountIncomplete;
            lblDelayedTaskAll.Content = TaskCountIncomplete;


            //Fill notifications Count

            foreach (Notification n in db.Notifications.ToList())
            {
                if (n.CreationDate.Year==currentDate.Year&&n.CreationDate.Month==currentDate.Month)
                {
                    notificationCount++;
                }
            }
            chrtNotificationCount.Value = notificationCount;
            lblNewNotificationCount.Content = notificationCount;


            //Fill New users count

            foreach (User u in db.Users.ToList())
            {
                if  (u.CreationDate.Year == currentDate.Year && u.CreationDate.Month == currentDate.Month)
                    {
                    newUsersCount++;
                    }
            }

            chrtNewUsersCount.Value = newUsersCount;
            lblNewUserCount.Content = newUsersCount;
            //Fill new customers count and deleted customers count
            foreach (Customer c in db.Customers.ToList())
            {
                if (c.CreationDate.Year==currentDate.Year && c.CreationDate.Month==currentDate.Month)
                {
                    newCustomersCount++;
                }

                if (c.DeactivationDate!=null&&c.DeactivationDate.Value.Year==currentDate.Year&&c.DeactivationDate.Value.Month==currentDate.Month&&c.IsActive==false)
                {
                    deletedCustomersCount++;
                }
            }
            chrtNewCustomersCount.Value = newCustomersCount;
            lblNewCustomerCount.Content = newCustomersCount;
            chrtDeletedCustomersCount.Value = deletedCustomersCount;
            lblDeletedCustomerCount.Content = deletedCustomersCount;

            //Fill Comment count

            foreach (Comment com in db.Comments.ToList())
            {
                if (com.CreationDate.Year==currentDate.Year&&com.CreationDate.Month==currentDate.Month)
                {
                    newCommentCount++;
                }
            }
            chrtNewCommenetCount.Value = newCommentCount;
            lblNewCommentCount.Content = newCommentCount;
            MessageBox.Show(currentUser.Role.Name);
            using (TextWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine("{0} {1} Success:  User {2} with privelegies level : {3} generated monthly report for {4}, {5}", DateTime.Now.ToLongTimeString(),
        DateTime.Now.ToShortDateString(), currentUser.Username, currentUser.Role.Name, DateTime.Now.ToString("MMMM"),DateTime.Now.Year);
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dtpStartTime.SelectedDate==null||dtpFinishTime.SelectedDate==null)
            {
                MessageBox.Show("Tarixləri seçməyini unutmayın","Status:Səhv",MessageBoxButton.OK,MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} Error:  User {2} with privelegies level : {3} failed to generate report : date intervalnot set", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username, currentUser.Role.Name);
                }
            }
            else if (dtpFinishTime.SelectedDate.Value< dtpStartTime.SelectedDate.Value)
            {
                MessageBox.Show("Başlanğıc tarix son tarixdən sonra ola bilməz!", "Status:Səhv", MessageBoxButton.OK, MessageBoxImage.Error);
                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("{0} {1} Error:  User {2} with privelegies level : {3} failed to generate report : date range wasnot set properly!", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToShortDateString(), currentUser.Username, currentUser.Role.Name);
                }
            }
            else
            {
                FillCustomDate();
            }
        }

        private void FillCustomDate()
        {
            DateTime start = dtpStartTime.SelectedDate.Value;
            DateTime finish = dtpFinishTime.SelectedDate.Value;
            DateTime current = DateTime.Now;
            int TaskCountAll = 0;
            int TaskCountFinished = 0;
            int TaskCountIncomplete = 0;
            int notificationCount = 0;
            int newUsersCount = 0;
            int newCustomersCount = 0;
            int deletedCustomersCount = 0;
            int newCommentCount = 0;
            #region Fill custom date Task
                        foreach (Models.Task t in db.Tasks.ToList())
                        {
                            if (t.CreationTime.InRange(start,finish))
                            {
                                TaskCountAll++;

                            }
                            if (t.DeadlineTime.InRange(start, finish)&& t.isFinised == true)
                            {
                                TaskCountFinished++;
                            }

                            if (t.CreationTime.InRange(start, finish) && finish>t.DeadlineTime && current >t.DeadlineTime && t.isFinised == false)
                            {
                                TaskCountIncomplete++;
                            }

                        }
                        chrtAllTasks.Value = TaskCountAll;
                        lblCreatedTaskCount.Content = TaskCountAll;
                        chrtCompletedTasks.Value = TaskCountFinished;
                        lblCompletedTaskCount.Content = TaskCountFinished;
                        chrtIncompleteTAskCount.Value = TaskCountIncomplete;
                        lblDelayedTaskAll.Content = TaskCountIncomplete;
                        #endregion
            #region Fill custom date notification statistics
                        foreach (Notification n in db.Notifications.ToList())
                        {
                            if (n.CreationDate.InRange(start,finish))
                            {
                                notificationCount++;
                            }
                        }
                        chrtNotificationCount.Value = notificationCount;
                        lblNewNotificationCount.Content = newCommentCount;
                        #endregion
            #region Fill custom date user statistics
            foreach (User u in db.Users.ToList())
            {
                if (u.CreationDate.InRange(start,finish))
                {
                    newUsersCount++;
                }
            }

            chrtNewUsersCount.Value = newUsersCount;
            lblNewUserCount.Content = newUsersCount;

            #endregion
            #region Fill custom date customer related statistics
            foreach (Customer c in db.Customers.ToList())
            {
                if (c.CreationDate.InRange(start,finish))
                {
                    newCustomersCount++;
                }

                if (c.DeactivationDate != null && c.CreationDate.InRange(start, finish) && c.IsActive == false)
                {
                    deletedCustomersCount++;
                }
            }
            chrtNewCustomersCount.Value = newCustomersCount;
            lblNewCustomerCount.Content = newCustomersCount;
            chrtDeletedCustomersCount.Value = deletedCustomersCount;
            lblDeletedCustomerCount.Content = deletedCustomersCount;
            #endregion
            #region Fill custom date comments statistics
            foreach (Comment com in db.Comments.ToList())
            {
                if (com.CreationDate.InRange(start,finish))
                {
                    newCommentCount++;
                }
            }
            chrtNewCommenetCount.Value = newCommentCount;
            lblNewCommentCount.Content = newCommentCount;
            #endregion
            using (TextWriter tw = new StreamWriter(path, true))
            {
                tw.WriteLine("{0} {1} Success:  User {2} with privelegies level : {3} generated report for period {4} -  {5}", DateTime.Now.ToLongTimeString(),
        DateTime.Now.ToShortDateString(), currentUser.Username, currentUser.Role.Name,start.ToShortDateString(),finish.ToShortDateString());
            }
        }

       

     

        private void btn_MonthlyReport_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser.RoleID==1||currentUser.RoleID==3)
            {
                fillChart();
            }
            if (currentUser.RoleID==2||currentUser.RoleID==3)
            {
                FillCurrentUserStatistics();

            }

            if (currentUser.RoleID==2)
            {
                FillUserChart();
            }


        }

        private void FillCurrentUserStatistics ()
        {
            int createdTaskCount = 0;
            int CompletedTaskCount = 0;
            int FailedTaskCount = 0;
            CRMEntities dbss = new CRMEntities();
            foreach (Models.Task tsk in dbss.Tasks.ToList())
            {
                if (tsk.UserID==currentUser.UserId&&tsk.CreationTime.Month==DateTime.Now.Month&&tsk.CreationTime.Year==DateTime.Now.Year)
                {
                    createdTaskCount++;
                }

                if (tsk.UserID == currentUser.UserId&&tsk.DeadlineTime.Month==DateTime.Now.Month&&tsk.DeadlineTime.Year==DateTime.Now.Year&&tsk.isFinised==true)
                {
                    CompletedTaskCount++;
                }

                if (tsk.isFinised == false && tsk.FinishTime == null && tsk.DeadlineTime.Month == DateTime.Now.Month && tsk.DeadlineTime.Year == DateTime.Now.Year && DateTime.Now > tsk.DeadlineTime &&currentUser.UserId==tsk.UserID)
                {
                    FailedTaskCount++;
                }
                lblUserCreatedTasks.Content = createdTaskCount;
                lblUserCompletedTasks.Content = CompletedTaskCount;
                lblUserIncompletedTasks.Content = FailedTaskCount;


            }
        }

        private void FillUserChart()
        {
            CRMEntities dbs = new CRMEntities();
            int allTasks = 0;
            int completedTasks = 0;
            int failedTasks = 0;

            foreach (Models.Task tsk in dbs.Tasks.ToList())
            {
                if (tsk.UserID==currentUser.UserId)
                {
                    if (tsk.CreationTime.Month==DateTime.Now.Month&&tsk.CreationTime.Year==DateTime.Now.Year)
                    {
                        allTasks++;
                    }

                    if (tsk.DeadlineTime.Month==DateTime.Now.Month&&tsk.DeadlineTime.Year==DateTime.Now.Year&&tsk.isFinised==true&&tsk.FinishTime.Value.Month==DateTime.Now.Month&&tsk.FinishTime.Value.Year==DateTime.Now.Year)
                    {
                        completedTasks++;
                    }

                    if (tsk.isFinised==false&&tsk.FinishTime==null&&tsk.DeadlineTime.Month==DateTime.Now.Month&&tsk.DeadlineTime.Year==DateTime.Now.Year && DateTime.Now > tsk.DeadlineTime)
                    {
                        failedTasks++;
                    }
                }

            }

            chrtUCreatedTasks.Value = allTasks;
            chrtUCompletedTasks.Value = completedTasks;
            chrtUFailedTasks.Value = failedTasks;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentUser.RoleID == 1 || currentUser.RoleID == 3)
            {
                fillChart();
            }
            if (currentUser.RoleID == 2 || currentUser.RoleID == 3)
            {
                FillCurrentUserStatistics();
            }

            if (currentUser.RoleID == 1)
            {
                grdCurrentUserStats.Visibility = Visibility.Hidden;
                grdAllStats.Margin = grdCurrentUserStats.Margin;

            }

            if (currentUser.RoleID == 2)
            {
                grdAllStats.Visibility = Visibility.Hidden;
                ChrtMain.Visibility = Visibility.Hidden;
                chrtUser.Visibility = Visibility.Visible;
                FillUserChart();
            }
        }
    }

   
}

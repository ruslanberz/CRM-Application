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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        CRMEntities db = new CRMEntities();
        public Report()
        {
            InitializeComponent();
            fillChart();
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
            chrtCompletedTasks.Value = TaskCountFinished;
            chrtIncompleteTAskCount.Value = TaskCountIncomplete;
            //Fill notifications Count

            foreach (Notification n in db.Notifications.ToList())
            {
                if (n.CreationDate.Year==currentDate.Year&&n.CreationDate.Month==currentDate.Month)
                {
                    notificationCount++;
                }
            }
            chrtNotificationCount.Value = notificationCount;

            //Fill New users count

            foreach (User u in db.Users.ToList())
            {
                if  (u.CreationDate.Year == currentDate.Year && u.CreationDate.Month == currentDate.Month)
                    {
                    newUsersCount++;
                    }
            }

            chrtNewUsersCount.Value = newUsersCount;
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
            chrtDeletedCustomersCount.Value = deletedCustomersCount;

            //Fill Comment count

            foreach (Comment com in db.Comments.ToList())
            {
                if (com.CreationDate.Year==currentDate.Year&&com.CreationDate.Month==currentDate.Month)
                {
                    newCommentCount++;
                }
            }
            chrtNewCommenetCount.Value = newCommentCount;

        }
    }

   
}

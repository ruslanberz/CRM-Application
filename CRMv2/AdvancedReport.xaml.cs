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
    /// Interaction logic for AdvancedReport.xaml
    /// </summary>
    public partial class AdvancedReport : Window
    {
        public AdvancedReport(List<vwTaskReport> tasks)
        {
            InitializeComponent();

            List<vwTaskReport> my = tasks;
            dgv.ItemsSource = my;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Müştəri adı",
                Width=  new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CustomerName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "İstifadəçi adı",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("UserName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Tapşırıq",
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Description")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Yaranma tarixi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CreationTime")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Son icra tarixi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("DeadlineTime")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Bitirilib",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("isFinished")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Bitirilmə tarixi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("FinishedTime")
            });
            dgv.Items.Refresh();

         
           

           


        }
        public AdvancedReport(List<vwUsersReport> user)
        {
            InitializeComponent();
            List<vwUsersReport> my = user;
            dgv.ItemsSource = my;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Kim tərəfindən yaradılıb",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CreatedBy")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ad Soyad",
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("FullName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "İsfifadəçi adı",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Username")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "E-poçt",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Email")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Hüsuslar",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("RoleName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Yaradılma tarixi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CreationDate")
            });
            dgv.Items.Refresh();
        }

        public AdvancedReport(List<Customer> customer)
        {
            InitializeComponent();

        }
        public AdvancedReport(List<Comment> customer)
        {
            InitializeComponent();

        }

        public AdvancedReport(List<Notification> notification)
        {
            InitializeComponent();

        }
    }
}

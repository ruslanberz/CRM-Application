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
using ClosedXML.Excel;
using System.Data;

namespace CRMv2
{
    /// <summary>
    /// Interaction logic for AdvancedReport.xaml
    /// </summary>
    
    public partial class AdvancedReport : Window
    {
        byte localReportType;
        public List<vwTaskReport> task4xlsx;
        public List<vwUsersReport> user4xslx;
        public List<vwNewCustomer> customer4xlsx;
        public List<vwCommentReport> comment4xlsx;
        string Start, End;
        public AdvancedReport(List<vwTaskReport> tasks,byte reportType,string start, string end)
        {
            InitializeComponent();
            localReportType = reportType;
             List<vwTaskReport> my = tasks;
            task4xlsx = tasks;
            dgv.ItemsSource = my;
            Start = start;
            End = end;
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
        public AdvancedReport(List<vwUsersReport> user, byte reportType, string start,string end)
        {
            InitializeComponent();
            localReportType = reportType;
            List<vwUsersReport> my = user;
            dgv.ItemsSource = my;
            user4xslx = user;
            Start = start;
            End = end;
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
                Header = "Hüquqlar",
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

        public AdvancedReport(List<vwNewCustomer> customer,byte reportType, string start,string end)
        {
            InitializeComponent();
            List<vwNewCustomer> my = customer;
            localReportType = reportType;
            dgv.ItemsSource = my;
            customer4xlsx = customer;
            Start = start;
            End = end;
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
                Header = "Müştəri",
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CustomerName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Əlaqəli şəxs",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("ContactPerson")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ünvan",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Address")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ofis telefon nömrəsi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("OfficePhone")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Mobil nömrə",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Mobile")
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
                Header = "Yaradılma tarixi",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CreationDate")
            });
            dgv.Items.Refresh();

        }
        public AdvancedReport(List<vwCommentReport> comment,byte reportType,string start,string end)
        {
            InitializeComponent();
            List<vwCommentReport> my = comment;
            dgv.ItemsSource = my;
            comment4xlsx = comment;
            Start = start;
            End = end;
            localReportType = reportType;
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
                Header = "Müştəri",
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("CustomerName")
            });
            dgv.Columns.Add(new DataGridTextColumn()
            {
                Header = "Rəy",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 12,
                Binding = new Binding("Text")
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

        public AdvancedReport(List<Notification> notification)
        {
            InitializeComponent();

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        
            dlg.FileName = "Report"; // Default file name
            dlg.DefaultExt = ".xlsx"; // Default file extension
            dlg.Filter = "Excel spreedsheets (.xlsx)|*.xlsx"; // Filter files by extension

            // Show save file dialog box
            if (dlg.ShowDialog() == false)
            {
                return;
                
            }

            if (localReportType == 1)
            {

                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Tasklar hesabatı");
                    int rowcnt = 1;
                    if (Start!="start"&&End!="end")
                    {
                        ws.Cell("A1").Value = "This report is generated for period :"+Start+" - "+End;
                        ws.Range("A1:C1").Row(rowcnt).Merge();
                    }
                    ws.Column(1).Width = 20;
                    ws.Column(2).Width = 20;
                    ws.Column(3).Width = 80;
                    ws.Column(4).Width = 20;
                    ws.Column(5).Width = 20;
                    ws.Column(6).Width = 20;
                    ws.Column(7).Width = 20;
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.BallBlue;
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rowcnt++;
                    ws.Cell(rowcnt, "A").Value = dgv.Columns[0].Header;
                    ws.Cell(rowcnt, "B").Value = dgv.Columns[1].Header;
                    ws.Cell(rowcnt, "C").Value = dgv.Columns[2].Header;
                    ws.Cell(rowcnt, "D").Value = dgv.Columns[3].Header;
                    ws.Cell(rowcnt, "E").Value = dgv.Columns[4].Header;
                    ws.Cell(rowcnt, "F").Value = dgv.Columns[5].Header;
                    ws.Cell(rowcnt, "G").Value = dgv.Columns[6].Header;
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    rowcnt++;
                    foreach (vwTaskReport item in task4xlsx)
                    {
                        ws.Cell(rowcnt, "A").Value = item.CustomerName;
                        ws.Cell(rowcnt, "B").Value = item.UserName;
                        ws.Cell(rowcnt, "C").Value = item.Description;
                        ws.Cell(rowcnt, "D").Value = item.CreationTime;
                        ws.Cell(rowcnt, "E").Value = item.DeadlineTime;
                        ws.Cell(rowcnt, "F").Value = item.isFinished;
                        ws.Cell(rowcnt, "G").Value = item.FinishedTime;
                        rowcnt++;
                    }
                    ws.Column("C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    workbook.SaveAs(dlg.FileName);
                    MessageBox.Show("OK","Status: OK",MessageBoxButton.OK,MessageBoxImage.Information);

                }


            }

            if (localReportType==2)
            {
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("İstifadəçi hesabatı");
                    int rowcnt = 1;
                    if (Start != "start" && End != "end")
                    {
                        ws.Cell("A1").Value = "This report is generated for period :" + Start + " - " + End;
                        ws.Range("A1:C1").Row(rowcnt).Merge();
                    }
                    ws.Column(1).Width = 25;
                    ws.Column(2).Width = 30;
                    ws.Column(3).Width = 20;
                    ws.Column(4).Width = 25;
                    ws.Column(5).Width = 20;
                    ws.Column(6).Width = 20;
              
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.BallBlue;
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rowcnt++;
                    ws.Cell(rowcnt, "A").Value = dgv.Columns[0].Header;
                    ws.Cell(rowcnt, "B").Value = dgv.Columns[1].Header;
                    ws.Cell(rowcnt, "C").Value = dgv.Columns[2].Header;
                    ws.Cell(rowcnt, "D").Value = dgv.Columns[3].Header;
                    ws.Cell(rowcnt, "E").Value = dgv.Columns[4].Header;
                    ws.Cell(rowcnt, "F").Value = dgv.Columns[5].Header;
                   
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    rowcnt++;
                    foreach (vwUsersReport item in user4xslx)
                    {
                        ws.Cell(rowcnt, "A").Value = item.CreatedBy;
                        ws.Cell(rowcnt, "B").Value = item.FullName;
                        ws.Cell(rowcnt, "C").Value = item.Username;
                        ws.Cell(rowcnt, "D").Value = item.Email;
                        ws.Cell(rowcnt, "E").Value = item.RoleName;
                        ws.Cell(rowcnt, "F").Value = item.CreationDate;
                       
                        rowcnt++;
                    }
                    ws.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Column("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    workbook.SaveAs(dlg.FileName);
                    MessageBox.Show("OK", "Status: OK", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

            if (localReportType == 3)
            {
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Müştəri hesabatı");
                    int rowcnt = 1;
                    if (Start != "start" && End != "end")
                    {
                        ws.Cell("A1").Value = "This report is generated for period :" + Start + " - " + End;
                        ws.Range("A1:C1").Row(rowcnt).Merge();
                    }
                    ws.Column(1).Width = 30;
                    ws.Column(2).Width = 30;
                    ws.Column(3).Width = 30;
                    ws.Column(4).Width = 40;
                    ws.Column(5).Width = 20;
                    ws.Column(6).Width = 20;
                    ws.Column(7).Width = 25;
                    ws.Column(8).Width = 20;

                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.BallBlue;
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rowcnt++;
                    ws.Cell(rowcnt, "A").Value = dgv.Columns[0].Header;
                    ws.Cell(rowcnt, "B").Value = dgv.Columns[1].Header;
                    ws.Cell(rowcnt, "C").Value = dgv.Columns[2].Header;
                    ws.Cell(rowcnt, "D").Value = dgv.Columns[3].Header;
                    ws.Cell(rowcnt, "E").Value = dgv.Columns[4].Header;
                    ws.Cell(rowcnt, "F").Value = dgv.Columns[5].Header;
                    ws.Cell(rowcnt, "G").Value = dgv.Columns[6].Header;
                    ws.Cell(rowcnt, "H").Value = dgv.Columns[7].Header;

                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    rowcnt++;
                    foreach (vwNewCustomer item in customer4xlsx)
                    {
                        ws.Cell(rowcnt, "A").Value = item.CreatedBy;
                        ws.Cell(rowcnt, "B").Value = item.CustomerName;
                        ws.Cell(rowcnt, "C").Value = item.ContactPerson;
                        ws.Cell(rowcnt, "D").Value = item.Address;
                        ws.Cell(rowcnt, "E").SetValue(item.OfficePhone);
                        ws.Cell(rowcnt, "F").SetValue(item.Mobile);
                        ws.Cell(rowcnt, "G").Value = item.Email;
                        ws.Cell(rowcnt, "H").Value = item.CreationDate;


                        rowcnt++;
                    }
                    ws.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Column("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    workbook.SaveAs(dlg.FileName);
                    MessageBox.Show("OK", "Status: OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (localReportType==4)
            {
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Rəy hesabatı");
                    int rowcnt = 1;
                    if (Start != "start" && End != "end")
                    {
                        ws.Cell("A1").Value = "This report is generated for period :" + Start + " - " + End;
                        ws.Range("A1:C1").Row(rowcnt).Merge();
                    }
                    ws.Column(1).Width = 30;
                    ws.Column(2).Width = 30;
                    ws.Column(3).Width = 60;
                    ws.Column(4).Width = 20;
                  
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.BallBlue;
                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rowcnt++;
                    ws.Cell(rowcnt, "A").Value = dgv.Columns[0].Header;
                    ws.Cell(rowcnt, "B").Value = dgv.Columns[1].Header;
                    ws.Cell(rowcnt, "C").Value = dgv.Columns[2].Header;
                    ws.Cell(rowcnt, "D").Value = dgv.Columns[3].Header;
                 

                    ws.Row(rowcnt).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Row(rowcnt).Style.Fill.BackgroundColor = XLColor.GreenYellow;
                    rowcnt++;
                    foreach (vwCommentReport item in comment4xlsx)
                    {
                        ws.Cell(rowcnt, "A").Value = item.CreatedBy;
                        ws.Cell(rowcnt, "B").Value = item.CustomerName;
                        ws.Cell(rowcnt, "C").Value = item.Text;
                        ws.Cell(rowcnt, "D").Value = item.CreationDate;
                        

                        rowcnt++;
                    }
                    ws.Column("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    workbook.SaveAs(dlg.FileName);
                    MessageBox.Show("OK", "Status: OK", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }
    }
}

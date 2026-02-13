using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace StoresManager
{
    public partial class ucStores : UserControl
    {
        public ucStores()
        {
            InitializeComponent();

            LoadUser();
            LoadRecordsPerPageOptions();

            ApplyModernGridStyle();
        }

        #region Pagination
        private int currentPage = 1;
        private int recordsPerPage = 10;
        private int totalRecords = 6;
        private int totalPages = 0;
        private int currentPrintRow = 0;
        #endregion

        public void Alert(string msg, frmToast.enType type)
        {
            frmToast frm = new frmToast();
            frm.showAlert(msg, type);
        }
        #region Functions
        #endregion

        private void LoadRecordsPerPageOptions()
        {
            // Populate the ComboBox with options
            cbRecordsPerPage.Items.Add("All"); //
            cbRecordsPerPage.Items.Add("10");
            cbRecordsPerPage.Items.Add("20");
            cbRecordsPerPage.Items.Add("30");
            // Set the default selected value I
            cbRecordsPerPage.SelectedIndex = 2;
        }

        private void UpdateNavigationLabels()
        {
            totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            lblCurrentPage.Text = $"Page {currentPage} of {totalPages}";
            lblTotalRecords.Text = "Total Records: " + totalRecords;
        }

        private void cbRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbRecordsPerPage.SelectedItem.ToString();

            if (selectedValue.ToUpper() == "ALL")
            {
                recordsPerPage = int.MaxValue;
            }
            else
            {
                recordsPerPage = int.Parse(selectedValue);
            }

            currentPage = 1;
            LoadUser();
            UpdateNavigationLabels();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = 1;
                LoadUser();
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadUser();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadUser();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (totalPages > 0)
            {
                currentPage = totalPages;
                LoadUser();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                currentPrintRow = 0;
                // Show the print preview dialog using printDocument1
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument1;
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowError("An error occurred: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // 1. تعريف الخطوط (هيكل ثابت)
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 11, FontStyle.Bold);
            Font dataFont = new Font("Arial", 10, FontStyle.Regular);
            Font footerFont = new Font("Arial", 9, FontStyle.Italic);

            // 2. توزيع الأعمدة (X Coordinates) - قم بتعديل هذه الأرقام حسب عرض بياناتك
            int col1 = 30;  // CoachID
            int col2 = 100; // CoachName
            int col3 = 350; // Phone
            int col4 = 500; // BranchID
            int col5 = 620; // ImagePath

            // 3. رسم اللوجو والعنوان
            if (Properties.Resources.icons8_warehouse_36 != null)
            {
                e.Graphics.DrawImage(Properties.Resources.icons8_warehouse_36, 350, 20, 100, 60);
            }

            e.Graphics.DrawString("Inventory Management System", titleFont, Brushes.Black, 250, 90);
            e.Graphics.DrawString("Location: Egypt | Official Records", dataFont, Brushes.Black, 310, 120);

            // 4. رسم رؤوس الجدول (على خط Y واحد = 180)
            int headerY = 180;
            e.Graphics.DrawString("ID", headerFont, Brushes.Black, col1, headerY);
            e.Graphics.DrawString("Name", headerFont, Brushes.Black, col2, headerY);
            e.Graphics.DrawString("Phone Number", headerFont, Brushes.Black, col3, headerY);
            e.Graphics.DrawString("Branch", headerFont, Brushes.Black, col4, headerY);
            e.Graphics.DrawString("Status/Path", headerFont, Brushes.Black, col5, headerY);

            // رسم خط تحت الهيدر
            e.Graphics.DrawLine(Pens.Black, col1, headerY + 20, 800, headerY + 20);

            // 5. طباعة البيانات
            int startY = 220;
            int rowHeight = 30; // المسافة بين كل سطر وسطر
            int itemsPerPage = (e.MarginBounds.Bottom - startY) / rowHeight;
            int count = 0;

            while (currentPrintRow < dgvStores.Rows.Count)
            {
                DataGridViewRow row = dgvStores.Rows[currentPrintRow];

                if (!row.IsNewRow)
                {
                    // استخراج القيم بأمان مع معالجة الـ Null
                    string id = row.Cells["coachid"].Value?.ToString() ?? "";
                    string name = row.Cells["coachname"].Value?.ToString() ?? "";
                    string phone = row.Cells["phone"].Value?.ToString() ?? "";
                    string branch = row.Cells["branchid"].Value?.ToString() ?? "";
                    string path = row.Cells["imagepath"].Value?.ToString() ?? "";

                    // رسم البيانات في الأعمدة المحددة
                    e.Graphics.DrawString(id, dataFont, Brushes.Black, col1, startY);

                    // الاسم قد يكون طويلاً، لذا نستخدم RectangleF لقص النص إذا زاد عن مساحته (اختياري)
                    e.Graphics.DrawString(name, dataFont, Brushes.Black, col2, startY);

                    e.Graphics.DrawString(phone, dataFont, Brushes.Black, col3, startY);
                    e.Graphics.DrawString(branch, dataFont, Brushes.Black, col4, startY);
                    e.Graphics.DrawString(path, dataFont, Brushes.Black, col5, startY);

                    startY += rowHeight;
                    currentPrintRow++;
                    count++;

                    // التقسيم لصفحات
                    if (count >= itemsPerPage)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }
                else { currentPrintRow++; }
            }

            e.HasMorePages = false;

            // 6. التذييل (Footer) - يظهر في آخر صفحة فقط
            int footerY = startY + 40;
            e.Graphics.DrawLine(Pens.Gray, col1, footerY, 800, footerY);
            e.Graphics.DrawString($"- Printed on: {DateTime.Now:yyyy-MM-dd HH:mm}", footerFont, Brushes.Gray, col1, footerY + 10);
            e.Graphics.DrawString("- End of Stores List Printout -", footerFont, Brushes.Gray, 330, footerY + 30);
        }








        public static string connectionStringGym = "Server=.;Database=CoachesDB;User Id=sa;Password=123456;";

        public static DataTable GetCoachesPaged(int pageNumber, int pageSize)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStringGym))
            {
                // المعادلة السحرية للتقسيم في SQL Server
                string query = @"SELECT * FROM Coaches 
                         ORDER BY CoachID 
                         OFFSET @Offset ROWS 
                         FETCH NEXT @PageSize ROWS ONLY;";

                SqlCommand command = new SqlCommand(query, connection);
                // حساب عدد الأسطر التي سيتم تخطيها
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                try
                {
                    connection.Open();
                    dt.Load(command.ExecuteReader());
                }
                catch (Exception ex) { /* Handle error */ }
            }
            return dt;
        }
        private void LoadUser()
        {
            // 1. جلب العدد الحقيقي من الداتابيز أولاً
            totalRecords = GetTotalCoachesCount();

            // 2. تحديث الحسابات والـ Labels
            UpdateNavigationLabels();

            // 3. جلب بيانات الصفحة الحالية فقط (باستخدام OFFSET و FETCH التي شرحناها سابقاً)
            dgvStores.DataSource = GetCoachesPaged(currentPage, recordsPerPage);
        }

        private void ucStores_Load(object sender, EventArgs e)
        {
            //LoadUser();
        }


        public static int GetTotalCoachesCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionStringGym))
            {
                string query = "SELECT COUNT(*) FROM Coaches;";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return count;
        }

        public static DataTable SearchCoaches(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStringGym))
            {
                // نستخدم % قبل وبعد الكلمة للبحث عن أي جزء من النص
                string query = "SELECT * FROM Coaches WHERE CoachName LIKE @Search OR Phone LIKE @Search;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Search", "%" + searchText + "%");

                try
                {
                    connection.Open();
                    dt.Load(command.ExecuteReader());
                }
                catch (Exception ex) { /* Handle error */ }
            }
            return dt;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;

            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                LoadUser();
            }
            else
            {
                // استدعاء دالة البحث وتحديث الجريد مباشرة
                dgvStores.DataSource = SearchCoaches(tbSearch.Text);
            }
        }

        private void ApplyModernGridStyle()
        {
            // 1. تحديد الخطوط
            Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold); // خط العناوين
            Font dataFont = new Font("Segoe UI", 12, FontStyle.Regular);  // خط البيانات

            // 2. تطبيق الخطوط على كل مستوى
            dgvStores.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgvStores.DefaultCellStyle.Font = dataFont;

            // 3. ضبط ارتفاع الصفوف تلقائياً ليناسب الخط الكبير
            // بدون هذا السطر، سيتم قص الكلام (Clipping)
            dgvStores.RowTemplate.Height = 35;
            dgvStores.ColumnHeadersHeight = 45;
            dgvStores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // يفضل ضبطه يدوياً للأداء
        }











        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddNewStore_Click(object sender, EventArgs e)
        {
            frmAddStore frm = new frmAddStore();
            frm.ShowDialog();
        }
    }
}
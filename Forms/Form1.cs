using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class Form1 : Form
    {
        // Database
        private string dbPath;
        private string connectionString;

        // Controls (nullable)
        private TextBox? txtStudentCode;
        private TextBox? txtFirstName;
        private TextBox? txtLastName;
        private TextBox? txtEmail;
        private TextBox? txtPhone;
        private TextBox? txtSearch;
        private Button? btnAdd;
        private Button? btnUpdate;
        private Button? btnDelete;
        private Button? btnRefresh;
        private Button? btnSearch;
        private DataGridView? dataGridView;
        private Label? lblTotalCount;

        public Form1()
        {
            // Database setup
            dbPath = Path.Combine(Application.StartupPath, "SchoolDB.sqlite");
            connectionString = $"Data Source={dbPath};Version=3;";

            // Form setup
            this.Text = "School Management System";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create UI
            InitializeUI();

            // Create Database and Load Data
            CreateDatabase();
            LoadData();
        }

        private void InitializeUI()
        {
            // Header Panel
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.BackColor = Color.FromArgb(41, 128, 185);

            Label lblTitle = new Label();
            lblTitle.Text = "🏫 SCHOOL MANAGEMENT SYSTEM";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Student Record Management System";
            lblSubtitle.Font = new Font("Segoe UI", 11);
            lblSubtitle.ForeColor = Color.FromArgb(230, 240, 250);
            lblSubtitle.Location = new Point(25, 50);
            lblSubtitle.AutoSize = true;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            this.Controls.Add(headerPanel);

            // Main Panel
            Panel mainPanel = new Panel();
            mainPanel.Location = new Point(15, 90);
            mainPanel.Size = new Size(955, 510);
            mainPanel.BackColor = Color.White;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(mainPanel);

            // Form Section Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "📝 STUDENT INFORMATION";
            lblFormTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(41, 128, 185);
            lblFormTitle.Location = new Point(15, 10);
            lblFormTitle.Size = new Size(200, 25);
            mainPanel.Controls.Add(lblFormTitle);

            // Separator Line
            Panel separator = new Panel();
            separator.Location = new Point(15, 40);
            separator.Size = new Size(925, 2);
            separator.BackColor = Color.FromArgb(200, 200, 200);
            mainPanel.Controls.Add(separator);

            // Input Fields - Row 1
            int yStart = 60;
            int labelWidth = 100;
            int fieldWidth = 200;
            int col1 = 20;
            int col2 = 360;

            // Student Code
            Label lblCode = new Label();
            lblCode.Text = "Student Code:";
            lblCode.Font = new Font("Segoe UI", 10);
            lblCode.Location = new Point(col1, yStart);
            lblCode.Size = new Size(labelWidth, 25);
            mainPanel.Controls.Add(lblCode);

            txtStudentCode = new TextBox();
            txtStudentCode.Font = new Font("Segoe UI", 10);
            txtStudentCode.Location = new Point(col1 + labelWidth, yStart);
            txtStudentCode.Size = new Size(fieldWidth, 28);
            txtStudentCode.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(txtStudentCode);

            // First Name
            Label lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.Font = new Font("Segoe UI", 10);
            lblFirstName.Location = new Point(col2, yStart);
            lblFirstName.Size = new Size(labelWidth, 25);
            mainPanel.Controls.Add(lblFirstName);

            txtFirstName = new TextBox();
            txtFirstName.Font = new Font("Segoe UI", 10);
            txtFirstName.Location = new Point(col2 + labelWidth, yStart);
            txtFirstName.Size = new Size(fieldWidth, 28);
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(txtFirstName);

            // Row 2
            int yRow2 = yStart + 45;

            // Last Name
            Label lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.Font = new Font("Segoe UI", 10);
            lblLastName.Location = new Point(col1, yRow2);
            lblLastName.Size = new Size(labelWidth, 25);
            mainPanel.Controls.Add(lblLastName);

            txtLastName = new TextBox();
            txtLastName.Font = new Font("Segoe UI", 10);
            txtLastName.Location = new Point(col1 + labelWidth, yRow2);
            txtLastName.Size = new Size(fieldWidth, 28);
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(txtLastName);

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 10);
            lblEmail.Location = new Point(col2, yRow2);
            lblEmail.Size = new Size(labelWidth, 25);
            mainPanel.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.Location = new Point(col2 + labelWidth, yRow2);
            txtEmail.Size = new Size(fieldWidth, 28);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(txtEmail);

            // Row 3
            int yRow3 = yRow2 + 45;

            // Phone
            Label lblPhone = new Label();
            lblPhone.Text = "Phone:";
            lblPhone.Font = new Font("Segoe UI", 10);
            lblPhone.Location = new Point(col1, yRow3);
            lblPhone.Size = new Size(labelWidth, 25);
            mainPanel.Controls.Add(lblPhone);

            txtPhone = new TextBox();
            txtPhone.Font = new Font("Segoe UI", 10);
            txtPhone.Location = new Point(col1 + labelWidth, yRow3);
            txtPhone.Size = new Size(fieldWidth, 28);
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(txtPhone);

            // Buttons Panel
            Panel buttonPanel = new Panel();
            buttonPanel.Location = new Point(15, yRow3 + 50);
            buttonPanel.Size = new Size(925, 45);
            buttonPanel.BackColor = Color.Transparent;
            mainPanel.Controls.Add(buttonPanel);

            // Create Buttons
            btnAdd = new Button();
            btnAdd.Text = "➕ ADD STUDENT";
            btnAdd.Size = new Size(130, 40);
            btnAdd.Location = new Point(0, 5);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Click += BtnAdd_Click;
            buttonPanel.Controls.Add(btnAdd);

            btnUpdate = new Button();
            btnUpdate.Text = "✏️ UPDATE STUDENT";
            btnUpdate.Size = new Size(130, 40);
            btnUpdate.Location = new Point(140, 5);
            btnUpdate.BackColor = Color.FromArgb(52, 152, 219);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.Click += BtnUpdate_Click;
            buttonPanel.Controls.Add(btnUpdate);

            btnDelete = new Button();
            btnDelete.Text = "🗑️ DELETE STUDENT";
            btnDelete.Size = new Size(130, 40);
            btnDelete.Location = new Point(280, 5);
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Click += BtnDelete_Click;
            buttonPanel.Controls.Add(btnDelete);

            btnRefresh = new Button();
            btnRefresh.Text = "🔄 REFRESH";
            btnRefresh.Size = new Size(130, 40);
            btnRefresh.Location = new Point(420, 5);
            btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += BtnRefresh_Click;
            buttonPanel.Controls.Add(btnRefresh);

            // Search Panel
            Panel searchPanel = new Panel();
            searchPanel.Location = new Point(15, yRow3 + 105);
            searchPanel.Size = new Size(925, 50);
            searchPanel.BackColor = Color.FromArgb(248, 249, 250);
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(searchPanel);

            Label lblSearch = new Label();
            lblSearch.Text = "🔍 SEARCH:";
            lblSearch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(41, 128, 185);
            lblSearch.Location = new Point(15, 15);
            lblSearch.Size = new Size(80, 25);
            searchPanel.Controls.Add(lblSearch);

            txtSearch = new TextBox();
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.Location = new Point(95, 12);
            txtSearch.Size = new Size(250, 28);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            searchPanel.Controls.Add(txtSearch);

            btnSearch = new Button();
            btnSearch.Text = "GO";
            btnSearch.Size = new Size(60, 30);
            btnSearch.Location = new Point(355, 11);
            btnSearch.BackColor = Color.FromArgb(41, 128, 185);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Click += BtnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            lblTotalCount = new Label();
            lblTotalCount.Text = "Total Records: 0";
            lblTotalCount.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotalCount.ForeColor = Color.FromArgb(41, 128, 185);
            lblTotalCount.Location = new Point(700, 15);
            lblTotalCount.Size = new Size(200, 25);
            searchPanel.Controls.Add(lblTotalCount);

            // DataGridView
            dataGridView = new DataGridView();
            dataGridView.Location = new Point(15, yRow3 + 170);
            dataGridView.Size = new Size(925, 250);
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dataGridView.CellClick += DataGridView_CellClick;
            mainPanel.Controls.Add(dataGridView);

            // Footer
            Label lblFooter = new Label();
            lblFooter.Text = "© 2024 School Management System | All Rights Reserved";
            lblFooter.Font = new Font("Segoe UI", 9);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            lblFooter.Location = new Point(15, 610);
            lblFooter.Size = new Size(955, 25);
            this.Controls.Add(lblFooter);
        }

        private void CreateDatabase()
        {
            if (!File.Exists(dbPath))
            {
                try
                {
                    SQLiteConnection.CreateFile(dbPath);
                    using var connection = new SQLiteConnection(connectionString);
                    connection.Open();

                    string createTable = @"CREATE TABLE Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentCode TEXT UNIQUE NOT NULL,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Phone TEXT,
                        IsActive INTEGER DEFAULT 1
                    )";

                    using var cmd = new SQLiteCommand(createTable, connection);
                    cmd.ExecuteNonQuery();

                    // Sample Data
                    string insertData = @"INSERT INTO Students (StudentCode, FirstName, LastName, Email, Phone)
                        VALUES 
                        ('STU001', 'John', 'Smith', 'john.smith@email.com', '03001234567'),
                        ('STU002', 'Emma', 'Johnson', 'emma.johnson@email.com', '03001234568'),
                        ('STU003', 'Michael', 'Brown', 'michael.brown@email.com', '03001234569')";

                    using var cmd2 = new SQLiteCommand(insertData, connection);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("✅ Database created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating database: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            if (dataGridView == null || lblTotalCount == null) return;

            try
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                string query = @"SELECT StudentID, StudentCode, FirstName, LastName, Email, Phone 
                                FROM Students WHERE IsActive = 1";
                using var cmd = new SQLiteCommand(query, connection);
                DataTable dt = new DataTable();
                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
                dataGridView.DataSource = dt;

                // Set column headers
                if (dataGridView.Columns["StudentID"] != null)
                    dataGridView.Columns["StudentID"].HeaderText = "ID";
                if (dataGridView.Columns["StudentCode"] != null)
                    dataGridView.Columns["StudentCode"].HeaderText = "Student Code";
                if (dataGridView.Columns["FirstName"] != null)
                    dataGridView.Columns["FirstName"].HeaderText = "First Name";
                if (dataGridView.Columns["LastName"] != null)
                    dataGridView.Columns["LastName"].HeaderText = "Last Name";

                lblTotalCount.Text = $"Total Records: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void SearchData()
        {
            if (dataGridView == null) return;

            if (string.IsNullOrEmpty(txtSearch?.Text))
            {
                LoadData();
                return;
            }

            try
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                string query = @"SELECT StudentID, StudentCode, FirstName, LastName, Email, Phone 
                                FROM Students WHERE IsActive = 1 AND 
                                (FirstName LIKE @search OR LastName LIKE @search OR 
                                 StudentCode LIKE @search OR Email LIKE @search)";
                using var cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                DataTable dt = new DataTable();
                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
                dataGridView.DataSource = dt;
                if (lblTotalCount != null) lblTotalCount.Text = $"Total Records: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message);
            }
        }

        private void AddStudent()
        {
            if (txtStudentCode == null || txtFirstName == null) return;

            if (string.IsNullOrEmpty(txtStudentCode.Text) || string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please enter Student Code and First Name!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                string query = @"INSERT INTO Students (StudentCode, FirstName, LastName, Email, Phone) 
                                 VALUES (@code, @first, @last, @email, @phone)";
                using var cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@code", txtStudentCode.Text);
                cmd.Parameters.AddWithValue("@first", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last", txtLastName?.Text ?? "");
                cmd.Parameters.AddWithValue("@email", txtEmail?.Text ?? "");
                cmd.Parameters.AddWithValue("@phone", txtPhone?.Text ?? "");
                cmd.ExecuteNonQuery();

                LoadData();
                ClearForm();
                MessageBox.Show("✅ Student added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStudent()
        {
            if (dataGridView == null) return;

            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to update!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["StudentID"].Value);
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                string query = @"UPDATE Students SET 
                                 StudentCode=@code, FirstName=@first, LastName=@last, 
                                 Email=@email, Phone=@phone 
                                 WHERE StudentID=@id";
                using var cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@code", txtStudentCode?.Text ?? "");
                cmd.Parameters.AddWithValue("@first", txtFirstName?.Text ?? "");
                cmd.Parameters.AddWithValue("@last", txtLastName?.Text ?? "");
                cmd.Parameters.AddWithValue("@email", txtEmail?.Text ?? "");
                cmd.Parameters.AddWithValue("@phone", txtPhone?.Text ?? "");
                cmd.ExecuteNonQuery();

                LoadData();
                ClearForm();
                MessageBox.Show("✅ Student updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteStudent()
        {
            if (dataGridView == null) return;

            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to delete!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["StudentID"].Value);
                    using var connection = new SQLiteConnection(connectionString);
                    connection.Open();
                    string query = "DELETE FROM Students WHERE StudentID=@id";
                    using var cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    LoadData();
                    ClearForm();
                    MessageBox.Show("✅ Student deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            if (txtStudentCode != null) txtStudentCode.Text = "";
            if (txtFirstName != null) txtFirstName.Text = "";
            if (txtLastName != null) txtLastName.Text = "";
            if (txtEmail != null) txtEmail.Text = "";
            if (txtPhone != null) txtPhone.Text = "";
            if (txtSearch != null) txtSearch.Text = "";
        }

        // Event Handlers
        private void BtnAdd_Click(object? sender, EventArgs e) => AddStudent();
        private void BtnUpdate_Click(object? sender, EventArgs e) => UpdateStudent();
        private void BtnDelete_Click(object? sender, EventArgs e) => DeleteStudent();
        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadData();
        private void BtnSearch_Click(object? sender, EventArgs e) => SearchData();
        private void TxtSearch_TextChanged(object? sender, EventArgs e) => SearchData();

        private void DataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView == null) return;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                if (txtStudentCode != null) txtStudentCode.Text = row.Cells["StudentCode"].Value?.ToString() ?? "";
                if (txtFirstName != null) txtFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
                if (txtLastName != null) txtLastName.Text = row.Cells["LastName"].Value?.ToString() ?? "";
                if (txtEmail != null) txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                if (txtPhone != null) txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            }
        }
    }
}
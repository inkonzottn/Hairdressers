using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HairdressersApp
{
    public class AdminMasterScheduleForm : Form
    {
        private int masterId;
        private string masterName;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";

        // UI Елементи
        private SplitContainer splitContainer;
        private MonthCalendar calendar;
        private CheckBox chkIsWorking;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
        private Button btnSaveDay;
        private Button btnGenerateMonth;
        private DataGridView dgvAppointments;
        private Label lblSelectedDate;

        public AdminMasterScheduleForm(int masterId, string masterName)
        {
            this.masterId = masterId;
            this.masterName = masterName;

            InitializeCustomComponents();
            LoadDataForSelectedDate();
        }

        private void InitializeCustomComponents()
        {
            this.Text = $"Розклад та записи: {masterName}";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9.5f);

            // Основний розділювач (Ліво - Графік, Право - Записи)
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 340,
                IsSplitterFixed = true
            };
            this.Controls.Add(splitContainer);

            // --- ЛІВА ПАНЕЛЬ ---
            calendar = new MonthCalendar
            {
                Location = new Point(15, 15),
                MaxSelectionCount = 1
            };
            calendar.DateSelected += (s, e) => LoadDataForSelectedDate();
            splitContainer.Panel1.Controls.Add(calendar);

            GroupControlPanel();

            // --- ПРАВА ПАНЕЛЬ ---
            lblSelectedDate = new Label
            {
                Text = "Записи на обраний день",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            splitContainer.Panel2.Controls.Add(lblSelectedDate);

            dgvAppointments = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            splitContainer.Panel2.Controls.Add(dgvAppointments);
            dgvAppointments.BringToFront();
        }

        private void GroupControlPanel()
        {
            Panel panelDaySettings = new Panel
            {
                Location = new Point(15, 200),
                Size = new Size(300, 200),
                BorderStyle = BorderStyle.FixedSingle
            };

            chkIsWorking = new CheckBox
            {
                Text = "Робочий день",
                Location = new Point(15, 15),
                AutoSize = true,
                Checked = true
            };
            chkIsWorking.CheckedChanged += (s, e) => ToggleTimeInputs(chkIsWorking.Checked);

            Label lblStart = new Label { Text = "Початок:", Location = new Point(15, 50), AutoSize = true };
            dtpStartTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "HH:mm",
                ShowUpDown = true,
                Location = new Point(100, 47),
                Width = 80
            };

            Label lblEnd = new Label { Text = "Кінець:", Location = new Point(15, 85), AutoSize = true };
            dtpEndTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "HH:mm",
                ShowUpDown = true,
                Location = new Point(100, 82),
                Width = 80
            };

            btnSaveDay = new Button
            {
                Text = "Зберегти день",
                Location = new Point(15, 125),
                Width = 120,
                Height = 30,
                BackColor = Color.LightSkyBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveDay.Click += BtnSaveDay_Click;

            panelDaySettings.Controls.AddRange(new Control[] {
                chkIsWorking, lblStart, dtpStartTime, lblEnd, dtpEndTime, btnSaveDay
            });

            btnGenerateMonth = new Button
            {
                Text = "Згенерувати графік на місяць",
                Location = new Point(15, 415),
                Size = new Size(300, 35),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerateMonth.Click += BtnGenerateMonth_Click;

            splitContainer.Panel1.Controls.Add(panelDaySettings);
            splitContainer.Panel1.Controls.Add(btnGenerateMonth);
        }

        private void ToggleTimeInputs(bool enable)
        {
            dtpStartTime.Enabled = enable;
            dtpEndTime.Enabled = enable;
        }

        private void LoadDataForSelectedDate()
        {
            DateTime selectedDate = calendar.SelectionStart.Date;
            lblSelectedDate.Text = $"Записи на {selectedDate:dd.MM.yyyy} ({masterName})";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Завантажуємо графік майстра на день
                string scheduleQuery = "SELECT startTime, endTime, isWorking FROM MasterSchedules WHERE masterId = @mId AND workDate = @wDate";
                using (SqlCommand cmd = new SqlCommand(scheduleQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@mId", masterId);
                    cmd.Parameters.AddWithValue("@wDate", selectedDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isWorking = Convert.ToBoolean(reader["isWorking"]);
                            chkIsWorking.Checked = isWorking;
                            ToggleTimeInputs(isWorking);

                            if (reader["startTime"] != DBNull.Value)
                                dtpStartTime.Value = selectedDate.Add((TimeSpan)reader["startTime"]);
                            if (reader["endTime"] != DBNull.Value)
                                dtpEndTime.Value = selectedDate.Add((TimeSpan)reader["endTime"]);
                        }
                        else
                        {
                            // Дефолтні значення, якщо дня в базі немає
                            chkIsWorking.Checked = false;
                            ToggleTimeInputs(false);
                            dtpStartTime.Value = selectedDate.AddHours(9);
                            dtpEndTime.Value = selectedDate.AddHours(18);
                        }
                    }
                }

                // 2. Завантажуємо записи клієнтів на цей день
                string appQuery = @"SELECT a.date AS [Час], c.firstName + ' ' + c.lastName AS [Клієнт], s.title AS [Послуга]
                                   FROM Appointments a
                                   INNER JOIN Clients c ON a.clientId = c.id
                                   INNER JOIN Services s ON a.serviceId = s.id
                                   WHERE a.masterId = @mId AND CAST(a.date AS DATE) = @wDate
                                   ORDER BY a.date";

                using (SqlCommand cmdApp = new SqlCommand(appQuery, conn))
                {
                    cmdApp.Parameters.AddWithValue("@mId", masterId);
                    cmdApp.Parameters.AddWithValue("@wDate", selectedDate);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdApp))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvAppointments.DataSource = dt;
                    }
                }
            }
        }

        private void BtnSaveDay_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = calendar.SelectionStart.Date;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Запит UPSERT (Оновлює якщо є, Вставляє якщо немає)
                string query = @"
                    IF EXISTS (SELECT 1 FROM MasterSchedules WHERE masterId = @mId AND workDate = @wDate)
                        UPDATE MasterSchedules SET startTime = @start, endTime = @end, isWorking = @isWorking 
                        WHERE masterId = @mId AND workDate = @wDate
                    ELSE
                        INSERT INTO MasterSchedules (masterId, workDate, startTime, endTime, isWorking)
                        VALUES (@mId, @wDate, @start, @end, @isWorking)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mId", masterId);
                    cmd.Parameters.AddWithValue("@wDate", selectedDate);
                    cmd.Parameters.AddWithValue("@start", chkIsWorking.Checked ? dtpStartTime.Value.TimeOfDay : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@end", chkIsWorking.Checked ? dtpEndTime.Value.TimeOfDay : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@isWorking", chkIsWorking.Checked);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Графік на день успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnGenerateMonth_Click(object sender, EventArgs e)
        {
            DateTime firstDayOfSelectedMonth = new DateTime(calendar.SelectionStart.Year, calendar.SelectionStart.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(firstDayOfSelectedMonth.Year, firstDayOfSelectedMonth.Month);

            DialogResult confirm = MessageBox.Show($"Згенерувати стандартний графік (Пн-Пт 9:00-18:00) на {firstDayOfSelectedMonth:MMMM yyyy}?",
                "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        for (int day = 1; day <= daysInMonth; day++)
                        {
                            DateTime currentDate = new DateTime(firstDayOfSelectedMonth.Year, firstDayOfSelectedMonth.Month, day);
                            bool isWeekend = (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday);

                            string query = @"
                                IF EXISTS (SELECT 1 FROM MasterSchedules WHERE masterId = @mId AND workDate = @wDate)
                                    UPDATE MasterSchedules SET startTime = @start, endTime = @end, isWorking = @isWorking 
                                    WHERE masterId = @mId AND workDate = @wDate
                                ELSE
                                    INSERT INTO MasterSchedules (masterId, workDate, startTime, endTime, isWorking)
                                    VALUES (@mId, @wDate, @start, @end, @isWorking)";

                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@mId", masterId);
                                cmd.Parameters.AddWithValue("@wDate", currentDate);
                                cmd.Parameters.AddWithValue("@start", isWeekend ? (object)DBNull.Value : new TimeSpan(9, 0, 0));
                                cmd.Parameters.AddWithValue("@end", isWeekend ? (object)DBNull.Value : new TimeSpan(18, 0, 0));
                                cmd.Parameters.AddWithValue("@isWorking", !isWeekend);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        MessageBox.Show("Графік на місяць успішно згенеровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataForSelectedDate();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Помилка при генерації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
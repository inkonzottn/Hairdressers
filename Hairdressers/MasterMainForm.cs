using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HairdressersApp
{
    public class MasterMainForm : Form
    {
        private int masterId;
        private string masterName;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";

        // UI Елементи
        private SplitContainer splitContainer;
        private MonthCalendar calendar;
        private Label lblWorkStatus;
        private Label lblWorkHours;
        private DataGridView dgvAppointments;
        private Label lblSelectedDate;

        public MasterMainForm(int masterId, string masterName)
        {
            this.masterId = masterId;
            this.masterName = masterName;

            InitializeCustomComponents();
            LoadMasterDataForSelectedDate();
        }

        private void InitializeCustomComponents()
        {
            this.Text = $"Кабінет майстра: {masterName}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f);

            // Основний розділювач (Ліво - Календар і статус, Право - Записи)
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 320,
                IsSplitterFixed = true
            };
            this.Controls.Add(splitContainer);

            // --- ЛІВА ПАНЕЛЬ (Календар та інформація про день) ---
            calendar = new MonthCalendar
            {
                Location = new Point(15, 15),
                MaxSelectionCount = 1
            };
            calendar.DateSelected += (s, e) => LoadMasterDataForSelectedDate();
            splitContainer.Panel1.Controls.Add(calendar);

            // Панель статусу робочого дня
            Panel panelInfo = new Panel
            {
                Location = new Point(15, 200),
                Size = new Size(285, 130),
                BorderStyle = BorderStyle.FixedSingle
            }
            ;

            Label lblInfoTitle = new Label
            {
                Text = "Інформація про день:",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            lblWorkStatus = new Label
            {
                Text = "Статус: завантаження...",
                Location = new Point(10, 40),
                AutoSize = true
            };

            lblWorkHours = new Label
            {
                Text = "Години: -",
                Location = new Point(10, 70),
                AutoSize = true
            };

            panelInfo.Controls.AddRange(new Control[] { lblInfoTitle, lblWorkStatus, lblWorkHours });
            splitContainer.Panel1.Controls.Add(panelInfo);

            // --- ПРАВА ПАНЕЛЬ (Список записів) ---
            lblSelectedDate = new Label
            {
                Text = "Мої записи на обраний день",
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


            // Створюємо кнопку виходу
            Button btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(80, 30),
                // Ставимо у верхній правий кут (можеш підкоригувати координати X та Y під свій дизайн)
                Location = new Point(this.ClientSize.Width - 95, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right, // Щоб кнопка трималася правого кута при зміні розміру вікна
                BackColor = Color.FromArgb(255, 230, 230), // Легкий червонуватий відтінок для акценту
                FlatStyle = FlatStyle.Flat
            };

            // Прив'язуємо подію кліку
            btnLogout.Click += BtnLogout_Click;

            // Додаємо кнопку на форму (або на конкретну панель/splitContainer, залежно де тобі зручніше)
            this.Controls.Add(btnLogout);
            btnLogout.BringToFront(); // Виводимо наперед, щоб не перекривалася іншими елементами
        }

        private void LoadMasterDataForSelectedDate()
        {
            DateTime selectedDate = calendar.SelectionStart.Date;
            lblSelectedDate.Text = $"Записи на {selectedDate:dd.MM.yyyy}";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Отримуємо графік майстра на цей день (щоб показати, чи працює він і з якої години)
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
                            if (isWorking)
                            {
                                lblWorkStatus.Text = "Статус: Робочий день 🟢";
                                lblWorkStatus.ForeColor = Color.DarkGreen;

                                TimeSpan start = reader["startTime"] != DBNull.Value ? (TimeSpan)reader["startTime"] : TimeSpan.Zero;
                                TimeSpan end = reader["endTime"] != DBNull.Value ? (TimeSpan)reader["endTime"] : TimeSpan.Zero;
                                
                                // Безпечне форматування без синтаксичного цукру інтерполяції
                                lblWorkHours.Text = $"Години: {start.ToString(@"hh\:mm")} - {end.ToString(@"hh\:mm")}";
                            }
                            else
                            {
                                lblWorkStatus.Text = "Статус: Вихідний 🔴";
                                lblWorkStatus.ForeColor = Color.DarkRed;
                                lblWorkHours.Text = "Години: —";
                            }
                        }
                        else
                        {
                            lblWorkStatus.Text = "Статус: Графік не задано ⚪";
                            lblWorkStatus.ForeColor = Color.Gray;
                            lblWorkHours.Text = "Години: —";
                        }
                    }
                }

                // 2. Завантажуємо записи клієнтів до цього конкретного майстра на обрану дату
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

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви дійсно хочете вийти з облікового запису?", "Підтвердження виходу", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); 
            }
        }
    }
}
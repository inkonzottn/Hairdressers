using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Hairdressers
{
    public class CreateAppointmentForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";
        private int clientId;

        // UI Елементи
        private ComboBox cmbServices;
        private Label lblServiceInfo;
        private DateTimePicker dtpDate;
        private FlowLayoutPanel pnlMasters; // Панель для радіобатонів майстрів
        private ListBox lstAvailableSlots;
        private Button btnBook;

        private int selectedServiceId = -1;
        private int selectedMasterId = -1;
        private int serviceDuration = 0;

        public CreateAppointmentForm(int clientId)
        {
            this.clientId = clientId;
            InitializeCustomComponents();
            LoadServices();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Створення нового запису";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9.5f);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // 1. Вибір послуги
            Label lblService = new Label { Text = "Оберіть послугу:", Location = new Point(20, 20), AutoSize = true };
            cmbServices = new ComboBox { Location = new Point(20, 45), Width = 440, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbServices.SelectedIndexChanged += CmbServices_SelectedIndexChanged;

            lblServiceInfo = new Label { Text = "Ціна: 0 грн | Тривалість: 0 хв", Location = new Point(20, 75), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };

            // 2. Вибір дати
            Label lblDate = new Label { Text = "Дата запису:", Location = new Point(20, 110), AutoSize = true };
            dtpDate = new DateTimePicker { Location = new Point(20, 135), Width = 200, Format = DateTimePickerFormat.Short, MinDate = DateTime.Today };
            dtpDate.ValueChanged += (s, e) => UpdateMastersAndSlots();

            // 3. Вибір майстра (Панель з радіобатонами)
            Label lblMaster = new Label { Text = "Доступні майстри:", Location = new Point(20, 175), AutoSize = true };
            pnlMasters = new FlowLayoutPanel
            {
                Location = new Point(20, 200),
                Size = new Size(440, 70),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            // 4. Доступний час
            Label lblTime = new Label { Text = "Вільні години (крок 10 хв + буфер 15 хв):", Location = new Point(20, 285), AutoSize = true };
            lstAvailableSlots = new ListBox { Location = new Point(20, 310), Size = new Size(440, 120) };

            // 5. Кнопка підтвердження
            btnBook = new Button
            {
                Text = "Підтвердити запис",
                Location = new Point(20, 445),
                Size = new Size(440, 40),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnBook.Click += BtnBook_Click;

            this.Controls.AddRange(new Control[] {
                lblService, cmbServices, lblServiceInfo,
                lblDate, dtpDate,
                lblMaster, pnlMasters,
                lblTime, lstAvailableSlots,
                btnBook
            });
        }

        private void LoadServices()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, title, price, durationMinutes FROM Services";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cmbServices.DataSource = dt;
                        cmbServices.DisplayMember = "title";
                        cmbServices.ValueMember = "id";
                    }
                }
            }
        }

        private void CmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServices.SelectedItem is DataRowView row)
            {
                selectedServiceId = Convert.ToInt32(row["id"]);
                decimal price = Convert.ToDecimal(row["price"]);
                serviceDuration = Convert.ToInt32(row["durationMinutes"]);

                lblServiceInfo.Text = $"Ціна: {price} грн | Тривалість: {serviceDuration} хв";
                UpdateMastersAndSlots();
            }
        }

        private void UpdateMastersAndSlots()
        {
            pnlMasters.Controls.Clear();
            lstAvailableSlots.Items.Clear();
            selectedMasterId = -1;

            if (selectedServiceId == -1) return;

            DateTime selectedDate = dtpDate.Value.Date;

            // Знаходимо майстрів, які роблять цю послугу І працюють у цей день
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT DISTINCT m.id, m.firstName, m.lastName
                    FROM Masters m
                    INNER JOIN MasterServices ms ON m.id = ms.masterId
                    INNER JOIN MasterSchedules sch ON m.id = sch.masterId
                    WHERE ms.serviceId = @serviceId 
                      AND sch.workDate = @workDate 
                      AND sch.isWorking = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@serviceId", selectedServiceId);
                    cmd.Parameters.AddWithValue("@workDate", selectedDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool hasMasters = false;
                        while (reader.Read())
                        {
                            hasMasters = true;
                            int mId = Convert.ToInt32(reader["id"]);
                            string mName = $"{reader["firstName"]} {reader["lastName"]}";

                            RadioButton rb = new RadioButton
                            {
                                Text = mName,
                                Tag = mId,
                                AutoSize = true,
                                Margin = new Padding(10, 10, 10, 10)
                            };
                            rb.CheckedChanged += MasterRadioButton_CheckedChanged;
                            pnlMasters.Controls.Add(rb);
                        }

                        if (!hasMasters)
                        {
                            Label lblNo = new Label { Text = "Немає вільних майстрів на цей день.", AutoSize = true, Padding = new Padding(5) };
                            pnlMasters.Controls.Add(lblNo);
                        }
                    }
                }
            }
        }

        private void MasterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                selectedMasterId = (int)rb.Tag;
                LoadAvailableSlotsForMaster();
            }
        }

        private void LoadAvailableSlotsForMaster()
        {
            lstAvailableSlots.Items.Clear();
            if (selectedMasterId == -1) return;

            List<TimeSpan> slots = GetAvailableSlots(selectedMasterId, dtpDate.Value.Date, serviceDuration);
            foreach (var slot in slots)
            {
                lstAvailableSlots.Items.Add(slot.ToString(@"hh\:mm"));
            }
        }

        private List<TimeSpan> GetAvailableSlots(int masterId, DateTime date, int serviceDurationMinutes)
        {
            List<TimeSpan> availableSlots = new List<TimeSpan>();
            TimeSpan workStart = TimeSpan.Zero;
            TimeSpan workEnd = TimeSpan.Zero;
            bool isWorking = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Отримуємо робочий час майстра на цей день
                string scheduleQuery = "SELECT startTime, endTime, isWorking FROM MasterSchedules WHERE masterId = @mId AND workDate = @wDate";
                using (SqlCommand cmd = new SqlCommand(scheduleQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@mId", masterId);
                    cmd.Parameters.AddWithValue("@wDate", date.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isWorking = Convert.ToBoolean(reader["isWorking"]);
                            if (isWorking)
                            {
                                workStart = (TimeSpan)reader["startTime"];
                                workEnd = (TimeSpan)reader["endTime"];
                            }
                        }
                    }
                }

                if (!isWorking) return availableSlots; // Якщо вихідний — вільних місць немає

                // 2. Отримуємо існуючі записи майстра на цей день разом із тривалістю їхніх послуг
                string appsQuery = @"
            SELECT a.date, s.durationMinutes 
            FROM Appointments a
            INNER JOIN Services s ON a.serviceId = s.id
            WHERE a.masterId = @mId AND CAST(a.date AS DATE) = @wDate";

                List<(TimeSpan start, TimeSpan endWithBuffer)> busyIntervals = new List<(TimeSpan, TimeSpan)>();

                using (SqlCommand cmdApps = new SqlCommand(appsQuery, conn))
                {
                    cmdApps.Parameters.AddWithValue("@mId", masterId);
                    cmdApps.Parameters.AddWithValue("@wDate", date.Date);

                    using (SqlDataReader reader = cmdApps.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime appDateTime = Convert.ToDateTime(reader["date"]);
                            TimeSpan appStart = appDateTime.TimeOfDay;
                            int duration = Convert.ToInt32(reader["durationMinutes"]);

                            // Додаємо 15 хвилин буфера до кінця запису
                            TimeSpan appEndWithBuffer = appStart.Add(TimeSpan.FromMinutes(duration + 15));
                            busyIntervals.Add((appStart, appEndWithBuffer));
                        }
                    }
                }

                // 3. Генеруємо слоти з кроком у 10 хвилин
                TimeSpan step = TimeSpan.FromMinutes(10);
                TimeSpan serviceLength = TimeSpan.FromMinutes(serviceDurationMinutes);
                TimeSpan bufferTime = TimeSpan.FromMinutes(15);

                for (TimeSpan current = workStart; current.Add(serviceLength) <= workEnd; current = current.Add(step))
                {
                    TimeSpan potentialEnd = current.Add(serviceLength);
                    bool isConflict = false;

                    // Перевіряємо конфлікт із зайнятими інтервалами (враховуючи буфер)
                    foreach (var busy in busyIntervals)
                    {
                        TimeSpan ourEndWithBuffer = potentialEnd.Add(bufferTime);

                        if (current < busy.endWithBuffer && busy.start < potentialEnd ||
                    (current < busy.start && ourEndWithBuffer > busy.start))
                        {
                            isConflict = true;
                            break;
                        }
                    }

                    if (!isConflict)
                    {
                        availableSlots.Add(current);
                    }
                }
            }

            return availableSlots;
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (selectedMasterId == -1 || selectedServiceId == -1 || lstAvailableSlots.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, оберіть послугу, майстра та вільний час!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan selectedTime = TimeSpan.Parse(lstAvailableSlots.SelectedItem.ToString());
            DateTime fullAppointmentDateTime = dtpDate.Value.Date.Add(selectedTime);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO Appointments (clientId, masterId, serviceId, date, status) VALUES (@clientId, @masterId, @serviceId, @date, @status)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@masterId", selectedMasterId);
                    cmd.Parameters.AddWithValue("@serviceId", selectedServiceId);
                    cmd.Parameters.AddWithValue("@date", fullAppointmentDateTime);
                    cmd.Parameters.AddWithValue("@status", "Заплановано");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запис успішно створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
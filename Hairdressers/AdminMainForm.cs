using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hairdressers
{
    public partial class AdminMainForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";
        private int selectedServiceId = -1;
        private int selectedClientId = -1;
        private int selectedMasterId = -1;
        private int selectedUserId = -1;

        public AdminMainForm()
        {
            InitializeComponent();
            LoadServices();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcAdmin.SelectedTab == tpServices)
            {
                LoadServices();
            }
            else if (tcAdmin.SelectedTab == tpClients)
            {
                LoadClients();
            }
            else if (tcAdmin.SelectedTab == tpMasters)
            {
                LoadMasters();
                InitSpecializations();
                InitServices();
            }
        }

        // ПОСЛУГИ
        private void LoadServices()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, title, durationMinutes, price FROM Services";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvServices.DataSource = dt;

                        if (dgvServices.Columns["id"] != null)
                            dgvServices.Columns["id"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження послуг: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtName.Text.Trim();
            string durationStr = txtDuration.Text.Trim();
            string priceStr = txtPrice.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(durationStr) || string.IsNullOrEmpty(priceStr))
            {
                MessageBox.Show("Заповніть усі поля!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(durationStr, out int duration) || !decimal.TryParse(priceStr, out decimal price))
            {
                MessageBox.Show("Тривалість має бути числом (хвилини), а ціна — коректним числом!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Services (title, durationMinutes, price) VALUES (@title, @duration, @price)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@price", price);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Послугу успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearServiceInputs();
                        LoadServices();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void dgvServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && selectedServiceId != -1)
            {
                DataGridViewRow row = dgvServices.Rows[e.RowIndex];
                selectedServiceId = Convert.ToInt32(row.Cells["id"].Value);
                txtName.Text = row.Cells["title"].Value.ToString();
                txtDuration.Text = row.Cells["durationMinutes"].Value.ToString();
                txtPrice.Text = row.Cells["price"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == -1)
            {
                MessageBox.Show("Оберіть послугу з таблиці для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtName.Text.Trim();
            string durationStr = txtDuration.Text.Trim();
            string priceStr = txtPrice.Text.Trim();

            if (!int.TryParse(durationStr, out int duration) || !decimal.TryParse(priceStr, out decimal price))
            {
                MessageBox.Show("Перевірте коректність введених чисел!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Services SET title = @title, durationMinutes = @duration, price = @price WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedServiceId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@price", price);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Послугу оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearServiceInputs();
                        LoadServices();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == -1)
            {
                MessageBox.Show("Оберіть послугу для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Ви впевнені, що хочете видалити цю послугу?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection connection = GetConnection())
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Services WHERE id = @id";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedServiceId);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Послугу видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearServiceInputs();
                            LoadServices();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ClearServiceInputs()
        {
            txtName.Clear();
            txtDuration.Clear();
            txtPrice.Clear();
            selectedServiceId = -1;
        }

        // КЛІЄНТИ

        private void LoadClients(string searchQuery = "")
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, firstName, lastName, phoneNumber FROM Clients";

                    // якщо є текст для пошуку
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE firstName LIKE @search OR lastName LIKE @search OR phoneNumber LIKE @search";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvClients.DataSource = dt;

                            if (dgvClients.Columns["id"] != null)
                                dgvClients.Columns["id"].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження клієнтів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchClient.Text.Trim();
            LoadClients(searchText);
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            string firstName = txtClientFirstName.Text.Trim();
            string lastName = txtClientLastName.Text.Trim();
            string phone = txtClientPhoneNumber.Text.Trim();

            if (!txtClientPhoneNumber.MaskCompleted)
            {
                MessageBox.Show("Введіть номер телефону повністю!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!phone.StartsWith("+38 (0"))
            {
                MessageBox.Show("Номер телефону має починатися з українського коду (наприклад, +38 (099)...)!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заповніть усі поля клієнта!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Clients (firstName, lastName, phoneNumber) VALUES (@firstName, @lastName, @phone)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Клієнта успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearClientInputs();
                        LoadClients();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClients.Rows[e.RowIndex];
                selectedClientId = Convert.ToInt32(row.Cells["id"].Value);
                txtClientFirstName.Text = row.Cells["firstName"].Value.ToString();
                txtClientLastName.Text = row.Cells["lastName"].Value.ToString();
                txtClientPhoneNumber.Text = row.Cells["phoneNumber"].Value.ToString();
            }
        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            if (selectedClientId == -1)
            {
                MessageBox.Show("Оберіть клієнта для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Ви впевнені, що хочете видалити цього клієнта?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection connection = GetConnection())
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Clients WHERE id = @id";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedClientId);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Клієнта видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearClientInputs();
                            LoadClients();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdateClient_Click(object sender, EventArgs e)
        {
            if (selectedClientId == -1)
            {
                MessageBox.Show("Оберіть клієнта з таблиці для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = txtClientFirstName.Text.Trim();
            string lastName = txtClientLastName.Text.Trim();
            string phone = txtClientPhoneNumber.Text.Trim();

            if (!txtClientPhoneNumber.MaskCompleted)
            {
                MessageBox.Show("Введіть номер телефону повністю!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!phone.StartsWith("+38 (0"))
            {
                MessageBox.Show("Номер телефону має починатися з українського коду (наприклад, +38 (099)...)!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заповніть усі поля клієнта!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Clients SET firstName = @firstName, lastName = @lastName, phoneNumber = @phone WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@id", selectedClientId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Дані клієнта успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearClientInputs();
                        LoadClients();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearClientInputs()
        {
            txtClientFirstName.Clear();
            txtClientLastName.Clear();
            txtClientPhoneNumber.Clear();
            selectedClientId = -1;
        }

        private void OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }


        // МАЙСТРИ

        private void LoadMasters(string searchQuery = "")
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT m.id, m.userId, m.specialization, m.firstName, m.lastName, u.login, u.passwordHash 
                             FROM Masters m 
                             INNER JOIN Users u ON m.userId = u.id";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE m.firstName LIKE @search OR m.lastName LIKE @search OR m.specialization LIKE @search OR u.login LIKE @search";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvMasters.DataSource = dt;

                            // ховаємо службові колонки
                            if (dgvMasters.Columns["id"] != null) dgvMasters.Columns["id"].Visible = false;
                            if (dgvMasters.Columns["userId"] != null) dgvMasters.Columns["userId"].Visible = false;
                            if (dgvMasters.Columns["login"] != null) dgvMasters.Columns["login"].Visible = false;      
                            if (dgvMasters.Columns["passwordHash"] != null) dgvMasters.Columns["passwordHash"].Visible = false;
                            if (dgvMasters.Columns["title"] != null) dgvMasters.Columns["title"].Visible = false;
                            if (dgvMasters.Columns["serviceId"] != null) dgvMasters.Columns["serviceId"].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження майстрів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearchMaster_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchMaster.Text.Trim();
            ClearMasterInputs();
            LoadMasters(searchText);
        }

        private void InitServices()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    DataTable dtSrv = new DataTable();
                    using (SqlDataAdapter srvAdapter = new SqlDataAdapter("SELECT id, title FROM Services", conn))
                    {
                        srvAdapter.Fill(dtSrv);
                    }

                    clbMasterServices.DataSource = dtSrv;
                    clbMasterServices.DisplayMember = "title";
                    clbMasterServices.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження майстрів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitSpecializations()
        {
            cmbMasterSpecialization.Items.Clear();
            cmbMasterSpecialization.Items.AddRange(new string[] {
                "Жіночий майстер",
                "Чоловічий майстер",
                "Pro-майстер",
                "Колорист"
            });
            cmbMasterSpecialization.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnAddMaster_Click(object sender, EventArgs e)
        {
            string firstName = txtMasterFirstName.Text.Trim();
            string lastName = txtMasterLastName.Text.Trim();
            string spec = cmbMasterSpecialization.Text.Trim();
            string login = txtMasterLogin.Text.Trim();
            string password = txtMasterPassword.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(spec) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заповніть усі поля, включаючи спеціалізацію, логін та пароль!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string passwordHash = HashPassword(password);
                        string userQuery = "INSERT INTO Users (login, passwordHash, roleId) OUTPUT INSERTED.id VALUES (@login, @passwordHash, @roleId)";
                        int newUserId;
                        int newMasterId;


                        using (SqlCommand cmdUser = new SqlCommand(userQuery, connection, transaction))
                        {
                            cmdUser.Parameters.AddWithValue("@login", login);
                            cmdUser.Parameters.AddWithValue("@passwordHash", passwordHash);
                            cmdUser.Parameters.AddWithValue("@roleId", 2);

                            newUserId = (int)cmdUser.ExecuteScalar();
                        }

                        string masterQuery = "INSERT INTO Masters (userId, specialization, firstName, lastName) OUTPUT INSERTED.id VALUES (@userId, @spec, @firstName, @lastName)";
                        using (SqlCommand cmdMaster = new SqlCommand(masterQuery, connection, transaction))
                        {
                            cmdMaster.Parameters.AddWithValue("@userId", newUserId);
                            cmdMaster.Parameters.AddWithValue("@spec", spec);
                            cmdMaster.Parameters.AddWithValue("@firstName", firstName);
                            cmdMaster.Parameters.AddWithValue("@lastName", lastName);

                            newMasterId = (int)cmdMaster.ExecuteScalar();
                        }

                        foreach (DataRowView rowView in clbMasterServices.CheckedItems)
                        {
                            int serviceId = Convert.ToInt32(rowView["id"]);

                            string linkQuery = "INSERT INTO MasterServices (masterId, serviceId) VALUES (@masterId, @serviceId)";
                            using (SqlCommand cmdLink = new SqlCommand(linkQuery, connection, transaction))
                            {
                                cmdLink.Parameters.AddWithValue("@masterId", newMasterId);
                                cmdLink.Parameters.AddWithValue("@serviceId", serviceId);
                                cmdLink.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Майстра та його обліковий запис успішно створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearMasterInputs();
                        LoadMasters();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Помилка створення майстра: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdateMaster_Click(object sender, EventArgs e)
        {
            if (selectedMasterId == -1 || selectedUserId == -1)
            {
                MessageBox.Show("Оберіть майстра з таблиці для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = txtMasterFirstName.Text.Trim();
            string lastName = txtMasterLastName.Text.Trim();
            string spec = cmbMasterSpecialization.Text.Trim();
            string login = txtMasterLogin.Text.Trim();
            string newPassword = txtMasterPassword.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(spec) || string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Заповніть обов'язкові поля!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            string hashedPassword = HashPassword(newPassword);
                            string userQuery = "UPDATE Users SET login = @login, passwordHash = @password WHERE id = @userId";
                            using (SqlCommand cmdUser = new SqlCommand(userQuery, conn, transaction))
                            {
                                cmdUser.Parameters.AddWithValue("@login", login);
                                cmdUser.Parameters.AddWithValue("@password", hashedPassword);
                                cmdUser.Parameters.AddWithValue("@userId", selectedUserId);
                                cmdUser.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string userQuery = "UPDATE Users SET login = @login WHERE id = @userId";
                            using (SqlCommand cmdUser = new SqlCommand(userQuery, conn, transaction))
                            {
                                cmdUser.Parameters.AddWithValue("@login", login);
                                cmdUser.Parameters.AddWithValue("@userId", selectedUserId);
                                cmdUser.ExecuteNonQuery();
                            }
                        }

                        string masterQuery = "UPDATE Masters SET specialization = @spec, firstName = @firstName, lastName = @lastName WHERE id = @id";
                        using (SqlCommand cmdMaster = new SqlCommand(masterQuery, conn, transaction))
                        {
                            cmdMaster.Parameters.AddWithValue("@spec", spec);
                            cmdMaster.Parameters.AddWithValue("@firstName", firstName);
                            cmdMaster.Parameters.AddWithValue("@lastName", lastName);
                            cmdMaster.Parameters.AddWithValue("@id", selectedMasterId);
                            cmdMaster.ExecuteNonQuery();
                        }

                        string deleteLinksQuery = "DELETE FROM MasterServices WHERE masterId = @masterId";
                        using (SqlCommand cmdDel = new SqlCommand(deleteLinksQuery, conn, transaction))
                        {
                            cmdDel.Parameters.AddWithValue("@masterId", selectedMasterId);
                            cmdDel.ExecuteNonQuery();
                        }

                        foreach (DataRowView rowView in clbMasterServices.CheckedItems)
                        {
                            int serviceId = Convert.ToInt32(rowView["id"]);

                            string linkQuery = "INSERT INTO MasterServices (masterId, serviceId) VALUES (@masterId, @serviceId)";
                            using (SqlCommand cmdLink = new SqlCommand(linkQuery, conn, transaction))
                            {
                                cmdLink.Parameters.AddWithValue("@masterId", selectedMasterId);
                                cmdLink.Parameters.AddWithValue("@serviceId", serviceId);
                                cmdLink.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Дані успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearMasterInputs();
                        LoadMasters();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Помилка оновлення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteMaster_Click(object sender, EventArgs e)
        {
            if (selectedMasterId == -1 || selectedUserId == -1)
            {
                MessageBox.Show("Оберіть майстра для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Ви впевнені, що хочете видалити цього майстра та його обліковий запис?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // видаляємо майстра з Masters
                            string delMaster = "DELETE FROM Masters WHERE id = @masterId";
                            using (SqlCommand cmd1 = new SqlCommand(delMaster, connection, transaction))
                            {
                                cmd1.Parameters.AddWithValue("@masterId", selectedMasterId);
                                cmd1.ExecuteNonQuery();
                            }

                            // видаляємо запис із Users
                            string delUser = "DELETE FROM Users WHERE id = @userId";
                            using (SqlCommand cmd2 = new SqlCommand(delUser, connection, transaction))
                            {
                                cmd2.Parameters.AddWithValue("@userId", selectedUserId);
                                cmd2.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Майстра та його обліковий запис успішно видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearMasterInputs();
                            LoadMasters();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Помилка видалення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dgvMasters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMasters.Rows[e.RowIndex];
                selectedMasterId = Convert.ToInt32(row.Cells["id"].Value);
                selectedUserId = Convert.ToInt32(row.Cells["userId"].Value);

                cmbMasterSpecialization.SelectedItem = row.Cells["specialization"].Value.ToString();
                txtMasterFirstName.Text = row.Cells["firstName"].Value.ToString();
                txtMasterLastName.Text = row.Cells["lastName"].Value.ToString();

                txtMasterLogin.Text = row.Cells["login"].Value.ToString();

                // знімаємо всі галочки у списку послуг
                for (int i = 0; i < clbMasterServices.Items.Count; i++)
                {
                    clbMasterServices.SetItemChecked(i, false);
                }

                // витягуємо з бази послуги, які належать цьому майстру
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT serviceId FROM MasterServices WHERE masterId = @masterId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@masterId", selectedMasterId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<int> assignedServices = new List<int>();
                            while (reader.Read())
                            {
                                assignedServices.Add(Convert.ToInt32(reader["serviceId"]));
                            }

                            // Ставимо галочки тим послугам, які є у майстра
                            for (int i = 0; i < clbMasterServices.Items.Count; i++)
                            {
                                DataRowView rowView = (DataRowView)clbMasterServices.Items[i];
                                int serviceId = Convert.ToInt32(rowView["id"]);

                                if (assignedServices.Contains(serviceId))
                                {
                                    clbMasterServices.SetItemChecked(i, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ClearMasterInputs()
        {
            cmbMasterSpecialization.SelectedIndex = -1;
            txtMasterFirstName.Clear();
            txtMasterLastName.Clear();
            txtMasterLogin.Clear();
            txtMasterPassword.Clear();
            selectedMasterId = -1;
            selectedUserId = -1;

            for (int i = 0; i < clbMasterServices.Items.Count; i++)
            {
                clbMasterServices.SetItemChecked(i, false);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Hairdressers
{
    public partial class AdminMainForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";
        private int selectedServiceId = -1;
        private int selectedClientId = -1;

        public AdminMainForm()
        {
            InitializeComponent();
            LoadServices();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
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
            //else if (tcAdmin.SelectedTab == tpMasters)
            //{
            //    // LoadMasters(); 
            //}
        }

        private void OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}

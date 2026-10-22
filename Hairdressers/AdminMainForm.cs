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

namespace Hairdressers
{
    public partial class AdminMainForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";
        private int selectedServiceId = -1;

        public AdminMainForm()
        {
            InitializeComponent();
            LoadServices();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

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
            if (e.RowIndex >= 0)
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
    }
}

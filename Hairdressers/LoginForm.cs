using HairdressersApp;
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

namespace Hairdressers
{
    public partial class LoginForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chorobish\source\repos\izvp\Hairdressers\Hairdressers\HairdressersDB.mdf;Integrated Security=True";
        private bool isStaffMode = true;

        public LoginForm()
        {
            InitializeComponent();
            SetupFormForStaff();
        }

        private void btnModeClient_Click(object sender, EventArgs e)
        {
            isStaffMode = false;
            lblTitle.Text = "Вхід / Реєстрація клієнта";
            panelStaff.Visible = false;
            panelClient.Visible = true;
        }

        private void btnModeStaff_Click(object sender, EventArgs e)
        {
            isStaffMode = true;
            lblTitle.Text = "Вхід для Персоналу";
            panelStaff.Visible = true;
            panelClient.Visible = false;
        }

        private void SetupFormForStaff()
        {
            panelStaff.Visible = true;
            panelClient.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (isStaffMode)
            {
                LoginStaff();
            }
            else
            {
                LoginOrRegisterClient();
            }
        }

        private void LoginStaff()
        {
            string login = txtStaffLogin.Text.Trim();
            string password = txtStaffPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passwordHash = HashPassword(password);

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT u.roleId, m.id AS masterId, m.firstName, m.lastName 
                        FROM Users u 
                        LEFT JOIN Masters m ON u.id = m.userId
                        WHERE u.login = @login AND u.passwordHash = @passwordHash";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roleId = Convert.ToInt32(reader["roleId"]);
                                MessageBox.Show("Успішний вхід!", "Вітаємо", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (roleId == 1)
                                {
                                    LoadClientMainForm(0, "Admin", "Admin");
                                }
                                else if (roleId == 2)
                                {
                                    int masterId = reader["masterId"] != DBNull.Value ? Convert.ToInt32(reader["masterId"]) : 0;
                                    string firstName = reader["firstName"] != DBNull.Value ? reader["firstName"].ToString() : "Майстер";
                                    string lastName = reader["lastName"] != DBNull.Value ? reader["lastName"].ToString() : "";
                                    string masterFullName = $"{firstName} {lastName}".Trim();

                                    LoadClientMainForm(masterId, masterFullName, "Master");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Невірний логін або пароль!", "Помилка входу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка підключення до БД: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoginOrRegisterClient()
        {
            string phone = txtClientPhone.Text.Trim();
            string lastName = txtClientLastName.Text.Trim();
            string firstName = txtClientFirstName.Text.Trim();

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Будь ласка, заповніть номер телефону, ім'я та прізвище!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT id, firstName, lastName FROM Clients WHERE phoneNumber = @phone";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            string roleName = "Client";
                            if (reader.Read())
                            {
                                // клієнт існує — звіряємо ім'я та прізвище для безпеки
                                string dbLastName = reader["lastName"].ToString();
                                string dbFirstName = reader["firstName"].ToString();
                                int clientId = Convert.ToInt32(reader["id"]);

                                bool isLastNameMatch = dbLastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
                                bool isFirstNameMatch = dbFirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase);

                                if (isLastNameMatch && isFirstNameMatch)
                                {
                                    MessageBox.Show($"Вітаємо з поверненням, {dbFirstName}!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    LoadClientMainForm(clientId, dbFirstName, roleName);
                                }
                                else
                                {
                                    MessageBox.Show("Введені дані не збігаються з власником цього номера телефону!", "Помилка безпеки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                // клієнта немає — реєструємо нового за всіма трьома параметрами
                                reader.Close();

                                string insertQuery = "INSERT INTO Clients (firstName, lastName, phoneNumber) VALUES (@firstName, @lastName, @phone)";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                                {
                                    insertCmd.Parameters.AddWithValue("@firstName", firstName);
                                    insertCmd.Parameters.AddWithValue("@lastName", lastName);
                                    insertCmd.Parameters.AddWithValue("@phone", phone);

                                    object newIdObj = insertCmd.ExecuteScalar();

                                    int clientId = 0;
                                    if (newIdObj != null && newIdObj != DBNull.Value)
                                    {
                                        clientId = Convert.ToInt32(newIdObj);
                                    }

                                    insertCmd.ExecuteNonQuery();

                                    MessageBox.Show("Реєстрація успішна! Ласкаво просимо.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    LoadClientMainForm(clientId, firstName, roleName);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка бази даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private void LoadClientMainForm(int id, string name, string role)
        {
            Form targetForm = null;

            switch (role)
            {
                case "Client":
                    targetForm = new ClientMainForm();
                    break;
                case "Master":
                    targetForm = new MasterMainForm(id, name);
                    break;
                case "Admin":
                    targetForm = new AdminMainForm();
                    break;
            }

            if (targetForm != null)
            {
                targetForm.FormClosed += (s, args) =>
                {
                    this.Show();
                };

                targetForm.Show();
                this.Hide();
            }
        }
    }
}
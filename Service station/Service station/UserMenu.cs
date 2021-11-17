using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service_station
{
    public partial class UserMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        SqlDataAdapter adapter = null;
        DataTable table = null;

        string id, userLogin;

        public UserMenu(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void личнаяИнформацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(id);
            personalInformation.ShowDialog();
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string password;

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT Password FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                password = cmd.ExecuteScalar().ToString();
            }

            Form passwordChange = new PasswordChange(password, id);
            passwordChange.ShowDialog();

            connection.Close();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Станция технического обслуживания\nРазработчик: Карчевский Илья\nНомер группы: 38ТП", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void отправитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form sendOrder = new SendOrder(id);
            sendOrder.ShowDialog();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form cancelOrder = new CancelOrder(userLogin);
            cancelOrder.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            if (checkBox1.Checked)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE UserLogin=@UserLogin AND IsItCompleted=@IsItCompleted; ", connection))
                {
                    cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                    cmd.Parameters.AddWithValue("@IsItCompleted", "0");
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE UserLogin=@UserLogin; ", connection))
                {
                    cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }

            connection.Close();
        }

        private void UserMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox1.Text);
            infromationAboutOrder.ShowDialog();
        }

        private void UserMenu_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Login FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                userLogin = cmd.ExecuteScalar().ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE UserLogin=@UserLogin; ", connection))
            {
                cmd.Parameters.AddWithValue("@UserLogin", userLogin);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            connection.Close();
        }
    }
}

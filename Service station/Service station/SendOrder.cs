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

namespace Service_station
{
    public partial class SendOrder : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        string userLogin;

        public SendOrder(string id)
        {
            InitializeComponent();

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> serviceName = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT Name FROM Services", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					serviceName.Add(reader.GetString(0));
				}
				reader.Close();
			}

			foreach (var item in serviceName)
			{
				comboBox1.Items.Add(item);
			}

			using (SqlCommand cmd = new SqlCommand("SELECT Login FROM Users WHERE id = @id", connection))
			{
				cmd.Parameters.AddWithValue("@id", id);
				userLogin = cmd.ExecuteScalar().ToString();
			}

			connection.Close();
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			string price;

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			using (SqlCommand cmd = new SqlCommand("SELECT Price FROM Services WHERE Name = @Name", connection))
			{
				cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
				price = cmd.ExecuteScalar().ToString();
			}

			textBox1.Text = price;

			connection.Close();
		}

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (comboBox1.Text == "" || richTextBox1.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка отправления заявки", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Applications] (UserLogin, Service, Description, Price, SparePartsPrice, IsItAccepted, IsItCompleted) VALUES (@UserLogin, @Service, @Description, @Price, @SparePartsPrice, @IsItAccepted, @IsItCompleted)", connection))
					{
						cmd1.Parameters.AddWithValue("@UserLogin", userLogin);
						cmd1.Parameters.AddWithValue("@Service", comboBox1.Text);
						cmd1.Parameters.AddWithValue("@Description", richTextBox1.Text);
						cmd1.Parameters.AddWithValue("@Price", textBox1.Text);
						cmd1.Parameters.AddWithValue("@SparePartsPrice", "0");
						cmd1.Parameters.AddWithValue("@IsItAccepted", "0");
						cmd1.Parameters.AddWithValue("@IsItCompleted", "0");

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное отправление заявки!", "Заявка отправлена", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка отправления заявки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
					Close();
				}
			}
		}
    }
}

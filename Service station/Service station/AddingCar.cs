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
    public partial class AddingCar : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

		string id;

        public AddingCar(string id)
        {
            InitializeComponent();
			this.id = id;

			comboBox1.Items.Add("Автоматическая");
			comboBox1.Items.Add("Механическая");

			comboBox2.Items.Add("Бензин");
			comboBox2.Items.Add("Дизель");
			comboBox2.Items.Add("Газ");
		}

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || textBox3.Text == "" || comboBox2.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления запчасти", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Cars] (UserID, Name, Year, Box, EngineVolume, Fuel) VALUES (@UserID, @Name, @Year, @Box, @EngineVolume, @Fuel)", connection))
					{
						cmd1.Parameters.AddWithValue("@UserID", id);
						cmd1.Parameters.AddWithValue("@Name", textBox1.Text);
						cmd1.Parameters.AddWithValue("@Year", textBox2.Text);
						cmd1.Parameters.AddWithValue("@Box", comboBox1.Text);
						cmd1.Parameters.AddWithValue("@EngineVolume", textBox3.Text);
						cmd1.Parameters.AddWithValue("@Fuel", comboBox2.Text);

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление автомобиля!", "Автомобиль добавлен", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления автомобиля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

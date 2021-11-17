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
    public partial class OrderProcessing : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        List<String> addedSpareParts = new List<String>();
        int sparePartsPrice = 0;

        public OrderProcessing()
        {
            InitializeComponent();

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> idApplication = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM Applications WHERE IsItAccepted=@IsItAccepted", connection))
			{
				cmd.Parameters.AddWithValue("@IsItAccepted", "1");
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					idApplication.Add(Convert.ToString(reader.GetInt32(0)));
				}
				reader.Close();
			}

			foreach (var item in idApplication)
			{
				comboBox1.Items.Add(item);
			}

			List<String> spareParts = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT Name FROM SpareParts", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					spareParts.Add(reader.GetString(0));
				}
				reader.Close();
			}

			foreach (var item in spareParts)
			{
				comboBox2.Items.Add(item);
			}

			connection.Close();
		}

        private void button2_Click(object sender, EventArgs e)
        {
			Form infromationAboutOrder = new InfromationAboutOrder(comboBox1.Text);
			infromationAboutOrder.ShowDialog();
		}

        private void button3_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			try
			{
				using (SqlCommand cmd = new SqlCommand("Update Applications Set IsItAccepted = @IsItAccepted Where id = @id", connection))
				{
					cmd.Parameters.AddWithValue("@IsItAccepted", "2");
					cmd.Parameters.AddWithValue("@id", comboBox1.Text);
					cmd.ExecuteNonQuery();
				}

				int price;

				using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM Applications WHERE id = @id", connection))
				{
					cmd.Parameters.AddWithValue("@id", comboBox1.Text);
					price = Convert.ToInt32(cmd.ExecuteScalar().ToString());
				}

				using (SqlCommand cmd = new SqlCommand("Update Applications Set Price = @Price Where id = @id", connection))
				{
					cmd.Parameters.AddWithValue("@Price", Convert.ToString(sparePartsPrice + price));
					cmd.Parameters.AddWithValue("@id", comboBox1.Text);
					cmd.ExecuteNonQuery();
				}

				using (SqlCommand cmd = new SqlCommand("Update Applications Set SparePartsPrice = @SparePartsPrice Where id = @id", connection))
				{
					cmd.Parameters.AddWithValue("@SparePartsPrice", Convert.ToString(sparePartsPrice));
					cmd.Parameters.AddWithValue("@id", comboBox1.Text);
					cmd.ExecuteNonQuery();
				}

				foreach (var item in addedSpareParts)
				{
					int numberOfUses;

					using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 NumberOfUses FROM SpareParts WHERE Name = @Name", connection))
					{
						cmd.Parameters.AddWithValue("@Name", item);
						numberOfUses = Convert.ToInt32(cmd.ExecuteScalar().ToString());
					}

					using (SqlCommand cmd = new SqlCommand("Update SpareParts Set NumberOfUses = @NumberOfUses Where Name = @Name", connection))
					{
						cmd.Parameters.AddWithValue("@NumberOfUses", Convert.ToString(++numberOfUses));
						cmd.Parameters.AddWithValue("@Name", item);
						cmd.ExecuteNonQuery();
					}
				}

				MessageBox.Show("Успешная обработка заказа!", "Вы начали выполнять заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch
			{
				MessageBox.Show("Ошибка обработки заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				connection.Close();
				Close();
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			string price;

			if (comboBox2.Text == "")
			{
				MessageBox.Show("Вы не выбрали запчасть!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				SqlConnection connection = new SqlConnection(sql);
				connection.Open();

				using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM SpareParts WHERE Name = @Name", connection))
				{
					cmd.Parameters.AddWithValue("@Name", comboBox2.Text);
					price = cmd.ExecuteScalar().ToString();
				}

				connection.Close();

				sparePartsPrice = sparePartsPrice + Convert.ToInt32(price);
				addedSpareParts.Add(comboBox2.Text);

				richTextBox1.Text = richTextBox1.Text + comboBox2.Text + "\n";

				label5.Text = "Цена запчастей: " + sparePartsPrice;
			}
		}

        private void button4_Click(object sender, EventArgs e)
        {
			addedSpareParts.Clear();
			richTextBox1.Text = "";
			sparePartsPrice = 0;
			label5.Text = "Цена запчастей: " + sparePartsPrice;
		}
    }
}

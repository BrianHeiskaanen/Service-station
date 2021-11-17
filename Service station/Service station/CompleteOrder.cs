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
    public partial class CompleteOrder : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        public CompleteOrder()
        {
            InitializeComponent();

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> idApplication = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM Applications WHERE IsItAccepted=2 AND IsItCompleted=0", connection))
			{
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
				using (SqlCommand cmd = new SqlCommand("Update Applications Set IsItCompleted = @IsItCompleted Where id = @id", connection))
				{
					cmd.Parameters.AddWithValue("@IsItCompleted", "1");
					cmd.Parameters.AddWithValue("@id", comboBox1.Text);
					cmd.ExecuteNonQuery();
				}

				MessageBox.Show("Успешное завершение заказа!", "Вы закончили заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch
			{
				MessageBox.Show("Ошибка завершения заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				connection.Close();
				Close();
			}
		}
    }
}

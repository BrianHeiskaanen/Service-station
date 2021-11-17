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
    public partial class ClientsCard : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        SqlDataAdapter adapter = null;
        DataTable table = null;

        public ClientsCard()
        {
            InitializeComponent();

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> userLogin = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT Login FROM Users", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					userLogin.Add(reader.GetString(0));
				}
				reader.Close();
			}

			foreach (var item in userLogin)
			{
				comboBox1.Items.Add(item);
			}

			connection.Close();
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE UserLogin=@UserLogin; ", connection))
            {
                cmd.Parameters.AddWithValue("@UserLogin", comboBox1.Text);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox1.Text);
            infromationAboutOrder.ShowDialog();
        }
    }
}

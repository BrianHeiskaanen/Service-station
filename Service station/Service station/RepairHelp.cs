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
    public partial class RepairHelp : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        SqlDataAdapter adapter = null;
        DataTable table = null;

        public RepairHelp()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            List<String> idApplications = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM Applications", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idApplications.Add(Convert.ToString(reader.GetInt32(0)));
                }
                reader.Close();
            }

            foreach (var item in idApplications)
            {
                comboBox1.Items.Add(item);
            }


            List<String> spareParts = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT Name FROM SpareParts WHERE NumberOfUses!=@NumberOfUses", connection))
            {
                cmd.Parameters.AddWithValue("@NumberOfUses", "0");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    spareParts.Add(reader.GetString(0));
                }
                reader.Close();
            }

            richTextBox1.Text = String.Join("\n", spareParts);

            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE id=@id; ", connection))
            {
                cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            List<String> price = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT Price FROM Applications WHERE id=@id", connection))
            {
                cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    price.Add(reader.GetString(0));
                }
                reader.Close();
            }

            List<String> sparePartsPrice = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT SparePartsPrice FROM Applications WHERE id=@id", connection))
            {
                cmd.Parameters.AddWithValue("@id", comboBox1.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sparePartsPrice.Add(reader.GetString(0));
                }
                reader.Close();
            }

            List<int> intPrice = price.ConvertAll(int.Parse);
            int priceSum = intPrice.ToArray().Sum();
            label2.Text = "Стоимость заказа: " + priceSum;

            List<int> intSparePartsPrice = sparePartsPrice.ConvertAll(int.Parse);
            int sparePartsPriceSum = intSparePartsPrice.ToArray().Sum();
            label3.Text = "Стоимость запчастей: " + sparePartsPriceSum;

            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            List<String> price = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT Price FROM Applications", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    price.Add(reader.GetString(0));
                }
                reader.Close();
            }

            List<String> sparePartsPrice = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT SparePartsPrice FROM Applications", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sparePartsPrice.Add(reader.GetString(0));
                }
                reader.Close();
            }

            List<int> intPrice = price.ConvertAll(int.Parse);
            int priceSum = intPrice.ToArray().Sum();
            label2.Text = "Общая стоимость заказов: " + priceSum;

            List<int> intSparePartsPrice = sparePartsPrice.ConvertAll(int.Parse);
            int sparePartsPriceSum = intSparePartsPrice.ToArray().Sum();
            label3.Text = "Общая стоимость запчастей: " + sparePartsPriceSum;

            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox1.Text);
            infromationAboutOrder.ShowDialog();
        }
    }
}

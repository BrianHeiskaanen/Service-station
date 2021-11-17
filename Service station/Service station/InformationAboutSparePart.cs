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
    public partial class InformationAboutSparePart : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        string id, name, price, numberOfUses;

        public InformationAboutSparePart(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void InformationAboutSparePart_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM SpareParts WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    name = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM SpareParts WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    price = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 NumberOfUses FROM SpareParts WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    numberOfUses = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = name;
                textBox2.Text = price;
                textBox3.Text = numberOfUses;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

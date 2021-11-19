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
    public partial class InformationAboutCars : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        string id, name, year, box, engineVolume, fuel;

        private void InformationAboutCars_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Cars WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    name = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Year FROM Cars WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    year = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Box FROM Cars WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    box = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 EngineVolume FROM Cars WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    engineVolume = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Fuel FROM Cars WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    fuel = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = name;
                textBox2.Text = year;
                textBox3.Text = box;
                textBox4.Text = engineVolume;
                textBox6.Text = fuel;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public InformationAboutCars(string id)
        {
            InitializeComponent();
            this.id = id;
        }
    }
}

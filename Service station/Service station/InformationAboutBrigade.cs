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
    public partial class InformationAboutBrigade : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        string id, name, description;

        public InformationAboutBrigade(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void InformationAboutBrigade_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Brigades WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    name = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Description FROM Brigades WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    description = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = name;
                richTextBox1.Text = description;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

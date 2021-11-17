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
    public partial class AddingBrigade : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        public AddingBrigade()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (textBox1.Text == "" || richTextBox1.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления бригады", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Brigades] (Name, Description) VALUES (@Name, @Description)", connection))
					{
						cmd1.Parameters.AddWithValue("@Name", textBox1.Text);
						cmd1.Parameters.AddWithValue("@Description", richTextBox1.Text);

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление бригады!", "Бригада добавлена", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления бригады!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

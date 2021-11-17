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
    public partial class AddingSparePart : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        public AddingSparePart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (textBox1.Text == "" || textBox2.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления запчасти", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [SpareParts] (Name, Price, NumberOfUses) VALUES (@Name, @Price, @NumberOfUses)", connection))
					{
						cmd1.Parameters.AddWithValue("@Name", textBox1.Text);
						cmd1.Parameters.AddWithValue("@Price", textBox2.Text);
						cmd1.Parameters.AddWithValue("@NumberOfUses", "0");

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление запчасти!", "Запчасть добавлена", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления запчасти!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

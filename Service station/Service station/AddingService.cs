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
    public partial class AddingService : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        public AddingService()
        {
            InitializeComponent();
        }

		private void button1_Click(object sender, EventArgs e)
		{
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (textBox1.Text == "" || richTextBox1.Text == "" || textBox2.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления услуги", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Services] (Name, Description, Price) VALUES (@Name, @Description, @Price)", connection))
					{
						cmd1.Parameters.AddWithValue("@Name", textBox1.Text);
						cmd1.Parameters.AddWithValue("@Description", richTextBox1.Text);
						cmd1.Parameters.AddWithValue("@Price", textBox2.Text);

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление услуги!", "Услуга добавлена", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

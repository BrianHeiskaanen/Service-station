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
    public partial class AdminMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Service station\Service station\ServiceStation.mdf;Integrated Security=True";

        SqlDataAdapter adapter, adapter1, adapter2, adapter3, adapter4 = null;
        DataTable table, table1, table2, table3, table4 = null;

        public AdminMenu()
        {
            InitializeComponent();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Brigades; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                table1 = new DataTable();
                adapter1.Fill(table1);
                dataGridView2.DataSource = table1;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView3.DataSource = table2;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM SpareParts; ", connection))
            {
                adapter3 = new SqlDataAdapter(cmd);
                table3 = new DataTable();
                adapter3.Fill(table3);
                dataGridView4.DataSource = table3;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
            {
                adapter4 = new SqlDataAdapter(cmd);
                table4 = new DataTable();
                adapter4.Fill(table4);
                dataGridView5.DataSource = table4;
            }

            connection.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(textBox4.Text);
            personalInformation.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form informationAboutBrigade = new InformationAboutBrigade(textBox1.Text);
            informationAboutBrigade.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form informationAboutService = new InformationAboutService(textBox2.Text);
            informationAboutService.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form informationAboutSparePart = new InformationAboutSparePart(textBox3.Text);
            informationAboutSparePart.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form infromationAboutOrder = new InfromationAboutOrder(textBox5.Text);
            infromationAboutOrder.ShowDialog();
        }

        private void карточкаКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form client_sCard = new ClientsCard();
            client_sCard.ShowDialog();
        }

        private void карточкаБригадыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form brigadesCard = new BrigadesCard();
            brigadesCard.ShowDialog();
        }

        private void справкаРемонтовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form repairHelp = new RepairHelp();
            repairHelp.ShowDialog();
        }

        private void принятьИлиОтменитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form acceptOrder = new AcceptOrder();
            acceptOrder.ShowDialog();
        }

        private void начатьВыполнениеЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form orderProcessing = new OrderProcessing();
            orderProcessing.ShowDialog();
        }

        private void закончитьВыполнениеЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form completeOrder = new CompleteOrder();
            completeOrder.ShowDialog();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            if (checkBox1.Checked)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE IsItCompleted=@IsItCompleted; ", connection))
                {
                    cmd.Parameters.AddWithValue("@IsItCompleted", "0");
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView5.DataSource = table;
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
                {
                    adapter = new SqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView5.DataSource = table;
                }
            }

            connection.Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Станция технического обслуживания\nРазработчик: Карчевский Илья\nНомер группы: 38ТП", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Brigades; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                table1 = new DataTable();
                adapter1.Fill(table1);
                dataGridView2.DataSource = table1;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView3.DataSource = table2;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM SpareParts; ", connection))
            {
                adapter3 = new SqlDataAdapter(cmd);
                table3 = new DataTable();
                adapter3.Fill(table3);
                dataGridView4.DataSource = table3;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications; ", connection))
            {
                adapter4 = new SqlDataAdapter(cmd);
                table4 = new DataTable();
                adapter4.Fill(table4);
                dataGridView5.DataSource = table4;
            }

            connection.Close();
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form addingService = new AddingService();
            addingService.ShowDialog();
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form deleteService = new DeleteService();
            deleteService.ShowDialog();
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form addingSparePart = new AddingSparePart();
            addingSparePart.ShowDialog();
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form deleteSpareParts = new DeleteSpareParts();
            deleteSpareParts.ShowDialog();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addingBrigade = new AddingBrigade();
            addingBrigade.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteBrigade = new DeleteBrigade();
            deleteBrigade.ShowDialog();
        }
    }
}

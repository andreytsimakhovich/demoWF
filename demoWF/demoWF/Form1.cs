using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace demoWF
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\demoWF\demoWF\demoWF\demoWF\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [users]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["firstName"]) + " " + Convert.ToString(sqlReader["lastName"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();

            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;

            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text)&& !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [users] (firstName, lastName) VALUES (@firstName, @lastName)", sqlConnection);

                command.Parameters.AddWithValue("firstName", textBox2.Text);
                command.Parameters.AddWithValue("lastname", textBox1.Text);

                await command.ExecuteNonQueryAsync();

                textBox1.Clear();
                textBox2.Clear();

                label7.Visible = true;
                label7.Text = "Пользователь добавлен в базу!";

            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля 'Имя' и 'Фамилия' обязательны к заполнению!";
            }
            
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [users]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["firstName"]) + " " + Convert.ToString(sqlReader["lastName"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if (label9.Visible)
                label9.Visible = false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [users] SET [firstName]=@firstName, [lastName]=@lastname WHERE [Id]=@Id",sqlConnection);

                command.Parameters.AddWithValue("firstName", textBox3.Text);
                command.Parameters.AddWithValue("lastName", textBox4.Text);
                command.Parameters.AddWithValue("Id", textBox5.Text);

                await command.ExecuteNonQueryAsync();

                label9.Visible = true;
                label9.Text = "Данные обновлены!";

                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                label8.Visible = true;
                label8.Text = "Поля 'ID', 'Имя' и 'Фамилия' должны быть заполнены!";
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Поле 'ID' должно быть заполнено!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label12.Visible)
                label12.Visible = false;

            if(!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {

                SqlCommand command = new SqlCommand("DELETE FROM [users] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();

                label11.Visible = true;
                label11.Text = "Пользователь удален!";

                textBox6.Clear();
            }
            else
            {
                label12.Visible = true;
                label12.Text = "Заполните поле 'ID'!";
            }

        }

        private async void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [users]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["firstName"]) + " " + Convert.ToString(sqlReader["lastName"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

    }
}

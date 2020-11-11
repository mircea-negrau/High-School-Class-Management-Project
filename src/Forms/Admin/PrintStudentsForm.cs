using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Catalog
{
    public partial class PrintStudentsForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        Student student = new Student();

        public PrintStudentsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void PrintStudentsForm_Load(object sender, EventArgs e)
        {
            fillGrid(new MySqlCommand("SELECT * FROM `student`"));

            if(radioButtonNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        public void fillGrid(MySqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);

            picCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand command;
            string query;
            if (radioButtonYes.Checked)
            {
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN'" + date1 + "' AND '" + date2 + "' AND gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN'" + date1 + "' AND '" + date2 + "' AND gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM `student` WHERE `birthdate` BETWEEN'" + date1 + "' AND '" + date2 + "'";
                }
                command = new MySqlCommand(query);
                fillGrid(command);
            }
            else
            {
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM `student`";
                }
                command = new MySqlCommand(query);
                fillGrid(command);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +@"\students_list.txt";
            
            using (var writer = new StreamWriter(path))
            {
                if(!File.Exists(path))
                {
                    File.Create(path);
                }

                DateTime bdate;

                for(int i=0;i<dataGridView1.Rows.Count;i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count-1; j++)
                    {
                        if (j == 4)
                        {
                            bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            writer.Write("\t" + bdate.ToString("yyyy-MM-dd") + "\t" + "|");
                        }
                        else if(j==dataGridView1.Columns.Count - 2)
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        else
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                        }
                    }
                    writer.WriteLine("");
                    writer.Write("--------------------------------------------------------------------------------------------------------------");
                    writer.WriteLine("");
                }
                writer.Close();
                MessageBox.Show("Baza de date exportată pe desktop");
            }
        }

        private void buttonExit1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
            this.Close();
        }

        bool mouseDown;
        private Point offset;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        //-------------------------------------------------
        //EXIT BUTTONS

        private void buttonExit1_MouseEnter(object sender, EventArgs e)
        {

            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow2));
        }

        private void buttonExit1_MouseLeave(object sender, EventArgs e)
        {

            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow1));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Catalog
{
    public partial class studentsListForm : Form
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

        public studentsListForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        Student student = new Student();


        private void studentsListForm_Load(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` ORDER BY `last_name`");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStdF = new UpdateDeleteStudentForm();
            updateDeleteStdF.textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStdF.textBoxPrenume.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStdF.textBoxNume.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStdF.comboBoxClasa.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            updateDeleteStdF.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;

            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "Female")
            {
                updateDeleteStdF.radioButtonFemale.Checked = true;
            }


            updateDeleteStdF.textBoxTelefon.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            updateDeleteStdF.textBoxAdresa.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStdF.pictureBoxStudentImage.Image = Image.FromStream(picture);
            updateDeleteStdF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` ORDER BY last_name");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonExit1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
            this.Close();
        }

        //Move using the panel
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

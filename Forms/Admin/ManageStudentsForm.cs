using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Catalog
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        Student student = new Student();

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

        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            fillGrid(new MySqlCommand("SELECT * FROM `student` ORDER BY `last_name`"));
            negru = buttonSearch.BackColor;
            albastru = buttonSearch.ForeColor;

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

            labelTotalStudents.Text = "Total studenți: " + dataGridView1.Rows.Count;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxPrenume.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxNume.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBoxClasa.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;
            if(dataGridView1.CurrentRow.Cells[5].Value.ToString()=="Female")
            {
                radioButtonFemale.Checked = true;
            }
            else
            {
                radioButtonMale.Checked = true;
            }
            textBoxTelefon.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBoxAdresa.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBoxStudentImage.Image = Image.FromStream(picture);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxPrenume.Text = "Prenume"; ;
            textBoxNume.Text = "Nume";
            comboBoxClasa.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            radioButtonMale.Checked = true;
            textBoxTelefon.Text = "Telefon";
            textBoxAdresa.Text = "Adresă";
            pictureBoxStudentImage.Image = null;
        }

        private void buttonDownloadImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = "Student_" + textBoxID.Text;
            if(pictureBoxStudentImage.Image==null)
            {
                MessageBox.Show("Nu există poză");
            }
            else if(svf.ShowDialog()==DialogResult.OK)
            {
                pictureBoxStudentImage.Image.Save(svf.FileName + ("." +ImageFormat.Jpeg.ToString()));
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM `student` WHERE CONCAT(`first_name`,`last_name`,`address`,`clasa`) LIKE'%" + textBoxSearch.Text + "%' ORDER BY `last_name`";
            MySqlCommand command = new MySqlCommand(query);
            fillGrid(command);
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            string fname = textBoxPrenume.Text;
            string lname = textBoxNume.Text;
            string clasa = comboBoxClasa.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBoxTelefon.Text;
            string address = textBoxAdresa.Text;
            string gender = "Male";
            if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }
            MemoryStream pic = new MemoryStream();

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year < 10) || (this_year - born_year > 100))
            {
                MessageBox.Show("Vârstă invalidă", "Dată de naștere invalidă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);
                if (student.insertStudent(fname, lname, clasa, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("Student adăugat", "Adaugă student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(new MySqlCommand("SELECT * FROM `student` ORDER BY `last_name`"));
                }
                else
                {
                    MessageBox.Show("Eroare!", "Adaugă student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Există câmpuri necompletate!", "Adaugă student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                string fname = textBoxPrenume.Text;
                string lname = textBoxNume.Text;
                string clasa = comboBoxClasa.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textBoxTelefon.Text;
                string address = textBoxAdresa.Text;
                string gender = "Male";
                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }
                MemoryStream pic = new MemoryStream();

                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;
                if ((this_year - born_year < 10) || (this_year - born_year > 100))
                {
                    MessageBox.Show("Vârstă invalidă", "Dată de naștere invalidă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif())
                {
                    pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, clasa, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Detalii student editate", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new MySqlCommand("SELECT * FROM `student` ORDER BY `last_name`"));
                    }
                    else
                    {
                        MessageBox.Show("Eroare!", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Există câmpuri necompletate!", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Introduceți un ID valid", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                if (MessageBox.Show("Sunteți sigur că vreți să ștergeți studentul?", "Șterge student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student șters.", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxID.Text = "";
                        textBoxPrenume.Text = "";
                        textBoxNume.Text = "";
                        comboBoxClasa.Text = "";
                        textBoxTelefon.Text = "";
                        textBoxAdresa.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudentImage.Image = null;
                        fillGrid(new MySqlCommand("SELECT * FROM `student` ORDER BY `last_name`"));
                    }
                    else
                    {
                        MessageBox.Show("Studentul nu a fost șters.", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Introduceți un ID valid", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool verif()
        {
            if ((textBoxPrenume.Text.Trim() == "") ||
                (textBoxNume.Text.Trim() == "") ||
                (textBoxTelefon.Text.Trim() == "") ||
                (comboBoxClasa.Text.Trim() == "") ||
                (textBoxAdresa.Text.Trim() == "") ||
                (pictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
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

        Color negru;
        Color albastru;
        private void buttonSearch_MouseEnter(object sender, EventArgs e)
        {
            buttonSearch.ForeColor = negru;
            buttonSearch.BackColor = albastru;
        }

        private void buttonSearch_MouseLeave(object sender, EventArgs e)
        {
            buttonSearch.ForeColor = albastru;
            buttonSearch.BackColor = negru;
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

        private void buttonAddStudent_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Adaugă elev", buttonAddStudent);
        }

        private void buttonDelete_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Șterge elev", buttonDelete);
        }

        private void buttonReset_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Refresh pagină", buttonReset);
        }

        private void buttonEdit_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Editează elev", buttonEdit);
        }
    }
}

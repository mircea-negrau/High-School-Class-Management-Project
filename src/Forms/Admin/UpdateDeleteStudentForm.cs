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
    public partial class UpdateDeleteStudentForm : Form
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

        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        Student student = new Student();

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
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

        private void buttonEditStudent_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Vârsta trebuie să fie între 10 și 100", "Dată de naștere invalidă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif())
                {
                    pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, clasa, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Detalii student ediate", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Introduceți un ID valid", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void buttonRemove_Click(object sender, EventArgs e)
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
                        textBoxTelefon.Text = "";
                        comboBoxClasa.Text = "";
                        textBoxAdresa.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudentImage.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Studentul nu a fost șters.", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }catch
            {
                MessageBox.Show("Introduceți un ID valid", "Șterge student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `clasa`, `birthdate`, `gender`, `phone`, `address`, `picture` FROM `student` WHERE `id`=" + id);

                DataTable table = student.getStudents(command);

                if (table.Rows.Count > 0)
                {
                    textBoxPrenume.Text = table.Rows[0]["first_name"].ToString();
                    textBoxNume.Text = table.Rows[0]["last_name"].ToString();
                    comboBoxClasa.Text = table.Rows[0]["clasa"].ToString();
                    textBoxTelefon.Text = table.Rows[0]["phone"].ToString();
                    textBoxAdresa.Text = table.Rows[0]["address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["birthdate"];

                    if (table.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButtonFemale.Checked = true;
                    }
                    else
                    {
                        radioButtonMale.Checked = true;
                    }

                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBoxStudentImage.Image = Image.FromStream(picture);
                }
                else
                {
                    MessageBox.Show("Introduceți un ID valid", "ID invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxPrenume.Text = "Prenume";
                    textBoxNume.Text = "Nume";
                    textBoxTelefon.Text = "Telefon";
                    comboBoxClasa.Text = "";
                    textBoxAdresa.Text = "Adresă";
                    dateTimePicker1.Value = DateTime.Now;
                    pictureBoxStudentImage.Image = null;
                }
            }
            catch
            {
            }
        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

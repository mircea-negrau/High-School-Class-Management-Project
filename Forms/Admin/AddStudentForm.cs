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
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }
        bool verif()
        {
            if ((textBoxPrenume.Text.Trim() == "") ||
                (textBoxNume.Text.Trim() == "") ||
                (comboBoxClasa.Text.Trim() == "") ||
                (textBoxTelefon.Text.Trim() == "") ||
                (textBoxAdresa.Text.Trim() == "") ||
                (pictureBoxStudentImage.Image == null) || (!"12A12B11A11B10A10B9A9B8A8B7A7B6A6B5A5B4A4B3A3B2A2B1A1B".Contains(comboBoxClasa.Text)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #region Buttons
        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if(opf.ShowDialog()==DialogResult.OK)
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
            Decolorare();
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            Decolorare();
            Student student = new Student();
            #region Variables 
            string fname = textBoxPrenume.Text;
            string lname = textBoxNume.Text;
            string clasa = comboBoxClasa.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBoxTelefon.Text;
            string address = textBoxAdresa.Text;
            string gender = "Male";
            if(radioButtonFemale.Checked)
            {
                gender = "Female";
            }
            MemoryStream pic = new MemoryStream();

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            #endregion
            if((this_year-born_year<10)||(this_year-born_year>100))
            {
                MessageBox.Show("Vârsta trebuie să fie între 10 și 100", "Dată de naștere invalidă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(verif())
            {
                pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);
                if(student.insertStudent(fname,lname,clasa,bdate,phone,gender,address,pic))
                {
                    MessageBox.Show("Student adăugat", "Adaugă student", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion        
        #region Color/Hover
        Color albastru;
        Color negru;
        internal void Decolorare()
        {
            textBoxAdresa.ForeColor = Color.WhiteSmoke;
            textBoxNume.ForeColor = Color.WhiteSmoke;
            textBoxPrenume.ForeColor = Color.WhiteSmoke;
            textBoxTelefon.ForeColor = Color.WhiteSmoke;

            panelAdresa.BackColor = Color.WhiteSmoke;
            panelPrenume.BackColor = Color.WhiteSmoke;
            panelNume.BackColor = Color.WhiteSmoke;
            panelTelefon.BackColor = Color.WhiteSmoke;

            if (textBoxAdresa.Text == "")
            {
                textBoxAdresa.Text = "Adresă";
            }
            if (textBoxNume.Text == "")
            {
                textBoxNume.Text = "Nume";
            }
            if (textBoxTelefon.Text == "")
            {
                textBoxTelefon.Text = "Telefon";
            }
            if (textBoxPrenume.Text == "")
            {
                textBoxPrenume.Text = "Prenume";
            }
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            albastru = buttonAddStudent.ForeColor;
            negru = buttonAddStudent.BackColor;
        }
        private void buttonUploadImage_MouseEnter(object sender, EventArgs e)
        {
            buttonUploadImage.ForeColor = negru;
            buttonUploadImage.BackColor = albastru;
        }

        private void buttonUploadImage_MouseLeave(object sender, EventArgs e)
        {
            buttonUploadImage.ForeColor = albastru;
            buttonUploadImage.BackColor = negru;
        }
        private void AddStudentForm_Click(object sender, EventArgs e)
        {
            Decolorare();
        }

        private void buttonAddStudent_MouseEnter(object sender, EventArgs e)
        {
            buttonAddStudent.Font = new Font(buttonAddStudent.Font.FontFamily, buttonAddStudent.Font.Size, FontStyle.Bold);
        }

        private void buttonAddStudent_MouseLeave(object sender, EventArgs e)
        {
            buttonAddStudent.Font = new Font(buttonAddStudent.Font.FontFamily, buttonAddStudent.Font.Size, FontStyle.Regular);
        }
        private void buttonExit1_MouseEnter(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow2));
        }

        private void buttonExit1_MouseLeave(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow1));
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            Decolorare();
        }

        private void radioButtonFemale_Click(object sender, EventArgs e)
        {
            Decolorare();
        }

        private void radioButtonMale_Click(object sender, EventArgs e)
        {
            Decolorare();
        }

        private void comboBoxClasa_Click(object sender, EventArgs e)
        {
            Decolorare();
        }
        #endregion
        #region RoundBorder
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
        #endregion
        #region TextBoxClicking
        private void textBoxLname_Click(object sender, EventArgs e)
        {
            if (textBoxNume.Text == "Nume")
                textBoxNume.Clear();
            panelNume.BackColor = Color.FromArgb(78, 184, 206);
            textBoxNume.ForeColor = Color.FromArgb(78, 184, 206);

            textBoxPrenume.ForeColor = Color.WhiteSmoke;
            textBoxAdresa.ForeColor = Color.WhiteSmoke;
            textBoxTelefon.ForeColor = Color.WhiteSmoke;

            panelPrenume.BackColor = Color.WhiteSmoke;
            panelAdresa.BackColor = Color.WhiteSmoke;
            panelTelefon.BackColor = Color.WhiteSmoke;

            if (textBoxPrenume.Text == "")
            {
                textBoxPrenume.Text = "Prenume";
            }
            if (textBoxTelefon.Text == "")
            {
                textBoxTelefon.Text = "Telefon";
            }
            if (textBoxAdresa.Text == "")
            {
                textBoxAdresa.Text = "Adresă";
            }
        }

        private void textBoxPrenume_Click(object sender, EventArgs e)
        {
            if (textBoxPrenume.Text == "Prenume")
                textBoxPrenume.Clear();
            panelPrenume.BackColor = Color.FromArgb(78, 184, 206);
            textBoxPrenume.ForeColor = Color.FromArgb(78, 184, 206);

            textBoxNume.ForeColor = Color.WhiteSmoke;
            textBoxAdresa.ForeColor = Color.WhiteSmoke;
            textBoxTelefon.ForeColor = Color.WhiteSmoke;

            panelNume.BackColor = Color.WhiteSmoke;
            panelAdresa.BackColor = Color.WhiteSmoke;
            panelTelefon.BackColor = Color.WhiteSmoke;
            
            if (textBoxNume.Text == "")
            {
                textBoxNume.Text = "Nume";
            }
            if (textBoxTelefon.Text == "")
            {
                textBoxTelefon.Text = "Telefon";
            }
            if (textBoxAdresa.Text == "")
            {
                textBoxAdresa.Text = "Adresă";
            }
        }

        private void textBoxTelefon_Click(object sender, EventArgs e)
        {
            if (textBoxTelefon.Text == "Telefon")
                textBoxTelefon.Clear();
            panelTelefon.BackColor = Color.FromArgb(78, 184, 206);
            textBoxTelefon.ForeColor = Color.FromArgb(78, 184, 206);

            textBoxNume.ForeColor = Color.WhiteSmoke;
            textBoxAdresa.ForeColor = Color.WhiteSmoke;
            textBoxPrenume.ForeColor = Color.WhiteSmoke;

            panelPrenume.BackColor = Color.WhiteSmoke;
            panelAdresa.BackColor = Color.WhiteSmoke;
            panelNume.BackColor = Color.WhiteSmoke;

            if (textBoxNume.Text == "")
            {
                textBoxNume.Text = "Nume";
            }
            if (textBoxPrenume.Text == "")
            {
                textBoxPrenume.Text = "Prenume";
            }
            if (textBoxAdresa.Text == "")
            {
                textBoxAdresa.Text = "Adresă";
            }
        }

        private void textBoxAdresa_Click(object sender, EventArgs e)
        {
            if (textBoxAdresa.Text == "Adresă")
                textBoxAdresa.Clear();
            panelAdresa.BackColor = Color.FromArgb(78, 184, 206);
            textBoxAdresa.ForeColor = Color.FromArgb(78, 184, 206);

            textBoxNume.ForeColor = Color.WhiteSmoke;
            textBoxPrenume.ForeColor = Color.WhiteSmoke;
            textBoxTelefon.ForeColor = Color.WhiteSmoke;

            panelPrenume.BackColor = Color.WhiteSmoke;
            panelNume.BackColor = Color.WhiteSmoke;
            panelTelefon.BackColor = Color.WhiteSmoke;

            if (textBoxNume.Text == "")
            {
                textBoxNume.Text = "Nume";
            }
            if (textBoxTelefon.Text == "")
            {
                textBoxTelefon.Text = "Telefon";
            }
            if (textBoxPrenume.Text == "")
            {
                textBoxPrenume.Text = "Prenume";
            }
        }
        #endregion
        #region BorderMoveBar
        bool mouseDown;
        private Point offset;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Decolorare();
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Decolorare();
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
        #endregion
        #region ExitButtons
        private void buttonExit1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
            this.Close();
        }
        #endregion

    }
}

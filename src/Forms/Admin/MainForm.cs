using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Catalog
{
    public partial class MainForm : Form
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

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; //remove form border
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50)); //create the ellipse
        }

        //---------------------------------------------------------------------------------------------------------------------
        //button clicking
        private void buttonClasa_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClassroomForm clsFrm = new ClassroomForm();
            clsFrm.ShowDialog();
            this.Close();
        }

        private void buttonAdaugaStudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStudentForm addStdF = new AddStudentForm();
            addStdF.ShowDialog();
            this.Close();
        }

        private void buttonListaStudenti_Click(object sender, EventArgs e)
        {
            this.Hide();
            studentsListForm stdListF = new studentsListForm();
            stdListF.ShowDialog();
            this.Close();
        }

        private void buttonEditeaza_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateDeleteStudentForm upDelStdF = new UpdateDeleteStudentForm();
            upDelStdF.ShowDialog();
            this.Close();
        }

        private void buttonFormular_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageStudentsForm mngStdF = new ManageStudentsForm();
            mngStdF.ShowDialog();
            this.Close();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            this.Hide();
            PrintStudentsForm prStdF = new PrintStudentsForm();
            prStdF.ShowDialog();
            this.Close();
        }

        //---------------------------------------------------------------------------------------------------------------------
        //button hovering

        private void buttonFormular_MouseHover(object sender, EventArgs e)
        {
            toolTipFormular.Show("Formular",buttonFormular);
        }

        private void buttonAdaugaStudent_MouseHover(object sender, EventArgs e)
        {
            toolTipAdauga.Show("Adaugă elev", buttonAdaugaStudent);
        }

        private void buttonEditeaza_MouseHover(object sender, EventArgs e)
        {
            toolTipEditeaza.Show("Editează elev", buttonEditeaza);
        }

        private void buttonPrint_MouseHover(object sender, EventArgs e)
        {
            toolTipPrint.Show("Export", buttonPrint);
        }

        private void buttonListaStudenti_MouseHover(object sender, EventArgs e)
        {
            toolTipLista.Show("Listă elevi", buttonListaStudenti);
        }

        private void buttonClasa_MouseHover(object sender, EventArgs e)
        {
            toolTipClasa.Show("Catalog", buttonClasa);
        }

        //---------------------------------------------------------------------------------------------------------------------
        //move using the panel

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
            if(mouseDown==true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }


        //---------------------------------------------------------------------------------------------------------------------
        //exiting application

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer1.Stop();
                Application.Exit();
            }
        }
        //-------------------------------------------------
        //EXIT

        private void buttonExit1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        //-------------------------------------------------
        //EXIT BUTTONS

        private void buttonExit1_MouseEnter_1(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow2));
        }

        private void buttonExit1_MouseLeave(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow1));
        }
    }
}

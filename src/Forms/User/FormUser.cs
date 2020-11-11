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
    public partial class FormUser : Form
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

        public FormUser()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; //remove form border
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50)); //create the ellipse
        }

        //---------------------------------------------------------------------------------------------------------------------
        //background color changing
        private void classroomArea_MouseEnter(object sender, EventArgs e)
        {
            panelClassroom.BackColor = Color.FromArgb(78, 184, 206);
        }

        private void studentArea_MouseEnter(object sender, EventArgs e)
        {
            panelStudent.BackColor = Color.FromArgb(78, 184, 206);
        }
        private void studentArea_MouseLeave(object sender, EventArgs e)
        {
            panelStudent.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void classroomArea_MouseLeave(object sender, EventArgs e)
        {
            panelClassroom.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void buttonStudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forms.User.FormStudent stdF = new Forms.User.FormStudent();
            stdF.ShowDialog();
            this.Close();
        }

        private void buttonClassroom_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forms.User.FormClassroom classroomF = new Forms.User.FormClassroom();
            classroomF.ShowDialog();
            this.Close();
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
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        
        //---------------------------------------------------------------------------------------------------------------------
        //exiting application

        
        private void buttonExit1_MouseEnter(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow2));
        }

        private void buttonExit1_MouseLeave_1(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow1));
        }

        private void buttonExit1_Click_1(object sender, EventArgs e)
        {
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }
}

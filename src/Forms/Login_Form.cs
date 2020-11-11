using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Catalog
{
    public partial class Login_Form : Form
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

        public Login_Form()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MY_DB db = new MY_DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `username`= @usn AND `password`= @pass", db.getConnection);
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;
            adapter.SelectCommand = command;
            try
                {
                    adapter.Fill(table);
                    if (table.Rows.Count == 2)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (table.Rows.Count == 1)
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                    else    
                    {
                        MessageBox.Show("Utilizator sau parolă greșită.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch(MySql.Data.MySqlClient.MySqlException ex)
                {
                    string temp = "Eroare! " + ex.Message.ToString();
                    if(temp=="Eroare! Unable to connect to any of the specified MySQL hosts.")
                        MessageBox.Show("Nu se poate face conexiunea la server!","Eroare!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Eroare! " + ex.Message.ToString());
                }  
        }

        private void textBoxUsername_Click(object sender, EventArgs e)
        {
            if(textBoxUsername.Text=="Username")
                textBoxUsername.Clear();
            pictureBoxUsername.BackgroundImage = Properties.Resources.user2;
            panelUsername.ForeColor = Color.FromArgb(78, 184, 206);
            textBoxUsername.ForeColor = Color.FromArgb(78, 184, 206);

            pictureBoxPassword.BackgroundImage = Properties.Resources.password1;
            textBoxPassword.ForeColor = Color.WhiteSmoke;
            textBoxPassword.ForeColor = Color.WhiteSmoke;

            if (textBoxPassword.Text == "")
            {
                textBoxPassword.PasswordChar = '\0';
                textBoxPassword.Text = "Password";
            }
            textBoxPassword.PasswordChar = '∙';
        }

        private void textBoxPassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Password")
                textBoxPassword.Clear();
            textBoxPassword.PasswordChar = '∙';
            pictureBoxPassword.BackgroundImage = Properties.Resources.password2;
            textBoxPassword.ForeColor = Color.FromArgb(78, 184, 206);
            textBoxPassword.ForeColor = Color.FromArgb(78, 184, 206);

            pictureBoxUsername.BackgroundImage = Properties.Resources.user1;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
            textBoxUsername.ForeColor = Color.WhiteSmoke;

            if (textBoxUsername.Text == "")
            {
                textBoxUsername.Text = "Username";
            }
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
            timer1.Start();
        }

        bool mouseDown;
        private Point offset;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
            textBoxPassword.PasswordChar = '∙';
            pictureBoxPassword.BackgroundImage = Properties.Resources.password1;
            textBoxPassword.ForeColor = Color.WhiteSmoke;
            textBoxPassword.ForeColor = Color.WhiteSmoke;

            pictureBoxUsername.BackgroundImage = Properties.Resources.user1;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
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

        private void Login_Form_Load(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '∙';
        }

        private void Login_Form_Click(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '∙';
            pictureBoxPassword.BackgroundImage = Properties.Resources.password1;
            textBoxPassword.ForeColor = Color.WhiteSmoke;
            textBoxPassword.ForeColor = Color.WhiteSmoke;

            pictureBoxUsername.BackgroundImage = Properties.Resources.user1;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
        }

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '∙';
            pictureBoxPassword.BackgroundImage = Properties.Resources.password1;
            textBoxPassword.ForeColor = Color.WhiteSmoke;
            textBoxPassword.ForeColor = Color.WhiteSmoke;

            pictureBoxUsername.BackgroundImage = Properties.Resources.user1;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
            textBoxUsername.ForeColor = Color.WhiteSmoke;
        }
    }
}

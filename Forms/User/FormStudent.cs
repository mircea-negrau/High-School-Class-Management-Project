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

namespace Catalog.Forms.User
{
    public partial class FormStudent : Form
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

        bool logged_in = false;
        string subjectSelected = "";
        string password = "";
        int id = -1;

        public FormStudent()
        {
            InitializeComponent(); 
            customizeDesign();
            this.FormBorderStyle = FormBorderStyle.None; //remove form border
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50)); //create the ellipse
            textBoxPassword.PasswordChar = '∙';
            dataGridView1.GridColor = Color.FromArgb(29, 31, 42);

            //----------------------------Invisible interface
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            textBoxSubject.Visible = false;
            panelSubject.Visible = false;
            panel3.Visible = false;
            textBoxMedie.Visible = false;
            label5.Visible = false;
            label4.Visible = false;
            textBoxNote.Visible = false;
        }
        #region Interface
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

        //--------------------------------------------------------------------------------
        //Exiting application

        private void buttonExit1_MouseEnter(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow2));
        }

        private void buttonExit1_MouseLeave(object sender, EventArgs e)
        {
            buttonExit1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.closeWindow1));
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

        private void buttonExit1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        #endregion
        #region RandomFunctions
        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void customizeDesign()
        {
            panelRealiste.Visible = false;
            panelUmaniste.Visible = false;
            panelLingviste.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelRealiste.Visible == true)
                panelRealiste.Visible = false;
            if (panelUmaniste.Visible == true)
                panelUmaniste.Visible = false;
            if (panelLingviste.Visible == true)
                panelLingviste.Visible = false;
        }
        private void buttonRealiste_Click(object sender, EventArgs e)
        {
            showSubMenu(panelRealiste);
        }
        
        private void buttonUmaniste_Click(object sender, EventArgs e)
        {
            showSubMenu(panelUmaniste);
        }

        private void buttonLingviste_Click(object sender, EventArgs e)
        {
            showSubMenu(panelLingviste);
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        #endregion
        #region Login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            password = textBoxPassword.Text;
            textBoxMedie.Text = "";
            textBoxSubject.Text = "";
            textBoxNote.Text = "";
            id = Convert.ToInt32(textBoxID.Text);

            MY_DB db = new MY_DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` INNER JOIN `users` ON student.id=users.id WHERE `users.id`= @id AND `users.password`= @pass", db.getConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
                if (table.Rows.Count>0)
                {
                    logged_in = true;
                    //----------------------------Visible interface
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = true;
                    textBoxSubject.Visible = true;
                    panelSubject.Visible = true;
                    panel3.Visible = true;
                    textBoxMedie.Visible = true;
                    label5.Visible = true;
                    label4.Visible = true;
                    textBoxNote.Visible = true;
                    panel2.Visible = false;
                    textBoxID.Visible= false;
                    textBoxPassword.Visible= false;
                    label1.Visible= false;
                    buttonLogin.Visible = false;
                    labelID.Visible = false;

                    string nume = table.Rows[0][1].ToString() + " " + table.Rows[0][2].ToString();
                    textBoxName.Text = nume;
                    textBoxClassroomLoggedIn.Text = "Clasa "+table.Rows[0]["clasa"].ToString();

                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBoxStudentImage.Image = Image.FromStream(picture);
                }
                else
                {
                    MessageBox.Show("ID sau parolă greșită.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logged_in = false;
                    string nume = "Please login";
                    textBoxName.Text = nume;
                    textBoxClassroomLoggedIn.Text = "Classroom #";
                    textBoxID.Text = "";
                    textBoxPassword.Text = "";
                    pictureBoxStudentImage.Image = Properties.Resources.defaultAvatar;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string temp = "Eroare! " + ex.Message.ToString();
                if (temp == "Eroare! Unable to connect to any of the specified MySQL hosts.")
                    MessageBox.Show("Nu se poate face conexiunea la server!", "Eroare!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Eroare! " + ex.Message.ToString());
                logged_in = false;
                string nume = "Please login";
                textBoxName.Text = nume;
            }
        }
        #endregion
        #region Sidebar Buttons
        private void button_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            subjectSelected = (sender as Button).Text;
            int ID = id;
            textBoxSubject.Text = subjectSelected;
            if (logged_in)
            { 
                try
                {
                    MY_DB db = new MY_DB();
                    Student student = new Student();
                    Count count = new Count();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    
                    MySqlCommand command = new MySqlCommand("SELECT `nota`,`date`,`teza` FROM `grades` WHERE `elev`=@id AND `materie`=@mat");
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = ID;
                        command.Parameters.Add("@mat", MySqlDbType.VarChar).Value = subjectSelected;
                    dataGridView1.ReadOnly = true;
                    DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                    dataGridView1.RowTemplate.Height = 40;
                    dataGridView1.DataSource = student.getStudents(command);
                    dataGridView1.AllowUserToAddRows = false;

                   
                    string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + subjectSelected + "' AND `teza`=false";
                    string result = count.execCount(average);

                    string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + subjectSelected + "' AND `teza`=false";
                    string testare = count.execCount(test);
                    if (Convert.ToInt32(testare) == 0)
                    {
                            textBoxMedie.Text = "Note insuficiente";
                            textBoxNote.Text = "0";
                    }
                    else
                    {
                        try
                        {
                            string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + subjectSelected + "' AND `teza`=true";
                            string TezaGasita = count.execCount(cautaTeza);
                            int ValTeza = Convert.ToInt32(TezaGasita);
                            test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + subjectSelected + "' AND `teza`=true";
                            testare = count.execCount(test);
                            if (Convert.ToInt32(testare) > 1)
                                textBoxMedie.Text = "EROARE TEZE";
                            else if (Convert.ToInt32(testare) == 1)
                            {
                                double number = Convert.ToDouble(result);
                                number *= 3;
                                number += ValTeza;
                                number /= 4;

                                textBoxMedie.Text = number.ToString();
                                textBoxNote.Text = count.totalGrades(subjectSelected, id.ToString());
                            }
                        }
                        catch
                        {
                            textBoxMedie.Text = result;
                            textBoxNote.Text = count.totalGrades(subjectSelected, id.ToString());
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    MY_DB db = new MY_DB();
                    Student student = new Student();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    
                    MySqlCommand command = new MySqlCommand("SELECT `data`,`motivat` FROM `absente` WHERE `elev`=@id AND `materie`=@mat");
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = ID;
                        command.Parameters.Add("@mat", MySqlDbType.VarChar).Value = subjectSelected;
                    dataGridView2.ReadOnly = true;
                    DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                    dataGridView2.RowTemplate.Height = 40;
                    dataGridView2.DataSource = student.getStudents(command);
                    dataGridView2.AllowUserToAddRows = false;
                }
                catch
                {
                }
            }
        }

        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormUser userF = new FormUser();
            userF.ShowDialog();
            this.Close();
        }
        #endregion
    }
}

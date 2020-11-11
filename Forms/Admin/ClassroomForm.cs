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

    public partial class ClassroomForm : Form
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
        Nota nota = new Nota();
        Absence absence = new Absence();
        Count count = new Count();

        public ClassroomForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxIDnota.Text = "";
            textBoxIDelev.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxNume.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
            try
            {
                string id = "", NumeMaterie = "";
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                NumeMaterie = comboBoxMaterie.Text;
                string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                string result = count.execCount(average);

                string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                string testare = count.execCount(test);
                if (Convert.ToInt32(testare) == 0)
                {
                    if (comboBoxMaterie.Text != "")
                    {
                        textBoxMedie.Text = "Note insuficiente";
                        textBoxNote.Text = "0";
                    }
                    else
                    {
                        textBoxMedie.Text = "";
                        textBoxNote.Text = "";
                    }
                }
                else
                {
                    try
                    {
                        string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                        string TezaGasita = count.execCount(cautaTeza);
                        int ValTeza = Convert.ToInt32(TezaGasita);
                        test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                            textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                        }
                    }
                    catch
                    {
                        textBoxMedie.Text = result;
                        textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                    }
                }

                MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                dataGridView2.ReadOnly = true;
                DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                dataGridView2.RowTemplate.Height = 40;
                dataGridView2.DataSource = student.getStudents(command);
                dataGridView2.AllowUserToAddRows = false;

                comboBoxEditNota.SelectedItem = "";
                textBoxIDnota.Text = "";
            }
            catch
            {
                textBoxMedie.Text = "-";
                textBoxNote.Text = "0";
                dataGridView2.DataSource = null;
            }

            try
            {
                string id = "", NumeMaterie = "";
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                NumeMaterie = comboBoxMaterie.Text;

                textBoxTotal.Text = count.totalAbsente(id);
                textBoxMotivate.Text = count.totalAbsenteMotivate(id);
                textBoxNemotivate.Text = count.totalAbsenteNemotivate(id);

                MySqlCommand command1 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "'");
                dataGridView3.ReadOnly = true;
                DataGridViewImageColumn picCol1 = new DataGridViewImageColumn();
                dataGridView3.RowTemplate.Height = 40;
                dataGridView3.DataSource = student.getStudents(command1);
                dataGridView3.AllowUserToAddRows = false;
            }
            catch
            {

            }

            try
            {
                string id = "", NumeMaterie = "";
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                NumeMaterie = comboBoxMaterie.Text;

                MySqlCommand command2 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "'");

                DataGridViewImageColumn picCol11 = new DataGridViewImageColumn();
                dataGridView4.RowTemplate.Height = 40;
                dataGridView4.DataSource = student.getStudents(command2);
                dataGridView4.AllowUserToAddRows = false;
                dataGridView4.ReadOnly = true;
                textBoxAbsMaterie.Text = count.totalAbsenteMaterie(id, NumeMaterie);
            }
            catch
            {
                textBoxAbsMaterie.Text = "0";
            }
        }

        internal void comboBoxMaterie_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxEditNota.SelectedItem = "";
            textBoxIDnota.Text = "";
            checkBoxTeza.Checked = false; 
            if (textBoxNume.Text != "")
            {
                try
                {
                    string id = "", NumeMaterie = "";
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    NumeMaterie = comboBoxMaterie.Text;
                    string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                    string result = count.execCount(average);

                        string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                        string testare = count.execCount(test);
                        if (Convert.ToInt32(testare) == 0)
                        {
                            if (comboBoxMaterie.Text != "")
                            {
                                textBoxMedie.Text = "Note insuficiente";
                                textBoxNote.Text = "0";
                            }
                            else
                            {
                                textBoxMedie.Text = "";
                                textBoxNote.Text = "";
                            }
                        }
                            else
                            {
                            try
                            {
                                string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                                string TezaGasita = count.execCount(cautaTeza);
                                int ValTeza = Convert.ToInt32(TezaGasita);
                                test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                                } 
                            }
                            catch
                            {
                                textBoxMedie.Text = result;
                                textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                            }
                        }

                    MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                    dataGridView2.ReadOnly = true;
                    DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                    dataGridView2.RowTemplate.Height = 40;
                    dataGridView2.DataSource = student.getStudents(command);
                    dataGridView2.AllowUserToAddRows = false;
                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);

                    comboBoxEditNota.SelectedItem = "";
                }
                catch
                {
                    textBoxMedie.Text = "-";
                    textBoxNote.Text = "0";
                    dataGridView2.DataSource = null;
                }

                try
                {
                    string id = "", NumeMaterie = "";
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    NumeMaterie = comboBoxMaterie.Text;

                    MySqlCommand command3 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "'");
                    dataGridView4.ReadOnly = true;
                    DataGridViewImageColumn picCol8 = new DataGridViewImageColumn();
                    dataGridView4.RowTemplate.Height = 40;
                    dataGridView4.DataSource = student.getStudents(command3);
                    dataGridView4.AllowUserToAddRows = false;
                    textBoxAbsMaterie.Text = count.totalAbsenteMaterie(id, NumeMaterie);
                }
                catch
                {

                }
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try {
                comboBoxEditNota.SelectedItem = dataGridView2.CurrentRow.Cells["nota"].Value.ToString();
                dateTimePickerEditData.Value = (DateTime)(dataGridView2.CurrentRow.Cells["date"].Value);
                textBoxIDnota.Text = dataGridView2.CurrentRow.Cells["id"].Value.ToString();
                checkBoxTeza.Checked = Convert.ToBoolean(dataGridView2.CurrentRow.Cells["teza"].Value);
            }
            catch
            {

            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
                if(MessageBox.Show("Salvați modificările?","Modificare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                    try
                    {
                        int idDeEditat=Convert.ToInt32(textBoxIDnota.Text);
                        int notaDeEditat = Convert.ToInt32(comboBoxEditNota.SelectedItem);
                        DateTime dataDeEditat = (DateTime)dateTimePickerEditData.Value;
                        bool tezaDeEditat;
                        if (checkBoxTeza.Checked == true)
                            tezaDeEditat = true;
                        else
                            tezaDeEditat = false;
                        if(nota.updateNota(idDeEditat,notaDeEditat,dataDeEditat,tezaDeEditat))
                        {
                            MessageBox.Show("Detalii notă ediate", "Editează notă", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           
                            string NumeMaterie = "", id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            NumeMaterie = comboBoxMaterie.Text;

                            MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                            dataGridView2.ReadOnly = true;
                            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                            dataGridView2.RowTemplate.Height = 40;
                            dataGridView2.DataSource = student.getStudents(command);
                            dataGridView2.AllowUserToAddRows = false;

                            string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                            string result = count.execCount(average);

                        string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
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
                                string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                                string TezaGasita = count.execCount(cautaTeza);
                                int ValTeza = Convert.ToInt32(TezaGasita);
                                test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                                }
                            }
                            catch
                            {
                                textBoxMedie.Text = result;
                                textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                            }
                        }
                    }
                        else
                        {
                            MessageBox.Show("Eroare!", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    }
                    catch
                    {
                        MessageBox.Show("Alegeți notă", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int IDnota = Convert.ToInt32(textBoxIDnota.Text);
                if (MessageBox.Show("Sunteți sigur că vreți să ștergeți nota?", "Șterge notă", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (nota.deleteGrade(IDnota))
                    {
                        MessageBox.Show("Notă ștearsă.", "Șterge notă", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string NumeMaterie = "", id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        NumeMaterie = comboBoxMaterie.Text;

                        MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                        dataGridView2.ReadOnly = true;
                        DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                        dataGridView2.RowTemplate.Height = 40;
                        dataGridView2.DataSource = student.getStudents(command);
                        dataGridView2.AllowUserToAddRows = false;

                        string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                        string result = count.execCount(average);

                        string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
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
                                string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                                string TezaGasita = count.execCount(cautaTeza);
                                int ValTeza = Convert.ToInt32(TezaGasita);
                                test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                                }
                            }
                            catch
                            {
                                textBoxMedie.Text = result;
                                textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Alegeți o notă", "Șterge notă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxClasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string numar = comboBoxClasa.Text.ToString();
            textBoxIDelev.Text = "";
            if (numar != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT student.id, student.last_name, student.first_name, student.clasa, student.gender, student.birthdate FROM student WHERE student.clasa='" + numar + "' ORDER BY student.last_name");
                dataGridView1.ReadOnly = true;
                DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                dataGridView1.RowTemplate.Height = 80;
                dataGridView1.DataSource = student.getStudents(command);

                /*DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "Vârstă";
                column.Name = "Vârstă";
            if(dataGridView1.Columns.Count!=7)
                dataGridView1.Columns.Add(column);

            DateTime now = DateTime.Today;
            dataGridView1.ReadOnly = false;
           for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                try
                {
                    DateTime dob = Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    int age = now.Year - dob.Year;
                    if (dob.Month > now.Month)
                        age--;
                    else if (dob.Month == now.Month)
                        if (dob.Day > now.Day)
                            age--;
                    dataGridView1[6, i].Value = age.ToString();
                }
                catch
                {

                }
            }
            dataGridView1.ReadOnly = true;

            /*string[] columns = { "Student_ID", "Last Name", "First Name", "MUZ"};
            string all_subjects = new MySqlCommand("Select distinct")
            dataGridView1.AutoGenerateColumns = false;
            foreach (var col in columns)
            {
                DataGridViewColumn miejsce = new DataGridViewTextBoxColumn();
                miejsce.DataPropertyName = col;
                miejsce.HeaderText = col;
                miejsce.Name = col;
                dataGridView1.Columns.Add(miejsce);
            }

            //Create row

            string avg_muz = "SELECT AVG(nota) FROM `grades` WHERE `elev`='15' AND `materie`='BIO'";
            string result = count.execCount(avg_muz);
            string[] row = { "15", "Mircea", "Negrau" , result};
            dataGridView1.Rows.Add(row);*/

                dataGridView1.AllowUserToAddRows = false;
                comboBoxMaterie.Text = "";
                textBoxMedie.Text = "";
                textBoxNote.Text = "";
                textBoxNume.Text = "";
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
                textBoxAbsMaterie.Text = "";
                comboBoxEditNota.SelectedItem = "";
                textBoxIDnota.Text = "";
                checkBoxTeza.Checked = false;

                //string[] adding = { "student.id", "student.last_name", "student.first_name", "student.clasa", "student.birthdate", "student.gender", "medie.BIO", "medie.CHI", "medie.DES", "medie.ECO", "medie.ENG", "medie.FIL", "medie.FIZ", "medie.FRA", "medie.GEO", "medie.INF", "medie.IST", "medie.MAT", "medie.MUZ", "medie.REL", "medie.ROM", "medie.SPO", "medie.TIC", "medie.PUR", "medie.TOTAL"};

            }
            else
            {
                MessageBox.Show("Clasă invalidă.", "Clasă invalidă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Adăugați notă?", "Adăugare notă", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int idElevDeEditat = Convert.ToInt32(textBoxIDelev.Text);
                    string materieDeEditat = comboBoxMaterie.Text.ToString();
                    int notaDeEditat = Convert.ToInt32(comboBoxEditNota.SelectedItem);
                    DateTime dataDeEditat = (DateTime)dateTimePickerEditData.Value;
                    bool tezaDeEditat;
                    if (checkBoxTeza.Checked == true)
                        tezaDeEditat = true;
                    else
                        tezaDeEditat = false;
                    if (nota.addNota(materieDeEditat, notaDeEditat, dataDeEditat, idElevDeEditat, tezaDeEditat))
                    {
                        MessageBox.Show("Notă adăugată", "Adaugă notă", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string NumeMaterie = "", id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        NumeMaterie = comboBoxMaterie.Text;

                        MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                        dataGridView2.ReadOnly = true;
                        DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                        dataGridView2.RowTemplate.Height = 40;
                        dataGridView2.DataSource = student.getStudents(command);
                        dataGridView2.AllowUserToAddRows = false;

                        string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                        string result = count.execCount(average);

                        string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
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
                                string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                                string TezaGasita = count.execCount(cautaTeza);
                                int ValTeza = Convert.ToInt32(TezaGasita);
                                test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                                }
                            }
                            catch
                            {
                                textBoxMedie.Text = result;
                                textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Eroare!", "Adaugă notă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Alegeți notă", "Adaugă notă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAddAbsence_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Elev absent?", "Adăugare absență", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int idElevDeEditat = Convert.ToInt32(textBoxIDelev.Text);
                    string materieDeEditat = comboBoxMaterie.Text.ToString();
                    DateTime dataDeEditat = (DateTime)dateTimePickerAbsenta.Value;

                    if(materieDeEditat!="" && idElevDeEditat>0)
                    {
                        if (absence.addAbsenta(idElevDeEditat, materieDeEditat, dataDeEditat))
                        {
                            MessageBox.Show("Absență înregistrată", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            try
                            {
                                string id = "", NumeMaterie = "";
                                id = textBoxIDelev.Text;
                                NumeMaterie = comboBoxMaterie.Text;

                                MySqlCommand command2 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "'");

                                DataGridViewImageColumn picCol11 = new DataGridViewImageColumn();
                                dataGridView4.RowTemplate.Height = 40;
                                dataGridView4.DataSource = student.getStudents(command2);
                                dataGridView4.AllowUserToAddRows = false;
                                dataGridView4.ReadOnly = true;
                                textBoxAbsMaterie.Text = count.totalAbsenteMaterie(id, NumeMaterie);
                            }
                            catch
                            {
                                textBoxAbsMaterie.Text = "0";
                            }

                            try
                            {
                                string id = "", NumeMaterie = "";
                                id = textBoxIDelev.Text;
                                NumeMaterie = comboBoxMaterie.Text;

                                textBoxTotal.Text = count.totalAbsente(id);
                                textBoxMotivate.Text = count.totalAbsenteMotivate(id);
                                textBoxNemotivate.Text = count.totalAbsenteNemotivate(id);

                                MySqlCommand command1 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "'");
                                dataGridView3.ReadOnly = true;
                                DataGridViewImageColumn picCol1 = new DataGridViewImageColumn();
                                dataGridView3.RowTemplate.Height = 40;
                                dataGridView3.DataSource = student.getStudents(command1);
                                dataGridView3.AllowUserToAddRows = false;
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Elev sau absență neselectată!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Eroare!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        //-----------------------------------------//-----------------------------------------//
        //Colorare butoane 

        Color albastru;
        Color negru;

        private void ClassroomForm_Load(object sender, EventArgs e)
        {
            albastru = buttonEdit.ForeColor;
            negru = buttonEdit.BackColor;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {

            buttonEdit.ForeColor = negru;
            buttonEdit.BackColor = albastru;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            buttonEdit.ForeColor = albastru;
            buttonEdit.BackColor = negru;
        }

        private void buttonRemove_MouseEnter(object sender, EventArgs e)
        {
            buttonRemove.ForeColor = negru;
            buttonRemove.BackColor = albastru;
        }

        private void buttonRemove_MouseLeave(object sender, EventArgs e)
        {
            buttonRemove.ForeColor = albastru;
            buttonRemove.BackColor = negru;
        }

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonAdd.ForeColor = negru;
            buttonAdd.BackColor = albastru;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonAdd.ForeColor = albastru;
            buttonAdd.BackColor = negru;
        }

        private void buttonAddAbsence_MouseEnter(object sender, EventArgs e)
        {
            buttonAddAbsence.ForeColor = negru;
            buttonAddAbsence.BackColor = albastru;
        }

        private void buttonAddAbsence_MouseLeave(object sender, EventArgs e)
        {
            buttonAddAbsence.ForeColor = albastru;
            buttonAddAbsence.BackColor = negru;
        }


        //----------------------------------------------------------------------------------------------------------------
        //Check-Box edit

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rPos = dataGridView3.CurrentCell.ColumnIndex;
            int idAbs = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
            if (rPos == 3)
            {
                if (MessageBox.Show("Schimbare status?", "Motivare absențe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(dataGridView3.CurrentRow.Cells[3].Value) == false)
                    {
                        bool motiv = true;
                        if (absence.updateAbsenta(idAbs, motiv))
                        {
                            MessageBox.Show("Absență motivată", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Eroare!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bool motiv = false;
                        if (absence.updateAbsenta(idAbs, motiv))
                        {
                            MessageBox.Show("Absență nemotivată", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Eroare!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    try
                    {
                        string id = "", NumeMaterie = "";
                        id = textBoxIDelev.Text;
                        NumeMaterie = comboBoxMaterie.Text;

                        MySqlCommand command2 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "'");

                        DataGridViewImageColumn picCol11 = new DataGridViewImageColumn();
                        dataGridView4.RowTemplate.Height = 40;
                        dataGridView4.DataSource = student.getStudents(command2);
                        dataGridView4.AllowUserToAddRows = false;
                        dataGridView4.ReadOnly = true;
                        textBoxAbsMaterie.Text = count.totalAbsenteMaterie(id, NumeMaterie);
                    }
                    catch
                    {
                        textBoxAbsMaterie.Text = "0";
                    }

                    try
                    {
                        string id = "", NumeMaterie = "";
                        id = textBoxIDelev.Text;
                        NumeMaterie = comboBoxMaterie.Text;

                        textBoxTotal.Text = count.totalAbsente(id);
                        textBoxMotivate.Text = count.totalAbsenteMotivate(id);
                        textBoxNemotivate.Text = count.totalAbsenteNemotivate(id);

                        MySqlCommand command1 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "'");
                        dataGridView3.ReadOnly = true;
                        DataGridViewImageColumn picCol1 = new DataGridViewImageColumn();
                        dataGridView3.RowTemplate.Height = 40;
                        dataGridView3.DataSource = student.getStudents(command1);
                        dataGridView3.AllowUserToAddRows = false;
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rPos = dataGridView4.CurrentCell.ColumnIndex;
            int idAbs = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value);
            if (rPos == 3)
            {
                if (MessageBox.Show("Schimbare status?", "Motivare absențe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(dataGridView4.CurrentRow.Cells[3].Value) == false)
                    {
                        bool motiv = true;
                        if (absence.updateAbsenta(idAbs, motiv))
                        {
                            MessageBox.Show("Absență motivată", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Eroare!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bool motiv = false;
                        if (absence.updateAbsenta(idAbs, motiv))
                        {
                            MessageBox.Show("Absență nemotivată", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Eroare!", "Absențe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    try
                    {
                        string id = "", NumeMaterie = "";
                        id = textBoxIDelev.Text;
                        NumeMaterie = comboBoxMaterie.Text;

                        MySqlCommand command2 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "'");

                        DataGridViewImageColumn picCol11 = new DataGridViewImageColumn();
                        dataGridView4.RowTemplate.Height = 40;
                        dataGridView4.DataSource = student.getStudents(command2);
                        dataGridView4.AllowUserToAddRows = false;
                        dataGridView4.ReadOnly = true;
                        textBoxAbsMaterie.Text = count.totalAbsenteMaterie(id, NumeMaterie);
                    }
                    catch
                    {
                        textBoxAbsMaterie.Text = "0";
                    }

                    try
                    {
                        string id = "", NumeMaterie = "";
                        id = textBoxIDelev.Text;
                        NumeMaterie = comboBoxMaterie.Text;

                        textBoxTotal.Text = count.totalAbsente(id);
                        textBoxMotivate.Text = count.totalAbsenteMotivate(id);
                        textBoxNemotivate.Text = count.totalAbsenteNemotivate(id);

                        MySqlCommand command1 = new MySqlCommand("SELECT `id`, `materie`, `data`, `motivat` FROM `absente` WHERE `elev`='" + id + "'");
                        dataGridView3.ReadOnly = true;
                        DataGridViewImageColumn picCol1 = new DataGridViewImageColumn();
                        dataGridView3.RowTemplate.Height = 40;
                        dataGridView3.DataSource = student.getStudents(command1);
                        dataGridView3.AllowUserToAddRows = false;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rPos = dataGridView2.CurrentCell.ColumnIndex;
            int idDeEditat = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            int notaDeEditat = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value);
            DateTime dataDeEditat = (DateTime)(dataGridView2.CurrentRow.Cells[2].Value);
            int Thesis=(Convert.ToInt32(dataGridView2.CurrentRow.Cells[3].Value)+1)%2;
            bool teza = true;
            if (Thesis == 0)
                teza = false;
            if (rPos == 3)
            {
                try
                {
                    if (nota.updateNota(idDeEditat, notaDeEditat, dataDeEditat, teza))
                    {
                        MessageBox.Show("Status notă modificat", "Editează notă", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string NumeMaterie = "", id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        NumeMaterie = comboBoxMaterie.Text;

                        MySqlCommand command = new MySqlCommand("SELECT `id`, `nota`, `date`, `teza` FROM `grades` WHERE `materie`='" + NumeMaterie + "' AND `elev`='" + id + "'");
                        dataGridView2.ReadOnly = true;
                        DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                        dataGridView2.RowTemplate.Height = 40;
                        dataGridView2.DataSource = student.getStudents(command);
                        dataGridView2.AllowUserToAddRows = false;

                        string average = "SELECT AVG(nota) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
                        string result = count.execCount(average);

                        string test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=false";
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
                                string cautaTeza = "SELECT `nota` FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
                                string TezaGasita = count.execCount(cautaTeza);
                                int ValTeza = Convert.ToInt32(TezaGasita);
                                test = "SELECT COUNT(*) FROM `grades` WHERE `elev`='" + id + "' AND `materie`='" + NumeMaterie + "' AND `teza`=true";
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
                                    textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                                }
                            }
                            catch
                            {
                                textBoxMedie.Text = result;
                                textBoxNote.Text = count.totalGrades(NumeMaterie, id);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Eroare!", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Alegeți notă", "Editează student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

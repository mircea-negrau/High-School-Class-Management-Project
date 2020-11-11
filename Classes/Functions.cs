using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Catalog
{
    class Student
    {
        MY_DB db = new MY_DB();
        public DataTable getStudents(MySqlCommand command)
        {
            command.Connection = db.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
        public bool insertStudent(string fname, string lname, string clasa, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`first_name`, `last_name`, `clasa`, `birthdate`, `gender`, `phone`, `address`, `picture`) VALUES (@fn,@ln,@cls,@bdt,@gdr,@phn,@adrs,@pic)", db.getConnection);
            #region Parameters
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@cls", MySqlDbType.VarChar).Value = clasa;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = picture.ToArray();
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool updateStudent(int id, string fname, string lname, string clasa, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `first_name`=@fn,`last_name`=@ln,`clasa`=@cls,`birthdate`=@bdt,`gender`=@gdr,`phone`=@phn,`address`=@adrs,`picture`=@pic WHERE `id`=@ID", db.getConnection);
            #region Parameters
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@cls", MySqlDbType.VarChar).Value = clasa;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = picture.ToArray();
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `id`=@studentID", db.getConnection);
            #region Parameters
            command.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id;
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
    }


    class Nota
    {
        MY_DB db = new MY_DB();
        public bool addNota(string materie, int nota, DateTime timp, int elev, bool teza)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `grades`(`materie`, `nota`, `date`, `elev`, `teza`) VALUES(@mat, @not, @dat, @elv, @tza)", db.getConnection);
            #region Parameters
            command.Parameters.Add("@mat", MySqlDbType.String).Value = materie;
            command.Parameters.Add("@not", MySqlDbType.Int32).Value = nota;
            command.Parameters.Add("@elv", MySqlDbType.Int32).Value = elev;
            command.Parameters.Add("@dat", MySqlDbType.Date).Value = timp;
            command.Parameters.Add("@tza", MySqlDbType.Byte).Value = teza;
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool updateNota(int id, int nota, DateTime timp, bool teza)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `grades` SET `nota`=@not,`date`=@date, `teza`=@tez WHERE `id`=@ID", db.getConnection);
            #region Parameters
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@not", MySqlDbType.Int32).Value = nota;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = timp;
            command.Parameters.Add("@tez", MySqlDbType.Byte).Value = teza;
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool deleteGrade(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `grades` WHERE `id`=@studentID", db.getConnection);

            command.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }

        }
    }

    class Absence
    {
        MY_DB db = new MY_DB();
        public bool addAbsenta(int id, string materie, DateTime timp)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `absente`(`elev`, `materie`, `data`) VALUES(@id, @mat, @dat)", db.getConnection);
            #region Parameters
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@mat", MySqlDbType.String).Value = materie;
            command.Parameters.Add("@dat", MySqlDbType.Date).Value = timp;
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool updateAbsenta(int id, bool motivat)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `absente` SET `motivat`=@mot WHERE `id`=@ID", db.getConnection);
            #region Parameters
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@mot", MySqlDbType.Byte).Value = motivat;
            #endregion
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

    }

    class Count
    {
        MY_DB db = new MY_DB();
        public string execCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            db.openConnection();
            string count = command.ExecuteScalar().ToString();
            db.closeConnection();

            return count;
        }
        public string totalStudent()
        {
            return execCount("SELECT COUNT(*) FROM `student`");
        }

        public string totalMaleStudents()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender`= 'Male'");
        }

        public string totalFemaleStudents()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender`= 'Female'");
        }

        public string totalGrades(string MaterieCautata, string IDstudent)
        {
            return execCount("SELECT COUNT(*) FROM grades WHERE '" + IDstudent + "'=grades.elev AND grades.materie='" + MaterieCautata + "' AND grades.teza=0");
        }

        public string totalAbsente(string IDstudent)
        {
            return execCount("SELECT COUNT(*) FROM absente WHERE '" + IDstudent + "'=elev");
        }

        public string totalAbsenteMaterie(string IDstudent, string MaterieCautata)
        {
            return execCount("SELECT COUNT(*) FROM absente WHERE '" + IDstudent + "'=elev AND materie='" + MaterieCautata + "'");
        }

        public string totalAbsenteMotivate(string IDstudent)
        {
            return execCount("SELECT COUNT(*) FROM absente WHERE '" + IDstudent + "'=elev AND motivat=1");
        }

        public string totalAbsenteNemotivate(string IDstudent)
        {
            return execCount("SELECT COUNT(*) FROM absente WHERE '" + IDstudent + "'=elev AND motivat=0");
        }

    }
}

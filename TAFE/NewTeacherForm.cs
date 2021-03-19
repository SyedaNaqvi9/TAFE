using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAFE
{
    public partial class NewTeacherForm : Form
    {
        teacher model = new teacher();
        teachercourse model5 = new teachercourse();
        tuser model2 = new tuser();
        public NewTeacherForm()
        {
            InitializeComponent();
            PopulateGridView();
            PopulateGridView2();
            PopulateGridView3();
        }
        //All the validation
        public bool ValidateForEmptiness(String str)
        {
            if (str == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateForLength(String str, int len)
        {
            if (str.Length != len)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateForNumeric(String str)
        {
            try
            {
                int num = int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateCombobox(ComboBox cmb)
        {
            if (cmb.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //will clear fields
        public void ClearFields()
        {
            textBox1.Text = textBox2.Text = textBox4.Text = textBox5.Text = textBox3.Text = textBox6.Text = textBox7.Text = comboBox1.Text = dateTimePicker1.Text = "";

        }
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from t in Context.teachers

                             select new
                             {
                                 t.teacherId,
                                 t.firstname,
                                 t.middlename,
                                 t.lastname,
                                 t.dob,
                                 t.gender,
                                 t.email,
                                 t.address,
                                 t.phone
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        public void PopulateGridView2()
        {
            using (var Context2 = new tafesystemEntities())
            {

                var TeachRecord = from tr in Context2.teachercourses

                              select new
                              {
                                  tr.teachercourseId,
                                  tr.coursename,
                                  tr.clustername,
                                  tr.semestername,
                                  tr.locationname,
                                  tr.mode,
                                  tr.status,
                                  tr.teacherId,
                                  tr.courseId
                              };
                dataGridView2.DataSource = TeachRecord.ToList();
            }
        }
        //add the teacher info
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateForEmptiness(textBox4.Text))
            {
                MessageBox.Show("First Name is required");
            }
            else if (!ValidateForEmptiness(textBox1.Text))
            {
                MessageBox.Show("Middle Name is required");
            }
            else if (!ValidateForEmptiness(textBox3.Text))
            {
                MessageBox.Show("Last Name is required");
            }
            else if (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Gender is Required");
            }
            else if (!ValidateForEmptiness(textBox6.Text))
            {
                MessageBox.Show(" Email is required");

            }
            else if (!ValidEmail(textBox6.Text))
            {
                MessageBox.Show("Enter email correctly");

            }

            else if (!ValidateForEmptiness(textBox5.Text))
            {
                MessageBox.Show(" Address is required");

            }
            else if (!ValidateForEmptiness(textBox7.Text))
            {
                MessageBox.Show(" Phone Number is required");

            }
            else if (!ValidateForNumeric(textBox7.Text))
            {
                MessageBox.Show("Phone should be in numbers");
            }

            else if (ValidateForLength(textBox7.Text, 11) == false)
            {
                MessageBox.Show("Enter a valid phone number");
            }
            else
            {
                int tId = Convert.ToInt32(textBox2.Text);
                model.teacherId = tId;
                model.firstname = textBox4.Text.Trim();
                model.middlename = textBox1.Text.Trim();
                model.lastname = textBox3.Text.Trim();
                model.dob = dateTimePicker1.Value.Date;
                model.gender = comboBox1.Text.Trim();
                model.email = textBox6.Text.Trim();
                model.address = textBox7.Text.Trim();
                model.phone = textBox5.Text.Trim();
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.teachers.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }
        //fill the fields 
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.teacherId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["teacherId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.teachers.Where(x => x.teacherId == model.teacherId).FirstOrDefault();
                    textBox2.Text = Convert.ToInt32(model.teacherId).ToString();
                    textBox4.Text = model.firstname;
                    textBox1.Text = model.middlename;
                    textBox3.Text = model.lastname;
                    dateTimePicker1.Value = Convert.ToDateTime(model.dob);
                    comboBox1.Text = model.gender;
                    textBox6.Text = model.email;
                    textBox7.Text = model.address;
                    textBox5.Text = model.phone;



                }
            }
        }
        //update the record 
        private void button3_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int tId = Convert.ToInt32(textBox2.Text);
                var update = tf.teachers.First(a => a.teacherId == tId);
                update.firstname = textBox4.Text;
                update.middlename = textBox1.Text;
                update.lastname = textBox3.Text;
                update.dob = dateTimePicker1.Value.Date;
                update.gender = comboBox1.Text;
                update.email = textBox6.Text;
                update.address = textBox7.Text;
                update.phone = textBox5.Text;
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();

        }
        //del a record
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.teachers.Attach(model);
                    tf.teachers.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //fill the datagrid 
        public void PopulateGridView3()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record3 = from tu in Context.tusers

                              select new
                              {
                                  tu.userId,
                                  tu.username,
                                  tu.password,
                                  tu.userType,
                                  tu.teacherId


                              };
                dataGridView3.DataSource = Record3.ToList();
            }
        }
        public void ClearFields3()
        {
            textBox14.Text = textBox13.Text = textBox10.Text = textBox11.Text = textBox12.Text = comboBox5.Text = "";

        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from t in Context.teachers

                             select new
                             {
                                 t.teacherId,
                                 t.firstname,
                                 t.middlename,
                                 t.lastname,
                                 t.dob,
                                 t.gender,
                                 t.email,
                                 t.address,
                                 t.phone
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //clear the fields
        public void ClearFields2()
        {
            textBox8.Text = textBox9.Text = textBox10.Text = comboBox4.Text = comboBox3.Text = comboBox8.Text = comboBox12.Text = comboBox2.Text = comboBox11.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //add a recrod 
        private void button10_Click(object sender, EventArgs e)
        {
            int tcId = Convert.ToInt32(textBox8.Text);
            model5.teachercourseId = tcId;
            model5.coursename = comboBox11.Text;
            model5.clustername = comboBox2.Text;
            model5.semestername = comboBox12.Text;
            model5.locationname = comboBox8.Text;
            model5.mode = comboBox3.Text;
            model5.status = comboBox4.Text;
            int tId = Convert.ToInt32(textBox9.Text);
            model5.teacherId = tId;
            int cId = Convert.ToInt32(textBox10.Text);
            model5.courseId = cId;
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                tf.teachercourses.Add(model5);

                tf.SaveChanges();
            }

            MessageBox.Show("Data submitted successsfully");
            PopulateGridView2();
            ClearFields2();
        }
        //update a record 
        private void button8_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int tcourId = Convert.ToInt32(textBox8.Text);
                var update2 = tf.teachercourses.First(a => a.teachercourseId == tcourId);
                update2.coursename = comboBox11.Text;
                update2.clustername = comboBox2.Text;
                update2.semestername = comboBox12.Text;
                update2.locationname = comboBox8.Text;
                update2.mode = comboBox3.Text;
                update2.status = comboBox4.Text;
                int teachId = Convert.ToInt32(textBox9.Text);
                update2.teacherId = teachId;
                int corId = Convert.ToInt32(textBox10.Text);
                update2.courseId = corId;
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView2();
            ClearFields2();
        }
    //fill the fields 
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                model5.teachercourseId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["teachercourseId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                   
                    model5 = tf.teachercourses.Where(x => x.teachercourseId == model5.teachercourseId).FirstOrDefault();
                    textBox8.Text = Convert.ToInt32(model5.teachercourseId).ToString();
                    comboBox11.Text = model5.coursename;
                    comboBox2.Text = model5.clustername;
                    comboBox12.Text = model5.semestername;
                    comboBox8.Text = model5.locationname;
                    comboBox3.Text = model5.mode;
                    comboBox4.Text = model5.status;
                    textBox9.Text = Convert.ToInt32(model5.teacherId).ToString();
                    textBox10.Text = Convert.ToInt32(model5.courseId).ToString();
                }
            }
        }
        //delete a record
        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model5);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.teachercourses.Attach(model5);
                    tf.teachercourses.Remove(model5);
                    tf.SaveChanges();
                    PopulateGridView2();
                    ClearFields2();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PopulateGridView2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearFields2();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ClearFields3();
        }
        //login for teacher
        private void button14_Click(object sender, EventArgs e)
        {
            int uId = Convert.ToInt32(textBox14.Text);
            model2.userId = uId;
            model2.username = textBox13.Text;
            model2.password = textBox12.Text;
            model2.userType = comboBox5.Text;
            int teacId = Convert.ToInt32(textBox11.Text);
            model2.teacherId = teacId;

            using (tafesystemEntities tf = new tafesystemEntities())
            {
                tf.tusers.Add(model2);

                tf.SaveChanges();
            }

            MessageBox.Show("SignUp successsful");
            PopulateGridView3();
            ClearFields3();
        }
        //login updation
        private void button13_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int uId = Convert.ToInt32(textBox14.Text);
                var update1 = tf.tusers.First(a => a.userId == uId);
                update1.username = textBox13.Text;
                update1.password = textBox12.Text;
                update1.userType = comboBox5.Text;


                int teacId = Convert.ToInt32(textBox11.Text);

                tf.SaveChanges();

            }
            MessageBox.Show("Signin Updated sucessfully");
            PopulateGridView3();
            ClearFields3();
        }
        //dele a signin info

        private void button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model2);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.tusers.Attach(model2);
                    tf.tusers.Remove(model2);
                    tf.SaveChanges();
                    PopulateGridView3();
                    ClearFields3();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //fill the fields 
        private void dataGridView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView3.CurrentRow.Index != -1)
            {
                model2.userId = Convert.ToInt32(dataGridView3.CurrentRow.Cells["userId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model2 = tf.tusers.Where(x => x.userId == model2.userId).FirstOrDefault();
                    textBox14.Text = Convert.ToInt32(model2.userId).ToString();
                    textBox13.Text = model2.username;
                    textBox12.Text = model2.password;
                    comboBox5.Text = model2.userType;
                    textBox11.Text = Convert.ToInt32(model2.userId).ToString();

                }
            }
        }

        private void NewTeacherForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

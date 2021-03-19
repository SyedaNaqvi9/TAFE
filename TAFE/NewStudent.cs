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
    public partial class NewStudent : Form
    {
        student model = new student();
        enrollment model1 = new enrollment();
       
        public NewStudent()
        {
            InitializeComponent();
            PopulateGridView();
            PopulateGridView2();
        }
     // Validation for empty fields 
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
        //Validation fro length of input 
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
        //Valiation fro numbers
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
        //Validation fro combobox selection
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
       // Validation for email
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
        //All the validation going on and then infromation will get submiited in the database 
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
                int sId = Convert.ToInt32(textBox2.Text);
                model.studentId = sId;
                model.firstname = textBox4.Text.Trim();
                model.middlename = textBox1.Text.Trim();
                model.lastname = textBox3.Text.Trim();
                model.dob = dateTimePicker1.Value.Date;
                model.gender = comboBox1.Text.Trim();
                model.email = textBox6.Text.Trim();
                model.address = textBox5.Text.Trim();
                model.phone = textBox7.Text.Trim();
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.students.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }
        //Clear button
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
            
            

        }
        //this will clear all the fields
        public void ClearFields()
        {
            textBox11.Text=textBox1.Text= textBox2.Text = textBox4.Text = textBox5.Text = textBox3.Text = textBox6.Text=textBox7.Text=comboBox1.Text=dateTimePicker1.Text="";
        }
        //this will populate the data grid with desired results 
       public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from s in Context.students

                             select new
                             {
                                 s.studentId,
                                 s.firstname,
                                 s.middlename,
                                 s.lastname,
                                 s.dob,
                                 s.gender,
                                 s.email,
                                 s.address,
                                 s.phone
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from s in Context.students

                             select new
                             {
                                 s.studentId,
                                 s.firstname,
                                 s.middlename,
                                 s.lastname,
                                 s.dob,
                                 s.gender,
                                 s.email,
                                 s.address,
                                 s.phone
                             };
                dataGridView1.DataSource = Record.ToList();
            }

       

        }


        //by pressing the id in the datagrid that entry will be filled in the textboxs/combobox
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.studentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["studentId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.students.Where(x => x.studentId == model.studentId).FirstOrDefault();
                    textBox2.Text = Convert.ToInt32(model.studentId).ToString();
                    textBox4.Text = model.firstname;
                    textBox1.Text = model.middlename;
                    textBox3.Text = model.lastname;
                    dateTimePicker1.Value = Convert.ToDateTime(model.dob);
                    comboBox1.Text = model.gender;
                    textBox6.Text = model.email;
                    textBox5.Text = model.address;
                    textBox7.Text = model.phone;



                }
            }
        }
        //this will update the student form
        private void button3_Click(object sender, EventArgs e)

        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int sId = Convert.ToInt32(textBox2.Text);
                var update = tf.students.First(a => a.studentId == sId);
                update.firstname = textBox4.Text;
                update.middlename = textBox1.Text;
                update.lastname = textBox3.Text;
                update.dob = dateTimePicker1.Value.Date;
                update.gender = comboBox1.Text;
                update.email = textBox6.Text;
                update.address = textBox5.Text;
                update.phone = textBox7.Text;
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
           
        }
        //this will delete the student record 
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.students.Attach(model);
                    tf.students.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //this will save the enetered data into the system
        private void button6_Click(object sender, EventArgs e)
        {
            int eId = Convert.ToInt32(textBox8.Text);
            model1.enrollmentId = eId;
            model1.coursename = comboBox10.Text.Trim();
            model1.semestername = comboBox9.Text.Trim();
            model1.locationname = comboBox2.Text.Trim();
            model1.mode = comboBox3.Text.Trim();
            model1.status = comboBox6.Text.Trim();
            model1.fee = comboBox4.Text.Trim();
            model1.result = comboBox5.Text.Trim();
            int studId = Convert.ToInt32(textBox9.Text);
            model1.studentId = studId;
            int courId = Convert.ToInt32(textBox10.Text);
            model1.courseId = courId;
            int locId = Convert.ToInt32(textBox11.Text);
            model1.locationId = locId;
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                tf.enrollments.Add(model1);

                tf.SaveChanges();
            }
            ClearFields2();
            MessageBox.Show("Data submitted successsfully");
            PopulateGridView2();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearFields2();
        }
        public void PopulateGridView2()
        {
            using (var Context = new tafesystemEntities())
            {

                var EnrolRecord = from en in Context.enrollments

                                  select new
                                  {
                                      en.enrollmentId,
                                      en.coursename,
                                      en.semestername,
                                      en.locationname,
                                      en.mode,
                                      en.status,
                                      en.fee,
                                      en.result,
                                      en.studentId,
                                      en.courseId,
                                      en.locationId
                                  };
                dataGridView2.DataSource = EnrolRecord.ToList();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            PopulateGridView2();
        }
        //this will update the enrollemnt enrties 
        private void button7_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int eId = Convert.ToInt32(textBox8.Text);
                var update2 = tf.enrollments.First(a => a.enrollmentId == eId);
                update2.coursename = comboBox10.Text;
                update2.semestername = comboBox9.Text;
                update2.locationname = comboBox2.Text;
                update2.mode = comboBox3.Text;
                update2.status = comboBox6.Text;
                update2.fee = comboBox4.Text;
                update2.result = comboBox5.Text;
                int studId = Convert.ToInt32(textBox9.Text);
                update2.studentId = studId;
                int courId = Convert.ToInt32(textBox10.Text);
                update2.courseId = courId;
                int locId = Convert.ToInt32(textBox11.Text);
                update2.locationId = locId;
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView2();
            ClearFields2();
        }
        // by clicking to any entry the datagrid it will be filled in the form's textboxes and comboboxs so user can edit them
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                model1.enrollmentId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["enrollmentId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model1= tf.enrollments.Where(x => x.enrollmentId == model1.enrollmentId).FirstOrDefault();
                    textBox8.Text = Convert.ToInt32(model1.enrollmentId).ToString();
                    comboBox10.Text = model1.coursename;
                    comboBox9.Text = model1.semestername;
                    comboBox2.Text = model1.locationname;
                    comboBox3.Text = model1.mode;
                    comboBox6.Text = model1.status;
                    comboBox4.Text = model1.fee;
                    comboBox5.Text = model1.result;
                    textBox9.Text = Convert.ToInt32(model1.studentId).ToString();
                    textBox10.Text = Convert.ToInt32(model1.courseId).ToString();
                    textBox11.Text = Convert.ToInt32(model1.locationId).ToString();
                }
            }
        }
        //this will clear all the fields of the enrollment form
        public void ClearFields2()
        {
            textBox10.Text = textBox9.Text = textBox8.Text = comboBox2.Text= comboBox3.Text = comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox9.Text = comboBox10.Text="";
        }
        //this will delete the selected record of enrolllment
        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model1);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.enrollments.Attach(model1);
                    tf.enrollments.Remove(model1);
                    tf.SaveChanges();
                    PopulateGridView2();
                    ClearFields2();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //this will open a payment form 
        private void button11_Click(object sender, EventArgs e)
        {
            EnrollmentPayment fe = new EnrollmentPayment();
            fe.Visible = true;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

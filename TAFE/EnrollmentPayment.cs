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
    public partial class EnrollmentPayment : Form
    {
        fee model3 = new fee();
        suser model4=new suser();

        public EnrollmentPayment()
        {
            InitializeComponent();
            PopulateGridView();
            PopulateGridView2();
        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Enter Amount of Paid fee");
            }

            else if (!ValidateForEmptiness(textBox2.Text))
            {
                MessageBox.Show("Student ID is Required");
            }

            else
            {
                int fId = Convert.ToInt32(textBox1.Text);
                model3.feeId = fId;
                model3.paidfee = comboBox1.Text;

                int stuId = Convert.ToInt32(textBox2.Text);
                model3.studentId = stuId;

                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.fees.Add(model3);

                    tf.SaveChanges();
                }

                MessageBox.Show("Payment made successsfully");
                PopulateGridView();
            }
        }

        private void EnrollmentPayment_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from f in Context.fees

                             select new
                             {
                                 f.feeId,
                                 f.paidfee,
                                 f.studentId
                                 
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int fId = Convert.ToInt32(textBox1.Text);
                var update2 = tf.fees.First(a => a.feeId == fId);
                update2.paidfee = comboBox1.Text;
              
                int studId = Convert.ToInt32(textBox2.Text);
               
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearField();
        }
        public void ClearField()
        {
            textBox1.Text = textBox2.Text= comboBox1.Text= "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model3.feeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["feeId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model3 = tf.fees.Where(x => x.feeId == model3.feeId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model3.feeId).ToString();
                    comboBox1.Text = model3.paidfee;
                  
                    textBox2.Text = Convert.ToInt32(model3.studentId).ToString();
                    
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public void PopulateGridView2()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record2 = from u in Context.susers

                             select new
                             {
                                 u.userId,
                                 u.username,
                                 u.password,
                                 u.userType,
                                 u.studentId
                                   

                             };
                dataGridView2.DataSource = Record2.ToList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          

             if (!ValidateForEmptiness(textBox4.Text))
            {
                MessageBox.Show("Username is Required");
            }
            else if (!ValidateForEmptiness(textBox5.Text))
            {
                MessageBox.Show("Password is Required");
            }
            else if (!ValidateCombobox(comboBox2))
            {
                MessageBox.Show("User Type is Required");
            }
            else if (!ValidateForEmptiness(textBox6.Text))
            {
                MessageBox.Show("Student ID is Required");
            }
            int uId = Convert.ToInt32(textBox3.Text);
            model4.userId = uId;
            model4.username = textBox4.Text;
            model4.password = textBox5.Text;
            model4.userType = comboBox2.Text;
            int stuId = Convert.ToInt32(textBox6.Text);
            model4.studentId = stuId;

            using (tafesystemEntities tf = new tafesystemEntities())
            {
                tf.susers.Add(model4);

                tf.SaveChanges();
            }

            MessageBox.Show("SignUp successsful");
            PopulateGridView2();
            ClearField2();
        }
        public void ClearField2()
        {
            textBox4.Text = textBox3.Text = textBox5.Text = comboBox2.Text = textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int uId = Convert.ToInt32(textBox3.Text);
                var update = tf.susers.First(a => a.userId == uId);
                update.username = textBox4.Text;
                update.password = textBox5.Text;
                update.userType = comboBox2.Text;
               

                int studId = Convert.ToInt32(textBox6.Text);
                update.studentId = studId;
                tf.SaveChanges();

            }
            MessageBox.Show("SignUp Updated sucessfully");
            PopulateGridView2();
            ClearField2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearField2();
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                model4.userId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["userId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model4 = tf.susers.Where(x => x.userId == model4.userId).FirstOrDefault();
                    textBox3.Text = Convert.ToInt32(model4.userId).ToString();
                    textBox4.Text = model4.username;
                    textBox5.Text = model4.password;
                    comboBox2.Text = model4.userType;
                    textBox6.Text = Convert.ToInt32(model4.studentId).ToString();

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model4);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.susers.Attach(model4);
                    tf.susers.Remove(model4);
                    tf.SaveChanges();
                    PopulateGridView2();
                    ClearField2();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
    }

    }

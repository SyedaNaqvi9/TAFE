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
    public partial class NewCourseForm : Form
    {
        course model = new course();
        public NewCourseForm()
        {
            InitializeComponent();
            PopulateGridView();
            ClearFields();
        }
        //validation fro combobox
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


        private void button3_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from c in Context.courses

                             select new
                             {
                                 c.courseId,
                                 c.coursename,
                                 c.mode,
                                 c.fee,
                               
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from c in Context.courses

                             select new
                             {
                                 c.courseId,
                                 c.coursename,
                                 c.mode,
                                 c.fee
                                 
                              };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //this will make user clear all the filled textbox and combobox
        public void ClearFields()
        {
            textBox1.Text = comboBox2.Text = comboBox2.Text = comboBox3.Text = comboBox1.Text = "";

        }
        //from here user can add a course in the system
        private void button1_Click(object sender, EventArgs e)
        {
            //perform validation on the entered data
            if  (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Course Name is Required");
            }

            else if (!ValidateCombobox(comboBox2))
            {
                MessageBox.Show("Course Mode is Required");
            }

            else if (!ValidateCombobox(comboBox3))
                {
                    MessageBox.Show("Course Fee is Required");      
        }
          
            else
            {
                int cId = Convert.ToInt32(textBox1.Text);
                model.courseId = cId;
                model.coursename = comboBox1.Text;
                model.mode = comboBox2.Text;
                model.fee = comboBox3.Text.Trim();

                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.courses.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }
        //this will clear all the fields in the course form
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //this will make the user update the course info
        private void button2_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int cId = Convert.ToInt32(textBox1.Text);
                var update = tf.courses.First(a => a.courseId == cId);
                update.coursename = comboBox1.Text;
                update.mode = comboBox2.Text;
                update.fee= comboBox3.Text;
               
                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
        }
        //this will make the user del the course info
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.courses.Attach(model);
                    tf.courses.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //this will fill the course form with the selected row in the datagrid 
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.courseId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["courseId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.courses.Where(x => x.courseId == model.courseId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model.courseId).ToString();
                    comboBox1.Text = model.coursename;
                    comboBox2.Text = model.mode;
                    comboBox3.Text = model.fee;
                }
            }
        }

        private void NewCourseForm_Load(object sender, EventArgs e)
        {

        }
    }
}

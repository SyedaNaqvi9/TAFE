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
    public partial class NewSemeterForm : Form
    {
        coursesemester model = new coursesemester();
        public NewSemeterForm()
        {
            InitializeComponent();
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
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cs in Context.coursesemesters

                             select new
                             {
                                 cs.semesterId,
                                 cs.semestername,
                                 
                                
                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        public void ClearFields()
        {
            textBox1.Text = comboBox1.Text = "";

        }
        //user can add a semester
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Semester Name is Required");
            }
            else
            {
                int csId = Convert.ToInt32(textBox1.Text);
                model.semesterId = csId;
                model.semestername = comboBox1.Text;



                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.coursesemesters.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }
        //user can update 
        private void button2_Click(object sender, EventArgs e)
        {
            using(tafesystemEntities tf = new tafesystemEntities())
            {
                int csId = Convert.ToInt32(textBox1.Text);
                var update = tf.coursesemesters.First(a => a.semesterId == csId);
                update.semestername = comboBox1.Text;
               


                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
        }
        //user can delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.coursesemesters.Attach(model);
                    tf.coursesemesters.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //user can see all the sem info
        private void button4_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cs in Context.coursesemesters

                             select new
                             {
                                 cs.semesterId,
                                 cs.semestername,
                                

                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //clear all the filled textbox /combobox
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //textbox will be filled according to the selected row in datagrid
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.semesterId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["semesterId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.coursesemesters.Where(x => x.semesterId == model.semesterId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model.semesterId).ToString();
                    comboBox1.Text = model.semestername;
                 
                }
            }
        }
    }
}

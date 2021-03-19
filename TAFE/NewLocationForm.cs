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
    public partial class NewLocationForm : Form
    {
        courselocation model = new courselocation();
        public NewLocationForm()
        {
            InitializeComponent();
            PopulateGridView();
            ClearFields();
        }
        //this will populate datagrid with data
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cl in Context.courselocations
                           

                             select new
                             {
                                 cl.locationId,
                              
                                 cl.locationname


                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //this will clear all the filled textbox
        public void ClearFields()
        {
            textBox1.Text = textBox2.Text="";

        }
        //user can add a location
        private void button1_Click(object sender, EventArgs e)
        {
            int clId = Convert.ToInt32(textBox1.Text);
            model.locationId = clId;
            model.locationname = textBox2.Text;
          

            using (tafesystemEntities tf = new tafesystemEntities())
            {
                tf.courselocations.Add(model);

                tf.SaveChanges();
            }
            ClearFields();
            MessageBox.Show("Data submitted successsfully");
            PopulateGridView();
        }
        //user can update the location info
        private void button2_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int clId = Convert.ToInt32(textBox1.Text);
                var update = tf.courselocations.First(a => a.locationId == clId);
                update.locationname = textBox2.Text;
               


                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
        }
        //user can delete the location info
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.courselocations.Attach(model);
                    tf.courselocations.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //user can see all the loc info
        private void button4_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cl in Context.courselocations

                             select new
                             {
                                 cl.locationId,
                                 cl.locationname,
                                


                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //this will fill the textbox with the selected row in datagrid 
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.locationId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["locationId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.courselocations.Where(x => x.locationId == model.locationId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model.locationId).ToString();
                    textBox2.Text = model.locationname;
                   

                }
            }
        }

        private void NewLocationForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


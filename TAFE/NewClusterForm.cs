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
    public partial class NewClusterForm : Form
    {
        cluster model = new cluster();
        public NewClusterForm()
        {
            InitializeComponent();
            PopulateGridView();
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
        //this will fill the datagrid 
        public void PopulateGridView()
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cl in Context.clusters

                             select new
                             {
                                 cl.clusterId,
                                 cl.clustername,
                                 cl.teacherId
                                

                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //this will clear all the fields 
        public void ClearFields()
        {
            textBox1.Text = textBox2.Text = textBox3.Text= "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //admin can add the cluster infromation in the system
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateForEmptiness(textBox2.Text))
            {
                MessageBox.Show("Enter Cluster Name");
            }
            if (!ValidateForEmptiness(textBox3.Text))
            {
                MessageBox.Show("Enter Teacher ID");
            }
            else {
                int clId = Convert.ToInt32(textBox1.Text);
                model.clusterId = clId;
                model.clustername = textBox2.Text;

                int tId = Convert.ToInt32(textBox3.Text);
                model.teacherId = tId;

                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.clusters.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }
        //this will update the clster info
        private void button2_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int clId = Convert.ToInt32(textBox1.Text);
                var update = tf.clusters.First(a => a.clusterId == clId);
                update.clustername = textBox2.Text;
           
                int tId = Convert.ToInt32(textBox3.Text);
                update.teacherId = tId;
                


                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
        }
        //this will make a cluster delete from the system
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.clusters.Attach(model);
                    tf.clusters.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }
        //a user can see the cluster record 
        private void button4_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from cl in Context.clusters

                             select new
                             {
                                 cl.clusterId,
                                 cl.clustername,
                                
                                 cl.teacherId,
                              


                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        //this will make the selected row in the datagrid to be filled in the cluster form 
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.clusterId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["clusterId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.clusters.Where(x => x.clusterId == model.clusterId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model.clusterId).ToString();
                    textBox2.Text = model.clustername;
                    //comboBox1.Text = model.locationname;
                    textBox3.Text = Convert.ToInt32(model.teacherId).ToString();
                   

                }
            }
        }

        private void NewClusterForm_Load(object sender, EventArgs e)
        {

        }
    }
}

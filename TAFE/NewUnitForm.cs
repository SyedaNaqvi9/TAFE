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
    public partial class NewUnitForm : Form
    {
        unit model = new unit();
        public NewUnitForm()
        {
            InitializeComponent();
            PopulateGridView();
            ClearFields();
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

                var Record = from u in Context.units

                             select new
                             {
                                 u.unitId,
                                 u.unitCode,
                                 u.unitTitle,
                                 u.essential

                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }
        public void ClearFields()
        {
            textBox1.Text = textBox2.Text = textBox3.Text=comboBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateForEmptiness(textBox2.Text))
            {
                MessageBox.Show("Unit Code is Required");
            }
            else if (!ValidateForEmptiness(textBox3.Text))
            {
                MessageBox.Show("Unit Title is Required");
            }

            else if (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Essential is Required");
            }
            else {
                int uId = Convert.ToInt32(textBox1.Text);
                model.unitId = uId;
                model.unitCode = textBox2.Text;
                model.unitTitle = textBox3.Text;
                model.essential = comboBox1.Text.Trim();

                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    tf.units.Add(model);

                    tf.SaveChanges();
                }
                ClearFields();
                MessageBox.Show("Data submitted successsfully");
                PopulateGridView();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                int uId = Convert.ToInt32(textBox1.Text);
                var update = tf.units.First(a => a.unitId == uId);
                update.unitCode = textBox2.Text;
                update.unitTitle = textBox3.Text;
                update.essential = comboBox1.Text;

                tf.SaveChanges();

            }
            MessageBox.Show("Record Updated sucessfully");
            PopulateGridView();
            ClearFields();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {

                var Record = from u in Context.units

                             select new
                             {
                                 u.unitId,
                                 u.unitCode,
                                 u.unitTitle,
                                 u.essential,

                             };
                dataGridView1.DataSource = Record.ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message box", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var entry = tf.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        tf.units.Attach(model);
                    tf.units.Remove(model);
                    tf.SaveChanges();
                    PopulateGridView();
                    ClearFields();
                    MessageBox.Show("Deleted Sucessfully");

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.unitId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["unitId"].Value);
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    model = tf.units.Where(x => x.unitId == model.unitId).FirstOrDefault();
                    textBox1.Text = Convert.ToInt32(model.unitId).ToString();
                    textBox2.Text = model.unitCode;
                    textBox3.Text = model.unitTitle;
                    comboBox1.Text = model.essential;
                }
            }
        }

       
    }
}

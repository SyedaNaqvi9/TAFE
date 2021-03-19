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
    public partial class TeacherMenu : Form
    {
        teacher obj = new teacher();
        int teacherId = Convert.ToInt32(LoginForm.staticuserId);
        public TeacherMenu()
        {
            InitializeComponent();
            textBox1.Text = LoginForm.staticusername;
            if (course.SelectedTab == tabPage1)
            {
                TeacherPersonalDetails();
               
                tabPage1.Text = "Personal Details";
                tabPage2.Text = "Course Details";

            }
            else if (course.SelectedTab == tabPage2)
            { 
               
                CurrentClusterDetails();

                //TeacherPastCourseDetails();
                tabPage1.Text = "Personal Details";
                tabPage2.Text = "Course Details";
            }
        }
        public void TeacherPersonalDetails()
        {
            using (var Context = new tafesystemEntities())
            {
                var result = (from t in Context.teachers
                              where t.teacherId == teacherId
                              select t).FirstOrDefault();
                if (result != null)
                {
                    textBox2.Text = result.firstname;
                    textBox3.Text = result.lastname;
                    textBox6.Text = result.email;
                    textBox7.Text = result.address;
                    textBox5.Text = result.phone;
                    textBox8.Text = result.teacherId.ToString();
                }
            }
        }
        public void TeacherPastCourseDetails()
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from tc in Contex.teachercourses
                              join t in Contex.teachers
                              on tc.teacherId equals t.teacherId
                              where tc.status == "Past"
                              select new
                              {

                                  t.teacherId,
                                  t.firstname,
                                  t.lastname,
                                  tc.coursename,
                                  tc.clustername,
                                  tc.status,

                                  tc.locationname,
                                  tc.semestername,


                              }).ToList();
                if (result != null)
                {
                    dataGridView1.DataSource = result.ToList();
                    dataGridView1.Refresh();
                }
            }
        }
        //it will show all the current cluster the teacher is teaching in a course 
        public void CurrentClusterDetails()
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from tc in Contex.teachercourses
                              join t in Contex.teachers

                              on tc.teacherId equals t.teacherId
                              where tc.teacherId == teacherId
                              join c in Contex.courses
                              on tc.courseId equals c.courseId
                              where tc.courseId == c.courseId

                              select new
                              {
                                  c.courseId,
                                  t.teacherId,
                                  t.firstname,
                                  t.lastname,
                                  tc.coursename,
                                  tc.clustername,
                                  tc.status,

                                  tc.locationname,
                                  tc.semestername,
                              }).ToList();
                if (result != null)
                {
                    dataGridView1.DataSource = result.ToList();
                    dataGridView1.Refresh();
                }
            }
        }
        //Teachers can edit their perosnal details from here 
        private void button2_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {
                var Record = (from t in Context.teachers
                              where t.teacherId == teacherId
                              select t).FirstOrDefault();
                if (Record != null)
                {
                    Record.address = textBox6.Text;
                    Record.phone = textBox7.Text;
                    Record.email = textBox5.Text;
                    Context.SaveChanges();
                    MessageBox.Show("Updated Successfully");
                    TeacherPersonalDetails();
                }
                else
                {
                    MessageBox.Show("Error in Updating Details Try again later!");
                }
            }
        }
       
        // this will make user logout from the system
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm f1 = new LoginForm();
            f1.Visible = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentClusterDetails();
            //TeacherPastCourseDetails();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           // TeacherPastCourseDetails();
            //CurrentClusterDetails();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        //this will show all the course information for a teacher 
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                int SelectedCourseId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["courseId"].Value);
                using (tafesystemEntities db = new tafesystemEntities())
                {
                    var result = (from c in db.courseclusterunits
                                  join clust in db.clusters on c.clusterId equals clust.clusterId
                                  join unit in db.units on c.unitId equals unit.unitId
                                  where c.courseId == SelectedCourseId
                                  select new
                                  {
                                      c.clusterId,
                                      clust.clustername,
                                      unit.unitCode,
                                      unit.unitTitle
                                  }).ToList();
                    if (result != null)
                    {
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = result;
                        dataGridView1.Refresh();
                    }
                }
            }
        }
    }
}

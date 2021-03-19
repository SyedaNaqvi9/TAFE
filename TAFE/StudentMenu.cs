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
    public partial class StudentMenu : Form
    {
        student obj = new student();
        int studentId = Convert.ToInt32(LoginForm.staticuserId);
        public StudentMenu()
        {
            InitializeComponent();
            textBox1.Text = LoginForm.staticusername;
            if(course.SelectedTab==tabPage1)
            {
                StudentPersonalDetails();
                tabPage1.Text = "Personal Details";
                tabPage2.Text = "Course Details";
                tabPage3.Text = "TimeTable";


            }
            else if(course.SelectedTab==tabPage2)
            {
                StudentPastCourseDetails();
                CurrentCourseDetails();
                tabPage1.Text = "Personal Details";
                tabPage2.Text = "Course Details";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        public void StudentPersonalDetails()
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from s in Contex.students
                              where s.studentId == studentId
                              select s).FirstOrDefault();
                if (result != null)
                {
                    textBox2.Text = result.firstname;
                    textBox3.Text = result.lastname;
                    textBox6.Text = result.email;
                    textBox7.Text = result.address;
                    textBox5.Text = result.phone;
                    textBox8.Text = result.studentId.ToString();
                }
            }
        }
        public void StudentPastCourseDetails()
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from E in Contex.enrollments
                              join s in Contex.students
                              on E.studentId equals s.studentId
                              where E.status == "Past"
                              select new
                              {
                                  s.studentId,
                                  s.firstname,
                                  s.lastname,
                                  E.coursename,
                                  
                                  E.locationname,
                                  E.semestername,
                                  
                                  E.result
                              }).ToList();
                if (result != null)
                {
                    dataGridView1.DataSource = result.ToList();
                    dataGridView1.Refresh();
                }
            }
        }
        public void CurrentCourseDetails()
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from E in Contex.enrollments
                              join s in Contex.students
                            
                              on E.studentId equals s.studentId
                              where E.studentId == studentId
                              join c in Contex.courses
                              on E.courseId equals c.courseId
                              where E.courseId == c.courseId

                              select new
                              {
                                  c.courseId,
                                  s.studentId,
                                  s.firstname,
                                  s.lastname,
                                  E.coursename,

                                  E.locationname,
                                  E.semestername,

                                  E.result
                              }).Take(2);
                if (result != null)
                {
                    dataGridView1.DataSource = result.ToList();
                    dataGridView1.Refresh();
                }
            }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        //this will show all the course a student has done or doing at the moment 
        private void button1_Click(object sender, EventArgs e)
        {
            //StudentPastCourseDetails();
            CurrentCourseDetails();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                                  }).ToList();//db.CourseClusterUnits.Where(x => x.CourseId == SelectedCourseId).ToList();
                    if (result != null)
                    {
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = result;
                        dataGridView1.Refresh();
                    }
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }
        //student can edit their perosnal details from here
        private void button2_Click(object sender, EventArgs e)
        {
            using (var Context = new tafesystemEntities())
            {
                var Record = (from t in Context.students
                                        where t.studentId == studentId
                                        select t).FirstOrDefault();
                if (Record != null)
                {
                    Record.address = textBox6.Text;
                    Record.phone = textBox7.Text;
                    Record.email = textBox5.Text;
                    Context.SaveChanges();
                    MessageBox.Show("Updated Successfully");
                    StudentPersonalDetails();
                }
                else
                {
                    MessageBox.Show("Error in Updating Details Try again later!");
                }
            }
        }
        //this will make student logout from the tafe system
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm f1 = new LoginForm();
            f1.Visible = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //this will show the timetable of a course a student is doing 
        private void button3_Click(object sender, EventArgs e)
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from ct in Contex.enrollments
                              join en in Contex.coursetimetables
                              on ct.locationId equals en.locationId
                              where ct.studentId == studentId
                             
                             

                              select new
                              {

                                  en.days,
                                  en.time,
                                  en.room,
                                  en.teachername,
                                  ct.courseId,
                                  ct.coursename,
                                  ct.locationId,
                                  ct.locationname,
                                  //ct.status


                              }).Take(5);
                if (result != null)
                {
                    dataGridView2.DataSource = result.ToList();
                    dataGridView2.Refresh();
                }
            }
        }
    }
    }

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
    public partial class AdminMenu : Form
    {
        //course model = new course();

        public AdminMenu()
        {
            InitializeComponent();
            if (tabControl1.SelectedTab == tabPage1)
            {

                tabPage1.Text = "Students";
                tabPage2.Text = "Teachers";
                tabPage3.Text = "Course";
                tabPage4.Text = "Units";
                tabPage5.Text = "College Locations";
                tabPage6.Text = "Semesters";
                tabPage7.Text = "Clusters";
                tabPage8.Text = "Offered Courses";
                tabPage9.Text = "Offer Courses Details";
            }


        }
        //Validation for comboBox
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
        //Validation for Emptiness of TextBox
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

     

        //this will serach student according to the selection of location and semester made by user 
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox1))
            {
                MessageBox.Show("Please Select Location Name");
            }
            else if (!ValidateCombobox(comboBox2))
            {
                MessageBox.Show("Please Select Semester Name");
            }
            else if (!ValidateCombobox(comboBox3))
            {
                MessageBox.Show("Please Select Mode");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    var Loadstudent = (from en in tf.enrollments
                                       join s in tf.students on en.studentId equals s.studentId
                                       where (en.locationname.Equals(comboBox1.Text))
                                       where (en.semestername.Equals(comboBox2.Text))
                                       where (en.mode.Equals(comboBox3.Text))


                                       select new
                                       {
                                           en.enrollmentId,
                                           s.studentId,
                                           s.firstname,
                                           s.lastname,
                                           s.address,
                                           s.email,
                                           s.phone,
                                           en.semestername,
                                           en.locationname,
                                           en.mode,
                                           en.coursename,
                                           en.fee



                                       });
                    dataGridView1.DataSource = Loadstudent.ToList();
                }
            }
        }
        //this will search the paid fee of the students
        private void button1_Click_1(object sender, EventArgs e)
        {
            if(!ValidateForEmptiness(textBox1.Text))
            {
                MessageBox.Show("Enter the Amount of fee");
            }
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                var loadfee = (from f in tf.fees
                               join s in tf.students on f.studentId equals s.studentId
                               where (f.paidfee.Equals(textBox1.Text))
                               join en in tf.enrollments on s.studentId equals en.studentId
                               where en.studentId == s.studentId

                               //orderby od.OrderID
                               select new
                               {
                                   f.feeId,
                                   f.paidfee,
                                   s.studentId,
                                   s.firstname,
                                   s.lastname,
                                   s.address,
                                   s.email,
                                   s.phone,
                                   en.coursename,
                                   en.courseId,
                                   en.locationname,
                                   en.semestername


                               });
                dataGridView1.DataSource = loadfee.ToList();

            }
        }
        //this will serach teacher based on a location

        private void button5_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox8))
            {
                MessageBox.Show("Please Select Location Name");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {

                    var loadteacher = (from t in tf.teachers
                                       join tc in tf.teachercourses on t.teacherId equals tc.teacherId
                                       where tc.locationname.Equals(comboBox8.Text)

                                       select new
                                       {
                                           t.teacherId,
                                           t.firstname,
                                           t.lastname,
                                           t.email,
                                           t.phone,
                                           t.address,
                                           tc.locationname,
                                           tc.semestername


                                       });
                    dataGridView4.DataSource = loadteacher.ToList();
                }
            }
        }
    //this will display all the courses that are in the system but not offered by tafe at the moment 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadnotoffer = (from c in tf.courses
                                    where !tf.Inlocationcourses.Any(cl => cl.courseId == c.courseId)
                                    select new
                                    {
                                        c.courseId,
                                        c.coursename
                                    });




                dataGridView5.DataSource = loadnotoffer.ToList();

            }
        }
        //this will display all the past courses in tafe
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
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
                                  tc.status,
                                  tc.coursename,
                                  tc.semestername,
                                  tc.locationname,
                                  tc.clustername,
                                  
                              }).ToList();
                if (result != null)
                {
                    dataGridView4.DataSource = result.ToList();
                    dataGridView4.Refresh();
                }
            }

        }
        //this will show all the unit who are not associated wih any cluster or course
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadunitalloct = (from u in tf.units
                                      where !tf.courseclusterunits.Any(ccu => ccu.unitId == u.unitId)
                                      select new
                                      {
                                          u.unitId,
                                          u.unitCode,
                                          u.unitTitle
                                      });




                dataGridView6.DataSource = loadunitalloct.ToList();

            }
        }
        //this will display the cluster,unit and location of a particular course 
        private void button7_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox6))
            {
                MessageBox.Show("Please Select Course Name");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    
                    var loadunitcluster = (from ccu in tf.courseclusterunits
                                           join c in tf.courses on ccu.courseId equals c.courseId
                                           where c.coursename.Equals(comboBox6.Text)
                                           join cl in tf.Inlocationcourses on c.courseId equals cl.courseId
                                           join loc in tf.courselocations on cl.locationId equals loc.locationId

                                           join cs in tf.Incoursesemesters on c.courseId equals cs.courseId
                                           join sem in tf.coursesemesters on cs.semesterId equals sem.semesterId
                                         
                                           join clu in tf.clusters on ccu.clusterId equals clu.clusterId
                                           join u in tf.units on ccu.unitId equals u.unitId




                                           orderby cs.semesterId
                                          
                                           select new
                                           {
                                               cs.semesterId,
                                               c.courseId,
                                               c.coursename,
                                               clu.clustername,
                                               u.unitCode,
                                               u.unitTitle,
                                               u.essential,
                                               sem.semestername,
                                               loc.locationname

                                           });
                    dataGridView6.DataSource = loadunitcluster.ToList();
                }
            }
        }

        //this will search the student according to the selection of course and location of user 
        private void button8_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox10))
            {
                MessageBox.Show("Please Select Course Name");
            }
            else if (!ValidateCombobox(comboBox9))
            {
                MessageBox.Show("Please Select Semester Name");
            }
            using (var Contex = new tafesystemEntities())
            {
                var result = (from E in Contex.enrollments
                              join s in Contex.students

                              on E.studentId equals s.studentId
                              where E.coursename.Equals(comboBox10.Text)
                              
                               where E.semestername.Equals(comboBox9.Text)

                              select new
                              {
                                 
                                  s.studentId,
                                  s.firstname,
                                  s.lastname,
                                  E.coursename,

                                  E.locationname,
                                  E.semestername,

                                  E.result
                              });
                if (result != null)
                {
                    dataGridView1.DataSource = result.ToList();
                    dataGridView1.Refresh();
                }
            }
        }
// this will search teacher based on a coursename and semester
        private void button9_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox11))
            {
                MessageBox.Show("Please Select Course Name");
            }
            else if (!ValidateCombobox(comboBox12))
            {
                MessageBox.Show("Please Select Semester Name");
            }
            using (var Contex = new tafesystemEntities())
            {
                var result = (from tc in Contex.teachercourses
                              join t in Contex.teachers

                              on tc.teacherId equals t.teacherId
                              where tc.coursename.Equals(comboBox11.Text)

                              where tc.semestername.Equals(comboBox12.Text)

                              select new
                              {

                                  t.teacherId,
                                  t.firstname,
                                  t.lastname,
                                  tc.coursename,

                                  tc.locationname,
                                  tc.semestername,

                                  
                              });
                if (result != null)
                {
                    dataGridView4.DataSource = result.ToList();
                    dataGridView4.Refresh();
                }
            }
        }
//this will display all the part time teachers
        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            using (var Contex = new tafesystemEntities())
            {
                var result = (from tc in Contex.teachercourses
                              join t in Contex.teachers
                              on tc.teacherId equals t.teacherId
                              where tc.mode == "Part-Time"
                              select new
                              {
                                  t.teacherId,
                                  t.firstname,
                                  t.lastname,
                                  tc.status,
                                  tc.semestername,
                                  tc.locationname,
                                  tc.clustername,

                              }).ToList();
                if (result != null)
                {
                    dataGridView4.DataSource = result.ToList();
                    dataGridView4.Refresh();
                }
            }

        }
        //it will display all the course in all location 
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                
                var loadlocation = (from c in tf.courses

                                  join cl in tf.Inlocationcourses on c.courseId equals cl.courseId
                                  join l in tf.courselocations on cl.locationId equals l.locationId 
                                  select new
                                  {

                                      c.courseId,
                                      c.coursename,
                                      l.locationname

                                  });
                dataGridView7.DataSource = loadlocation.ToList();

            }
        }
        //this will load semster information
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadsemester = (from c in tf.coursesemesters

                                  

                                    select new
                                    {
                                        c.semesterId,
                                        c.semestername
                                    });
                dataGridView8.DataSource = loadsemester.ToList();

            }
        }

        //this will open a new student form and admin can add,delete,update and see all students record
        private void button10_Click_1(object sender, EventArgs e)
        {
            NewStudent fn = new NewStudent();
            fn.Visible = true;

        }

        //this will open a new course form where admin can add delete, see and update teacher records 
        private void button12_Click(object sender, EventArgs e)
        {
            NewTeacherForm f1 = new NewTeacherForm();
            f1.Visible = true;
            this.Close();
        }
        //this will open a new course form so admin can add delete see and update all courses in system
        private void button13_Click(object sender, EventArgs e)
        {
            NewCourseForm fc = new NewCourseForm();
            fc.Visible = true;
            this.Close();
        }
        //this will open a new unit form so admin can see and update,delete and add a units
        private void button14_Click(object sender, EventArgs e)
        {
            NewUnitForm fu = new NewUnitForm();
            fu.Visible = true;
            this.Close();
        }
        //this will open a new course form  so admin can add, updaate,see and delete a course location
        private void button15_Click(object sender, EventArgs e)
        {
            NewLocationForm fl = new NewLocationForm();
            fl.Visible = true;
            
        }
        //this will open a new semester form
        private void button16_Click(object sender, EventArgs e)
        {
            NewSemeterForm fs = new NewSemeterForm();
            fs.Visible = true;
        }

        //this will make the user logout from the system
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm fl = new LoginForm();
            fl.Visible = true;
            this.Close();
        }


        //this will validate the courseId and semster name and then given the infromation about a course and semester 
        private void button17_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox14))
            {
                MessageBox.Show("Please Select Course ID");
            }
            else if (!ValidateCombobox(comboBox13))
            {
                MessageBox.Show("Please Select Semester Name");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                    int courId = Convert.ToInt32(comboBox14.Text);
                    var loadoffer = (from ccu in tf.courseclusterunits
                                     join c in tf.Inlocationcourses on ccu.courseId equals c.courseId

                                     where c.courseId == courId


                                     join cs in tf.Incoursesemesters on c.courseId equals cs.courseId
                                     join css in tf.coursesemesters on cs.semesterId equals css.semesterId
                                     where css.semestername.Equals(comboBox13.Text)

                                     join clu in tf.clusters on ccu.clusterId equals clu.clusterId
                                     join cl in tf.courselocations on c.locationId equals cl.locationId
                                     join u in tf.units on ccu.unitId equals u.unitId
                                     join t in tf.teachers on clu.teacherId equals t.teacherId

                                     orderby cs.semesterId
                                     orderby cl.locationname
                                     select new
                                     {
                                         cs.semesterId,
                                         ccu.clusterId,
                                         ccu.unitId,
                                         c.courseId,

                                         clu.clustername,
                                         u.unitCode,
                                         u.unitTitle,
                                         u.essential,
                                         t.teacherId,
                                         t.firstname,
                                         t.middlename,
                                         t.lastname,

                                         cl.locationId,
                                         cl.locationname,

                                         css.semestername


                                     });
                    dataGridView9.DataSource = loadoffer.ToList();
                }
            }
        }
      
        //this will give the locations for all the courses avaliable 
        private void button3_Click_1(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {
                
                var loadcourse = (from c in tf.courses

                                  join cl in tf.Inlocationcourses on c.courseId equals cl.courseId
                                  join loc in tf.courselocations on cl.locationId equals loc.locationId
                
                                  select new
                                  {

                                      c.courseId,
                                      c.coursename,
                                      loc.locationname,


                                  });
                dataGridView2.DataSource = loadcourse.ToList();

            }
        }
        //this will open a new cluster form
        private void button4_Click_1(object sender, EventArgs e)
        {
            NewClusterForm fc = new NewClusterForm();
            fc.Visible = true;
        }
        //this will show all the cluster and unit infromation in all locations 
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadcluster = (from clus in tf.clusters
                                  
                                   join loc in tf.clusterlocations on clus.clusterId equals loc.clusterId


                                   select new
                                   {

                                       clus.clusterId,
                                       clus.clustername,
                                     
                                       loc.locationId,
                                       
                                    


                                   });
                dataGridView3.DataSource = loadcluster.ToList();

                }
            }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox4))
            {
                MessageBox.Show("Please Select Location Name");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {

                    var loadteacher = (from t in tf.teachers
                                       join tc in tf.teachercourses on t.teacherId equals tc.teacherId
                                       where tc.locationname.Equals(comboBox4.Text)
                                       join c in tf.courses on tc.courseId equals c.courseId

                                       select new
                                       {
                                           t.teacherId,
                                           c.courseId,
                                           c.coursename,
                                           t.firstname,
                                           t.lastname,
                                           t.email,
                                           t.phone,
                                           t.address,
                                           tc.locationname,
                                           tc.semestername


                                       });
                    dataGridView7.DataSource = loadteacher.ToList();
                }
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox5))
            {
                MessageBox.Show("Please Select Semester Name");
            }
            else
            {
                using (var Contex = new tafesystemEntities())
                {
                    var result = (from c in Contex.coursesemesters
                                  where c.semestername.Equals(comboBox5.Text)

                                  select new
                                  {
                                      c.semesterId,
                                      c.semestername


                                  });
                    if (result != null)
                    {
                        dataGridView8.DataSource = result.ToList();
                        dataGridView8.Refresh();
                    }
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (!ValidateCombobox(comboBox7))
            {
                MessageBox.Show("Please Select Location Name");
            }
            else
            {
                using (tafesystemEntities tf = new tafesystemEntities())
                {

                    int locId = Convert.ToInt32(comboBox7.Text);
                    var loadsemesterlocation = (from cs in tf.Incoursesemesters
                                                    // join tc in tf.teachercourses on t.teacherId equals tc.teacherId
                                                join cl in tf.Inlocationcourses on cs.courseId equals cl.courseId
                                                where cl.locationId == locId


                                                select new
                                                {
                                                    cs.semesterId,
                                                    cl.locationId


                                                }); ; ; ;
                    dataGridView8.DataSource = loadsemesterlocation.ToList();
                }
            }
        }
        public void ClearFields()
        {
            comboBox1.Text = comboBox10.Text = comboBox9.Text = comboBox2.Text =comboBox3.Text=textBox1.Text= "";
        }
        public void ClearFields1()
        {
            comboBox8.Text = comboBox11.Text = comboBox11.Text = "";
        }
        //this will clear the form 
        private void button19_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //this will clear the form 
        private void button20_Click(object sender, EventArgs e)
        {
            ClearFields1();
        }
        //this will clear the form 
        public void ClearFields2()
        {
             comboBox6.Text = "";
        }
        //this will clear the form 
        public void ClearFields3()
        {
            comboBox4.Text = "";
        }
        //this will clear the form 
        public void ClearFields4()
        {
            comboBox5.Text = comboBox7.Text = "";
        }
        //this will clear the form 
        public void ClearFields5()
        {
            comboBox13.Text = comboBox14.Text="";
        }
        //this will clear the form 

        private void button22_Click(object sender, EventArgs e)
        {
            ClearFields2();
        }
        //this will clear the form 
        private void button21_Click(object sender, EventArgs e)
        {
            ClearFields3();
        }
        //this will clear the form 

        private void button23_Click(object sender, EventArgs e)
        {
            ClearFields4();
        }
        //this will clear the form 

        private void button24_Click(object sender, EventArgs e)
        {
            
            ClearFields5();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadnotunit = (from u in tf.units
                                  // join ccu in tf.courseclusterunits on u.unitId  equals ccu.unitId
                                   where !tf.courseclusterunits.Any(ccu => u.unitId == ccu.unitId)
                                   
                                    select new
                                    {
                                       
                                        u.unitId,
                                        u.unitTitle,
                                        u.unitCode
                                       
                                    });




                dataGridView6.DataSource = loadnotunit.ToList();

            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            using (tafesystemEntities tf = new tafesystemEntities())
            {

                var loadcluster = (from clus in tf.Inlocationcourses

                                   join loc in tf.coursetimetables on clus.courseId equals loc.courseId
                                   //join c in tf.teachercourses on  loc.courseId equals c.courseId 
                                 //  join cs in tf.Incoursesemesters on c.courseId equals cs.courseId

                                   select new
                                   {

                                       //clus.clusterId,
                                       //clus.clustername,
                                       loc.days,
                                       loc.time,
                                       loc.teachername,
                                       loc.room,
                                       loc.courseId,
                                       loc.locationId




                                   });
                dataGridView10.DataSource = loadcluster.ToList();

            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
          
                using (tafesystemEntities tf = new tafesystemEntities())
                {
                int loctId =Convert.ToInt32(comboBox15.Text);
                    var loadtimetable = (from clus in tf.Inlocationcourses
                                       join loc in tf.coursetimetables on clus.locationId equals loc.locationId
                                       where loc.locationId==loctId
                                       //join c in tf.courses on tc.courseId equals c.courseId
                                       //join tt in tf.teachercourses on  tc.courseId equals tt.courseId
                                       //join cs in tf.Incoursesemesters on c.courseId equals cs.courseId
                                       select new
                                       {
                                           loc.days,
                                           loc.time,
                                           loc.teachername,
                                           loc.room,
                                           loc.courseId,
                                           loc.locationId





                                       });
                    dataGridView10.DataSource = loadtimetable.ToList();
                }
            }

        
    }
    }



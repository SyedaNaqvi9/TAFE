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
    public partial class LoginForm : Form
    {
        public static string staticusername;
        public static int staticuserId;
        // int studentid = Convert.ToInt32(user.staticuserId);
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("User Name is required");
            }

            else if (textBox2.Text == "")
            {
                MessageBox.Show("Password is required");
            }
            else
            {
                using (var Context = new tafesystemEntities())
                {
                    var result1 = (from user in Context.susers
                                   where user.username == textBox1.Text && user.password == textBox2.Text
                                   select user).FirstOrDefault();

                    if (result1 != null)
                    {
                        staticuserId = result1.userId;
                        staticusername = textBox1.Text;

                        if (result1.userType == "Student")
                        {
                            StudentMenu f2 = new StudentMenu();
                            f2.Text = "Logged In as: " + textBox1.Text + " & Your StudentID is: " + result1.userId;
                            f2.Show();
                            this.Hide();
                        }
                    }
                    // }
                    else
                    {
                        using (var Context2 = new tafesystemEntities())
                        {
                            var result2 = (from user in Context2.tusers
                                           where user.username == textBox1.Text && user.password == textBox2.Text
                                           select user).FirstOrDefault();

                            if (result2 != null)
                            {
                                staticuserId = result2.userId;
                                staticusername = textBox1.Text;
                                if (result2.userType == "Teacher")
                                {
                                    TeacherMenu f3 = new TeacherMenu();
                                    f3.Text = "Logged In as: " + textBox1.Text + " & Your TeacherID is: " + result2.userId;
                                    f3.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                using (var Context3 = new tafesystemEntities())
                                {
                                    var result3 = (from user in Context3.ausers
                                                   where user.username == textBox1.Text && user.password == textBox2.Text
                                                   select user).FirstOrDefault();

                                    if (result3 != null)
                                    {
                                        staticuserId = result3.userId;
                                        staticusername = textBox1.Text;
                                        if (result3.userType == "Admin")
                                        {
                                            AdminMenu f3 = new AdminMenu();
                                            f3.Text = "Logged In as: " + textBox1.Text + " & Your AdminID is: " + result3.userId;
                                            f3.Show();
                                            this.Hide();
                                        }
                                    }


                                    else
                                    {
                                        MessageBox.Show("Invalid Userid/password");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    

                        private void button2_Click(object sender, EventArgs e)
                        {
                            Close();
                        }
                    }
                }
            


    


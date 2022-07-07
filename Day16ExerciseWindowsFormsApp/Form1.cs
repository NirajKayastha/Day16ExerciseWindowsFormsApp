using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Day16ExerciseWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //add values in combobox
            comboGender.Text = "--Select--";
            comboGender.Items.Add("Male");
            comboGender.Items.Add("Female");
            comboGender.Items.Add("Others");
       

        }
        private void ValidateEmail()
        {
            try
            {   // email validation
                string email = txtEmaild.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                    MessageBox.Show(email + " is Valid Email Address");
                
                else
                    MessageBox.Show(email + " is Invalid Email Address");
                  // ClearText();
                //txtEmaild.Focus();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            ValidateEmail();
          
            try
            {
                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRConn"].ConnectionString))
                {
                    using (cmd = new SqlCommand("usp_AddNewUser", con)) // sp to add new user
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@Birthdate", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@Gender", comboGender.Text);
                        cmd.Parameters.AddWithValue("@Emaild", txtEmaild.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", txtConfirmPassword.Text);


                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();

                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("New User Created");
            }
            catch (Exception ex)
            {

         
            }
          


        }
        private void ClearText()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            comboGender.Text = "";
            txtEmaild.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearText();//method call
    
        }

        private void txtEmaild_TextChanged(object sender, EventArgs e)
        {
            //ValidateEmail(); //method call
        }
    }
}

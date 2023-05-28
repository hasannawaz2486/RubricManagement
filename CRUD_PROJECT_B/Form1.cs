using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using CRUD1_PROJECT_B;
using MaterialSkin.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using iText.Pdfa;
using itextsharp;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using itextsharp.pdfa;

namespace CRUD_PROJECT_B
{
    public partial class Form1 : MaterialForm
    {

        readonly MaterialSkin.MaterialSkinManager materialSkinManager;

        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo500, MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Indigo100, MaterialSkin.Accent.Pink200, MaterialSkin.TextShade.WHITE);
        }

        private void mcboxtxtbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MBInsert_Click(object sender, EventArgs e)
        {

            if (firstnametxtbox.Text != "" && lastnametxtbox.Text != "" && contacttxtbox.Text != "" && emailtxtbox.Text != "" && reginotxtbox.Text != "" && MCBstatus.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName,@Contact,@Email,@RegistrationNumber,@Status)", con);
                //cmd.Parameters.AddWithValue("@id", idtxtbox.Text);
                cmd.Parameters.AddWithValue("@FirstName", firstnametxtbox.Text);
                cmd.Parameters.AddWithValue("@LastName", lastnametxtbox.Text);
                cmd.Parameters.AddWithValue("@Contact", contacttxtbox.Text);
                cmd.Parameters.AddWithValue("@Email", emailtxtbox.Text);
                cmd.Parameters.AddWithValue("@RegistrationNumber", reginotxtbox.Text);
                if (MCBstatus.Text == "Active")
                {
                    cmd.Parameters.AddWithValue("@Status", 5);
                    // cmd.Parameters.
                    //cmd.Parameters.RemoveAt("@Status");


                }
                else
                {

                    cmd.Parameters.AddWithValue("@Status", 6);
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                cleardata();
                databind();
            }
            else
            {
                MessageBox.Show(" please enter all details of student", "Warning");

            }
        }

        private void MBremove_Click(object sender, EventArgs e)
        {

            // it will not delete the student from the dataBase it will just simply update the id of the student from  5 to 6

            if (MessageBox.Show("do you want permanat delete or not", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!(reginotxtbox.Text == ""))
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("DELETE StudentResult From Student  WHERE Student.Id=StudentResult.StudentId and Student.RegistrationNumber=@reg ", con);
                    cmd.Parameters.AddWithValue("@reg", reginotxtbox.Text);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand("DELETE StudentAttendance From Student  WHERE  Student.Id=StudentAttendance.StudentId and Student.RegistrationNumber=@reg ", con);
                    cmd1.Parameters.AddWithValue("@reg", reginotxtbox.Text);
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("DELETE Student WHERE RegistrationNumber=@reg1 ", con);
                    cmd2.Parameters.AddWithValue("@reg1", reginotxtbox.Text);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("deleted succesfuly");
                    databind();

                }
                else
                {
                    MessageBox.Show("Enter registration to delete student");
                }



                // this code will permanently delete the student from the database and all other tables
                //try
                //{
                //var con1 = Configuration.getInstance().getConnection();

                ////string q1 = "DELETE StudentResult From Student  WHERE Student.Id=StudentResult.StudentId and Student.RegistrationNumber=@reg";
                ////string q2 = "DELETE StudentAttendance From Student  WHERE  Student.Id=StudentAttendance.StudentId and Student.RegistrationNumber=@reg";

                //string query1 = "DELETE StudentResult From Student  WHERE Student.Id=StudentResult.StudentId and Student.RegistrationNumber=@reg1 ";
                //string Query2 = "DELETE StudentAttendance From Student  WHERE  Student.Id=StudentAttendance.StudentId and Student.RegistrationNumber=@reg1 ";
                //string query3 = "DELETE Student WHERE RegistrationNumber=@reg3";

                //SqlCommand cmd1 = new SqlCommand(query1, con1);
                //cmd1.Parameters.AddWithValue("@reg1", reginotxtbox.Text);
                //cmd1.ExecuteNonQuery();
                //SqlCommand cmd_1 = new SqlCommand(Query2, con1);
                //cmd1.Parameters.AddWithValue("@reg1", reginotxtbox.Text);
                //cmd1.ExecuteNonQuery();
                //SqlCommand cmd_2 = new SqlCommand(query3, con1);
                //cmd_2.Parameters.AddWithValue("@reg3", reginotxtbox.Text);
                //cmd_2.ExecuteNonQuery();
                //MessageBox.Show("delted");
                //databind();


                //    ////////////////////////////////////////////////////////

                //    //string qopa1="  DELETE FROM StudentResult WHERE StudentId IN(SELECT Id FROM Student WHERE RegistrationNumber = @reg)";

                //    //string qopa2=" DELETE FROM StudentAttendance  WHERE StudentId IN(SELECT Id FROM Student WHERE RegistrationNumber = @reg2)";

                //    // string qqqqq3=" DELETE FROM Student  WHERE RegistrationNumber = @reg3";

                //    //  SqlCommand cmd1 = new SqlCommand(qopa1, con1);
                //    //  cmd1.Parameters.AddWithValue("@reg", reginotxtbox.Text);
                //    //  cmd1.ExecuteNonQuery();
                //    //  SqlCommand cmd_1 = new SqlCommand(qopa2, con1);
                //    //  cmd1.Parameters.AddWithValue("@reg2", reginotxtbox.Text);
                //    //  cmd1.ExecuteNonQuery();
                //    //  SqlCommand cmd_2 = new SqlCommand(qqqqq3, con1);
                //    //  cmd_2.Parameters.AddWithValue("@reg3", reginotxtbox.Text);
                //    //  cmd_2.ExecuteNonQuery();

                //    ///////////////////////////////////////////////////


                //    // String qu_1 = "DELETE sr, sa, s  FROM StudentResult  as  sr  JOIN Student  as s ON s.Id = sr.StudentId JOIN StudentAttendance as  sa ON sa.StudentId = s.Id WHERE s.RegistrationNumber = @reg";
                //    //again
                //    //string qq1 = "DELETE StudentResult, StudentAttendance, Student\r\nFROM Student\r\nLEFT JOIN StudentResult ON Student.Id = StudentResult.StudentId\r\nLEFT JOIN StudentAttendance ON Student.Id = StudentAttendance.StudentId\r\nWHERE Student.RegistrationNumber = @reg";
                //    //again nd again
                //    //string qqq = "WITH cte AS (\r\n    SELECT s.Id\r\n    FROM Student s\r\n    WHERE s.RegistrationNumber = @reg\r\n)\r\nDELETE FROM StudentResult \r\nWHERE StudentId IN (SELECT Id FROM cte);\r\n\r\nDELETE FROM StudentAttendance \r\nWHERE StudentId IN (SELECT Id FROM cte);\r\n\r\nDELETE FROM Student \r\nWHERE Id IN (SELECT Id FROM cte)";

                //    //SqlCommand cmd1__ = new SqlCommand(qqq, con1);
                //    //cmd1__.Parameters.AddWithValue("@reg", reginotxtbox.Text);
                //    //cmd1__.ExecuteNonQuery();
                //    ////////////////////////////
                //    ///
                //    string pola = reginotxtbox.Text;
                //    MessageBox.Show(pola);
                //    string qopa1 = "  DELETE FROM StudentResult WHERE StudentId IN(SELECT Id FROM Student WHERE RegistrationNumber = '"+ pola + "')";

                //    string qopa2 = " DELETE FROM StudentAttendance  WHERE StudentId IN(SELECT Id FROM Student WHERE RegistrationNumber = '"+ pola + "')";

                //    string qqqqq3 = " DELETE FROM Student  WHERE RegistrationNumber = '"+ pola + "'";

                //    SqlCommand cmd1 = new SqlCommand(qopa1, con1);
                //   // cmd1.Parameters.AddWithValue("@reg", reginotxtbox.Text);
                //    cmd1.ExecuteNonQuery();
                //    SqlCommand cmd_1 = new SqlCommand(qopa2, con1);
                //    //cmd1.Parameters.AddWithValue("@reg2", reginotxtbox.Text);
                //    cmd1.ExecuteNonQuery();
                //    SqlCommand cmd_2 = new SqlCommand(qqqqq3, con1);
                //   // cmd_2.Parameters.AddWithValue("@reg3", reginotxtbox.Text);
                //    cmd_2.ExecuteNonQuery();
                //    cleardata();
                //    databind();
                //    MessageBox.Show("Deleted Succesfully");
                //}
                //catch(Exception exc)
                //{
                //    MessageBox.Show(exc.Message);
                //}
            }
            else
            {
                var con = Configuration.getInstance().getConnection();
                string var1 = reginotxtbox.Text;
                int valu1 = 6;
                SqlCommand cmd = new SqlCommand("update  Student set Status= " + valu1 + " Where RegistrationNumber='" + var1 + "'", con);
                string id_ = reginotxtbox.Text.ToString();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully removed");
                databind();
            }


        }

        private void MBsearch_Click(object sender, EventArgs e)
        {

            string var1 = mcboxtxtbox.Text;
            string var2 = mcboxtxtbox.SelectedText.ToString();
            if (var1 != "")
            {


                if (var1 == "first name")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @FirstName=FirstName", con);
                    cmd.Parameters.AddWithValue("@FirstName", firstnametxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();

                }
                else if (var1 == "last name")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @LastName=LastName", con);
                    cmd.Parameters.AddWithValue("@LastName", lastnametxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();

                }
                else if (var1 == "email")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @Email=Email", con);
                    cmd.Parameters.AddWithValue("@Email", emailtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();
                }
                else if (var1 == "registrationNum")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @RegistrationNumber=RegistrationNumber", con);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", reginotxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();

                }
                else if (var1 == "contact")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @Contact=Contact", con);
                    cmd.Parameters.AddWithValue("@Contact", contacttxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();
                }
                else if (var1 == "status")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Email,Contact,RegistrationNumber,status from Student where @Status=Status", con);

                    if (MCBstatus.Text == "Active")
                    {
                        cmd.Parameters.AddWithValue("@Status", 5);

                    }
                    else
                    {

                        cmd.Parameters.AddWithValue("@Status", 6);
                    }
                    //cmd.Parameters.AddWithValue("@Contact", contacttxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DGStudentinfo.DataSource = dt;
                    cleardata();
                }
            }
            else
            {
                cleardata();
                MessageBox.Show("Error", "warning");
            }
        }

        private void MBupdate_Click(object sender, EventArgs e)
        {

            if (firstnametxtbox.Text != "" && lastnametxtbox.Text != "" && contacttxtbox.Text != "" && emailtxtbox.Text != "" && reginotxtbox.Text != "" && MCBstatus.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("update  Student set FirstName=@FirstName ,LastName=@LastName,Contact=@Contact,Email=@Email,RegistrationNumber=@RegistrationNumber ,Status=@Status   where RegistrationNumber=@RegistrationNo", con);
                //cmd.Parameters.AddWithValue("@id", idtxtbox.Text);
                cmd.Parameters.AddWithValue("@FirstName", firstnametxtbox.Text);
                cmd.Parameters.AddWithValue("@LastName", lastnametxtbox.Text);
                cmd.Parameters.AddWithValue("@Contact", contacttxtbox.Text);
                cmd.Parameters.AddWithValue("@Email", emailtxtbox.Text);
                cmd.Parameters.AddWithValue("@RegistrationNo", reginotxtbox.Text);
                if (MCBstatus.Text == "Active")
                {
                    cmd.Parameters.AddWithValue("@Status", 5);

                }
                else
                {

                    cmd.Parameters.AddWithValue("@Status", 6);
                }
                // cmd.Parameters.AddWithValue("@Status", MCBstatus.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated succesfully");
                cleardata();
                databind();
            }
            else
            {

                MessageBox.Show(" please fill data first", "Warning");

            }





        }

        private void MBShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Student where Status <> 6", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DGStudentinfo.DataSource = dt;
            DGStudentinfo.Columns["ID"].Visible = false;
            DGStudentinfo.Columns["Status"].Visible = false;



        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       public  void cleardata()
        {
            firstnametxtbox.Text = "";
            lastnametxtbox.Text = "";
            contacttxtbox.Text = "";
            emailtxtbox.Text = "";
            reginotxtbox.Text = "";
            MCBstatus.Text = "";

        }
        public void cleardataclo()
        {
            clonametxtbox.Text = "";
            clodatecreatedtxtbox.Text = "";
            clodateupdatedtxtbox.Text = "";
            DTP_DUtxtbox.Text = "";
            DTP_DCtxtbox.Text = "";
          
        }

       public  void clearDateAssessment()
        {
            Titletxtbox.Text = "";
            DTP_MASS.Text = "";
            totalmarkstxtbox.Text = "";
            twtxtbox.Text = "";
        }
       public  void databind()
        {
            DGStudentinfo.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DGStudentinfo.DataSource = dt;
            DGStudentinfo.Columns["ID"].Visible = false;
            DGStudentinfo.Refresh();
        }
        public void databindclo()
        {
            clogridview.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            clogridview.DataSource = dt;
           // clogridview.Columns["ID"].Visible = false;
            clogridview.Refresh();
        }



        void databindassesment()
        {
            dgvmanageass.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvmanageass.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
            dgvmanageass.Refresh();
        }
        void cleardatamanageassessment()
        {
            Titletxtbox.Text = "";
            DTP_MASS.Text = "";
            totalmarkstxtbox.Text = "";
            twtxtbox.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectBDataSet.Clo' table. You can move, or remove it, as needed.
            this.cloTableAdapter.Fill(this.projectBDataSet.Clo);
            materialLabel1.Visible = false;
            idtxtbox.Visible = false;
        }

        private void DGStudentinfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGStudentinfo.CurrentRow.Selected = true;
            firstnametxtbox.Text = DGStudentinfo.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
            lastnametxtbox.Text = DGStudentinfo.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
            contacttxtbox.Text = DGStudentinfo.Rows[e.RowIndex].Cells["Contact"].Value.ToString();
            emailtxtbox.Text = DGStudentinfo.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            reginotxtbox.Text = DGStudentinfo.Rows[e.RowIndex].Cells["RegistrationNumber"].Value.ToString();
            MCBstatus.Text = DGStudentinfo.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            //txtaddress.Text = DGStudentinfo.Rows[e.RowIndex].Cells["Address"].Value.ToString();
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void marktheevalutionpg_Click(object sender, EventArgs e)
        {

        }

        private void ManageRubricLevelpg_Click(object sender, EventArgs e)
        {

        }

        private void ManageAssessmentspg_Click(object sender, EventArgs e)
        {

        }

        private void ManageRubricpg_Click(object sender, EventArgs e)
        {

        }

        private void ManageCLOsPG_Click(object sender, EventArgs e)
        {

        }

        private void ManageStudentpg_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contacttxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialLabel5_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel7_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel6_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel4_Click(object sender, EventArgs e)
        {

        }

        private void idtxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstnametxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastnametxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailtxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void reginotxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialLabel8_Click(object sender, EventArgs e)
        {

        }

        private void MCBstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            clogridview.DataSource = dt;
            // clogridview.Columns["ID"].Visible = false;
        }

        private void btnInsertCLO_Click(object sender, EventArgs e)
        {

            if (clonametxtbox.Text != "" && DTP_DCtxtbox.Text != "" && DTP_DUtxtbox.Text != "")//&& emailtxtbox.Text != "" && reginotxtbox.Text != "" && MCBstatus.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name, @DateCreated,@DateUpdated)", con);
                cmd.Parameters.AddWithValue("@Name", clonametxtbox.Text);
                cmd.Parameters.AddWithValue("@DateCreated", DTP_DCtxtbox.Text);
                cmd.Parameters.AddWithValue("@DateUpdated", DTP_DUtxtbox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                cleardataclo();
                databindclo();
            }
            else
            {
                MessageBox.Show(" please enter all details of student", "Warning");

            }
        }

        private void btnremove_Click(object sender, EventArgs e)//facing 1 dificulty while date and time
        {
            //try
            //{


            //        var con = Configuration.getInstance().getConnection();
            //        SqlCommand cmd = new SqlCommand("DELETE  from Clo Where Id=@Id", con);
            //        string id = clonametxtbox.Text.ToString();
            //        cmd.Parameters.AddWithValue("@Id", int.Parse(IdforClotodelete.Text));
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("Successfully removed");
            //        databindclo();




            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);

            //}




            // asli wala delete

            //bc 
            try
            {


                var con = Configuration.getInstance().getConnection();
                string Query1 = "DELETE StudentResult FROM StudentResult JOIN RubricLevel ON RubricLevel.Id = StudentResult.RubricMeasurementId JOIN AssessmentComponent ON AssessmentComponent.Id = StudentResult.AssessmentComponentId  JOIN Rubric ON Rubric.Id = RubricLevel.RubricId OR Rubric.Id = AssessmentComponent.RubricId  JOIN Clo ON Clo.Id = Rubric.CloId  WHERE Clo.Name = @name1";
                string Query2 = "DELETE rl FROM RubricLevel rl JOIN Rubric r ON rl.RubricId = r.Id JOIN Clo c ON r.CloId = c.Id WHERE c.Name = @name2";
                string Query3 = "DELETE ac FROM AssessmentComponent ac JOIN Rubric r ON ac.RubricId = r.Id JOIN Clo c ON r.CloId = c.Id WHERE c.Name = @name3";
                string Query4 = "DELETE Rubric From Clo  WHERE Clo.Id=Rubric.CloId and Clo.Name=@name4";
                string Query5 = "DELETE Clo WHERE Name=@name5";

                SqlCommand cmd1 = new SqlCommand(Query1, con);
                cmd1.Parameters.AddWithValue("@name1", clonametxtbox.Text);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand(Query2, con);
                cmd2.Parameters.AddWithValue("@name2", clonametxtbox.Text);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand(Query3, con);
                cmd3.Parameters.AddWithValue("@name3", clonametxtbox.Text);
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand(Query4, con);
                cmd4.Parameters.AddWithValue("@name4", clonametxtbox.Text);
                cmd4.ExecuteNonQuery();
                SqlCommand cmd5 = new SqlCommand(Query5, con);
                cmd5.Parameters.AddWithValue("@name5", clonametxtbox.Text);
                cmd5.ExecuteNonQuery();
                databindassesment();
                databindasscom();
                databindclo();
                dataBindRLevel();
                databindrubric();
                DatabindClassAttendance();
                MessageBox.Show("Deleted", "clo deleted Succesfiulty!");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (clonametxtbox.Text != "" && DTP_DCtxtbox.Text != "" && DTP_DUtxtbox.Text != "")
            {
                
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("update  Clo set Name=@Name ,DateCreated=@DateUpdated where  Name=@Name", con);
                    cmd.Parameters.AddWithValue("@Name", clonametxtbox.Text);
                    cmd.Parameters.AddWithValue("@DateCreated", DTP_DCtxtbox.Text);
                    cmd.Parameters.AddWithValue("@DateUpdated", DTP_DUtxtbox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated succesfully");
                    cleardataclo();
                    databindclo();
              

            }
            else
            {

                MessageBox.Show(" please fill data first", "Warning");

            }

        }

        private void clogridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            clogridview.CurrentRow.Selected = true;
            IdforClotodelete.Text= clogridview.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            clonametxtbox.Text = clogridview.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            DTP_DCtxtbox.Text = clogridview.Rows[e.RowIndex].Cells["DateCreated"].Value.ToString();
            DTP_DUtxtbox.Text = clogridview.Rows[e.RowIndex].Cells["DateUpdated"].Value.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string var1 = CBoxtext.Text;// SelectedText.ToString();
            //MessageBox.Show(var1);
            if (var1 != "" || DTP_DCtxtbox.Text != "" || DTP_DUtxtbox.Text != "")// && var2. != ""  )// MCBstatus.Text == "Active" || MCBstatus.Text == "InActive")
            {


                if (var1 == "Name")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Name,DateCreated,DateUpdated from Clo  where @Name=Name", con);
                    cmd.Parameters.AddWithValue("@Name", clonametxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    clogridview.DataSource = dt;
                    cleardataclo();

                }
                else if (var1 == "DateCreated")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Name,DateCreated,DateUpdated  from Clo where @DateCreated=DateCreated", con);
                    cmd.Parameters.AddWithValue("@DateCreated", DTP_DCtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    clogridview.DataSource = dt;
                    cleardataclo();

                }
                else if (var1 == "DateUpdated")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Name,DateCreated,DateUpdated  where @DateUpdated=DateUpdated", con);
                    cmd.Parameters.AddWithValue("@DateUpdated", DTP_DUtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    clogridview.DataSource = dt;
                    cleardataclo();
                }

            }
            else
            {
                cleardataclo();
                MessageBox.Show("Error", "warning");
            }


        }

        private void materialTabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
















        private void dgvmanageass_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvmanageass.CurrentRow.Selected = true;
            Titletxtbox.Text = dgvmanageass.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            DTP_MASS.Text = dgvmanageass.Rows[e.RowIndex].Cells["DateCreated"].Value.ToString();
            totalmarkstxtbox.Text = dgvmanageass.Rows[e.RowIndex].Cells["TotalMarks"].Value.ToString();
            twtxtbox.Text = dgvmanageass.Rows[e.RowIndex].Cells["TotalWeightage"].Value.ToString();
        }

        private void MBInsertAss_Click(object sender, EventArgs e)
        {

            if (Titletxtbox.Text != "" && DTP_MASS.Text != "" && totalmarkstxtbox.Text != "" && twtxtbox.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Assessment values (@Title,@DateCreated,@TotalMarks,@TotalWeightage)", con);
                //cmd.Parameters.AddWithValue("@id", idtxtbox.Text);
                cmd.Parameters.AddWithValue("@Title", Titletxtbox.Text);
                cmd.Parameters.AddWithValue("@DateCreated", DTP_MASS.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", totalmarkstxtbox.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", twtxtbox.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                cleardatamanageassessment();
                databindassesment();
            }
            else
            {
                MessageBox.Show(" please enter all details of student", "Warning");

            }
        }

        private void MBRemove_Ass_Click(object sender, EventArgs e)
        {

            //try
            //{
                if (Titletxtbox.Text != "")
                {


                    //var con = Configuration.getInstance().getConnection();
                    //SqlCommand cmd = new SqlCommand("DELETE  from Assessment Where Title=@Title", con);
                    //string id = Titletxtbox.Text.ToString();
                    //cmd.Parameters.AddWithValue("@Title", Titletxtbox.Text);//id =Titletxtbox
                    //cmd.ExecuteNonQuery();
                    //MessageBox.Show("Successfully removed");

                    // string Query2 = "DELETE StudentResult FROM StudentResult JOIN RubricLevel ON RubricLevel.Id = StudentResult.RubricMeasurementId JOIN AssessmentComponent ON AssessmentComponent.Id = StudentResult.AssessmentComponentId  JOIN Rubric ON Rubric.Id = RubricLevel.RubricId OR Rubric.Id = AssessmentComponent.RubricId  JOIN Clo ON Clo.Id = Rubric.CloId  WHERE Clo.Name = @name1";
                    //string Query3 = "DELETE rl FROM RubricLevel rl JOIN Rubric r ON rl.RubricId = r.Id JOIN Clo c ON r.CloId = c.Id WHERE c.Name = @name2";
                    var con = Configuration.getInstance().getConnection();
                    string query1_withJoin = "DELETE sr\r\nFROM StudentResult sr\r\nJOIN AssessmentComponent ac ON sr.AssessmentComponentId = ac.Id\r\nJOIN Assessment a ON a.Id = ac.AssessmentId\r\nWHERE a.Title = @t1 AND a.DateCreated = @d1";
                    string query2 = "DELETE AssessmentComponent From Assessment  WHERE Assessment.Id=AssessmentComponent.AssessmentId and Assessment.Title=@t2 and Assessment.DateCreated=@t2";
                    string query3 = "DELETE Assessment WHERE Title=@t3 and DateCreated=@d3 ";


                    SqlCommand cmd = new SqlCommand(query1_withJoin, con);
                    cmd.Parameters.AddWithValue("@t1", Titletxtbox.Text);
                    cmd.Parameters.AddWithValue("@d1", Convert.ToDateTime(DTP_MASS.Text));
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand(query2, con);
                    cmd1.Parameters.AddWithValue("@t2", Titletxtbox.Text);
                    cmd1.Parameters.AddWithValue("@d2", Convert.ToDateTime(DTP_MASS.Text));
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand(query3, con);
                    cmd2.Parameters.AddWithValue("@t3", Titletxtbox.Text);
                    cmd2.Parameters.AddWithValue("@d3", Convert.ToDateTime(DTP_MASS.Text));
                    cmd2.ExecuteNonQuery();
                    databindassesment();
                    MessageBox.Show("deleted!");
                }
                else
                {
                    MessageBox.Show("enter title  nd date to delete ", "warning");
                }
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);
            //}

        }

        private void MBUpdate_Ass_Click(object sender, EventArgs e)
        {

            if (Titletxtbox.Text != "" && DTP_MASS.Text != "" && totalmarkstxtbox.Text != "" && twtxtbox.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("update  Assessment set Title=@Title ,DateCreated=@DateCreated,TotalMarks=@TotalMarks,TotalWeightage=@TotalWeightage  where Title=@Title", con);
                //cmd.Parameters.AddWithValue("@id", idtxtbox.Text);
                cmd.Parameters.AddWithValue("@Title", Titletxtbox.Text);
                cmd.Parameters.AddWithValue("@DateCreated", DTP_MASS.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", totalmarkstxtbox.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", twtxtbox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated succesfully");
                cleardatamanageassessment();
                databindassesment();
            }
            else
            {

                MessageBox.Show(" please fill data first", "Warning");

            }
        }

        private void MBSearch_Ass_Click(object sender, EventArgs e)
        {

            string var1 = MCBOSSASScombo.Text;
            if (var1 != "")// && var2. != ""  )// MCBstatus.Text == "Active" || MCBstatus.Text == "InActive")
            {


                if (var1 == "Title")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment where @Title=Title", con);
                    cmd.Parameters.AddWithValue("@Title", Titletxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvmanageass.DataSource = dt;
                    cleardatamanageassessment();

                }
                else if (var1 == "DateCreated")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment where @DateCreated=DateCreated", con);
                    cmd.Parameters.AddWithValue("@DateCreated", DTP_MASS.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvmanageass.DataSource = dt;
                    cleardatamanageassessment();

                }
                else if (var1 == "TotalMarks")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment where @TotalMarks=TotalMarks", con);
                    cmd.Parameters.AddWithValue("@TotalMarks", totalmarkstxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvmanageass.DataSource = dt;
                    cleardatamanageassessment();

                }
                else if (var1 == "TotalWeightage")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment where @TotalWeightage=TotalWeightage", con);
                    cmd.Parameters.AddWithValue("@TotalWeightage", twtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvmanageass.DataSource = dt;
                    cleardatamanageassessment();

                }
                else
                {
                    MessageBox.Show("Don't try to do that", "yaka");
                }
            }
            else
            {
                MessageBox.Show("can't emmpty", "warnign");
            }


        }

        private void MBShowAss__Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvmanageass.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
        }

        private void MBShowAss__Click_1(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvmanageass.DataSource = dt;
        }

        void cleardatarubcri()
        {



            idrubtxtbox.Text = "";
            detailsrubtxtbox.Text = "";
            cloiddrubrictxtbox.Text = "";

        }
        void databindrubric()
        {

            dgvrubric.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvrubric.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
            dgvrubric.Refresh();
        }
        private void MBINSERTRUB_Click(object sender, EventArgs e)
        {

            if (detailsrubtxtbox.Text != "" && comboBox9_.Text != "")// && totalmarkstxtbox.Text != "" && twtxtbox.Text != "")
            {

                //var con = Configuration.getInstance().getConnection();
                //SqlCommand command = new SqlCommand("select MAX(Id) from Rubric", con);
                //int RubricId = Convert.ToInt32(command.ExecuteScalar());
                //SqlCommand cmd1 = new SqlCommand("INSERT into Rubric values(@Id,@Details,@CloId)", con);
                //cmd1.Parameters.AddWithValue("@Id", RubricId + 1);
                //cmd1.Parameters.AddWithValue("@Details", detailsrubtxtbox.Text);
                //cmd1.Parameters.AddWithValue("@CloId",int.Parse(cloiddrubrictxtbox.Text) );

                //cmd1.ExecuteNonQuery();










                var con = Configuration.getInstance().getConnection();
                SqlCommand command = new SqlCommand("select MAX(Id) from Rubric", con);
                int RubricId = Convert.ToInt32(command.ExecuteScalar());
                //int RubricId = 0;
                SqlCommand cmd1 = new SqlCommand("INSERT into Rubric values(@Id,@Details,@CloId)", con);
                cmd1.Parameters.AddWithValue("@Id", RubricId + 1);
                cmd1.Parameters.AddWithValue("@Details", detailsrubtxtbox.Text);
                cmd1.Parameters.AddWithValue("@CloId", int.Parse(comboBox9_.Text));

                cmd1.ExecuteNonQuery();

                cleardatarubcri();
                databindrubric();
                MessageBox.Show("clo insert successfully");



            }
            else
            {
                MessageBox.Show(" please enter all details of student", "Warning");

            }
        }

        private void delrubbtn_Click(object sender, EventArgs e)
        {
            if (idrubtxtbox.Text != "")
            {


                //var con = Configuration.getInstance().getConnection();
                //SqlCommand cmd = new SqlCommand("DELETE  from Rubric Where Details=@Details", con);
                //string id = Titletxtbox.Text.ToString();
                //cmd.Parameters.AddWithValue("@Details", detailsrubtxtbox.Text);//id =Titletxtbox
                //cmd.ExecuteNonQuery();
                //MessageBox.Show("Successfully removed");
                //cleardatarubcri();
                //databindrubric();
                
                var con = Configuration.getInstance().getConnection();
                string query1_withcp = "DELETE StudentResult From Rubric ,RubricLevel ,AssessmentComponent  WHERE ((Rubric.Id=RubricLevel.RubricId and RubricLevel.Id=StudentResult.RubricMeasurementId) or (Rubric.Id=AssessmentComponent.RubricId and AssessmentComponent.Id=StudentResult.AssessmentComponentId)) and Rubric.Id=@r_idddd ";
                string query1 = "DELETE StudentResult\r\nFROM StudentResult\r\nJOIN RubricLevel ON RubricLevel.Id = StudentResult.RubricMeasurementId\r\nJOIN Rubric ON Rubric.Id = RubricLevel.RubricId\r\nLEFT JOIN AssessmentComponent ON AssessmentComponent.Id = StudentResult.AssessmentComponentId AND AssessmentComponent.RubricId = Rubric.Id\r\nWHERE Rubric.Id = @q1\r\n";
                string query2 = "DELETE RubricLevel From Rubric  WHERE Rubric.Id=RubricLevel.RubricId and Rubric.Id=@q2 ";
                string query3 = "DELETE AssessmentComponent From Rubric  WHERE Rubric.Id=AssessmentComponent.RubricId and Rubric.Id=@q3 ";
                string query4 = "DELETE Rubric WHERE Id=@q4 ";
                SqlCommand cmd = new SqlCommand(query1, con);
                cmd.Parameters.AddWithValue("@q1", int.Parse(idrubtxtbox.Text));
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(query2, con);
                cmd1.Parameters.AddWithValue("@q2", int.Parse(idrubtxtbox.Text));
                cmd1.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand(query3, con);
                cmd3.Parameters.AddWithValue("@q3", int.Parse(idrubtxtbox.Text));
                cmd3.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand(query4, con);
                cmd2.Parameters.AddWithValue("@q4", int.Parse(idrubtxtbox.Text));
                cmd2.ExecuteNonQuery();
                MessageBox.Show("deleted succesfuly");

            }
            else
            {
                MessageBox.Show("enter Details  like ID of rubric to delete ", "warning");
            }
        }

        private void btnSearchrub_Click(object sender, EventArgs e)
        {
            string var1 = materialComboBox1.Text;// SelectedText.ToString();
            //MessageBox.Show(var1);
            if (var1 != "" )//&& detailsrubtxtbox.Text != "" && cloiddrubrictxtbox.Text != "")// && var2. != ""  )// MCBstatus.Text == "Active" || MCBstatus.Text == "InActive")
            {


                if (var1 == "Details")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Details,CloId from Rubric  where @Details=Details", con);
                    cmd.Parameters.AddWithValue("@Details", detailsrubtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvrubric.DataSource = dt;
                    cleardatarubcri();

                }
                else if (var1 == "CloId")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Details,CloId from Rubric  where @CloId=CloId", con);
                    cmd.Parameters.AddWithValue("@CloId", int.Parse(comboBox9_.Text));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvrubric.DataSource = dt;
                    cleardatarubcri();


                }


            }
            else
            {
                MessageBox.Show("Warning", "data is not complete on boxes");
            }
        }

        private void Updaterubbtn_Click(object sender, EventArgs e)// not working 
        {
            if (detailsrubtxtbox.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("update  Rubric set Details=@Details,CloId=@CloId  where CloId=@CloId ", con); //and ID!= OldId
                //cmd.Parameters.AddWithValue("@id", idtxtbox.Text);
                cmd.Parameters.AddWithValue("@Details", detailsrubtxtbox.Text);
                //string value = cloiddrubrictxtbox.Text;
                //int vali = int.Parse(value);
                //MessageBox.Show(value);//, vali.ToString());
                cmd.Parameters.AddWithValue("@CloId", int.Parse(cloiddrubrictxtbox.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated succesfully");

                cleardatarubcri();
                databindrubric();

            }
            else
            {

                MessageBox.Show(" please fill data first", "Warning");

            }
        }


        private void showrubricbtn_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvrubric.DataSource = dt;
        }

        private void dgvrubric_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void detailrubtxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvrubric_CellContentClick_1(object sender, DataGridViewCellEventArgs e) // pdf generate kerna ha abhi
        {
            dgvrubric.CurrentRow.Selected = true;
            detailsrubtxtbox.Text = dgvrubric.Rows[e.RowIndex].Cells["Details"].Value.ToString();
            cloiddrubrictxtbox.Text = dgvrubric.Rows[e.RowIndex].Cells["CloId"].Value.ToString();

       

        }

        private void dgvrubric_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            dgvrubric.CurrentRow.Selected = true;
            detailsrubtxtbox.Text = dgvrubric.Rows[e.RowIndex].Cells["Details"].Value.ToString();
            comboBox9_.Text = dgvrubric.Rows[e.RowIndex].Cells["CloId"].Value.ToString();

        }

        private void mrublevelgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mrublevelgridview.CurrentRow.Selected = true;
            // detailsrubtxtbox.Text = mrublevelgridview.Rows[e.RowIndex].Cells["Id"].Value.ToString();

            detailsrubtxtbox.Text = mrublevelgridview.Rows[e.RowIndex].Cells["CloId"].Value.ToString();
            cloiddrubrictxtbox.Text = mrublevelgridview.Rows[e.RowIndex].Cells["Details"].Value.ToString();

            cloiddrubrictxtbox.Text = mrublevelgridview.Rows[e.RowIndex].Cells["MeasurementLevel"].Value.ToString();
        }
        public void dataBindRLevel()//kerna ha 
        {
            //rubricidtxtboxinrlevel.Text = "";

            //detalsrubriclevel.Text = "";
            // measurmentlevelinrubriclevel.Text = "";

            mrublevelgridview.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            mrublevelgridview.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
            mrublevelgridview.Refresh();
        }
        public void cleardatarubriclevel()//kerna ha 
        {

            rubricidtxtboxinrlevel.Text = "";

            detalsrubriclevel.Text = "";
            measurmentlevelinrubriclevel.Text = "";

        }
        private void insertbtn_Click(object sender, EventArgs e)
        {

            //eror a naa ha 

            //if (rubricidtxtboxinrlevel.Text != "" && detalsrubriclevel.Text != "" && measurmentlevelinrubriclevel.Text != "")// && totalmarkstxtbox.Text != "" && twtxtbox.Text != "")
            //{
                try
                {


                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd_N = new SqlCommand("SET IDENTITY_INSERT RubricLevel ON", con);
                    cmd_N.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand("INSERT into RubricLevel (Id,RubricId,Details,MeasurementLevel) values(@Id,@RubricId,@Details,@MeasurementLevel)", con);
                    cmd1.Parameters.AddWithValue("@Id", int.Parse(rlvlid.Text));
                    cmd1.Parameters.AddWithValue("@RubricId", int.Parse(comboBox4.Text));
                    cmd1.Parameters.AddWithValue("@Details", detalsrubriclevel.Text);
                    cmd1.Parameters.AddWithValue("@MeasurementLevel", int.Parse(measurmentlevelinrubriclevel.Text));

                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Entersed sUccesfully");

                    dataBindRLevel();
                    cleardatarubriclevel();

                    cmd_N = new SqlCommand("SET IDENTITY_INSERT RubricLevel OFF", con);
                    cmd_N.ExecuteNonQuery();

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }


            //}
            //else
            //{
            //    MessageBox.Show(" please enter all details of the   Rubric Level", "Warning");

            //}


        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE  from RubricLevel Where ID=@rlid", con);
                string id = Titletxtbox.Text.ToString();
                cmd.Parameters.AddWithValue("@rlid", rlvlid.Text);//id =Titletxtbox
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully removed");
                dataBindRLevel();
                cleardatarubriclevel();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {

        }

        private void shownbtn_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            mrublevelgridview.DataSource = dt;
            // clogridview.Columns["ID"].Visible = false;

        }

        private void mrublevelgridview_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            mrublevelgridview.CurrentRow.Selected = true;
            rlvlid.Text = classattencdecegv.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            rubricidtxtboxinrlevel.Text = classattencdecegv.Rows[e.RowIndex].Cells["RubricId"].Value.ToString();
            detalsrubriclevel.Text = classattencdecegv.Rows[e.RowIndex].Cells["Details"].Value.ToString();
            measurmentlevelinrubriclevel.Text = classattencdecegv.Rows[e.RowIndex].Cells["MeasurementLevel"].Value.ToString();

        }







        public void DatabindClassAttendance()
        {

            classattencdecegv.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            classattencdecegv.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
            classattencdecegv.Refresh();
        }
        public void ClearDataClasAttendence()
        {

            classatendenceidtxtbox.Text = "";

            dateTimePickerclassatttxtbox.Text = "";
        }
        private void insertbtnclasssattendence_Click(object sender, EventArgs e)//id auto generate
        {
            if (dateTimePickerclassatttxtbox.Text != "") //classatendenceidtxtbox.Text != "" &&     //&& measurmentlevelinrubriclevel.Text != "")// && totalmarkstxtbox.Text != "" && twtxtbox.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd1 = new SqlCommand("INSERT into ClassAttendance values(@AttendanceDate)", con);
                cmd1.Parameters.AddWithValue("@Id", int.Parse(classatendenceidtxtbox.Text));
                cmd1.Parameters.AddWithValue("@AttendanceDate", dateTimePickerclassatttxtbox.Text);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Entered succesfuly");
                ClearDataClasAttendence();
                DatabindClassAttendance();
            }
            else
            {
                MessageBox.Show(" please enter all details of class Attendence", "Warning");

            }
        }

        private void deletebtnclasaattanedenc_Click(object sender, EventArgs e)
        {
            if (classatendenceidtxtbox.Text != "")
            {


                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE  from ClassAttendance Where ID=@ID", con);
                string id = Titletxtbox.Text.ToString();
                cmd.Parameters.AddWithValue("@ID", classatendenceidtxtbox.Text);//id =Titletxtbox
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully removed");
                ClearDataClasAttendence();
                DatabindClassAttendance();

            }
            else
            {
                MessageBox.Show("enter Details to delete ", "warning");
            }
        }

        private void btnupdateclassattendence_Click(object sender, EventArgs e)
        {
            if (dateTimePickerclassatttxtbox.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("update  ClassAttendance set Id=@Id,AttendanceDate=@AttendanceDate  where Id=@Id ", con); //and ID!= OldId
                cmd.Parameters.AddWithValue("@Id", int.Parse(classatendenceidtxtbox.Text));
                cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePickerclassatttxtbox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated succesfully");
                ClearDataClasAttendence();
                DatabindClassAttendance();

            }
            else
            {

                MessageBox.Show(" please fill data first", "Warning");

            }
        }

        private void classatendesearchbtn_Click(object sender, EventArgs e)
        {

            string var1 = comboooo.Text;// SelectedText.ToString();
            //MessageBox.Show(var1);
            if (var1 != "" && dateTimePickerclassatttxtbox.Text != "" || classatendenceidtxtbox.Text != "")// && var2. != ""  )// MCBstatus.Text == "Active" || MCBstatus.Text == "InActive")
            {


                if (var1 == "Id")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Id,AttendanceDate from ClassAttendance  where @Id=Id", con);
                    cmd.Parameters.AddWithValue("@Id", classatendenceidtxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    classattencdecegv.DataSource = dt;
                    ClearDataClasAttendence();
                    DatabindClassAttendance();

                }
                else if (var1 == "AttendanceDate")
                {


                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Select Id,AttendanceDate from ClassAttendance  where @AttendanceDate=AttendanceDate", con);
                    cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePickerclassatttxtbox.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    classattencdecegv.DataSource = dt;
                    ClearDataClasAttendence();
                    DatabindClassAttendance();

                }


            }
            else
            {
                MessageBox.Show("Warning", "data is not complete on boxes");
            }
        }

        private void Showclassattbtn_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            classattencdecegv.DataSource = dt;
        }

        private void classattencdecegv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            classattencdecegv.CurrentRow.Selected = true;
            classatendenceidtxtbox.Text = classattencdecegv.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            dateTimePickerclassatttxtbox.Text = classattencdecegv.Rows[e.RowIndex].Cells["AttendanceDate"].Value.ToString();
        }

        private void dataGridViewstudentattendcia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var date = Convert.ToDateTime(DTMM.Text);
            int Id = 0;
            var con = Configuration.getInstance().getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Select Id from ClassAttendance Where AttendanceDate=@Da", con);
                cmd.Parameters.AddWithValue("@Da", date);
                Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                Id = 0;
            }

            if (Id == 0)
            {
                MessageBox.Show("Date has not added");
            }
            else
            {
                string student = dataGridView5.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                SqlCommand cmd2 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@date", con);
                cmd2.Parameters.AddWithValue("@date", date);
                int adateid = Convert.ToInt32(cmd2.ExecuteScalar());
                SqlCommand cmd3 = new SqlCommand("Select StudentId from StudentAttendance where AttendanceId=@CA and StudentId=@Id", con);
                cmd3.Parameters.AddWithValue("@CA", adateid);
                cmd3.Parameters.AddWithValue("@Id", student);
                int scheck = Convert.ToInt32(cmd3.ExecuteScalar());
                if (e.ColumnIndex == 0)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 4);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 4);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 3);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 3);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 1);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId =@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 1);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 2);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 2);
                        cmd1.ExecuteNonQuery();
                    }
                }
            }




        }

        private void dataGridViewtoseeclassattendenceid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM ClassAttendance", con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
        }

        private void comboBoxsclassattid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id,FirstName+LastName as Name,RegistrationNumber FROM Student where Status=5", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;



            




        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentAttendance where Status=5", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuAttendenceview3rdgv.DataSource = dt;
        }

        private void materialButton8_Click(object sender, EventArgs e)// not using 
        {

        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)//not  using 
        {

        }
        

        private void dataGridViewstudentattendcia_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {



            var date = Convert.ToDateTime(DTMM.Text);
            int Id = 0;
            var con = Configuration.getInstance().getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Select Id from ClassAttendance Where AttendanceDate=@Date", con);
                cmd.Parameters.AddWithValue("@Date", date);
                Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                Id = 0;
            }

            if (Id == 0)
            {
                MessageBox.Show("Date has not added");
            }
            else
            {
                string student = dataGridView5.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                SqlCommand cmd2 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@date", con);
                cmd2.Parameters.AddWithValue("@date", date);
                int adateid = Convert.ToInt32(cmd2.ExecuteScalar());
                SqlCommand cmd3 = new SqlCommand("Select StudentId from StudentAttendance where AttendanceId=@CA and StudentId=@Id", con);
                cmd3.Parameters.AddWithValue("@CA", adateid);
                cmd3.Parameters.AddWithValue("@Id", student);
                int scheck = Convert.ToInt32(cmd3.ExecuteScalar());
                if (e.ColumnIndex == 0)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 1);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 1);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 2);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 2);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[3].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 3);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId =@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 3);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    dataGridView5.Rows[e.RowIndex].Cells[1].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[2].Value = false;
                    dataGridView5.Rows[e.RowIndex].Cells[0].Value = false;
                    if (scheck == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 4);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                        cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                        cmd1.Parameters.AddWithValue("@StudentId", student);
                        cmd1.Parameters.AddWithValue("@AttendanceStatus", 4);
                        cmd1.ExecuteNonQuery();
                    }
                }
            }










            //if (dataGridViewstudentattendcia.Columns["Present_"].Index == e.ColumnIndex)
            //{
            //    MessageBox.Show("pp");
            //}


            //if (dataGridViewstudentattendcia.Columns["Late"].Index == e.ColumnIndex)
            //{
            //    MessageBox.Show("ksjfn");
            //}


            //if (dataGridViewstudentattendcia.Columns["Absent"].Index == e.ColumnIndex)
            //{
            //    MessageBox.Show("oo");
            //}


            //if (dataGridViewstudentattendcia.Columns["Leave_"].Index == e.ColumnIndex)
            //{
            //    MessageBox.Show("ll");
            //}


        }

        private void materialButton10_Click(object sender, EventArgs e)
        {

        }

        private void materialButton9_Click(object sender, EventArgs e)//view students
        {
            var date = Convert.ToDateTime(DTMM.Text);
            var con1 = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select max(Id) from ClassAttendance", con1);
           int  Ids = (Int32)cmd1.ExecuteScalar();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select distinct RegistrationNumber , FirstName + ' ' + LastName As Name,(select Name from Lookup where LookupId=StudentAttendance.AttendanceStatus) As Status From Student JOIN StudentAttendance on Student.Id=StudentAttendance.StudentId JOIN ClassAttendance on ClassAttendance.Id=StudentAttendance.AttendanceId  Where Status = 5 and AttendanceDate=@AttendanceDate", con);
            cmd.Parameters.AddWithValue("@Id", Ids);
            cmd.Parameters.AddWithValue("@AttendanceDate", date);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuAttendenceview3rdgv.DataSource = dt;
        }

        private void materialButton10_Click_1(object sender, EventArgs e)
        {

            
            //var con = Configuration.getInstance().getConnection();
            //var date = DateTime.Now.ToString("yyyy-MM-dd");
            //DateTime dates = Convert.ToDateTime(DTMM.Text);
            //SqlCommand cmd1 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@Date", con);
            //cmd1.Parameters.AddWithValue("@Date", dates);
            //int id = Convert.ToInt32(cmd1.ExecuteScalar());
            //if (id == 0)
            //{
            //    SqlCommand cmd = new SqlCommand("INSERT Into ClassAttendance Values(@AttendanceDate)", con);
            //    cmd.Parameters.AddWithValue("@AttendanceDate", dates);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Date has been Added");
            //}
            //else
            //{
            //    MessageBox.Show("Date has Already Added");
            //}





        }

        private void materialButton9_Click_1(object sender, EventArgs e)
        {
            var date = Convert.ToDateTime(DTMM.Text);
            var con1 = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select max(Id) from ClassAttendance", con1);
            int Ids = (Int32)cmd1.ExecuteScalar();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select distinct RegistrationNumber , FirstName + ' ' + LastName As Name,(select Name from Lookup where LookupId=StudentAttendance.AttendanceStatus) As Status From Student JOIN StudentAttendance on Student.Id=StudentAttendance.StudentId JOIN ClassAttendance on ClassAttendance.Id=StudentAttendance.AttendanceId  Where Status = 5 and AttendanceDate=@AttendanceDate", con);
            cmd.Parameters.AddWithValue("@Id", Ids);
            cmd.Parameters.AddWithValue("@AttendanceDate", date);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            stuAttendenceview3rdgv.DataSource = dt;
        }

        private void mbaddattenddate_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dates = Convert.ToDateTime(DTMM.Text);
            SqlCommand cmd1 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@Date_lite", con);
            cmd1.Parameters.AddWithValue("@Date_lite", dates);
            int id = Convert.ToInt32(cmd1.ExecuteScalar());
            if (id == 0)
            {
                SqlCommand cmd = new SqlCommand("INSERT Into ClassAttendance Values(@AttendanceDate_)", con);
                cmd.Parameters.AddWithValue("@AttendanceDate_", dates);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Date has been Added");
            }
            else
            {
                MessageBox.Show("Date has Already Added");
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string query = "SELECT  Distinct s.Id, s.FirstName, s.LastName, sa.AttendanceStatus FROM Student s LEFT JOIN StudentAttendance sa ON s.Id = sa.StudentId";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();

            // Step 3: Create a PDF document
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("your_report.pdf", FileMode.Create));
            document.Open();

            // Step 4: Populate the PDF document
            while (reader.Read())
            {
                // Add data to the PDF document
                string id = reader["Id"].ToString();
                string First_Name = reader["FirstName"].ToString();
                string Last_Name = reader["LastName"].ToString();
                string Attendance_Status = reader["AttendanceStatus"].ToString();

                if (Attendance_Status == "1")
                {
                    document.Add(new Paragraph(id + "               " + First_Name + "" + Last_Name + "             " + "Present"));
                }
                if (Attendance_Status == "2")
                {
                    document.Add(new Paragraph(id + "               " + First_Name + "" + Last_Name + "             " + "Absent"));
                }
                if (Attendance_Status == "3")
                {
                    document.Add(new Paragraph(id + "               " + First_Name + "" + Last_Name + "             " + "Leave"));
                }
                if (Attendance_Status == "4")
                {
                    document.Add(new Paragraph(id + "               " + First_Name + "" + Last_Name + "             " + "Late"));
                }
               // document.Add(new Paragraph(id + "               " + First_Name + "" + Last_Name + "             " + Attendance_Status));
            }

            // Step 5: Save the PDF document
            document.Close();
            writer.Close();
        }




        public void dataclearasscomp()
        {
           // comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
          //  comboBox2.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }

        public void databindasscom()
        {
            dataGridViewasscom.DataSource = null;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewasscom.DataSource = dt;
            //clogridview.Columns["ID"].Visible = false;
            dataGridViewasscom.Refresh();
        }


        private void materialButton7_Click_1(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewasscom.DataSource = dt;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && dateTimePicker2.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {


                var con = Configuration.getInstance().getConnection();
                string assid = comboBox2.Text;
                string query_ = "Select Sum(ac.TotalMarks) From Assessment as ass Join AssessmentComponent as ac On ass.Id = ac.AssessmentId Where ass.Id = '" + assid + "'";
                SqlCommand cmd = new SqlCommand(query_, con);

                SqlDataReader r1, r2;
                r1 = cmd.ExecuteReader();
                int marks = 0;
                int totmark = 0;
                while (r1.Read())
                {
                    try
                    {
                        marks = (int)r1.GetValue(0);
                        //MessageBox.Show(marks.ToString());

                    }
                    catch (Exception)
                    {
                        marks = 0;
                    }

                }

                marks = marks + Convert.ToInt32(textBox2.Text);
                r1.Close();
                string query2 = "Select TotalMarks From Assessment Where Id = '" + assid + "'";
                MessageBox.Show(query2);
                SqlCommand cmd1 = new SqlCommand(query2, con);
                r2 = cmd1.ExecuteReader();
                while (r2.Read())
                {
                    totmark = (int)r2.GetValue(0);
                }
                MessageBox.Show(totmark.ToString());

                r2.Close();

                MessageBox.Show(marks.ToString(), totmark.ToString());
                if (marks >= totmark)
                {

                    MessageBox.Show(marks.ToString(), totmark.ToString());
                    string query3 = "INSERT  INTO  AssessmentComponent values( @Name, @RubricId, @TotalMarks, @DateCreated,@DateUpdated,@AssessmentId)";
                    SqlCommand cmd2 = new SqlCommand(query3, con);


                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@RubricId", Convert.ToInt32(comboBox2.Text));
                    cmd2.Parameters.AddWithValue("@TotalMarks", Convert.ToInt32(textBox2.Text));
                    cmd2.Parameters.AddWithValue("@DateCreated", Convert.ToDateTime(dateTimePicker1.Value));
                    cmd2.Parameters.AddWithValue("@DateUpdated", Convert.ToDateTime(dateTimePicker1.Value));
                    cmd2.Parameters.AddWithValue("@AssessmentId", Convert.ToInt32(comboBox3.Text));

                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Assessment Level Successfully Added!!!!!!!!!!");

                    dataclearasscomp();
                    databindasscom();
                }


                else
                {
                    MessageBox.Show("Your addedd component marks excedded your assessment marks by  " + (marks - totmark) + "");
                }
            }
            
            else
            {
                MessageBox.Show("warning", "enter data compeletely frist!!!");
            }



        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""  && comboBox2.Text != "" && comboBox3.Text != "")//&& dateTimePicker1.Text != "" && dateTimePicker2.Text != ""
            {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Delete from AssessmentComponent WHERE RubricId=@rid and AssessmentId=@aid and Name=@name", con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@rid", comboBox2.Text);
            cmd.Parameters.AddWithValue("@aid", comboBox3.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Assessment Component Record Deleted Successfully!");

            }
            else 
            {
                MessageBox.Show("Warning", "enter the details to delete the data!!!");

            }
        }

        private void materialButton4_Click(object sender, EventArgs e)//working 
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && dateTimePicker2.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET RubricId=@rid,TotalMarks=@tm, DateCreated=@d_c,DateUpdated=@d_u,AssessmentId=@aid  where Name=@name", con);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@rid", comboBox2.Text);
                cmd.Parameters.AddWithValue("@tm", textBox2.Text);
                cmd.Parameters.AddWithValue("@d_c", Convert.ToDateTime(dateTimePicker1.Text));
                cmd.Parameters.AddWithValue("@d_u", DateTime.Now);
                cmd.Parameters.AddWithValue("@aid", comboBox3.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Assessment Levels  Updated Successfully Click View To show Updates");
            }
            else
            {
                MessageBox.Show("warnig", "entr the details of data to modify or update!!!");
            }

        }

        private void materialButton5_Click(object sender, EventArgs e)
        {

        }

        private void materialButton8_Click_1(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Rubric ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox2.Items.Add(dr["Id"].ToString());


            }
            comboBox3.Items.Clear();

            var con_1 = Configuration.getInstance().getConnection();
            SqlCommand cmd_1 = new SqlCommand("Select Id from Assessment", con_1);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da_1 = new SqlDataAdapter(cmd_1);
            DataTable dt_1 = new DataTable();
            da_1.Fill(dt_1);
            foreach (DataRow dr1 in dt_1.Rows)
            {

                comboBox3.Items.Add(dr1["Id"].ToString());


            }
        }

        private void materialLabel35_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void rlvlid_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // rlvlid.Items.Clear();
            comboBox4.Items.Clear();

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Rubric ", con);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox4.Items.Add(dr["Id"].ToString());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton1_Click_1(object sender, EventArgs e)
        {
            try
            {


                var con = Configuration.getInstance().getConnection();


                SqlCommand cmd = new SqlCommand("INSERT  INTO  StudentResult values(@StudentId, @AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentId", int.Parse(comboBox8.Text));
                cmd.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(comboBox7.Text));
                cmd.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(comboBox6.Text));
                cmd.Parameters.AddWithValue("@EvaluationDate", Convert.ToDateTime(dateTimePicker3.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Successfully Added!!!!!!!!!!");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void materialButton13_Click(object sender, EventArgs e)
        {
            //comboBox8.Items.Clear();

            //var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd1 = new SqlCommand("Select Id from Student", con);
            //cmd1.ExecuteNonQuery();
            //SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
           
            //foreach (DataRow dr in dt.Rows)
            //{

            //    comboBox8.Items.Add(dr["Id"].ToString());



            //}






            //comboBox7.Items.Clear();

            ////var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd2 = new SqlCommand("select AssessmentComponent.Id from AssessmentComponent,Assessment where Assessment.Id=AssessmentComponent.AssessmentId and Assessment.Title=@ai", con);
            //cmd2.Parameters.AddWithValue("@ai", comboBox7.Text);

            //cmd2.ExecuteNonQuery();
            //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            //DataTable dt2 = new DataTable();
            //da2.Fill(dt2);
            ///*comboBox2.DisplayMember = "AssessmentComponent.Name";
            //comboBox2.ValueMember = "AssessmentComponent.Id";
            //comboBox2.DataSource = dt;*/
            //foreach (DataRow dr in dt2.Rows)
            //{

            //    comboBox2.Items.Add(dr["Id"].ToString());


            //}
          //  }
          ////  MessageBox.Show(" Assessments Component  IDs Loaded Succesfully");





          //  //comboBox3.Items.Clear();

          //  ////var con = Configuration.getInstance().getConnection();
          //  //SqlCommand cmd3 = new SqlCommand("select RubricLevel.Id from RubricLevel,Rubric,AssessmentComponent where AssessmentComponent.RubricId=Rubric.Id and  Rubric.Id=RubricLevel.RubricId and AssessmentComponent.Id=@aid", con);
          //  //cmd3.Parameters.AddWithValue("@aid", comboBox2.Text);
          //  //cmd3.ExecuteNonQuery();
          //  //SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
          //  //DataTable dt3 = new DataTable();
          //  //da3.Fill(dt3);
          //  //foreach (DataRow dr in dt3.Rows)
          //  //{

          //  //    comboBox3.Items.Add(dr["Id"].ToString());



          //  //}



          //  comboBox5.Items.Clear();

          //  //var con = Configuration.getInstance().getConnection();
          //  SqlCommand cmd3 = new SqlCommand("select RubricLevel.Id from RubricLevel,Rubric,AssessmentComponent where AssessmentComponent.RubricId=Rubric.Id and  Rubric.Id=RubricLevel.RubricId and AssessmentComponent.Id=@aid", con);
          //  cmd3.Parameters.AddWithValue("@aid", comboBox5.Text);
          //  cmd3.ExecuteNonQuery();
          //  SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
          //  DataTable dt3 = new DataTable();
          //  da3.Fill(dt3);
          //  foreach (DataRow dr in dt3.Rows)
          //  {

          //      comboBox3.Items.Add(dr["Id"].ToString());



          //  }
          //  MessageBox.Show(" all data loaded succesfully  ");

        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string query = "select Student.FirstName AS StudentName,Rubric.Details as R_Details,AssessmentComponent.Name as Component,RubricLevel.Details,RubricLevel.MeasurementLevel,(RubricLevel.MeasurementLevel/(select cast(MAX(RubricLevel.MeasurementLevel) as decimal) from RubricLevel)*AssessmentComponent.TotalMarks) as Obtained  from Student,AssessmentComponent,Rubric,RubricLevel,StudentResult where AssessmentComponent.RubricId = Rubric.Id and RubricLevel.Id = StudentResult.RubricMeasurementId   and Student.Id = StudentResult.StudentId  and StudentResult.AssessmentComponentId = AssessmentComponent.Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewevaluation.DataSource = dt;
        }

        private void materialButton13_Click_1(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select RubricLevel.Id from RubricLevel,Rubric,AssessmentComponent where AssessmentComponent.RubricId=Rubric.Id and  Rubric.Id=RubricLevel.RubricId and AssessmentComponent.Id=@aid", con);
            cmd.Parameters.AddWithValue("@aid", comboBox7.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox6.Items.Add(dr["Id"].ToString());



            }
            MessageBox.Show(" Rubric Measurment IDs will be loaded only if Assessment Component is Selected ");

        }

        private void materialButton16_Click(object sender, EventArgs e)
        {
                  pdf(dataGridViewevaluation, "Report Hasan", " Evaluation Report ");
        }

        public void pdf(DataGridView dgw, string filename, string str)// working on it 
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 90;

            pdftable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftable.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            PdfPTable pdftableblank = new PdfPTable(1);
            pdftableblank.WidthPercentage = 100;
            pdftableblank.DefaultCell.Padding = 10;
            pdftableblank.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftableblank.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftableblank.DefaultCell.BorderWidth = 0;
            //FOOTER SECTION---------------------------------------------
            PdfPTable pdftablefooter = new PdfPTable(1);
            pdftablefooter.DefaultCell.BorderWidth = 0;
            pdftablefooter.WidthPercentage = 80;
            pdftablefooter.DefaultCell.PaddingTop = -130;
            pdftablefooter.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            //FOOTER END-------------------------------------------------


            PdfPTable pdftable1 = new PdfPTable(1);
            PdfPTable pdftable2 = new PdfPTable(1);
            PdfPTable pdftable3 = new PdfPTable(1);
            PdfPTable pdftable4 = new PdfPTable(1);
            System.Drawing.Font fonth1 = new System.Drawing.Font("Currier", 16);

            pdftable1.DefaultCell.PaddingTop = -100;
            pdftable1.WidthPercentage = 80;
            pdftable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable1.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            pdftable1.DefaultCell.BorderWidth = 0;

            pdftable2.DefaultCell.PaddingTop = -50;
            pdftable2.DefaultCell.PaddingBottom = 30;
            pdftable2.WidthPercentage = 80;
            pdftable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable2.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftable2.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            pdftable2.DefaultCell.BorderWidth = 0;


            pdftable3.DefaultCell.PaddingTop = 5;
            pdftable3.DefaultCell.PaddingBottom = 10;
            pdftable3.WidthPercentage = 40;
            pdftable3.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable3.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftable3.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(101, 54, 0);
            pdftable3.DefaultCell.BorderWidth = 0;

            pdftable4.DefaultCell.Padding = 0;
            pdftable4.WidthPercentage = 100;
            pdftable4.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable4.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            pdftable4.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(0, 0, 0);
            pdftable4.DefaultCell.BorderWidth = 0;

            Chunk c1 = new Chunk("", FontFactory.GetFont("Helvatica"));
            c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c1.Font.SetStyle(0);
            c1.Font.Size = 24;
            Phrase p1 = new Phrase();
            p1.Add(c1);
            pdftable1.AddCell(p1);

            Chunk c2 = new Chunk("Department of Computer Science UET Lahore", FontFactory.GetFont("Helvatica"));
            c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c2.Font.SetStyle(0);
            c2.Font.Size = 11;
            Phrase p2 = new Phrase();
            p2.Add(c2);
            pdftable2.AddCell(p2);

            Chunk c3 = new Chunk(str, FontFactory.GetFont("Helvatica"));
            c3.Font.Color = new iTextSharp.text.BaseColor(255, 255, 255);
            c3.Font.SetStyle(0);
            c3.Font.Size = 16;
            Phrase p3 = new Phrase();
            p3.Add(c3);
            pdftable3.AddCell(p3);

            Chunk c4 = new Chunk("PDF Report Of Students Evaluation", FontFactory.GetFont("Times New Roman"));
            c4.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c4.Font.SetStyle(0);
            c4.Font.Size = 16;
            Phrase p4 = new Phrase();
            p4.Add(c4);
            pdftableblank.AddCell(p4);

            Chunk c5 = new Chunk("", FontFactory.GetFont("Times New Roman"));
            c5.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c5.Font.SetStyle(0);
            c5.Font.Size = 16;
            Phrase p5 = new Phrase();
            p5.Add(c5);
            pdftable4.AddCell(p5);
            //IMAGE
            string img = @"C:\Users\Hasan\Downloads\logo.png";
            iTextSharp.text.Image jgp = iTextSharp.text.Image.GetInstance(img);
            jgp.ScaleToFit(140f, 140f);
            jgp.SpacingBefore = 100f;
            jgp.SpacingAfter = 100f;
            jgp.Alignment = Element.ALIGN_LEFT;

            Chunk cnkfooter = new Chunk(DateTime.Now.ToString("dddd, MMMM d, yyyy"), FontFactory.GetFont("Times New Roman"));
            cnkfooter.Font.Size = 10;
            pdftablefooter.AddCell(new Phrase(cnkfooter));


            for (int j = 0; j < dgw.Columns.Count; j++)
            {

                Chunk c6 = new Chunk(dgw.Columns[j].HeaderText, FontFactory.GetFont("Times New Roman"));
                c6.Font.Color = new iTextSharp.text.BaseColor(101, 54, 0);
                c6.Font.SetStyle(1);

                c6.Font.Size = 9;
                Phrase p6 = new Phrase();
                p6.Add(c6);
                pdftable.AddCell(p6);


            }

            pdftable.HeaderRows = 1;
            //ADD ROWS--------------------------------------------------------
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                for (int k = 0; k < dgw.Columns.Count; k++)
                {
                    if (dgw[k, i].Value != null)
                    {
                        Chunk c7 = new Chunk(dgw[k, i].Value.ToString(), FontFactory.GetFont("Times New Roman"));
                        c7.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                        c7.Font.SetStyle(0);

                        c7.Font.Size = 7;
                        Phrase p7 = new Phrase();
                        p7.Add(c7);
                        pdftable.AddCell(p7);
                    }

                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = filename;
            saveFileDialoge.DefaultExt = ".pdf";
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialoge.FileName, FileMode.Create))
                {
                    Document document = new Document(PageSize.ROYAL_QUARTO, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(document, stream);

                    document.Open();


                    ////////////////////////////////////////////////////////////////
                    document.Add(jgp);

                    document.Add(pdftablefooter);
                    document.Add(pdftable1);
                    document.Add(pdftable2);
                    document.Add(pdftable4);
                    document.Add(pdftableblank);
                    document.Add(pdftable3);
                    //document.Add(pdftableblank);
                    document.Add(pdftable);
                    document.NewPage();
                    ///////////////////////////////////////////////////////////////
                    document.Close();
                    stream.Close();
                }

                MessageBox.Show("PDF Saved! you can open it from\n  '" + saveFileDialoge.FileName + "'", "EXPORT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(saveFileDialoge.FileName);
            }

        }

        private void materialButton15_Click(object sender, EventArgs e)
        {

            comboBox5.Items.Clear();
            comboBox8.Items.Clear();
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd1 = new SqlCommand("Select Id from Student", con);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            foreach (DataRow dr in dt1.Rows)
            {
                                comboBox8.Items.Add(dr["Id"].ToString());

            }

            MessageBox.Show("Succesfully loaded");

            var con2 = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title from Assessment ", con2);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox5.Items.Add(dr["Title"].ToString());


            }


            MessageBox.Show("Succesfully loaded!");
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select AssessmentComponent.Id from AssessmentComponent,Assessment where Assessment.Id=AssessmentComponent.AssessmentId and Assessment.Title=@ai", con);
            cmd.Parameters.AddWithValue("@ai", comboBox5.Text);

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox7.Items.Add(dr["Id"].ToString());
            }
            MessageBox.Show(" Assessments Component  IDs Loaded Succesfully");
        }

        private void materialButton12_Click(object sender, EventArgs e)
        {
           
        }

        private void materialButton10_Click_2(object sender, EventArgs e)
        {
            try
            {


                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Delete from StudentResult WHERE StudentId=@sid", con);
                cmd.Parameters.AddWithValue("@sid", comboBox8.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Result Record has been Deleted Successfully!");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void dataGridViewevaluation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void materialButton17_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
          

            string query = "SELECT s.Id, s.FirstName, s.LastName, sa.AttendanceStatus FROM Student s LEFT JOIN StudentAttendance sa ON s.Id = sa.StudentId";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();

            // Step 3: Create a PDF document
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Students_Attendence_Report.pdf", FileMode.Create));
            document.Open();

            // Step 4: Populate the PDF document
            PdfPTable table = new PdfPTable(4); // 4 columns
            table.WidthPercentage = 100; // table width to 100%

            // Add table headers
            table.AddCell("ID");
            table.AddCell("First Name");
            table.AddCell("Last Name");
            table.AddCell("Attendance Status");

            while (reader.Read())
            {
                // Add data to the PDF table
                string id = reader["Id"].ToString();
                string First_Name = reader["FirstName"].ToString();
                string Last_Name = reader["LastName"].ToString();
                string Attendance_Status = reader["AttendanceStatus"].ToString();
                if (Attendance_Status == "1")
                {
                    table.AddCell(id);
                    table.AddCell(First_Name);
                    table.AddCell(Last_Name);
                    table.AddCell("Present");

                }
                if (Attendance_Status == "2")
                {
                    table.AddCell(id);
                    table.AddCell(First_Name);
                    table.AddCell(Last_Name);
                    table.AddCell("Absent");

                }
                if (Attendance_Status == "3")
                {
                    table.AddCell(id);
                    table.AddCell(First_Name);
                    table.AddCell(Last_Name);
                    table.AddCell("Leave");

                }
                if (Attendance_Status == "4")
                {
                    table.AddCell(id);
                    table.AddCell(First_Name);
                    table.AddCell(Last_Name);
                    table.AddCell("Late");

                }
              
            }

            // Add the table to the PDF document
            document.Add(table);

            // Step 5: Save the PDF document
            document.Close();
            writer.Close();


       



        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void materialButton22_Click(object sender, EventArgs e)// attendence wise reprt working
        {


            //var con = Configuration.getInstance().getConnection();
            //string query = "SELECT s.Id, s.FirstName, s.LastName, sa.AttendanceStatus FROM Student s LEFT JOIN StudentAttendance sa ON s.Id = sa.StudentId";
            //SqlCommand command = new SqlCommand(query, con);
            //SqlDataReader reader = command.ExecuteReader();

            //// Step 3: Create a PDF document
            //Document document = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Students_Attendence_Report.pdf", FileMode.Create));
            //document.Open();

            //// Step 4: Populate the PDF document
            //PdfPTable table = new PdfPTable(4); // 4 columns
            //table.WidthPercentage = 100; // table width to 100%

            //// Add table headers
            //table.AddCell("ID");
            //table.AddCell("First Name");
            //table.AddCell("Last Name");
            //table.AddCell("Attendance Status");

            //while (reader.Read())
            //{
            //    // Add data to the PDF table
            //    string id = reader["Id"].ToString();
            //    string First_Name = reader["FirstName"].ToString();
            //    string Last_Name = reader["LastName"].ToString();
            //    string Attendance_Status = reader["AttendanceStatus"].ToString();
            //    if (Attendance_Status == "1")
            //    {
            //        table.AddCell(id);
            //        table.AddCell(First_Name);
            //        table.AddCell(Last_Name);
            //        table.AddCell("Present");

            //    }
            //    if (Attendance_Status == "2")
            //    {
            //        table.AddCell(id);
            //        table.AddCell(First_Name);
            //        table.AddCell(Last_Name);
            //        table.AddCell("Absent");

            //    }
            //    if (Attendance_Status == "3")
            //    {
            //        table.AddCell(id);
            //        table.AddCell(First_Name);
            //        table.AddCell(Last_Name);
            //        table.AddCell("Leave");

            //    }
            //    if (Attendance_Status == "4")
            //    {
            //        table.AddCell(id);
            //        table.AddCell(First_Name);
            //        table.AddCell(Last_Name);
            //        table.AddCell("Late");

            //    }

            //}

            //// Add the table to the PDF document
            //document.Add(table);

            //// Step 5: Save the PDF document
            //document.Close();
            //writer.Close();
            //MessageBox.Show("succesfully");
            MessageBox.Show("Report has been processing...");
            pdf(stuAttendenceview3rdgv, "Student Attendance Report", "Attendance Report");

          
        }

        private void materialButton21_Click(object sender, EventArgs e)
        {
            CloReport clorp = new CloReport();
            //this.Hide();
            clorp.Show();
        }

        private void materialButton20_Click(object sender, EventArgs e)
        {
            assessmentreport ass = new assessmentreport();
            this.Hide();
            ass.Show();
        }

        private void dataGridViewassreport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void materialButton19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("report has been genrated");

            pdf(dataGridViewevaluation, "report by Hasan Nawaz", "evalution report");
        }

        private void materialButton18_Click(object sender, EventArgs e)
        {
            studentreport studentreportstudent = new studentreport();
            studentreportstudent.Show();    
        }

        private void materialTabControl1_Click(object sender, EventArgs e)
        {
            comboBox9_.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Clo ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox9_.Items.Add(dr["Id"].ToString());


            }
        }

        private void materialLabel49_Click(object sender, EventArgs e)
        {
            comboBox9_.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Clo ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox9_.Items.Add(dr["Id"].ToString());


            }
        }
    }
}











//if (dgvrubric.Columns["Pdf rubric"].Index == e.ColumnIndex)
////{
//    // Create a new PDF document
//    Document doc = new Document();

//    // Create a new PdfWriter object, which writes the document to a file or stream
//    PdfWriter.GetInstance(doc, new FileStream("example.pdf", FileMode.Create));

//    // Open the document for writing
//    doc.Open();

//    // Add content to the document
//    doc.Add(new Paragraph("Hello, World!"));

//    // Close the document
//    doc.Close();


//////oprn pdf file

////string pdfPath = @"D:\Database\MIDProjectB\2021-cs-52\CRUD_PROJECT_B\bin\Debug";

////// Open the PDF file using the default PDF viewer on the user's machine
////System.Diagnostics.Process.Start(pdfPath);







//string pdfPath = @"D:\Database\MIDProjectB\2021-cs-52\CRUD_PROJECT_B\bin\Debug";

//// Set the process start information to open the PDF file in Microsoft Edge...example.pdf
//ProcessStartInfo psi = new ProcessStartInfo();
//psi.FileName = "msedge.exe";
//psi.Arguments = "\"" + pdfPath + "/example.pdf";

//// Start the process
//Process.Start(psi);
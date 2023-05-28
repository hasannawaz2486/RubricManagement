using CRUD1_PROJECT_B;
using iText.Kernel.Pdf.Annot;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace CRUD_PROJECT_B
{
    public partial class studentreport : Form
    {
        public studentreport()
        {
            InitializeComponent();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string Query = "select Student.FirstName,Rubric.Details,AssessmentComponent.Name as Component,RubricLevel.Details,RubricLevel.MeasurementLevel,(RubricLevel.MeasurementLevel/(select cast(MAX(RubricLevel.MeasurementLevel) as decimal) from RubricLevel)*AssessmentComponent.TotalMarks) as Obtained  from Student,AssessmentComponent,Rubric,RubricLevel,StudentResult,Clo,Assessment where AssessmentComponent.RubricId = Rubric.Id and RubricLevel.Id = StudentResult.RubricMeasurementId   and Student.Id = StudentResult.StudentId  and StudentResult.AssessmentComponentId = AssessmentComponent.Id and Clo.Id=Rubric.CloId  and Assessment.Id=AssessmentComponent.AssessmentId and Student.RegistrationNumber=@s";
            string query1withjoin = "SELECT s.FirstName, r.Details, ac.Name AS Component, rl.Details, rl.MeasurementLevel, \r\n       (rl.MeasurementLevel / (SELECT CAST(MAX(RubricLevel.MeasurementLevel) AS DECIMAL) FROM RubricLevel) * ac.TotalMarks) AS Obtained\r\nFROM Student s\r\nJOIN StudentResult sr ON s.Id = sr.StudentId\r\nJOIN AssessmentComponent ac ON sr.AssessmentComponentId = ac.Id\r\nJOIN Rubric r ON ac.RubricId = r.Id\r\nJOIN RubricLevel rl ON sr.RubricMeasurementId = rl.Id\r\nJOIN Clo c ON r.CloId = c.Id\r\nJOIN Assessment a ON ac.AssessmentId = a.Id\r\nWHERE s.RegistrationNumber = @regnumber\r\n";
            SqlCommand cmd = new SqlCommand(query1withjoin, con);
            cmd.Parameters.AddWithValue("@regnumber", comboBox11student.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewassemsntreport.DataSource = dt;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

            pdf(dataGridViewassemsntreport, "Reports of Student", "Student Reports");
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            //studentreport sr= new studentreport();
            this.Hide();
        }

        private void studentreport_Load(object sender, EventArgs e)
        {
            comboBox11student.Items.Clear();

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select RegistrationNumber from Student ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                comboBox11student.Items.Add(dr["RegistrationNumber"].ToString());


            }
        }



        public void pdf(DataGridView dgw, string filename, string str)
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
    }
}

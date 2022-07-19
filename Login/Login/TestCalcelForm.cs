using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class TestCalcelForm : Form
    {
        public TestCalcelForm()
        {
            InitializeComponent();
        }

        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillAllInformation();
        }

        private void FillAllInformation()
        {
            try
            {
                var db = new SANITARIUMEntities();
                var tb = new DignosisTestSaleInfo();

                var select = db.DignosisTestSaleInfoes.Where(x => x.Id.ToString() == txtSearch.Text && x.ReturnStatus.ToString() == lblReturnStatus.Text && x.Paid.ToString() != "0").FirstOrDefault();

                txtName.Text = select.Name;
                txtAge.Text = select.Age.ToString();
                cbxAgeStatus.SelectedValue = select.AgeStatusId;
                txtAddress.Text = select.Address;
                txtPhone.Text = select.Phone.ToString();
                cbxGender.SelectedValue = select.GenderId;
                cbxDoctor.SelectedValue = select.DoctorId;
                cbxRefer.SelectedValue = select.ReferId;
                txtVAT.Text = select.VAT.ToString();
                txtDiscount.Text = select.Discount.ToString();
                txtTotalAmount.Text = select.Total_Cost.ToString();
                txtPaidAmount.Text = select.Paid.ToString();
                txtDue.Text = select.Due.ToString();
                txtTReg.Text = select.T_Reg.ToString();
                dtpTestSaleDate.Value = select.Date.Value;
                FillTestGrid();
                lblBillingOfficer.Text = select.BillOfficer;
                
            }
            catch
            {
                MessageBox.Show("Recourd not found.Try again.Thank you.");
            }

        }

        private void FillTestGrid()
        {
            var db = new SANITARIUMEntities();
            dgvTest.DataSource = db.StoreTests.Where(a => a.S_RegNumber.ToString() == txtTReg.Text).ToList();
        }

        private void TestCalcelForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet4.DignosisTestSaleInfo' table. You can move, or remove it, as needed.
            this.dignosisTestSaleInfoTableAdapter1.Fill(this.sANITARIUMDataSet4.DignosisTestSaleInfo);
            // TODO: This line of code loads data into the 'sANITARIUMDataSet6.DignosisTestSaleReturn' table. You can move, or remove it, as needed.
            this.dignosisTestSaleReturnTableAdapter.Fill(this.sANITARIUMDataSet6.DignosisTestSaleReturn);


            FillDoctior();
            FillGender();
            FillAgeStatus();
            FillRefer();
            FillCancelTest();
            dgvTest.DataSource = String.Empty;
            var logus = MotherForm.currentUser;
            lbluser.Text = logus;
            ClearText();
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            FillPatient();
        }

        private void FillPatient()
        {
            DateTime now = DateTime.Now.Date;
            var db = new SANITARIUMEntities();
            dgvAllPatient.DataSource = db.DignosisTestSaleInfoes.Where(x => x.ReturnStatus.ToString() == "0" && x.Date == now.Date).ToList();
        }

        private void ClearText()
        {
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            cbxAgeStatus.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            cbxGender.Text = string.Empty;
            cbxDoctor.Text = string.Empty;
            cbxRefer.Text = string.Empty;
            txtVAT.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtPaidAmount.Text = string.Empty;
            txtDue.Text = string.Empty;
            txtTReg.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtReturnAmount.Text = string.Empty;
        }

        private void FillCancelTest()
        {
            DateTime now = DateTime.Now.Date;
            var db = new SANITARIUMEntities();
            //dgvTestCalcel.DataSource = db.DignosisTestSaleReturns.Where(x=> x.ReturnDate == now.Date).ToList();
            dgvTestCalcel.DataSource = db.DignosisTestSaleInfoes.Where(x => x.ReturnStatus.ToString() == "1" && x.Date == now.Date).ToList();
        }

        private void FillRefer()
        {
            var obj = new Manager();
            var list = obj.GetRefer();
            cbxRefer.DisplayMember = "R_Name";
            cbxRefer.ValueMember = "Id";
            cbxRefer.DataSource = list;
        }

        private void FillAgeStatus()
        {
            var obj = new Manager();
            var list = obj.GetAgeStatus();
            cbxAgeStatus.DisplayMember = "AgeStatus";
            cbxAgeStatus.ValueMember = "Id";
            cbxAgeStatus.DataSource = list;
        }

        private void FillGender()
        {
            var obj = new Manager();
            var list = obj.GetGender();
            cbxGender.DisplayMember = "Gender";
            cbxGender.ValueMember = "Id";
            cbxGender.DataSource = list;
        }

        private void FillDoctior()
        {
            var obj = new Manager();
            var list = obj.GetDoctor();
            cbxDoctor.DisplayMember = "D_Name";
            cbxDoctor.ValueMember = "Id";
            cbxDoctor.DataSource = list;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure save data?", "Save Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblReturnStatus.Text = "1";
                SaveReturnTest();                
                CancelTestSaleUpdate();

                if (MessageBox.Show("Do you went print this documnet?", "Print cancel test", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printDocument();
                }
                
                FillCancelTest();
                ClearText();
                dgvTest.DataSource = string.Empty;
                lblReturnStatus.Text = "0";                
            }
            
        }

        private void CancelTestSaleUpdate()
        {
            var db = new SANITARIUMEntities();

            var tb = db.DignosisTestSaleInfoes.ToList().Where(a => a.Id == int.Parse(txtSearch.Text.Trim())).FirstOrDefault();

            tb.Paid = Convert.ToInt32(txtPaidAmount.Text.Trim());
            tb.VAT = txtVAT.Text.Trim();
            tb.Discount = Convert.ToInt32(txtDiscount.Text.Trim());
            tb.Total_Cost = Convert.ToInt32(txtTotalAmount.Text.Trim());
            tb.Due = Convert.ToInt32(txtDue.Text.Trim());
            tb.ReturnStatus = Convert.ToInt32(lblReturnStatus.Text.Trim());
            db.SaveChanges();
        }

        private void SaveReturnTest()
        {
            var db = new SANITARIUMEntities();
            var tb = new DignosisTestSaleReturn();

            tb.ReturnDate = Convert.ToDateTime(dtpReturnDate.Value);
            tb.SaleDate = Convert.ToDateTime(dtpTestSaleDate.Value);
            tb.DignosticId = Convert.ToInt32(txtSearch.Text.Trim());
            tb.T_Reg = Convert.ToInt32(txtTReg.Text.Trim());
            tb.TestReturnOfficer = lbluser.Text;
            tb.TotalCost = Convert.ToInt32(txtTotalAmount.Text.Trim());
            tb.VAT = Convert.ToInt32(txtVAT.Text.Trim());
            tb.Discount = Convert.ToInt32(txtDiscount.Text.Trim());
            tb.Due = Convert.ToInt32(txtDue.Text.Trim());
            tb.Paid = Convert.ToInt32(txtPaidAmount.Text.Trim());
            tb.ReturnAmount = Convert.ToInt32(txtReturnAmount.Text.Trim());
            tb.Discription = lblDiscription.Text.Trim();

            db.DignosisTestSaleReturns.Add(tb);
            db.SaveChanges();            


           //MessageBox.Show("Test cancel successfully.", "Test Return", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtReturnAmount_TextChanged(object sender, EventArgs e)
        {
            if(txtPaidAmount.Text == txtReturnAmount.Text)
            {
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument();
        }

        private void printDocument()
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Hospital Managment Software", new Font("Arial", 36, FontStyle.Bold), Brushes.Black, new Point(50, 10));
            e.Graphics.DrawString("Shaheb Bazar, College Road, Kaliakair, Gazipur", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(130, 70));
            e.Graphics.DrawString("E-mail: samim.hossen5274@gmail.com", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(220, 105));
            e.Graphics.DrawString("Mobail: +880 1762-164746, +880 1533-021557", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(190, 135));
            e.Graphics.DrawString("Date: " + dtpReturnDate.Value.ToShortDateString(), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(650, 135));
            e.Graphics.DrawString("Id: " + txtTReg.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(50, 135));
            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 140));

            int infoHeight = 170;
            int infoweight = 50;
            e.Graphics.DrawString("Name: " + txtName.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight));
            e.Graphics.DrawString("Phone: " + txtPhone.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 440, infoHeight));
            e.Graphics.DrawString("Age: " + txtAge.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight + 25));
            e.Graphics.DrawString("     " + cbxAgeStatus.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 65, infoHeight + 25));
            e.Graphics.DrawString("Refer: " + cbxRefer.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 440, infoHeight + 25));
            e.Graphics.DrawString("Gender: " + cbxGender.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 200, infoHeight + 25));
            e.Graphics.DrawString("Address: " + txtAddress.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight + 50));
            e.Graphics.DrawString("Doctor: " + cbxDoctor.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight + 75));

            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 255));

            e.Graphics.DrawString("Test Name", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(50, 285));
            e.Graphics.DrawString("Price", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(700, 285));

            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 295));


            int height = 285;
            for (int l = numberOfItemsPrintedSoFar; l < dgvTest.Rows.Count; l++)
            {
                numberOfItemsPerPage = numberOfItemsPerPage + 1;

                if (numberOfItemsPerPage <= 14)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= dgvTest.Rows.Count)
                    {

                        height += dgvTest.Rows[0].Height + 15;
                        e.Graphics.DrawString(dgvTest.Rows[l].Cells[0].FormattedValue.ToString(), dgvTest.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(50, height, dgvTest.Columns[0].Width, dgvTest.Rows[0].Height));
                        e.Graphics.DrawString(dgvTest.Rows[l].Cells[1].FormattedValue.ToString(), dgvTest.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(700, height, dgvTest.Columns[0].Width, dgvTest.Rows[0].Height));
                        e.Graphics.DrawString("___________________________________________________________________________________________________________________", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, height + 10));
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                }
                else
                {
                    numberOfItemsPerPage = 0;
                    e.HasMorePages = true;
                    return;

                }
            }

            e.Graphics.DrawString(txtVAT.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 40));
            e.Graphics.DrawString("VAT:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 40));

            e.Graphics.DrawString(txtDiscount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 70));
            e.Graphics.DrawString("Discount:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(600, height + 70));

            e.Graphics.DrawString(txtTotalAmount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 100));
            e.Graphics.DrawString("Total:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 100));

            e.Graphics.DrawString(txtPaidAmount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 130));
            e.Graphics.DrawString("Paid:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 130));

            e.Graphics.DrawString(txtDue.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 165));
            e.Graphics.DrawString("Due:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(642, height + 165));

            e.Graphics.DrawString("Billing Officer : " + lblBillingOfficer.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(25, height + 40));

            e.Graphics.DrawString("Return Amount : ", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(25, height + 135));
            e.Graphics.DrawString(txtReturnAmount.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(200, height + 135));
            
            e.Graphics.DrawString("Pay Status: Full Return !" , new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(25, height + 200));
            e.Graphics.DrawString("Return Officer : " + lbluser.Text + ";", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(25, height + 180));


            e.Graphics.DrawString("Hospital Managment Software।।Devolopment By: SAMIM HooseN: +880 1762-164746  ।।", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(25, height + 245));




            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;

            //print section endt---------------------------------------------------------------
        }
        
    }
}

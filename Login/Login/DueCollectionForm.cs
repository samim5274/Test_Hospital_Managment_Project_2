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
    public partial class DueCollectionForm : Form
    {
        public DueCollectionForm()
        {
            InitializeComponent();
        }

        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDueRecord();            
        }

        private void ClearText2()
        {

            txtDueCollection.Text = string.Empty;
            txtReminderDue.Text = string.Empty;
            txtDueDiscount.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }

        private void SearchDueRecord()
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Write the currect ID and then Press ENTER or SEARCH button.", "Search Faild!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                groupBox1.Visible = false;
            }
            else if (txtSearch.Text != null)
            {
                try
                {
                    var db = new SANITARIUMEntities();
                    var tb = new DignosisTestSaleInfo();
                    var tb2 = new StoreTest();

                    var selector = db.DignosisTestSaleInfoes.Where(a => a.T_Reg.ToString() == txtSearch.Text).FirstOrDefault();

                    txtName.Text = selector.Name;
                    txtAge.Text = selector.Age.ToString();
                    cbxAgeStatus.SelectedValue = selector.AgeStatusId;
                    txtAddress.Text = selector.Address;
                    txtPhone.Text = selector.Phone.ToString();
                    cbxGender.SelectedValue = selector.GenderId;
                    cbxDoctor.SelectedValue = selector.DoctorId;
                    cbxRefer.SelectedValue = selector.ReferId;
                    txtVAT.Text = selector.VAT.ToString();
                    txtDiscount.Text = selector.Discount.ToString();
                    txtTotalAmount.Text = selector.Total_Cost.ToString();
                    txtPaidAmount.Text = selector.Paid.ToString();
                    txtDue.Text = selector.Due.ToString();
                    txtReg.Text = selector.T_Reg.ToString();
                    lblDue.Text = selector.DueStatus.ToString();
                    lblBillOfficer.Text = selector.BillOfficer;

                    FillGridView();

                    //VisibleAllComponent();
                    groupBox1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Record not fount", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    groupBox1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Write the currect ID and then Press ENTER or SEARCH button.", "Search Faild!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                groupBox1.Visible = false;
            }
        }

        private void VisibleAllComponent()
        {
            txtName.Visible = true;
            txtAge.Visible = true;
            cbxAgeStatus.Visible = true;
            txtAddress.Visible = true;
            txtPhone.Visible = true;
            cbxGender.Visible = true;
            cbxDoctor.Visible = true;
            cbxRefer.Visible = true;
            txtVAT.Visible = true;
            txtDiscount.Visible = true;
            txtTotalAmount.Visible = true;
            txtPaidAmount.Visible = true;
            txtDue.Visible = true;
            txtReg.Visible = true;
            dgvTest.Visible = true;
            txtDueDiscount.Visible = true;
            txtDueCollection.Visible = true;
            btnSave.Visible = true;
        }

        private void FillGridView()
        {
            var db = new SANITARIUMEntities();
            dgvTest.DataSource = db.StoreTests.Where(a => a.S_RegNumber.ToString() == txtReg.Text).ToList();
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

        private void DueCollectionForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet3.DignosisTestSaleInfo' table. You can move, or remove it, as needed.
            this.dignosisTestSaleInfoTableAdapter.Fill(this.sANITARIUMDataSet3.DignosisTestSaleInfo);
            FillGridDuelist();
            // TODO: This line of code loads data into the 'sANITARIUMDataSet1.StoreTest' table. You can move, or remove it, as needed.
            this.storeTestTableAdapter.Fill(this.sANITARIUMDataSet1.StoreTest);

            FillDoctior();
            FillGender();
            FillAgeStatus();
            FillRefer();
            ClearText();
            dgvTest.DataSource = string.Empty;
            var logus = MotherForm.User;
            lbluser.Text = logus;
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
            txtReg.Text = string.Empty;
        }

        private void FillGridDuelist()
        {
            var db = new SANITARIUMEntities();
            dgvDueList.DataSource = db.DignosisTestSaleInfoes.Where(a => a.DueStatus.ToString() == lblDueStatus.Text).ToList();

        }

        private void txtDueCollection_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDueCollection.Text == "")
                {
                    MessageBox.Show("Input String was not in a correct foramte.","Due Collection Fail.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    txtDueCollection.Text = "0";
                    txtReminderDue.Text = txtDue.Text;
                }
                
                double num1, num2, num3, sum1, sum2;
                num1 = double.Parse(txtDue.Text);
                num2 = double.Parse(txtDueCollection.Text);
                num3 = double.Parse(txtDueDiscount.Text);
                sum1 = num2 + num3;
                sum2 = num1 - sum1;
                txtReminderDue.Text = Convert.ToString(sum2);

                if (txtReminderDue.Text == "0")
                {
                    btnSave.Enabled = true;
                    lblDue.Text = "0";
                }
                else
                {
                    btnSave.Enabled = false;
                    lblDue.Text = "1";
                }

                if (txtReminderDue.Text == "0")
                {
                    lblPaidStatus.Text = "Full Paid";
                }
                else
                {
                    lblPaidStatus.Text = "Due !";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Input String was not in a correct foramte !", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtDueDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                if (txtDueDiscount.Text == "")
                {
                    txtReminderDue.Text = txtDue.Text;
                    txtDueDiscount.Text = "0";
                }

                double num1, num2, num3, sum1, sum2;
                num1 = double.Parse(txtDue.Text);
                num2 = double.Parse(txtDueDiscount.Text);
                num3 = double.Parse(txtDueCollection.Text);
                sum1 = num2 + num3;
                sum2 = num1 - sum1;
                txtReminderDue.Text = Convert.ToString(sum2);   
                
                if(txtReminderDue.Text == "0")
                {
                    btnSave.Enabled = true;
                    lblDue.Text = "0";
                }
                else
                {
                    btnSave.Enabled = false;
                    lblDue.Text = "1";
                }  

                if (txtReminderDue.Text == "0")
                {
                    lblPaidStatus.Text = "Full Paid";
                }
                else
                {
                    lblPaidStatus.Text = "Due !";
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Input String was not in a correct foramte.Thank You !", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DueCollection();
            FillGridDuelist();
            ClearText();
            ClearText2();
        }

        private void DueCollection()
        {
            try
            {
                var db = new SANITARIUMEntities();
                var tb = new DignosisTestSaleInfo();

                var found = db.DignosisTestSaleInfoes.ToList().Where(a => a.T_Reg == int.Parse(txtSearch.Text.Trim())).FirstOrDefault();

                found.Name = txtName.Text.Trim();
                found.DueCollectionDate = Convert.ToDateTime(dtpTestSaleDay.Value);
                found.Address = txtAddress.Text.Trim();
                found.GenderId = Convert.ToInt32(cbxGender.SelectedValue);
                found.Age = Convert.ToInt32(txtAge.Text.Trim());
                found.AgeStatusId = Convert.ToInt32(cbxAgeStatus.SelectedValue);
                found.Phone = Convert.ToInt32(txtPhone.Text.Trim());
                found.DoctorId = Convert.ToInt32(cbxDoctor.SelectedValue);
                found.ReferId = Convert.ToInt32(cbxRefer.SelectedValue);
                found.Paid = Convert.ToInt32(txtPaidAmount.Text.Trim());
                found.VAT = txtVAT.Text.Trim();
                found.Discount = Convert.ToInt32(txtDiscount.Text.Trim());
                found.Total_Cost = Convert.ToInt32(txtTotalAmount.Text.Trim());

                found.Due = Convert.ToInt32(txtReminderDue.Text.Trim());
                found.DueDiscount = Convert.ToInt32(txtDueDiscount.Text.Trim());
                found.DueCollection = Convert.ToInt32(txtDueCollection.Text.Trim());
                found.DueCollectionOfficer = lbluser.Text;
                found.T_Reg = Convert.ToInt32(txtReg.Text.Trim());
                found.DueStatus = Convert.ToInt32(lblDue.Text.Trim());

                db.SaveChanges();
                MessageBox.Show("Due Collection Successfully.", "Data chnaged update Save.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                PrintDocument();
                groupBox1.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument();
        }

        private void PrintDocument()
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
            e.Graphics.DrawString("Date: " + dtpTestSaleDay.Value.ToShortDateString(), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(650, 135));
            e.Graphics.DrawString("Id: " + txtReg.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(50, 135));
            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 140));



            int infoHeight = 170;
            int infoweight = 50;
            e.Graphics.DrawString("Name: " + txtName.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight));
            e.Graphics.DrawString("Phone: " + txtPhone.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 440, infoHeight));
            e.Graphics.DrawString("Age: " + txtAge.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight + 25));
            e.Graphics.DrawString("   " + cbxAgeStatus.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 65, infoHeight + 25));
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



            e.Graphics.DrawString("Billing Officer: "+lblBillOfficer.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(25, height + 40));
            e.Graphics.DrawString("Previous Pay Status: Due !" , new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(25, height + 70));
            //e.Graphics.DrawString("সেবাই আমাদের ধর্ম এবং আমরা সর্বত্র আপনাদের সেবাই নিয়োজিত আছি।।ধন্যদাব।।আবার আসবেন।।", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(45, height + 245));

            e.Graphics.DrawString("Due Discount: " , new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(25, height + 105));
            e.Graphics.DrawString(txtDueDiscount.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(200, height + 105));

            e.Graphics.DrawString("Due Collection: " , new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(25, height + 135));
            e.Graphics.DrawString(txtDueCollection.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(200, height + 135));

            e.Graphics.DrawString("__________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, height + 145));

            e.Graphics.DrawString("Reminder Due: ", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(25, height + 175));
            e.Graphics.DrawString(txtReminderDue.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(200, height + 175));

            e.Graphics.DrawString("Pay Status: "+lblPaidStatus.Text, new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(25, height + 200));
            e.Graphics.DrawString("Due Collection Officer: " + lbluser.Text + ";", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(25, height + 240));





            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;

        }
    }
 }


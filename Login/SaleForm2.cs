using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace Login
{
    public partial class SaleForm2 : Form
    {
        public SaleForm2()
        {
            InitializeComponent();
        }


        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;


        private void x_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaleForm2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet2.StoreTest' table. You can move, or remove it, as needed.
            this.storeTestTableAdapter.Fill(this.sANITARIUMDataSet2.StoreTest);
            LoadUserReg();
            dgvStore.DataSource = string.Empty;
            //dgvStore.DataSource = dbobj.StoreTests.Where(a => a.S_RegNumber.ToString() == lblReg.Text).ToList();

            FilGender();
            FillAgeStatus();
            FillDiscoutnAuthority();
            FillDoctor();
            FillRefer();
            ClearText2();

            //if (MessageBox.Show("Are you sure to delete ..?", "Doctor Manage", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

        }

        private void LoadUserReg()
        {
            var dbobj = new SANITARIUMEntities();
            var logUs = MotherForm.User;
            var usReg = dbobj.DignosisTestSaleInfoes.Where(a => a.Id.ToString() == lblCount.Text).ToList();
            var cun = usReg.Count();
            var max = dbobj.DignosisTestSaleInfoes.Count() + 1;
            var max2 = dbobj.DignosisTestSaleInfoes.Count();
            lblCount.Text = "Registation: " + max2 + ";      " + "Next Registation No: " + max + ";    " + " Billing Officer: " + logUs + ";";
            lblReg.Text = "" + max;
            lblReg.Visible = false;
            lblUserPrint.Text = logUs;
            lblTotalReg.Text = ""+max;
        }
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTestname.Text != null)
            {
                
                if (e.KeyChar == (char)Keys.Enter)
                {
                    try
                    {
                        var db = new SANITARIUMEntities();
                        var DTD = new DignosticTestDetail();
                        var store = new StoreTest();
                        var selector = db.DignosticTestDetails.Where(a => a.TestName == txtTestname.Text).FirstOrDefault();
                        var item = selector.TestName.ToString();

                        txtFillTestName.Text = selector.TestName;
                        txtFillPrice.Text = selector.TestCost.ToString();
                        txtFillTestId.Text = selector.Id.ToString();

                        TestStore();
                        FillGrid();
                        ClearText();

                    }
                    catch
                    {
                        MessageBox.Show("No record found.Write some thing,then press ENTER. Try again.", "No Result", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Fast write some thing,then press ENTER.Thank You!", "Scarch Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FillGrid()
        {
            var db = new SANITARIUMEntities();
            //dgvStore.DataSource = db.StoreTests.ToList();      (-----sorasori data grid view a shoe korbe.-------)
            dgvStore.DataSource = db.StoreTests.Where(a => a.S_RegNumber.ToString() == lblReg.Text).ToList();
            FillPrice();
        }

        private void FillPrice()
        {
            lblTotal.Text = "0";
            for (int i = 0; i < dgvStore.Rows.Count; i = i + 1)
            {
                lblTotal.Text = Convert.ToString(double.Parse(lblTotal.Text) + double.Parse(dgvStore.Rows[i].Cells[1].Value.ToString()));
            }
            txtTotalAmount.Text = lblTotal.Text;
        }

        

        private void TestStore()
        {
            var db = new SANITARIUMEntities();
            var ts = new StoreTest();

            ts.S_TestName = txtFillTestName.Text.Trim();
            ts.S_TestPrice = Convert.ToInt32(txtFillPrice.Text);
            ts.S_RegNumber = Convert.ToInt32(lblReg.Text);
            ts.TestId = Convert.ToInt32(txtFillTestId.Text.Trim());
            db.StoreTests.Add(ts);
            db.SaveChanges();
        }

        private void dgvStore_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtFillTestName.Text = dgvStore.SelectedRows[0].Cells[0].Value.ToString();
            txtFillPrice.Text = dgvStore.SelectedRows[0].Cells[1].Value.ToString();
            //deleteItem(); (-----Mouse ar 1 click a delete hobe.-----)
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteItem();
        }

        private void deleteItem()
        {
            if (txtFillTestName.Text == "" && txtFillPrice.Text == "")
            {
                MessageBox.Show("Fast select the test name,Then try to delete.Otherwise you cann't delete the test.Thank you.", "Delete Faild.!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                txtTestname.Focus();
            }
            else
            {
                var obj = new SANITARIUMEntities();
                var tb = new StoreTest();
                var store = obj.StoreTests.ToList();
                var cach = obj.StoreTests.Where(a => a.S_TestName == txtFillTestName.Text && a.S_RegNumber.ToString() == lblReg.Text).FirstOrDefault();
                obj.Entry(cach).State = EntityState.Deleted;
                obj.SaveChanges();
                FillGrid();
                ClearText();
            }
        }


        //(---------------------------akhan theke auto Complete code soro------------------------------------)
        private void FillRefer()
        {
            var obj = new Manager();
            var list = obj.GetRefer();
            cbxRefer.DisplayMember = "R_Name";
            cbxRefer.ValueMember = "Id";
            cbxRefer.DataSource = list;
        }

        private void FillDoctor()
        {
            var obj = new Manager();
            var list = obj.GetDoctor();
            cbxDoctor.DisplayMember = "D_Name";
            cbxDoctor.ValueMember = "Id";
            cbxDoctor.DataSource = list;
        }

        private void FillDiscoutnAuthority()
        {
            var obj = new Manager();
            var list = obj.GetDiscountAuthor();
            cbxDisAuthor.DisplayMember = "Da_Name";
            cbxDisAuthor.ValueMember = "Id";
            cbxDisAuthor.DataSource = list;
        }

        private void FillAgeStatus()
        {
            var obj = new Manager();
            var list = obj.GetAgeStatus();
            cbxAgeStatus.DisplayMember = "AgeStatus";
            cbxAgeStatus.ValueMember = "Id";
            cbxAgeStatus.DataSource = list;
        }

        private void FilGender()
        {
            var obj = new Manager();
            var list = obj.GetGender();
            cbxGender.DisplayMember = "Gender";
            cbxGender.ValueMember = "Id";
            cbxGender.DataSource = list;
        }

        private void ClearText()
        {
            txtFillTestName.Text = string.Empty;
            txtFillPrice.Text = string.Empty;
            txtTestname.Text = string.Empty;
            txtFillTestId.Text = string.Empty;
        }

        private void ClearText2()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAge.Text = string.Empty;
            cbxGender.Text = string.Empty;
            cbxAgeStatus.Text = string.Empty;
            cbxDisAuthor.Text = string.Empty;
            cbxDoctor.Text = string.Empty;
            cbxRefer.Text = string.Empty;
            txtVatP.Text = string.Empty;
            txtVatTaka.Text = string.Empty;
            txtDiscountP.Text = string.Empty;
            txtDiscountTaka.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            dgvStore.DataSource = string.Empty;
            txtPaidAmount.Text = string.Empty;
            txtDue.Text = string.Empty;
            chkDiscountCent.Checked = false;
            chkDiscountTaka.Checked = false;
            chkVatP.Checked = false;
            chkVatTaka.Checked = false;
            txtVatP.Visible = false;
            txtVatTaka.Visible = false;
            txtDiscountP.Visible = false;
            txtDiscountTaka.Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearText2();
            ClearText();
        }

        public static int sumx;

        private void button15_Click_1(object sender, EventArgs e)
        {
            //txtTotalAmount.Text = (float.Parse(label3.Text) - float.Parse(label5.Text)).ToString();

            //if (txtVatP.Text == "0" && txtVatTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck");
            //    txtTotalAmount.Text = lblTotal.Text;

            //}
            //else if (txtVatP.Text != null && txtVatTaka.Text == "0")
            //{
            //    double num1, num2, num3, sum, sum2, sum3;
            //    num1 = double.Parse(txt100.Text);
            //    num2 = double.Parse(txtVatP.Text);
            //    num3 = double.Parse(txtTotalAmount.Text);
            //    sum = num1 + num2;
            //    var sumx = sum.ToString();
            //    sum2 = sum * num3;
            //    sum3 = sum2 / num1;
            //    txtTotalAmount.Text = Convert.ToString(sum3);
            //}
            //else if (txtVatTaka.Text != null)
            //{
            //    double num1, num2, sum;
            //    num1 = double.Parse(txtVatTaka.Text);
            //    num2 = double.Parse(txtTotalAmount.Text);
            //    sum = num1 + num2;
            //    txtTotalAmount.Text = Convert.ToString(sum);
            //}

        }

        private void chkVatP_MouseClick(object sender, MouseEventArgs e)
        {
            VatCentChick();
        }

        private void VatCentChick()
        {
            if (chkVatP.Checked)
            {
                txtVatP.Visible = true;
                chkVatTaka.Checked = false;
                txtVatTaka.Visible = false;
                txtVatP.Text = "0";
                txtVatTaka.Text = "0";
                button15.Enabled = true;
                txtTestname.Enabled = false;
            }
            else if (chkVatP.Checked == false)
            {
                txtVatP.Visible = false;
                //chkVatTaka.Checked = true;
                //txtVatTaka.Visible = true;
                txtVatP.Text = "0";
                //txtTotalAmount.Text = lblTotal.Text;
                button15.Enabled = false;
                txtTestname.Enabled = true;
            }
        }

        private void chkVatTaka_MouseClick(object sender, MouseEventArgs e)
        {
            VatTakaChick();
        }

        private void VatTakaChick()
        {
            if (chkVatTaka.Checked)
            {
                txtVatTaka.Visible = true;
                chkVatP.Checked = false;
                txtVatP.Visible = false;
                txtVatTaka.Text = "0";
                txtVatP.Text = "0";
                button15.Enabled = true;
                txtTestname.Enabled = false;
            }
            else if (chkVatTaka.Checked == false)
            {
                txtVatTaka.Visible = false;
                //chkVatP.Checked = true;
                //txtVatP.Visible = true;
                txtVatTaka.Text = "0";
                //txtTotalAmount.Text = lblTotal.Text;
                button15.Enabled = false;
                txtTestname.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (txtDiscountP.Text == "0" && txtDiscountTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck !");
            //    txtTotalAmount.Text = lblTotal.Text;
            //}
            //else if (txtDiscountP.Text !=null && txtDiscountTaka.Text == "0")
            //{
            //    double num1, num2, num3, sum1, sum2, sum3;
            //    num1 = double.Parse(txtDiscountP.Text);
            //    num2 = double.Parse(txt100.Text);
            //    num3 = double.Parse(txtTotalAmount.Text);
            //    sum1 = num2 - num1;
            //    sum2 = sum1 * num3;
            //    sum3 = sum2 / num2;
            //    txtTotalAmount.Text = Convert.ToString(sum3);
            //}
            //else if(txtDiscountTaka !=null && txtDiscountP.Text == "0")
            //{
            //    double num1, num3, sum1;
            //    num1 = double.Parse(txtDiscountTaka.Text);
            //    num3 = double.Parse(txtTotalAmount.Text);
            //    sum1 = num3 - num1;
            //    txtTotalAmount.Text = Convert.ToString(sum1);
            //}
        }

        private void chkDiscountCent_MouseClick(object sender, MouseEventArgs e)
        {
            DiscountCentChick();
        }

        private void DiscountCentChick()
        {
            if (chkDiscountCent.Checked)
            {
                txtDiscountTaka.Visible = false;
                chkDiscountTaka.Checked = false;
                txtDiscountP.Visible = true;
                txtDiscountTaka.Text = "0";
                txtDiscountP.Text = "0";
                button2.Enabled = true;
                cbxDisAuthor.Enabled = true;
                txtTestname.Enabled = false;

                if (cbxDisAuthor.Text == "")
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            else if (chkDiscountCent.Checked == false)
            {
                txtDiscountP.Visible = false;
                //chkDiscountTaka.Checked = true;
                //txtDiscountTaka.Visible = true;
                txtDiscountTaka.Text = "0";
                txtDiscountP.Text = "0";
                //txtTotalAmount.Text = lblTotal.Text;
                button2.Enabled = false;
                cbxDisAuthor.Enabled = false;
                txtTestname.Enabled = true;
            }
        }

        private void chkDiscountTaka_MouseClick(object sender, MouseEventArgs e)
        {
            DicsountChickProgress();
        }

        private void DicsountChickProgress()
        {
            if (chkDiscountTaka.Checked)
            {
                txtDiscountTaka.Visible = true;
                chkDiscountCent.Checked = false;
                txtDiscountP.Visible = false;
                txtDiscountTaka.Text = "0";
                txtDiscountP.Text = "0";
                button2.Enabled = true;
                cbxDisAuthor.Enabled = true;
                txtTestname.Enabled = false;

                if (cbxDisAuthor.Text == "")
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            else if (chkDiscountTaka.Checked == false)
            {
                txtDiscountTaka.Visible = false;
                //chkDiscountCent.Checked = true;
                //txtDiscountP.Visible = true;
                txtDiscountTaka.Text = "0";
                txtDiscountP.Text = "0";
                //txtTotalAmount.Text = lblTotal.Text;
                button2.Enabled = false;
                cbxDisAuthor.Enabled = false;
                txtTestname.Enabled = true;
            }
        }

        private void txtVatP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, num3, sum, sum2, sum3, sum4;
                num1 = double.Parse(txt100.Text);
                num2 = double.Parse(txtVatP.Text);
                num3 = double.Parse(lblTotal.Text);
                sum = num1 + num2;
                sum2 = sum * num3;
                sum3 = sum2 / num1;
                sum4 = sum3 - num3;
                lblTotalVat.Text = sum4.ToString();
                txtTotalVat.Text = Convert.ToString(sum4);
            }
            catch
            {
                //
            }
            //if (txtVatP.Text == "0" && txtVatTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck");
            //    txtTotalVat.Text = "0";

            //}
            //else if (txtVatP.Text != null && txtVatTaka.Text == "0")
            //{
            //    //double num1, num2, num3, sum, sum2, sum3, sum4;
            //    //num1 = double.Parse(txt100.Text);
            //    //num2 = double.Parse(txtVatP.Text);
            //    //num3 = double.Parse(lblTotal.Text);
            //    //sum = num1 + num2;
            //    //sum2 = sum * num3;
            //    //sum3 = sum2 / num1;
            //    //sum4 = sum3 - num3;
            //    //lblTotalVat.Text = sum4.ToString();
            //    //txtTotalVat.Text = Convert.ToString(sum4);
            //}
            //else if (txtVatTaka.Text != null)
            //{
            //    //double num1, num2, sum;
            //    //num1 = double.Parse(txtVatTaka.Text);
            //    //num2 = double.Parse(lblTotal.Text);
            //    //sum = num1 + num2;
            //    //txtTotalAmount.Text = Convert.ToString(sum);
            //}
        }

        private void txtVatTaka_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, sum, sum2;
                num1 = double.Parse(txtVatTaka.Text);
                num2 = double.Parse(lblTotal.Text);
                sum = num1 + num2;
                sum2 = sum - num2;
                lblTotalVat.Text = sum2.ToString();
                txtTotalVat.Text = Convert.ToString(sum2);
            }
            catch
            {
                //
            }
            //if (txtVatTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck !");
            //    txtTotalVat.Text = "0";
            //}
            //else
            //{
            //    double num1, num2, sum, sum2;
            //    num1 = double.Parse(txtVatTaka.Text);
            //    num2 = double.Parse(lblTotal.Text);
            //    sum = num1 + num2;
            //    sum2 = sum - num2;
            //    lblTotalVat.Text = sum2.ToString();
            //    txtTotalVat.Text = Convert.ToString(sum2);
            //}

        }

        private void txtDiscountP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, num3, sum1, sum2, sum3, sum4;
                num1 = double.Parse(txtDiscountP.Text);
                num2 = double.Parse(txt100.Text);
                num3 = double.Parse(lblTotal.Text);
                sum1 = num2 - num1;
                sum2 = sum1 * num3;
                sum3 = sum2 / num2;
                sum4 = num3 - sum3;
                lblTotalDiscount.Text = sum4.ToString();
                txtTotalDiscount.Text = Convert.ToString(sum4);
            }
            catch
            {
                //
            }
            //if (txtDiscountP.Text == "0" && txtDiscountTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck !");
            //    txtTotalDiscount.Text = "0";
            //}
            //else if (txtDiscountP.Text != null && txtDiscountTaka.Text == "0")
            //{
            //    double num1, num2, num3, sum1, sum2, sum3, sum4;
            //    num1 = double.Parse(txtDiscountP.Text);
            //    num2 = double.Parse(txt100.Text);
            //    num3 = double.Parse(lblTotal.Text);
            //    sum1 = num2 - num1;
            //    sum2 = sum1 * num3;
            //    sum3 = sum2 / num2;
            //    sum4 = num3 - sum3;
            //    lblTotalDiscount.Text = sum4.ToString();
            //    txtTotalDiscount.Text = Convert.ToString(sum4);
            //}
            //else if (txtDiscountTaka != null && txtDiscountP.Text == "0")
            //{
            //    //double num1, num3, sum1;
            //    //num1 = double.Parse(txtDiscountTaka.Text);
            //    //num3 = double.Parse(txtTotalAmount.Text);
            //    //sum1 = num3 - num1;
            //    //txtTotalAmount.Text = Convert.ToString(sum1);
            //}
        }

        private void txtDiscountTaka_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num3, sum1, sum2;
                num1 = double.Parse(txtDiscountTaka.Text);
                num3 = double.Parse(lblTotal.Text);
                sum1 = num3 - num1;
                sum2 = num3 - sum1;
                lblTotalDiscount.Text = sum2.ToString();
                txtTotalDiscount.Text = Convert.ToString(sum2);
            }
            catch
            {
                //
            }
            //if (txtDiscountTaka.Text == "0")
            //{
            //    MessageBox.Show("Fuck !");
            //    txtTotalDiscount.Text = "0";
            //}
            //else
            //{
            //    double num1, num3, sum1, sum2;
            //    num1 = double.Parse(txtDiscountTaka.Text);
            //    num3 = double.Parse(lblTotal.Text);
            //    sum1 = num3 - num1;
            //    sum2 = num3 - sum1;
            //    lblTotalDiscount.Text = sum2.ToString();
            //    txtTotalDiscount.Text = Convert.ToString(sum2);
            //}
        }

        private void txtTotalVat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, num3, sum1, sum2;
                num1 = double.Parse(txtTotalVat.Text);
                num2 = double.Parse(txtTotalDiscount.Text);
                num3 = double.Parse(lblTotal.Text);
                sum1 = num1 - num2;
                sum2 = sum1 + num3;
                txtTotalAmount.Text = Convert.ToString(sum2);
            }
            catch
            {
                //
            }
        }

        private void txtTotalDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, num3, sum1, sum2;
                num1 = double.Parse(txtTotalVat.Text);
                num2 = double.Parse(txtTotalDiscount.Text);
                num3 = double.Parse(lblTotal.Text);
                sum1 = num1 - num2;
                sum2 = sum1 + num3;
                txtTotalAmount.Text = Convert.ToString(sum2);
            }
            catch
            {
                //
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure save data?","Save Info",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                var db = new SANITARIUMEntities();
                var tb = new DignosisTestSaleInfo();

                tb.Name = txtName.Text.Trim();
                tb.Date = Convert.ToDateTime(dtpTestSaleDay.Value);
                tb.Address = txtAddress.Text.Trim();
                tb.GenderId = Convert.ToInt32(cbxGender.SelectedValue);
                tb.Age = Convert.ToInt32(txtAge.Text.Trim());
                tb.AgeStatusId = Convert.ToInt32(cbxAgeStatus.SelectedValue);
                tb.Phone = Convert.ToInt32(txtPhone.Text.Trim());
                tb.DoctorId = Convert.ToInt32(cbxDoctor.SelectedValue);
                tb.ReferId = Convert.ToInt32(cbxRefer.SelectedValue);
                tb.DiscountAuthorityId = Convert.ToInt32(cbxDisAuthor.SelectedValue);
                tb.Total_Cost = Convert.ToInt32(lblTotal.Text);
                tb.Discount = Convert.ToInt32(txtTotalDiscount.Text.Trim());
                tb.Due = Convert.ToInt32(txtDue.Text.Trim());
                tb.Paid = Convert.ToInt32(txtPaidAmount.Text.Trim());
                tb.VAT = txtTotalVat.Text.Trim();
                tb.BillOfficer = lblUserPrint.Text;
                tb.T_Reg = Convert.ToInt32(lblTotalReg.Text);
                tb.DueStatus = Convert.ToInt32(lblDueStatus.Text);
                tb.ReturnStatus = Convert.ToInt32(lblReturnStatus.Text.Trim());

                db.DignosisTestSaleInfoes.Add(tb);
                db.SaveChanges();

                
                if(MessageBox.Show("Dignostic Test Sale successfully. Are you went to PRINT this document?","Print Authontication",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    //printDocument1.Print(); one click print code.
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }                

                ClearText2();
                LoadUserReg();
            }
            
            
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtPaidAmount.Text == "")
                {
                    button1.Enabled = false;
                    txtPaidAmount.Text = "0";

                    if (txtPaidAmount.Text == "0")
                    {
                        button1.Enabled = false;
                    }
                    else
                    {
                        button1.Enabled = true;
                    }
                }
                else
                {
                    button1.Enabled = true;
                }

                double num1, num2, sum;
                num1 = double.Parse(txtTotalAmount.Text);
                num2 = double.Parse(txtPaidAmount.Text);
                sum = num1 - num2;
                txtDue.Text = Convert.ToString(sum);
                
                if (txtTotalAmount.Text == "0" && txtPaidAmount.Text == "0")
                {
                    lblFullPaid.Text = "No Transection !";
                }
                else if (txtDue.Text != "0")
                {
                    lblFullPaid.Text = "Due !";
                    lblDueStatus.Text = "1";
                }
                else if (txtTotalAmount.Text == txtPaidAmount.Text)
                {
                    lblFullPaid.Text = "Full Paid.";
                    lblDueStatus.Text = "0";
                }
            }
            catch
            {
                //
            }
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1, num2, sum;
                num1 = double.Parse(txtTotalAmount.Text);
                num2 = double.Parse(txtPaidAmount.Text);
                sum = num1 - num2;
                txtDue.Text = Convert.ToString(sum);

                if (txtTotalAmount.Text == "0" && txtPaidAmount.Text == "0")                    
                {
                    lblFullPaid.Text = "No Transection !";
                }
                else if (txtDue.Text != "0")
                {
                    lblFullPaid.Text = "Due !";
                    lblDueStatus.Text = "1";
                }
                else if (txtTotalAmount.Text == txtPaidAmount.Text)
                {
                    lblFullPaid.Text = "Full Paid.";
                    lblDueStatus.Text = "0";
                }
            }
            catch
            {
                //
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Hospital Managment Software", new Font("Arial", 36, FontStyle.Bold), Brushes.Black, new Point(50, 10));
            e.Graphics.DrawString("Shaheb Bazar, College Road, Kaliakair, Gazipur", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(130, 70));
            e.Graphics.DrawString("E-mail: samim.hossen5274@gmail.com", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(220, 105));
            e.Graphics.DrawString("Mobail: +880 1762-164746, +880 1533-021557", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(190, 135));
            e.Graphics.DrawString("Date: " + dtpTestSaleDay.Value.ToShortDateString(), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(650, 135));
            e.Graphics.DrawString("Id: "+lblTotalReg.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(50,135));
            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 140));



            int infoHeight = 170;
            int infoweight = 50;
            e.Graphics.DrawString("Name: "+txtName.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight));
            e.Graphics.DrawString("Phone: " + txtPhone.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 440, infoHeight));
            e.Graphics.DrawString("Age: " + txtAge.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight + 25));
            e.Graphics.DrawString("   " + cbxAgeStatus.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight+65, infoHeight + 25));
            e.Graphics.DrawString("Refer: " + cbxRefer.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight+440, infoHeight+25));
            e.Graphics.DrawString("Gender: " + cbxGender.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight + 200, infoHeight + 25));
            e.Graphics.DrawString("Address: " + txtAddress.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight+50));
            e.Graphics.DrawString("Doctor: " + cbxDoctor.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(infoweight, infoHeight+75));




            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 255));

            e.Graphics.DrawString("Test Name", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(50, 285));
            e.Graphics.DrawString("Price", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(700, 285));

            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 295));
            

            //==========================================  akahn theke grid view ar data print code soro  ==========================================================///

            int height = 285;
            for (int l = numberOfItemsPrintedSoFar; l < dgvStore.Rows.Count; l++)
            {
                numberOfItemsPerPage = numberOfItemsPerPage + 1;

                if (numberOfItemsPerPage <= 14)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= dgvStore.Rows.Count)
                    {

                        height += dgvStore.Rows[0].Height+15;
                        e.Graphics.DrawString(dgvStore.Rows[l].Cells[0].FormattedValue.ToString(), dgvStore.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(50, height, dgvStore.Columns[0].Width, dgvStore.Rows[0].Height));
                        e.Graphics.DrawString(dgvStore.Rows[l].Cells[1].FormattedValue.ToString(), dgvStore.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(700, height, dgvStore.Columns[0].Width, dgvStore.Rows[0].Height));
                        e.Graphics.DrawString("___________________________________________________________________________________________________________________", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, height +10));
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

            //e.Graphics.DrawString("___________________________________________________________________________________________________________________",
            //    new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, height + 10));
            

            e.Graphics.DrawString(lblTotalVat.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 40));
            e.Graphics.DrawString("VAT:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 40));

            e.Graphics.DrawString(lblTotalDiscount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 70));
            e.Graphics.DrawString("Discount:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(600, height + 70));

            e.Graphics.DrawString(txtTotalAmount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 100));
            e.Graphics.DrawString("Total:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 100));

            e.Graphics.DrawString(txtPaidAmount.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 130));
            e.Graphics.DrawString("Paid:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(638, height + 130));

            e.Graphics.DrawString(txtDue.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, height + 165));
            e.Graphics.DrawString("Due:", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(642, height + 165));

            



            e.Graphics.DrawString("Billing Officer: " + lblUserPrint.Text + ";", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(25, height + 150));
            e.Graphics.DrawString("Discount Author: " + cbxDisAuthor.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(25, height + 170));
            e.Graphics.DrawString("Paid Status: " + lblFullPaid.Text, new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(25, height + 200));
            e.Graphics.DrawString("সেবাই আমাদের ধর্ম এবং আমরা সর্বত্র আপনাদের সেবাই নিয়োজিত আছি।।ধন্যদাব।।আবার আসবেন।।", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Point(45, height+245));

            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;

        }

        private void btnPrintPreView_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //printer control ==================================
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;

            var ppc = new PrintPreviewControl();
            ppc.Document = printDocument1;
            ppc.Location = new Point(50,50);
            ppc.Width = 700;
            ppc.Height = 1000;
            ppc.Name = "PPC";

            this.Controls.Add(ppc);
        }
        
    }
}

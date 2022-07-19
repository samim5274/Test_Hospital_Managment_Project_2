using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class TestSaleForm : Form
    {
        public TestSaleForm()
        {
            InitializeComponent();
        }

        private void TestSaleForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet1.StoreTest' table. You can move, or remove it, as needed.
            this.storeTestTableAdapter.Fill(this.sANITARIUMDataSet1.StoreTest);
            dtpTestSaleDay.Value = DateTime.Now;
            DateTime now = DateTime.Now.Date;

            var db = new SANITARIUMEntities();
            var us = MotherForm.currentUser;
            var usReg = db.DignosisTestSaleInfoes.Where(a => a.Name == us && a.Date == now.Date).ToList();
            var cou = usReg.Count();
            var max = db.DignosisTestSaleInfoes.Count();
            var max2 = db.DignosisTestSaleInfoes.Count() + 1;
            //lblNextRegNo.Text ="Next Registation Number: "+ max2.ToString();
            lblNextRegNo.Text = max2.ToString();
            lbluser.Text = "User: " + us + "; ";
            lblReg.Text = "Today Total Registation: " + max.ToString() + " ;";

            dgvStore.DataSource = String.Empty;
            FilGender();
            FillAgeStatus();
            FillDiscoutnAuthority();
            FillDoctor();
            FillRefer();
            FillTest();
        }
        
        private void FillTest()
        {
            var obj = new Manager();
            var list = obj.GetTestName();
            cbxTestName.DisplayMember = "TestName";
            cbxTestName.ValueMember = "Id";
            cbxTestName.DataSource = list;
        }

        private void FillRefer()
        {
            var obj = new Manager();
            var list = obj.GetRefer();
            cbxRefer.DisplayMember = "Name";
            cbxRefer.ValueMember = "Id";
            cbxRefer.DataSource = list;
        }

        private void FillDoctor()
        {
            var obj = new Manager();
            var list = obj.GetDoctor();
            cbxDoctor.DisplayMember = "Name";
            cbxDoctor.ValueMember = "Id";
            cbxDoctor.DataSource = list;
        }

        private void FillDiscoutnAuthority()
        {
            var obj = new Manager();
            var list = obj.GetDiscountAuthor();
            cbxDisAuthor.DisplayMember = "Name";
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

        private void txtTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                var objdb = new SANITARIUMEntities();
                var objTTb = new DignosticTestDetail();
                var store = new StoreTest();
                var selector = objdb.DignosticTestDetails.Where(a => a.TestName == txtTest.Text).FirstOrDefault();
                var item = selector.TestName.ToString();
                //int i = listBox1.Items.Count + 1;
                //listBox1.Items.Add(i + " " + item); i++;
                int i = listBox1.Items.Count;
                listBox1.Items.Add(item);

                txtTest.Clear();                                
                txtTestName.Text = selector.TestName;
                txtTestPrice.Text = selector.TestCost.ToString();

                TestStore();
                textClear();

                //var d = objdb.DignosisTestSaleInfoes.Where(a => a.Id.ToString() == txtTotalAmount.Text).ToList();
                //var total = Convert.ToSingle(d.Sum(r => r.Total_Cost));
                //var totalPrice = Math.Round(total);
                //txtTotalAmount.Text = totalPrice.ToString();
            }
        }

        private void textClear()
        {
            txtTestName.Text = String.Empty;
            txtTestPrice.Text = string.Empty;
        }

        private void TestStore()
        {
            var db2 = new SANITARIUMEntities();
            var TS = new StoreTest();

            TS.S_TestName = txtTestName.Text.Trim();
            TS.S_TestPrice = Convert.ToInt32(txtTestPrice.Text.Trim());
            TS.S_RegNumber = Convert.ToInt32(lblNextRegNo.Text.Trim());

            db2.StoreTests.Add(TS);
            db2.SaveChanges();
            

            if (TS.S_RegNumber.ToString() == lblNextRegNo.Text)
            {
                FillGrid();
            }
            else
            {                
                dgvStore.DataSource = String.Empty;
            }
                    
        }

        private void FillGrid()
        {
            var db2 = new SANITARIUMEntities();
            var TS = new StoreTest();     
              
            dgvStore.DataSource = db2.StoreTests.Where(a=>a.S_RegNumber.ToString() == lblNextRegNo.Text).ToList();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new SANITARIUMEntities();
                var SaleTest = new DignosisTestSaleInfo();

                SaleTest.Name = txtName.Text.Trim();
                SaleTest.Address = txtAddress.Text.Trim();
                SaleTest.GenderId = Convert.ToInt32(cbxGender.SelectedValue);
                SaleTest.Age = Convert.ToInt32(txtAge.Text.Trim());
                SaleTest.AgeStatusId = Convert.ToInt32(cbxAgeStatus.SelectedValue);
                SaleTest.Phone = Convert.ToInt32(txtPhone.Text.Trim());
                SaleTest.Date = Convert.ToDateTime(dtpTestSaleDay.Value);
                SaleTest.DoctorId = Convert.ToInt32(cbxDoctor.SelectedValue);
                SaleTest.ReferId = Convert.ToInt32(cbxRefer.SelectedValue);
                SaleTest.DiscountAuthorityId = Convert.ToInt32(cbxDisAuthor.SelectedValue);                
                SaleTest.Total_Cost = Convert.ToInt32(txtTotalAmount.Text.Trim());
                SaleTest.Paid = Convert.ToInt32(txtPaidAmount.Text.Trim());
                SaleTest.Due = Convert.ToInt32(txtTotalAmount.Text.Trim());
                SaleTest.Discount = Convert.ToInt32(txtDiscountTaka.Text.Trim());                


                db.DignosisTestSaleInfoes.Add(SaleTest);
                db.SaveChanges();
                MessageBox.Show("Save Successfully","Data Saves",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DeleteItem();
            textClear();
            RemoveItem();
        }

        private void DeleteItem()
        {
            var db = new SANITARIUMEntities();
            var tb = new StoreTest();

            var itm = db.StoreTests.ToList();

            var catchRow = db.StoreTests.Where(a => a.S_TestName == txtTestName.Text).FirstOrDefault();
            db.Entry(catchRow).State = EntityState.Deleted;
            db.SaveChanges();
            FillGrid();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveItem();            
        }

        private void RemoveItem()
        {
            if (listBox1.SelectedItem != null)
            {
                txtTestName.Text = listBox1.SelectedItem.ToString();
            }

            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                DeleteItem();
                FillGrid();
            }
        }
        
        private void dgvStore_MouseClick(object sender, MouseEventArgs e)
        {
            txtTestName.Text = dgvStore.SelectedRows[0].Cells[0].Value.ToString();
            txtTestPrice.Text = dgvStore.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //DeleteItem();
            //textClear();
            if (listBox1.SelectedItem != null)
            {
                txtTestName.Text = listBox1.SelectedItem.ToString();
            }

            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                DeleteItem();
                FillGrid();
            }
        }
    }
}

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
    public partial class ReportGenaretForm : Form
    {
        public ReportGenaretForm()
        {
            InitializeComponent();
        }

        private void ReportGenaretForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet8.ReportGenaret' table. You can move, or remove it, as needed.
            this.reportGenaretTableAdapter1.Fill(this.sANITARIUMDataSet8.ReportGenaret);
            FillTestName();
            LoadCount();
            //dgvReportValue.DataSource = string.Empty;
            ClearTest();
        }

        private void ClearTest()
        {
            txtPartOfTest.Text = string.Empty;
            txtReferenceValu.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtUnit.Text = string.Empty;
            cbxTestName.Text = string.Empty;
        }

        private void LoadCount()
        {
            var db = new SANITARIUMEntities();
            var Reg = db.ReportGenarets.Where(x => x.TestId.ToString() == label8.Text).ToList();
            var cun = Reg.Count();
            var max = db.ReportGenarets.Count();
            label8.Text = "" + max;
        }

        private void FillTestName()
        {
            var db = new Manager();
            var list = db.GetTestName();
            cbxTestName.DisplayMember = "TestName";
            cbxTestName.ValueMember = "Id";
            cbxTestName.DataSource = list;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new SANITARIUMEntities();
                var tb = new ReportGenaret();
                
                tb.TestId = Convert.ToInt32(cbxTestName.SelectedValue);
                tb.TestName = txtPartOfTest.Text.Trim();
                tb.Result = txtResult.Text.Trim();
                tb.Unit = txtUnit.Text.Trim();
                tb.ReferenceValue = txtReferenceValu.Text.Trim();

                db.ReportGenarets.Add(tb);
                db.SaveChanges();
                ClearTest();
                LoadCount();
            }
            catch
            {
                MessageBox.Show("Not possible","Unsuccessfull !", MessageBoxButtons.RetryCancel,MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                var db = new SANITARIUMEntities();
                var tb = new DignosisTestSaleInfo();
                var tb2 = new StoreTest();
                var tb3 = new ReportGenaret();

                var PT = db.DignosisTestSaleInfoes.Where(x=>x.Id.ToString() == txtSearch.Text && x.ReturnStatus.ToString() == lbltestReturnStatus.Text).FirstOrDefault();          
            
                txtName.Text = PT.Name;
                txtAddress.Text = PT.Address;
                txtPhone.Text = PT.Phone.ToString();
                txtS_Reg.Text = PT.T_Reg.ToString();

                dgvTest.DataSource = db.StoreTests.Where(x => x.S_RegNumber.ToString() == txtS_Reg.Text).ToList();
            
                dgvReport.DataSource = db.ReportGenarets.Where(x => x.TestId.ToString() == txtTestId.Text).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvTest_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtTestId.Text = dgvTest.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void txtTestId_TextChanged(object sender, EventArgs e)
        {
            FillTestGrid();
        }

        private void FillTestGrid()
        {
            var db = new SANITARIUMEntities();

            dgvReport.DataSource = db.ReportGenarets.Where(x => x.TestId.ToString() == txtTestId.Text).ToList();
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUTestName.Text = dgvReport.SelectedRows[0].Cells[0].Value.ToString();
            txtUResult.Text = dgvReport.SelectedRows[0].Cells[1].Value.ToString();
            txtUUnit.Text = dgvReport.SelectedRows[0].Cells[2].Value.ToString();
            txtUReferenceValu.Text = dgvReport.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var db = new SANITARIUMEntities();
            var tb = db.ReportGenarets.ToList().Where(x => x.TestName == txtUTestName.Text.Trim()).FirstOrDefault();

            tb.TestName = txtUTestName.Text.Trim();
            tb.Result = txtUResult.Text.Trim();
            tb.Unit = txtUUnit.Text.Trim();
            tb.ReferenceValue = txtUReferenceValu.Text.Trim();

            db.SaveChanges();
            FillTestGrid();
            ClearText();
        }

        private void ClearText()
        {
            txtUTestName.Text = string.Empty;
            txtUResult.Text = string.Empty;
            txtUUnit.Text = string.Empty;
            txtUReferenceValu.Text = string.Empty;

        }
    }
}

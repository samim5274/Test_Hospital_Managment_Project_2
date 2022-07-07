using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login
{
    public partial class xxxxxxxxxxxxxx : Form
    {
        public xxxxxxxxxxxxxx()
        {
            InitializeComponent();
        }

        int n = 0;

        private void txtTestname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                var db = new SANITARIUMEntities();
                var DTD = new DignosticTestDetail();
                var store = new StoreTest();
                var selector = db.DignosticTestDetails.Where(a => a.TestName == txtTestname.Text).FirstOrDefault();
                var item = selector.TestName.ToString();

                txtFillTestName.Text = selector.TestName;
                txtFillPrice.Text = selector.TestCost.ToString();

                DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dgvStore);
                    newRow.Cells[0].Value = n + 1;
                    newRow.Cells[1].Value = txtFillTestName.Text;
                    newRow.Cells[2].Value = txtFillPrice.Text;
                    dgvStore.Rows.Add(newRow);
                    n++;                              

            }
        }

        private void xxxxxxxxxxxxxx_Load(object sender, EventArgs e)
        {
            
            // TODO: This line of code loads data into the 'sANITARIUMDataSet4.DignosisTestSaleInfo' table. You can move, or remove it, as needed.
            this.dignosisTestSaleInfoTableAdapter.Fill(this.sANITARIUMDataSet4.DignosisTestSaleInfo);

            dtpEDate.Value = DateTime.Now;
            dtpSDate.Value = DateTime.Now;
            
        }
        

        private void btnShow2_Click(object sender, EventArgs e)
        {
            var db = new SANITARIUMEntities();
            if(txtName.Text == "")
            {
                dgvSaleReport.DataSource = db.DignosisTestSaleInfoes.ToList();
            }
            else if (txtName.Text != "")
            {
                dgvSaleReport.DataSource = db.DignosisTestSaleInfoes.Where(x => x.Name.ToString() == txtName.Text).ToList();
            }            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var SD = Convert.ToDateTime(dtpSDate.Value);
            var ED = Convert.ToDateTime(dtpEDate.Value);
            var mngr = new Manager();
            var list = mngr.GetSaleSummary(SD,ED);
            dgvSaleReport.DataSource = list;
        }
    }
}

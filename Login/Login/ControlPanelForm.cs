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
    public partial class ControlPanelForm : Form
    {
        public ControlPanelForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var db = new SANITARIUMEntities();
            var objTb = new UserInfo();

            var selector = db.UserInfoes.Where(a => a.Username == cbxUser.Text).FirstOrDefault();

            if (chkProfail.Checked) { selector.Profail = "Y"; } else { selector.Profail = "N"; }
            if (chkService.Checked) { selector.Services = "Y"; } else { selector.Services = "N"; }
            if (chkAbout.Checked) { selector.About = "Y"; } else { selector.About = "N"; }
            if (chkContact.Checked) { selector.Contact = "Y"; } else { selector.Contact = "N"; }
            if (chkPermission.Checked) { selector.Permission = "Y"; } else { selector.Permission = "N"; }


            db.SaveChanges();
            MessageBox.Show("User Permission Udated.","Permission",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            FillGrid();
            Unchechbox();
        }

        private void txtScarch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Scarch();
            }
        }

        private void Scarch()
        {
            var db = new SANITARIUMEntities();
            var objTb = new UserInfo();

            var getdata = db.UserInfoes.Where(a => a.Username == txtScarch.Text).FirstOrDefault();

            cbxUser.SelectedItem = getdata.Username.ToString();

        }

        private void ControlPanelForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANITARIUMDataSet.UserInfo' table. You can move, or remove it, as needed.
            this.userInfoTableAdapter.Fill(this.sANITARIUMDataSet.UserInfo);
            ThrowData();
            FillGrid();
            Unchechbox();
            userName();
        }

        private void userName()
        {
            var UserC = MotherForm.User;
            label1.Text = UserC;
        }

        private void Unchechbox()
        {
            chkProfail.Checked = false;
            chkPermission.Checked = false;
            chkService.Checked = false;
            chkContact.Checked = false;
            chkAbout.Checked = false;
        }

        private void FillGrid()
        {
            var db = new SANITARIUMEntities();
            var Tb = new UserInfo();
            dgvUserlist.DataSource = db.UserInfoes.ToList();
        }

        private void ThrowData()
        {
            var db = new SANITARIUMEntities();
            cbxUser.Items.Clear();
            cbxUser.SelectedIndex = -1;
            var getname = db.UserInfoes.ToList();
            foreach (var item in getname)
            {
                cbxUser.Items.Add(item.Username);
            }
            cbxUser.SelectedIndex = -1;
        }

        private void dgvUserlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUserlist.Rows[e.RowIndex].Cells[e.ColumnIndex].Value !=null)
            {
                dgvUserlist.CurrentRow.Selected = true;
                cbxUser.Text = dgvUserlist.Rows[e.RowIndex].Cells["Username"].FormattedValue.ToString();
            }            
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.userInfoTableAdapter.FillBy(this.sANITARIUMDataSet.UserInfo);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}

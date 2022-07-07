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
    public partial class MotherForm : Form
    {
        public static string User;
        public static string currentUser;
        

        public MotherForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userLogin();
        }

        private void userLogin()
        {
            var db = new SANITARIUMEntities();
            var tb = new AdminTable();

            var user = db.AdminTables.Where(a => a.UserAdmin == txtUsername.Text && a.PassAdmin == txtPassword.Text).FirstOrDefault();

            
            if (user != null)
            {
                gpLogin.Visible = false;
                menuStrip1.Visible = profailToolStripMenuItem.Visible = servicesToolStripMenuItem.Visible = aboutToolStripMenuItem.Visible = contactToolStripMenuItem.Visible = teamToolStripMenuItem.Visible = settingToolStripMenuItem.Visible = true;
                lbllog.Text = txtUsername.Text.ToUpper();
                txtUsername.Text = txtPassword.Text = "";
                lbllog.Show();
                User = user.UserAdmin;
                currentUser = user.UserAdmin;
            }
            else if ( txtUsername.Text == "manager" && txtPassword.Text == "manager")
            {
                gpLogin.Visible = false;
                menuStrip1.Visible = profailToolStripMenuItem.Visible = servicesToolStripMenuItem.Visible = aboutToolStripMenuItem.Visible = contactToolStripMenuItem.Visible = teamToolStripMenuItem.Visible = settingToolStripMenuItem.Visible = false;
                lbllog.Text = txtUsername.Text.ToUpper();
                txtUsername.Text = txtPassword.Text = "";
                lbllog.Show();
            }
            else
            {
                usersLogin();
            }
            txtUsername.Focus();

        }

        private void usersLogin()
        {
            var db = new SANITARIUMEntities();
            var tb = new UserInfo();

            var user1 = db.UserInfoes.Where(a => a.Username == txtUsername.Text && a.Password == txtPassword.Text).FirstOrDefault();
            if(user1 != null)
            {
                gpLogin.Visible = false;
                if (user1.Profail == "Y") { profailToolStripMenuItem.Visible = true; } else { profailToolStripMenuItem.Visible = false; }
                if (user1.Permission == "Y") { servicesToolStripMenuItem.Visible = true; } else { servicesToolStripMenuItem.Visible = false; }
                if (user1.Permission == "Y") { aboutToolStripMenuItem.Visible = true; } else { aboutToolStripMenuItem.Visible = false; }
                if (user1.Permission == "Y") { teamToolStripMenuItem.Visible = true; } else { teamToolStripMenuItem.Visible = false; }
                if (user1.Permission == "Y") { settingToolStripMenuItem.Visible = true; } else { settingToolStripMenuItem.Visible = false; }

                User = user1.FastName + " " + user1.LastName;
                lbllog.Show();
                lbllog.Text = " " + User;

                currentUser = user1.Username;
            }
            else
            {
                MessageBox.Show("Plase chick the username and password and try again","Login fail",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            txtUsername.Focus();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutGroup();
            gpLogin.Visible = true;
            profailToolStripMenuItem.Visible = servicesToolStripMenuItem.Visible = aboutToolStripMenuItem.Visible = contactToolStripMenuItem.Visible = teamToolStripMenuItem.Visible = settingToolStripMenuItem.Visible = false;
            lbllog.Hide();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void LogoutGroup()
        {
            try
            {
                this.ActiveMdiChild.Close();
            }
            catch (Exception)
            {

            }
        }

        private void testSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new TestSaleForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new ControlPanelForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutGroup();
            gpLogin.Visible = true;
            profailToolStripMenuItem.Visible = servicesToolStripMenuItem.Visible = aboutToolStripMenuItem.Visible = contactToolStripMenuItem.Visible = teamToolStripMenuItem.Visible = settingToolStripMenuItem.Visible = false;
            lbllog.Hide();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void MotherForm_Load(object sender, EventArgs e)
        {

        }

        private void testSale2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new SaleForm2();
            obj.MdiParent = this;
            obj.Show();
        }

        private void xxxxxxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new xxxxxxxxxxxxxx();
            obj.MdiParent = this;
            obj.Show();
        }

        private void dueCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new DueCollectionForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void saleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new SaleReportForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void testCancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new TestCalcelForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void reportGenaretToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new ReportGenaretForm();
            obj.MdiParent = this;
            obj.Show();
        }
    }
}

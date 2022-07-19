using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class SaleReportForm : Form
    {
        public SaleReportForm()
        {
            InitializeComponent();
        }


        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        private void btnShow_Click(object sender, EventArgs e)
        {
            var SD = Convert.ToDateTime(dtpSDate.Value);
            var ED = Convert.ToDateTime(dtpEDate.Value);
            var mngr = new Manager();
            var list = mngr.GetSaleSummary(SD, ED);
            dgvSaleReport.DataSource = list;
        }

        private void label2_Click(object sender, EventArgs e)
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
            e.Graphics.DrawString("___________________________________________________________________________________________________________________",
                new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 140));

            int height = 285;
            for (int l = numberOfItemsPrintedSoFar; l < dgvSaleReport.Rows.Count; l++)
            {
                numberOfItemsPerPage = numberOfItemsPerPage + 1;

                if (numberOfItemsPerPage <= 14)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= dgvSaleReport.Rows.Count)
                    {

                        height += dgvSaleReport.Rows[0].Height + 15;
                        e.Graphics.DrawString(dgvSaleReport.Rows[l].Cells[0].FormattedValue.ToString(), dgvSaleReport.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(50, height, dgvSaleReport.Columns[0].Width, dgvSaleReport.Rows[0].Height));
                        e.Graphics.DrawString(dgvSaleReport.Rows[l].Cells[1].FormattedValue.ToString(), dgvSaleReport.Font = new Font("Book Antiqua", 14), Brushes.Black, new RectangleF(150, height, dgvSaleReport.Columns[0].Width, dgvSaleReport.Rows[0].Height));
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

            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;
        }

    }
}

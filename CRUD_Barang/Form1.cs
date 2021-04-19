using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Barang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtCari.Text))
                {
                    this.table_barangTableAdapter.Fill(this.appData.table_barang);
                    tablebarangBindingSource.DataSource = this.appData.table_barang;
                    //dataGridView.DataSource = tablebarangBindingSource;
                }
                else
                {
                    var query = from o in this.appData.table_barang
                                where o.Nama.Contains(txtCari.Text) || o.Harga == txtCari.Text || o.Jumlah == txtCari.Text || o.Tanggal.Contains(txtCari.Text)
                                select o;
                    tablebarangBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    tablebarangBindingSource.RemoveCurrent();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNama.Focus();
                this.appData.table_barang.Addtable_barangRow(this.appData.table_barang.Newtable_barangRow());
                tablebarangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tablebarangBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNama.Focus();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                tablebarangBindingSource.EndEdit();
                table_barangTableAdapter.Update(this.appData);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tablebarangBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.table_barang' table. You can move, or remove it, as needed.
            this.table_barangTableAdapter.Fill(this.appData.table_barang);
            tablebarangBindingSource.DataSource = this.appData.table_barang;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCatedraMDB
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void FormInicio_Load(object sender, EventArgs e)
        {

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            pnlContenedor.Controls.Clear();

            FormProductos frm = new FormProductos();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(frm);

            frm.Show();
        }

        private void btnMovpanel_Click(object sender, EventArgs e)
        {

        }
    }
}

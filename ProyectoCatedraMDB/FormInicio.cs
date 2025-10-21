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

            lblUsuario.Text = SesionUsuario.Nombre;
        }

        public void MostrarOcultarBotonesPorCargo(string cargo)
        {
            if (cargo.ToUpper() == "ADMINISTRADOR")
            {
                btnUsuarios.Visible = true;
            }
            else { 
                btnUsuarios.Visible = false;
            }
        }

        private void FormInicio_Load(object sender, EventArgs e){}

        // Botones Panel Vertical
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            pnlContenedor.Controls.Clear();

            FormProveedores frm = new FormProveedores();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(frm);

            frm.Show();
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            pnlContenedor.Controls.Clear();

            FormUsuarios frm = new FormUsuarios();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(frm);

            frm.Show();
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
        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            pnlContenedor.Controls.Clear();

            FormAuditoria frm = new FormAuditoria();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(frm);

            frm.Show();
        }
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Está seguro que desea cerrar la sesión actual?",
            "Confirmar Cierre de Sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                SesionUsuario.Nombre = null;
                SesionUsuario.Cargo = null;

                FormLogin loginForm = new FormLogin();
                loginForm.Show();
                this.Close();
            }
        }

        // Sin funcion
        private void btnMovpanel_Click(object sender, EventArgs e) { }
    }
}

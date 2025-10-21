using ProyectoCatedraMDB.Modelo;
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
    public partial class FormAgregarProveedor : Form
    {
        public FormAgregarProveedor()
        {
            InitializeComponent();
        }

        private void FormAgregarProveedor_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreProveedor = txtNombre.Text.Trim();
            string contacto = txtContacto.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreProveedor) || string.IsNullOrWhiteSpace(contacto))
            {
                MessageBox.Show("Por favor, complete tanto el Nombre del Proveedor como el Contacto.", "Campos Faltantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Desea agregar el proveedor: {nombreProveedor}?",
                "Confirmar Agregar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    if (db.Proveedores.Any(p => p.NombreProveedor.Equals(nombreProveedor, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"El proveedor con el nombre '{nombreProveedor}' ya existe en la base de datos.", "Error de Duplicidad",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Proveedores nuevoProveedor = new Proveedores
                    {
                        NombreProveedor = nombreProveedor,
                        Contacto = contacto
                    };

                    db.Proveedores.Add(nuevoProveedor);
                    db.SaveChanges();

                    MessageBox.Show("Proveedor agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombre.Text = string.Empty;
                    txtContacto.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Error al agregar el proveedor: " + mensajeError, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormProveedores frm = new FormProveedores();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }
    }
}

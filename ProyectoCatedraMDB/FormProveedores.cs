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
    public partial class FormProveedores : Form
    {
        public FormProveedores()
        {
            InitializeComponent();
        }
        private void FormProveedores_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var lista = db.Proveedores
                      .Select(datos => new
                      {
                          ID = datos.IdProveedor,
                          Nombre = datos.NombreProveedor,
                          Contracto = datos.Contacto
                      })
                      .ToList();    
                dgvProveedores.DataSource = lista;
            }
        }

        // Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormAgregarProveedor frm = new FormAgregarProveedor();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona el proveedor que deseas editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedorAEditar = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells["ID"].Value);

            pnlBody.Controls.Clear();

            FormEditarProveedor frm = new FormEditarProveedor(idProveedorAEditar);

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);
            frm.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona el proveedor que deseas eliminar.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedorAEliminar = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells["ID"].Value);
            string nombreProveedor = dgvProveedores.SelectedRows[0].Cells["Nombre"].Value.ToString();

            DialogResult resultado = MessageBox.Show($"¿Está seguro que desea eliminar permanentemente al proveedor '{nombreProveedor}'?",
                                                      "Confirmar Eliminación",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var proveedor = db.Proveedores.FirstOrDefault(p => p.IdProveedor == idProveedorAEliminar);

                    if (proveedor != null)
                    {
                        db.Proveedores.Remove(proveedor);
                        db.SaveChanges();

                        MessageBox.Show($"Proveedor '{nombreProveedor}' eliminado exitosamente.", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarProveedores();
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                string mensajeError = "No se puede eliminar este proveedor porque tiene registros de ingresos de productos asociados (Clave Foránea).";

                if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    mensajeError += "\nDesvincule primero los registros de ingreso antes de eliminar.";
                }

                MessageBox.Show(mensajeError, "Error de Restricción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Error al eliminar el proveedor: " + mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

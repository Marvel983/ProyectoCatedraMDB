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
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var lista = db.Usuarios
                      .Select(datos => new
                      {
                          ID = datos.IdUsuario,
                          Nombre = datos.NombreUsuario,
                          Cargo = datos.CargoUsuario,
                          Activo = datos.Activo
                      })
                      .ToList();
                dgvUsuarios.DataSource = lista;
            }
        }

        // Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormAgregarUsuarios frm = new FormAgregarUsuarios();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona el usuario que deseas editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuarioAEditar = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["ID"].Value);

            pnlBody.Controls.Clear();

            FormEditarUsuarios frm = new FormEditarUsuarios(idUsuarioAEditar);

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);
            frm.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona el usuario que deseas eliminar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuarioAEliminar = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["ID"].Value);
            int idUsuarioLogueado = SesionUsuario.IdEmpleado;

            // 1. Validación de seguridad: No permitir que el usuario logueado se elimine a sí mismo
            if (idUsuarioAEliminar == idUsuarioLogueado)
            {
                MessageBox.Show("No puedes eliminar tu propia cuenta de usuario.", "Error de Seguridad",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 2. Mensaje de confirmación para ELIMINACIÓN
            DialogResult result = MessageBox.Show("Advertencia: Al eliminar el usuario, se perderá su registro de acceso y se afectará el historial de IngresoProductos (IdUsuario se pondrá en NULL o fallará si el historial lo impide). ¿Deseas ELIMINAR al usuario?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (DBTiendaEntities db = new DBTiendaEntities())
                    {
                        var usuarioAEliminar = db.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuarioAEliminar);

                        if (usuarioAEliminar != null)
                        {
                            // 3. Realizar la eliminación física
                            db.Usuarios.Remove(usuarioAEliminar);

                            int rowsAffected = db.SaveChanges();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"El usuario '{usuarioAEliminar.NombreUsuario}' ha sido eliminado correctamente.", "Éxito",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Recargar la lista
                                CargarUsuarios();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el usuario.", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
                {
                    // Capturar error de integridad referencial si la FK no permite ON DELETE SET NULL
                    MessageBox.Show("Error de integridad de datos: El usuario no puede ser eliminado porque tiene registros asociados (IngresoProductos) que impiden la acción. Considere desactivar la cuenta en su lugar.",
                                    "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                    MessageBox.Show("Ocurrió un error inesperado al intentar eliminar el usuario: " + mensajeError, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

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
    public partial class FormEditarUsuarios : Form
    {
        private int _idUsuarioEditando;
        public FormEditarUsuarios(int idUsuario)
        {
            InitializeComponent();
            this._idUsuarioEditando = idUsuario;
        }
        private void FormEditarUsuarios_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatosUsuario(this._idUsuarioEditando);
        }

        private void CargarCombos()
        {
            // Cargar Cargos (cbCargo)
            cbCargo.Items.Add("Administrador");
            cbCargo.Items.Add("Empleado");

            // Cargar Estado Activo (cbActivo)
            var estados = new[]
            {
                new { Nombre = "Activo", Valor = true },
                new { Nombre = "Inactivo", Valor = false }
            };

            cbActivo.DataSource = estados;
            cbActivo.DisplayMember = "Nombre";
            cbActivo.ValueMember = "Valor";
        }

        private void CargarDatosUsuario(int idUsuario)
        {
            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var usuario = db.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

                    if (usuario != null)
                    {
                        lblIdUsuario.Text = usuario.IdUsuario.ToString();
                        txtNombre.Text = usuario.NombreUsuario.Trim();
                        txtContra.Text = "";

                        cbCargo.SelectedItem = usuario.CargoUsuario.Trim();
                        cbActivo.SelectedValue = usuario.Activo;
                    }
                    else
                    {
                        MessageBox.Show("Error: Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnRegresar_Click(null, null); // Regresar si falla la carga
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botones
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
        cbCargo.SelectedItem == null ||
        cbActivo.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete los campos Nombre y seleccione Cargo/Estado.", "Campos Faltantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea guardar los cambios del usuario?",
                "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string nuevoNombre = txtNombre.Text.Trim();
            string nuevoCargo = cbCargo.SelectedItem.ToString();
            bool nuevoEstadoActivo = (bool)cbActivo.SelectedValue;
            string nuevaContrasena = txtContra.Text; 

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var usuarioAEditar = db.Usuarios.FirstOrDefault(u => u.IdUsuario == this._idUsuarioEditando);

                    if (usuarioAEditar != null)
                    {
                        // 3. Validar Nombre Único (si lo cambió)
                        if (usuarioAEditar.NombreUsuario.Trim() != nuevoNombre &&
                            db.Usuarios.Any(u => u.NombreUsuario.Trim() == nuevoNombre))
                        {
                            MessageBox.Show($"El nombre de usuario '{nuevoNombre}' ya está en uso por otro usuario.", "Error de Duplicidad",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 4. Aplicar Cambios
                        usuarioAEditar.NombreUsuario = nuevoNombre;
                        usuarioAEditar.CargoUsuario = nuevoCargo;
                        usuarioAEditar.Activo = nuevoEstadoActivo;

                        // 5. Actualizar Contraseña SOLO si se ingresó algo
                        if (!string.IsNullOrWhiteSpace(nuevaContrasena))
                        {
                            // NOTA DE SEGURIDAD: En un sistema real, aquí se aplica el hashing.
                            usuarioAEditar.Contrasena = nuevaContrasena;
                            MessageBox.Show("Contraseña actualizada exitosamente.", "Aviso de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // 6. Guardar y Redirigir
                        db.SaveChanges();

                        MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRegresar_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Error al actualizar el usuario: " + mensajeError, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormUsuarios frm = new FormUsuarios();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }

        // Sin Funciones
        private void label7_Click(object sender, EventArgs e) { }

      
    }
}

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
    public partial class FormAgregarUsuarios : Form
    {
        public FormAgregarUsuarios()
        {
            InitializeComponent();
        }

        private void FormAgregarUsuarios_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }

        // Cargar Datos
        private void CargarCombos()
        {
            // Cargar Cargos
            cbCargo.Items.Add("Administrador");
            cbCargo.Items.Add("Empleado");
            cbCargo.SelectedIndex = 0; 

            var estados = new[]
            {
                new { Nombre = "Activo", Valor = true },
                new { Nombre = "Inactivo", Valor = false }
            };

            cbActivo.DataSource = estados;
            cbActivo.DisplayMember = "Nombre";
            cbActivo.ValueMember = "Valor";
            cbActivo.SelectedValue = true;
        }

        // Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
        string.IsNullOrWhiteSpace(txtContra.Text) ||
        cbCargo.SelectedItem == null ||
        cbActivo.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos Faltantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtención de datos
            string nombreUsuario = txtNombre.Text.Trim();
            string contrasena = txtContra.Text; // En un sistema real, aquí se encriptaría.
            string cargo = cbCargo.SelectedItem.ToString();
            bool activo = (bool)cbActivo.SelectedValue;

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    // Opcional: Verificar si el nombre de usuario ya existe (UNIQUE constraint)
                    if (db.Usuarios.Any(u => u.NombreUsuario.Trim() == nombreUsuario))
                    {
                        MessageBox.Show($"El nombre de usuario '{nombreUsuario}' ya está en uso.", "Error de Duplicidad",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 3. Crear el nuevo objeto Usuario
                    Usuarios nuevoUsuario = new Usuarios
                    {
                        NombreUsuario = nombreUsuario,
                        Contrasena = contrasena,
                        CargoUsuario = cargo,
                        Activo = activo
                    };

                    db.Usuarios.Add(nuevoUsuario);
                    db.SaveChanges();

                    MessageBox.Show("Usuario agregado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    btnRegresar_Click(sender, e); 
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Ocurrió un error al agregar el usuario: " + mensajeError, "Error de DB",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtContra.Clear();
            cbCargo.SelectedIndex = 0;
            cbActivo.SelectedValue = true;
            txtNombre.Focus();
        }

        // Sin funciones
        private void label8_Click(object sender, EventArgs e) { }
    }
}

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
    public partial class FormEditarProveedor : Form
    {
        private int IdProveedorEditando { get; set; }

        public FormEditarProveedor(int idProveedor)
        {
            InitializeComponent();
            this.IdProveedorEditando = idProveedor;
        }

        private void FormEditarProveedor_Load(object sender, EventArgs e)
        {
            CargarDatosProveedor(this.IdProveedorEditando);
        }

        // Cargar Datos
        private void CargarDatosProveedor(int id)
        {
            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var proveedor = db.Proveedores
                                     .FirstOrDefault(p => p.IdProveedor == id);

                    if (proveedor != null)
                    {
                        lblIdProveedor.Text = proveedor.IdProveedor.ToString();
                        txtNombre.Text = proveedor.NombreProveedor.Trim();
                        txtContacto.Text = proveedor.Contacto.Trim();

                    }
                    else
                    {
                        MessageBox.Show("Error: El proveedor a editar no fue encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del proveedor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botones
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombre.Text.Trim();
            string nuevoContacto = txtContacto.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre) || string.IsNullOrWhiteSpace(nuevoContacto))
            {
                MessageBox.Show("Por favor, complete tanto el Nombre del Proveedor como el Contacto.", "Campos Faltantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea guardar los cambios del proveedor?",
                "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var proveedorAEditar = db.Proveedores.FirstOrDefault(p => p.IdProveedor == this.IdProveedorEditando);

                    if (proveedorAEditar != null)
                    {
                        if (proveedorAEditar.NombreProveedor.Trim() != nuevoNombre &&
                            db.Proveedores.Any(p => p.NombreProveedor.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageBox.Show($"El nombre de proveedor '{nuevoNombre}' ya está en uso.", "Error de Duplicidad",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 5. Aplicar Cambios
                        proveedorAEditar.NombreProveedor = nuevoNombre;
                        proveedorAEditar.Contacto = nuevoContacto;

                        db.SaveChanges();

                        MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRegresar_Click(sender, e); 
                    }
                    else
                    {
                        MessageBox.Show("Error: El proveedor no fue encontrado para la edición.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Error al actualizar el proveedor: " + mensajeError, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

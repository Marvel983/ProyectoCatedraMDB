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
    public partial class FormAgregarProducto : Form
    {
        public FormAgregarProducto()
        {
            InitializeComponent();
        }

        private void FormAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProveedores();
        }

        private void CargarCategorias()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var categorias = db.Categoria.ToList();
                cbCategoria.DataSource = categorias;
                cbCategoria.DisplayMember = "NombreCategoria";
                cbCategoria.ValueMember = "IdCategoria";
                if (cbCategoria.Items.Count > 0)
                {
                    cbCategoria.SelectedIndex = 0;
                }
            }
        }

        private void CargarProveedores()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var proveedores = db.Proveedores
                                     .Select(p => new {
                                         ID = p.IdProveedor,
                                         Nombre = p.NombreProveedor,
                                     })
                                     .ToList();

                cbProveedor.DataSource = proveedores;
                cbProveedor.DisplayMember = "Nombre"; 
                cbProveedor.ValueMember = "ID";       
            }
        }

        //Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                cbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete todos los campos y seleccione una categoría.", "Campos Faltantes",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudStock.Value <= 0)
            {
                MessageBox.Show("La cantidad de stock inicial debe ser mayor a cero.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio ingresado no es un formato numérico válido.", "Error de Formato",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idCategoriaSeleccionada = (int)cbCategoria.SelectedValue;
            int cantidadIngreso = (int)nudStock.Value;

            int idProveedorSeleccionado;
            try
            {
                idProveedorSeleccionado = (int)cbProveedor.SelectedValue;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Error al obtener el ID del proveedor. Asegúrese de que el ComboBox esté configurado correctamente.", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idUsuarioLogueado = SesionUsuario.IdEmpleado;
            if (idUsuarioLogueado <= 0)
            {
                MessageBox.Show("Error de seguridad: No se pudo identificar al usuario logueado. Por favor, reinicie la aplicación e inicie sesión.",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    Productos nuevoProducto = new Productos
                    {
                        NombreProducto = txtNombre.Text.Trim(),
                        DescripcionProd = txtDescripcion.Text.Trim(),
                        PrecioProd = precio,
                        IdCategoria = idCategoriaSeleccionada
                    };

                    db.Productos.Add(nuevoProducto);
                    db.SaveChanges();

                    Stock nuevoStock = new Stock
                    {
                        IdProducto = nuevoProducto.IdProducto,
                        CantidadActual = cantidadIngreso,
                        FechaActualizacion = DateTime.Now
                    };

                    db.Stock.Add(nuevoStock);

                    IngresoProductos nuevoIngreso = new IngresoProductos
                    {
                        IdProducto = nuevoProducto.IdProducto,
                        IdProveedor = idProveedorSeleccionado,
                        CantidadIngreso = cantidadIngreso,
                        IdUsuario = idUsuarioLogueado,
                        FechaIngreso = DateTime.Now
                    };

                    db.IngresoProductos.Add(nuevoIngreso);
                    db.SaveChanges();

                    MessageBox.Show("Producto y registro de stock inicial agregado exitosamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = "Error al guardar el producto: ";
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    mensajeError += ex.InnerException.InnerException.Message;
                }
                else
                {
                    mensajeError += ex.Message;
                }
                MessageBox.Show(mensajeError, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormProductos frm = new FormProductos();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }
        
        // Limpiar los campos
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cbCategoria.SelectedIndex = 0;
            txtNombre.Focus();
        }

        // Sin funciones
        private void label7_Click(object sender, EventArgs e) { }
        private void gb_Agregar_Producto_Enter(object sender, EventArgs e){}
    }
}

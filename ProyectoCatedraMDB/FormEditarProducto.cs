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
    public partial class FormEditarProducto : Form
    {
        private int IdProductoEditando { get; set; }

        public FormEditarProducto(int idProducto)
        {
            InitializeComponent();
            this.IdProductoEditando = idProducto; 
        }
        private void FormEditarProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProveedores();
            CargarDatosProducto(this.IdProductoEditando);
        }
        // Cargar Datos
        private void CargarDatosProducto(int id)
        {
            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    var producto = db.Productos
                                     .FirstOrDefault(p => p.IdProducto == id);

                    var stock = db.Stock.FirstOrDefault(s => s.IdProducto == id);

                    if (producto != null)
                    {
                        lblIdProducto.Text = producto.IdProducto.ToString();
                        txtNombre.Text = producto.NombreProducto.Trim();
                        txtDescripcion.Text = producto.DescripcionProd.Trim();
                        txtPrecio.Text = producto.PrecioProd.ToString();

                        if (producto.IdCategoria.HasValue)
                        {
                            cbCategoria.SelectedValue = producto.IdCategoria.Value;
                        }

                        if (stock != null)
                        {
                            nudStock.Maximum = decimal.MaxValue;
                            nudStock.Value = stock.CantidadActual;
                        }
                        else
                        {
                            nudStock.Value = 0; 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: El producto a editar no fue encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                                       Nombre = p.NombreProveedor
                                   })
                                   .ToList();

                cbProveedor.DataSource = proveedores;
                cbProveedor.DisplayMember = "Nombre"; 
                cbProveedor.ValueMember = "ID";       
            }
        }

        // Botones
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
        !decimal.TryParse(txtPrecio.Text, out decimal precio) ||
        cbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Verifique que todos los campos sean válidos.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea guardar los cambios del producto?",
                "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int stockNuevo = (int)nudStock.Value;
            int? idProveedorSeleccionado = cbProveedor.SelectedValue != null ? (int?)cbProveedor.SelectedValue : null;

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
                    Productos productoExistente = db.Productos.FirstOrDefault(p => p.IdProducto == this.IdProductoEditando);
                    Stock stockExistente = db.Stock.FirstOrDefault(s => s.IdProducto == this.IdProductoEditando);

                    if (productoExistente != null && stockExistente != null)
                    {
                        int stockAnterior = stockExistente.CantidadActual;

                        productoExistente.NombreProducto = txtNombre.Text.Trim();
                        productoExistente.DescripcionProd = txtDescripcion.Text.Trim();
                        productoExistente.PrecioProd = precio;
                        productoExistente.IdCategoria = (int)cbCategoria.SelectedValue;

                        stockExistente.CantidadActual = stockNuevo;
                        stockExistente.FechaActualizacion = DateTime.Now;

                        if (stockNuevo > stockAnterior)
                        {
                            int cantidadAñadida = stockNuevo - stockAnterior;

                            if (idProveedorSeleccionado == null || idProveedorSeleccionado <= 0)
                            {
                                MessageBox.Show("Debe seleccionar un proveedor válido para registrar el aumento de stock.", "Error de Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            IngresoProductos nuevoIngreso = new IngresoProductos
                            {
                                IdProducto = this.IdProductoEditando,

                                IdProveedor = idProveedorSeleccionado.Value,

                                CantidadIngreso = cantidadAñadida,
                                IdUsuario = idUsuarioLogueado,
                                FechaIngreso = DateTime.Now
                            };
                            db.IngresoProductos.Add(nuevoIngreso);
                        }

                        else if (stockNuevo < stockAnterior)
                        {
                            MessageBox.Show($"ADVERTENCIA: El stock se redujo de {stockAnterior} a {stockNuevo} unidades. No se registró una salida en IngresoProductos.",
                                            "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        db.SaveChanges();

                        MessageBox.Show("Producto y stock actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RegresarAFormProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error: No se pudo encontrar el producto o el stock para la edición.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException != null ? ex.InnerException.InnerException.Message : ex.Message;
                MessageBox.Show("Error al actualizar el producto: " + mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RegresarAFormProductos()
        {
            btnRegresar_Click(null, null);
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
    }
}

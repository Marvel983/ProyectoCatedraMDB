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

                    if (producto != null)
                    {
                        lblIdProducto.Text = producto.IdProducto.ToString();
                        txtNombre.Text = producto.NombreProducto.Trim();
                        txtDescripcion.Text = producto.DescripciónProd.Trim();
                        txtPrecio.Text = producto.PrecioProd.ToString();

                        if (producto.IdCategoria.HasValue)
                        {
                            cbCategoria.SelectedValue = producto.IdCategoria.Value;
                        }

                        nudStock.Value = 0;
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
                                       Nombre = p.NombreProveedor
                                   })
                                   .ToList();

                cbProveedor.DataSource = proveedores;
                cbProveedor.DisplayMember = "Nombre";
                cbProveedor.ValueMember = "Nombre";
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

            int cantidadIngreso = (int)nudStock.Value;
            string nombreProveedor = cbProveedor.SelectedValue?.ToString(); 
            int idUsuarioLogueado = SesionUsuario.IdEmpleado;

            try
            {
                using (DBTiendaEntities db = new DBTiendaEntities())
                {
                    Productos productoExistente = db.Productos.FirstOrDefault(p => p.IdProducto == this.IdProductoEditando);

                    if (productoExistente != null)
                    {
                        productoExistente.NombreProducto = txtNombre.Text.Trim();
                        productoExistente.DescripciónProd = txtDescripcion.Text.Trim();
                        productoExistente.PrecioProd = precio;
                        productoExistente.IdCategoria = (int)cbCategoria.SelectedValue;

                        if (cantidadIngreso > 0)
                        {
                            if (string.IsNullOrEmpty(nombreProveedor))
                            {
                                MessageBox.Show("Debe seleccionar un proveedor para registrar el ingreso de stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; 
                            }

                            IngresoProductos nuevoIngreso = new IngresoProductos
                            {
                                IdProducto = this.IdProductoEditando,
                                NombreProveedor = nombreProveedor,
                                CantidadIngreso = cantidadIngreso,
                                IdUsuario = idUsuarioLogueado,
                                FechaIngreso = DateTime.Now
                            };

                            db.IngresoProductos.Add(nuevoIngreso);
                        }
                        db.SaveChanges();

                        MessageBox.Show("Producto y stock actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RegresarAFormProductos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

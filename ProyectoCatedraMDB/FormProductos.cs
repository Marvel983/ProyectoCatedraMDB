using ProyectoCatedraMDB.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCatedraMDB
{
    public partial class FormProductos : Form
    {
        public FormProductos()
        {
            InitializeComponent();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
            CargarProductosStock();
        }

        // Cargar contenido
        private void CargarProductos()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var lista = db.Productos
                      .Select(datos => new
                      {
                            ID = datos.IdProducto,
                            Nombre = datos.NombreProducto,
                            Precio = datos.PrecioProd,
                            IDCategoria = datos.IdCategoria
                      })
                      .ToList();
                dgvProductos.DataSource = lista;
            }
        }
        private void CargarProductosStock()
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var lista = db.Stock
                      .Select(datos => new
                      {
                          ID = datos.IdStock,
                          IdProductos = datos.IdProducto,
                          Cantidad = datos.CantidadActual
                      })
                      .ToList();
                dgvStock.DataSource = lista;
            }
        }

        // Botones 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();

            FormAgregarProducto frm = new FormAgregarProducto();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);

            frm.Show();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona el producto que deseas editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProductoAEditar = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["ID"].Value);

            pnlBody.Controls.Clear();

            FormEditarProducto frm = new FormEditarProducto(idProductoAEditar);

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnlBody.Controls.Add(frm);
            frm.Show();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["ID"].Value);

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", 
                    "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (DBTiendaEntities db = new DBTiendaEntities())
                        {
                            var productoAEliminar = db.Productos.FirstOrDefault(p => p.IdProducto == idProducto);

                            if (productoAEliminar != null)
                            {
                                db.Productos.Remove(productoAEliminar);
                                int rowsAffected = db.SaveChanges();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarProductos();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el producto o no se pudo eliminar.", "Advertencia",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
                    {
                        MessageBox.Show("Error de integridad de datos. El producto no puede ser eliminado porque tiene registros asociados (Stock o Ingresos).",
                                        "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Sin funciones
        private void pnlBody_Paint(object sender, PaintEventArgs e) { }
    }
}

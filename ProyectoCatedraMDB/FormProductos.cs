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
    public partial class FormProductos : Form
    {
        public FormProductos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

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
                            Stock = datos.Stock,
                            IDCategoria = datos.IdCategoria
                      })
                      .ToList();
                dgvProductos.DataSource = lista;
            }
        }

        private void pnlBody_Paint(object sender, PaintEventArgs e) { }
    }
}

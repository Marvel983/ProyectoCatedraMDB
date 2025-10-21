using ProyectoCatedraMDB.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCatedraMDB
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e){}
        
        // Botones
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del sistema?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
               Application.Exit();
            }
        }
        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string contrasenaIngresada = txtContra.Text.Trim();

            if (string.IsNullOrEmpty(usuarioIngresado))
            {
                MessageBox.Show("Por favor, ingrese el nombre de usuario.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else if (string.IsNullOrEmpty(contrasenaIngresada))
            {
                MessageBox.Show("Por favor, ingrese la contraseña.",
                               "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ValidarLogin(usuarioIngresado, contrasenaIngresada))
            {
            }
        }

        private bool ValidarLogin(string idUsuario, string contrasena)
        {
            using (DBTiendaEntities db = new DBTiendaEntities())
            {
                var usuario = db.Usuarios
                    .FirstOrDefault(user => user.NombreUsuario.Trim() == idUsuario
                                         && user.Contrasena.Trim() == contrasena
                                         && user.Activo == true); 

                if (usuario != null)
                {
                    SesionUsuario.IdEmpleado = usuario.IdUsuario;
                    SesionUsuario.Nombre = usuario.NombreUsuario.Trim();
                    SesionUsuario.Cargo = usuario.CargoUsuario.Trim();

                    string cargo = SesionUsuario.Cargo;

                    if (cargo == "Administrador" || cargo == "Empleado")
                    {
                        FormInicio acceso = new FormInicio();

                        acceso.MostrarOcultarBotonesPorCargo(cargo);
                        acceso.Show();
                        this.Hide();

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("El rol asignado a este usuario no tiene permisos de acceso.",
                                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("¡Usuario o contraseña incorrectos, o el usuario está inactivo!",
                                    "ERROR de Credenciales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContra.Clear();
                    txtUsuario.Focus(); 

                    return false;
                }
            }
        }
    }
}

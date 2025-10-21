using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedraMDB
{
    public static class SesionUsuario
    {
        public static int IdEmpleado { get; set; }
        public static string Nombre { get; set; }
        public static string Cargo { get; set; }

        public static void CerrarSesion()
        {
            IdEmpleado = 0;
            Nombre = null;            
            Cargo = null;
        }
    }
}

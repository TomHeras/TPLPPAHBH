using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario:IUsuario
    {
        private int idusuario;

        public int Idusuario
        {
            get { return idusuario; }
            set { idusuario = value; }
        }


        private string usuario;

        public string Usuarios
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private bool baja_logica;

        public bool Baja_logica
        {
            get { return baja_logica; }
            set { baja_logica = value; }
        }

        public Iidioma Idioma { get; set; }


        private int _UsuDVH;

        public int UsuDVH
        {
            get { return _UsuDVH; }
            set { _UsuDVH = value; }
        }

        int IUsuario.idusuario { get { return idusuario; } set { idusuario = value; } }
        string IUsuario.usuario { get { return usuario; } set { usuario = value; } }

        string IUsuario.password { get { return password; } set { password = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad.Singleton;
using Seguridad;
using Seguridad.Composite;
namespace BLL
{
    public class Usuario
    {
        BE.Usuario user = new BE.Usuario();
        DAL.Usuario Mapper = new DAL.Usuario();
        DAL.Usuario maperrepo;

        public Usuario()
        {
            maperrepo = new DAL.Usuario();
        }
        public string login(string usu, string pass)
        {
            if (SingletonSesion.Instancia.IsLogged())
                throw new Exception("ya hay una sesion iniciada");
            var a = Mapper.traerumbreusuario(usu);
            if (a == null)
                return ("no existe el usuario");
            if (!Encriptador.Hash(pass).Equals(a.Password))
                return ("contraseña incorrecta");
            else
            {
                SingletonSesion.Instancia.LogIn(a);
                return ("Inicio de Sesion Exitoso");
            }

        }

        public void Logout()
        {
            if (!SingletonSesion.Instancia.IsLogged())
                throw new Exception("No hay sesión iniciada"); //doble validación, anulo en boton en formulario y valido en la bll
            SingletonSesion.Instancia.Logout();
        }


        public List<BE.Usuario> Listar()
        {
            List<BE.Usuario> listausu = maperrepo.Listar();
            return maperrepo.Listar();
        }
        public void GuardarPermisos(Patente_Usuario u)
        {
            Mapper.GuardarPermisos(u);
        }
    }
}

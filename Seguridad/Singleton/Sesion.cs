using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Seguridad.Singleton
{
    public class Sesion
    {
        BE.IUsuario _user;
        //static IList<IdiomaObserver> _observers = new List<IdiomaObserver>();
        public IUsuario Usuario
        {
            get { return _user; }
        }

        ///metodos para cerrar sesion
        public void LogIn(IUsuario usuario)
        {
            _user = usuario;
        }
        public void Logout()
        {
            _user = null;
        }
        public bool IsLogged()
        {
            return _user != null;
        }
    }
}

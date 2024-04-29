using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Composite
{
    public class Patente_Usuario
    {
        public Patente_Usuario()
        {
            _patentes = new List<Componente>();
        }


        public int Idusuarios { get; set; }
        public string Nombre { get; set; }


        List<Componente> _patentes;



        public List<Componente> Permisos
        {
            get
            {
                return _patentes;
            }
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}

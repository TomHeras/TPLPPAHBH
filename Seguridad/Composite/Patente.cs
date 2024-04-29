using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Composite
{
   public class Patente:Componente
    {
        public override IList<Componente> Hijos
        {
            get
            {
                return new List<Componente>();
            }

        }
        public override void AgregarHijo(Componente c)
        {

        }

        public override void VaciarHijos()
        {

        }

       
    }
}

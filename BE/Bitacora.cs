using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Bitacora
    {

        public int id_bitacora { get; set; }
        public int id_usuario { get; set; }
        public string actividad { get; set; }
        public string criticidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
    }
}

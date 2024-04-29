using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public interface Iidioma
    {
        int Id { get; set; }
        string Nombre { get; set; }
        bool Default { get; set; }
    }
}

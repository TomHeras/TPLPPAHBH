using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public interface IUsuario
    {   
        int idusuario { get; set; }
        string usuario { get; set; }
        string password { get; set; }
    }
}

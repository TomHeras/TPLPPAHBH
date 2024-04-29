using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Composite
{
    public enum Tipopatente
    {
        //aca agregamos todos los permisos
        //admin 
        //permisosacceso
        
        //permisosgestion
        puedeconfigurarpatentesyfamilias,
        puedeaccederseg,
        puedeconfigurarseguridadusuarios,
        puedeabmusuarios,
        //webmaster
        //permisosacceso
        puedeaccederwm,
        //permisosgestion
        puedegestionarrestore,
        puedegestionarbackup,
        puedegestionarbitacora,
        //clientes        
        
        //permisosgestion
        puedeaccederacarrito,
        puedeaccederaproductos,
        puedecomprarproductos,
    }
}

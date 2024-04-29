using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class Bitacora
    {
        DAL.Bitacora mpbitacora = new DAL.Bitacora();

        public string InsertarEvento(BE.Bitacora bit)
        {
            return mpbitacora.InsertarEvento(bit);
        }

        public List<BE.Detalle_Bitacora> Listar_Bitacora()
        {
            List<BE.Detalle_Bitacora> listabit = mpbitacora.Listar_Bitacora();
            return listabit;
        }

        public List<BE.Detalle_Bitacora> GetBitacoraUser(string user)
        {
            return mpbitacora.GetBitacoraUser(user);
        }

        public List<BE.Detalle_Bitacora> GetBitacoraUserByDate(string f1, string f2)
        {
            return mpbitacora.GetBitacoraUserByDate(f1, f2);
        }

        public List<BE.Detalle_Bitacora> GetBitacoraUserByFilter(string u, string f1, string f2, string c)
        {
            return mpbitacora.GetBitacoraUserByFilter(u, f1, f2, c);
        }
        public DataTable FiltrarCriticidad(string c)
        {
            DataTable dt = mpbitacora.FiltrarCriticidad(c);
            return dt;
        }
        public List<BE.Detalle_Bitacora> Listar_BitacoraSiguiente(int startIndex, int endIndex)
        {
            List<BE.Detalle_Bitacora> listabit = mpbitacora.Listar_BitacoraSiguiente(startIndex, endIndex);
            return listabit;
        }
    }
}

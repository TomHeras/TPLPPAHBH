using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Seguridad
{
    class Bitacora
    {

       SqlConnection Conexion = new SqlConnection(@"Data Source=DESKTOP-QI5JC7C\TOOM;Initial Catalog=TPMODELOS;Integrated Security=True");
        

        public void GenerarBackUp()
        {
            string Direccion;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string año = DateTime.Now.Year.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minutos = DateTime.Now.Minute.ToString();
            string segundos = DateTime.Now.Second.ToString();
            string Nombre = "/Backup" + dia + "-" + mes + "-" + año + "-" + hora + "hs-" + minutos + "min-" + segundos + "seg.bak";
            Direccion= "C:/Users/TOOM/Desktop/backups";
            string Direccion1 = Direccion + Nombre;

            string cmd = " BACKUP DATABASE [TPMODELOS] TO  DISK = N'" + Direccion1 + "' WITH NOFORMAT, NOINIT,   NAME = N'BackupRestore-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            SqlCommand command = new SqlCommand(cmd, Conexion);
            Conexion.Open();
            command.ExecuteNonQuery();
            Conexion.Close();
        }

        public void GenerarRestore(string nombre)
        {
            string direccion = "C:\\Users\\TOOM\\Desktop\\backups\\";
            string direccion1 = direccion + nombre;
            string database = "TPMODELOS";
            

            Conexion.Open();
            string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            SqlCommand bu2 = new SqlCommand(sqlStmt2, Conexion);
            bu2.ExecuteNonQuery();

            string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + direccion1 + "'WITH REPLACE;";
            SqlCommand bu3 = new SqlCommand(sqlStmt3, Conexion);
            bu3.ExecuteNonQuery();

            string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
            SqlCommand bu4 = new SqlCommand(sqlStmt4, Conexion);
            bu4.ExecuteNonQuery();

            Conexion.Close();
        }
       
        
    }
}

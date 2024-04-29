using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Accesos
    {
        public SqlConnection conexion = new SqlConnection();

        public string crearconeion()
        {
            var cs = new SqlConnectionStringBuilder();
            /////
            ///
            //conxion para pc escritorio
            //cs.ConnectionString = @"Data Source=DESKTOP-QI5JC7C\TOOM;Initial Catalog=TPHBH;Integrated Security=True";//revisar  base de datos
            ////
            ///
            //Conexion para la notebook
            cs.ConnectionString = @"Data Source=DESKTOP-81ATIN0\SQLEXPRESS;Initial Catalog=TPHBH;Integrated Security=True";

            return cs.ConnectionString;
        }


        public DataTable Leer(string NombreProcedimiento, SqlParameter[] parametros)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = crearconeion(); // instancia de clase SQlConnection y le paso la cadena de conexion          
            sql.Open();//abre conexion
            DataTable tabla = new DataTable(); //crea nueva tabla
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = new SqlCommand();
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            adaptador.SelectCommand.CommandText = NombreProcedimiento;
            if (parametros != null)
            {
                adaptador.SelectCommand.Parameters.AddRange(parametros);
            }
            adaptador.SelectCommand.Connection = sql;
            adaptador.Fill(tabla);
            sql.Close();
            return tabla;
        }

        public string Escribir(string NombreProcedimiento, SqlParameter[] parametros)
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = crearconeion();
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = NombreProcedimiento;
            cmd.Connection = sql;
            cmd.Parameters.AddRange(parametros);
            try
            {

                cmd.ExecuteNonQuery();
                mensaje = "Operación Realizada con Exito";
            }

            catch (Exception Ex)
            {
                mensaje = Ex.Message;
            }
            sql.Close();
            return mensaje;
        }

        public void ejecutarconsulta(string consulta)
        {
            conexion.ConnectionString = @"Data Source=DESKTOP-QI5JC7C\TOOM;Initial Catalog=TPHBH;Integrated Security=True";
            conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = consulta;
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }
}

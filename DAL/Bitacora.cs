using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public class Bitacora
    {
        Accesos AR = new Accesos();
        public string InsertarEvento(BE.Bitacora bit)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@Id_Usuario", bit.id_usuario);
            parametros[1] = new SqlParameter("@Actividad", bit.actividad);
            parametros[2] = new SqlParameter("@Criticidad", bit.criticidad);
            parametros[3] = new SqlParameter("@Fecha", bit.fecha);
            parametros[4] = new SqlParameter("@Hora", bit.hora);
            mensaje = AR.Escribir("Grabar_Bitacora", parametros);
            return mensaje;
        }

        public List<BE.Detalle_Bitacora> Listar_Bitacora()
        {
            var cnn = new SqlConnection(AR.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"SELECT TOP 10 usu.Usunick, bita.Actividad, bita.Criticidad, bita.Fecha, bita.Hora 
FROM Bitacora bita 
INNER JOIN Usuarios usu ON bita.Id_Usuario = usu.Idusu;";
            cmd.CommandText = sql;
            var reader = cmd.ExecuteReader();
            var lista = new List<BE.Detalle_Bitacora>();
            while (reader.Read())
            {
                BE.Detalle_Bitacora db = new BE.Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("Usunick"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));

                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        public List<BE.Detalle_Bitacora> Listar_BitacoraSiguiente(int startIndex, int endIndex)
        {
            var cnn = new SqlConnection(AR.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            // Construir la consulta dinámica con los valores recibidos
            var sql = $@"SELECT usu.Usunick, bita.Actividad, bita.Criticidad, bita.Fecha, bita.Hora 
        FROM Bitacora bita 
        INNER JOIN Usuarios usu ON bita.Id_Usuario = usu.Idusu
        ORDER BY bita.Fecha, bita.Hora
        OFFSET {startIndex} ROWS
        FETCH NEXT {endIndex - startIndex + 1} ROWS ONLY;"; // Calcular el número de registros

            cmd.CommandText = sql;
            var reader = cmd.ExecuteReader();
            var lista = new List<BE.Detalle_Bitacora>();

            while (reader.Read())
            {
                BE.Detalle_Bitacora db = new BE.Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("Usunick"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));

                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        public List<BE.Detalle_Bitacora> GetBitacoraUser(string user)
        {
            var cnn = new SqlConnection(AR.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select Usunick,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuarios usu on bita.Id_Usuario = usu.Idusu where Usunick= @f";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@f", user);
            var reader = cmd.ExecuteReader();
            var lista = new List<BE.Detalle_Bitacora>();
            while (reader.Read())
            {
                BE.Detalle_Bitacora db = new BE.Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("Usunick"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));
                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        public List<BE.Detalle_Bitacora> GetBitacoraUserByDate(string f1, string f2)
        {
            var cnn = new SqlConnection(AR.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select Idusu,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuarios usu on bita.Id_Usuario = usu.Idusu where Fecha BETWEEN @f1 and @f2";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@f1", f1);
            cmd.Parameters.AddWithValue("@f2", f2);
            var reader = cmd.ExecuteReader();
            var lista = new List<BE.Detalle_Bitacora>();
            while (reader.Read())
            {
                BE.Detalle_Bitacora db = new BE.Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("Usunick"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));
                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }

        public List<BE.Detalle_Bitacora> GetBitacoraUserByFilter(string u, string f1, string f2, string c)
        {
            var cnn = new SqlConnection(AR.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"SELECT Idusu, Actividad, Criticidad, Fecha, Hora 
FROM Bitacora bita 
INNER JOIN Usuarios usu ON bita.Id_Usuario = usu.Idusu
where Idusu = @u and CONVERT(DATE, Fecha, 103) BETWEEN @f1 AND @f2 and Criticidad=@c;;
";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@f1", f1);
            cmd.Parameters.AddWithValue("@f2", f2);
            cmd.Parameters.AddWithValue("@c", c);
            var reader = cmd.ExecuteReader();
            var lista = new List<BE.Detalle_Bitacora>();
            while (reader.Read())
            {
                BE.Detalle_Bitacora db = new BE.Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("Usunick"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));

                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }

        public DataTable FiltrarCriticidad(string c)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.crearconeion();
            SqlCommand cmd = new SqlCommand("crit_bitacora", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@c", c);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }



    }
}

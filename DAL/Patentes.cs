using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad.Composite;
using Seguridad;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
    public class Patentes
    {
        Accesos acceso = new Accesos();


        public List<Patente> listar()
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            var sql = $@"select t1.Idpat, t1.Patnom, t2.id_patente_hijo from Patente t1 inner join Patente_patente t2 on t1.IdPat=t2.id_patente_padre;";

            cmd.CommandText = sql;
            var lista = new List<Patente>();

            return lista;
        }
        public List<BE.Usuario_patente_auxiliar> listarUSUPAT()
        {
            List<BE.Usuario_patente_auxiliar> listadatos = new List<BE.Usuario_patente_auxiliar>();
            DataTable tabla = acceso.Leer("Trarrelacion", null);

            foreach (DataRow item in tabla.Rows)
            {
                BE.Usuario_patente_auxiliar usudatos = new BE.Usuario_patente_auxiliar();
                usudatos.Idusu = int.Parse(item["IdUsu"].ToString());
                usudatos.Idpat = int.Parse(item["IdPat"].ToString());
                listadatos.Add(usudatos);

            }

            return listadatos;
        }
        public IList<Patente> traerpatentes()
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from Patente p where p.PatDesc is not null;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Patente>();


            while (reader.Read())
            {

                var id = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var nombre = reader.GetString(reader.GetOrdinal("PatNom"));
                var patente = reader.GetString(reader.GetOrdinal("PatDesc"));



                Patente c = new Patente();
                c.Id = id;
                c.Nombre = nombre;
                c.Permiso = (Tipopatente)Enum.Parse(typeof(Tipopatente), patente);
                lista.Add(c);

            }
            reader.Close();
            cnn.Close();


            return lista;

        }


        public IList<Familia> traerfamilias()
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from Patente p where p.PatDesc is  null;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Familia>();

            while (reader.Read())
            {

                var id = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var nombre = reader.GetString(reader.GetOrdinal("PatNom"));

                Familia c = new Familia();
                c.Id = id;
                c.Nombre = nombre;
                lista.Add(c);
            }
            reader.Close();
            cnn.Close();
            return lista;
        }

        public Componente GuardarComponente(Componente p, bool esfamilia)
        {
            try
            {
                var cnn = new SqlConnection(acceso.crearconeion());
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;
                var sql = $@"insert into Patente (PatNom,PatDesc) values (@nombre,@descripcion);  SELECT IdPat AS LastId_patente FROM Patente WHERE IdPat = @@Identity;       ";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));


                if (esfamilia)
                    cmd.Parameters.Add(new SqlParameter("descripcion", DBNull.Value));

                else
                    cmd.Parameters.Add(new SqlParameter("descripcion", p.Permiso.ToString()));

                var id = cmd.ExecuteScalar();
                p.Id = (int)id;
                return p;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Array obtenerpermisos()
        {
            return Enum.GetValues(typeof(Tipopatente));
        }


        public IList<Componente> GetAll(string familia)
        {
            var where = "is NULL";

            if (!String.IsNullOrEmpty(familia))
            {
                where = familia;
            }


            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"with recursivo as (
                        select sp2.id_patente_padre, sp2.id_patente_hijo  from Patente_Patente SP2
                        where sp2.id_patente_padre {where} --acá se va variando la familia que busco
                        UNION ALL 
                        select sp.id_patente_padre, sp.id_patente_hijo from Patente_Patente sp 
                        inner join recursivo r on r.id_patente_hijo= sp.id_patente_padre
                        )
                        select r.id_patente_padre,r.id_patente_hijo,p.IdPat,p.PatNom, p.PatDesc
                        from recursivo r 
                        inner join Patente p on r.id_patente_hijo = p.IdPat
						
                        ";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Componente>();

            while (reader.Read())
            {
                int id_padre = 0;
                if (reader["id_patente_padre"] != DBNull.Value)
                {
                    id_padre = reader.GetInt32(reader.GetOrdinal("id_patente_padre"));
                }

                var id = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var nombre = reader.GetString(reader.GetOrdinal("PatNom"));

                var patente = string.Empty;
                if (reader["PatDesc"] != DBNull.Value)
                    patente = reader.GetString(reader.GetOrdinal("PatDesc"));


                Componente c;

                if (string.IsNullOrEmpty(patente))//usamos este campo para identificar. Solo las patentes van a tener un permiso del sistema relacionado
                    c = new Familia();

                else
                    c = new Patente();

                c.Id = id;
                c.Nombre = nombre;
                if (!string.IsNullOrEmpty(patente))
                    c.Permiso = (Tipopatente)Enum.Parse(typeof(Tipopatente), patente);

                var padre = traercomponente(id_padre, lista);

                if (padre == null)
                {
                    lista.Add(c);
                }
                else
                {
                    padre.AgregarHijo(c);
                }



            }
            reader.Close();
            cnn.Close();


            return lista;
        }


        private Componente traercomponente(int id, IList<Componente> lista)
        {

            Componente component = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (component == null && lista != null)
            {
                foreach (var c in lista)
                {
                    var l = traercomponente(id, c.Hijos);
                    if (l != null && l.Id == id) return l;
                    else
                    if (l != null)
                        return traercomponente(id, l.Hijos);
                }
            }
            return component;
        }



        public void GuardarFamilia(Familia c)
        {

            try
            {

                var cnn = new SqlConnection(acceso.crearconeion());
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;


                var sql = $@"delete from Patente_Patente where id_patente_padre=@id;       ";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("id", c.Id));
                cmd.ExecuteNonQuery();

                foreach (var item in c.Hijos)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;


                    sql = $@"insert into Patente_Patente (id_patente_padre,id_patente_hijo) values (@id_permiso_padre,@id_permiso_hijo) ";

                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("id_permiso_padre", c.Id));
                    cmd.Parameters.Add(new SqlParameter("id_permiso_hijo", item.Id));

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void FillUserComponents(Patente_Usuario u)
        {

            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from Usuario_Patente up inner join Patente p on up.IdPat=p.IdPat where IdUsu=@id";
            cmd2.Parameters.AddWithValue("@id", u.Idusuarios);
            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())
            {

                var idp = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var nombrep = reader.GetString(reader.GetOrdinal("PatNom"));
                var permisop = String.Empty;
                if (reader["PatDesc"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("PatDesc"));

                Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (Tipopatente)Enum.Parse(typeof(Tipopatente), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            reader.Close();
        }

        public bool BuscarPermisos(Tipopatente patente, Patente_Usuario u)
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from Usuario_Patente up inner join Patente p on up.IdPat=p.IdPat where IdUsu=@id";
            cmd2.Parameters.AddWithValue("@id", u.Idusuarios);
            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())
            {

                var idp = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var nombrep = reader.GetString(reader.GetOrdinal("PatNom"));
                var permisop = String.Empty;
                if (reader["PatDesc"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("PatDesc"));

                Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (Tipopatente)Enum.Parse(typeof(Tipopatente), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            bool existe = false;
            foreach (var item in u.Permisos)
            {
                if (item.Permiso.Equals(patente))
                    return true;
                else
                {
                    existe = isInRole(item, patente, existe);
                    if (existe) return true;
                }

            }
            reader.Close();
            return existe;
        }

        bool isInRole(Componente c, Tipopatente patente, bool existe)
        {

            if (c.Permiso.Equals(patente))
                existe = true;
            else
            {
                foreach (var item in c.Hijos)
                {
                    existe = isInRole(item, patente, existe);
                    if (existe) return true;
                }

            }

            return existe;
        }


        public void ValidarPermisos(Patente_Usuario u)
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from Usuario_Patente up inner join Patente p on up.IdPat=p.IdPat where IdUsu=@id";
            cmd2.Parameters.AddWithValue("@id", u.Idusuarios);
            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())
            {
                var nombrep = reader.GetString(reader.GetOrdinal("PatNom"));
                var idp = reader.GetInt32(reader.GetOrdinal("IdPat"));
                var permisop = String.Empty;
                if (reader["PatDesc"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("PatDesc"));

                Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (Tipopatente)Enum.Parse(typeof(Tipopatente), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            reader.Close();
        }

        public void FillFamilyComponents(Familia familia)
        {
            familia.VaciarHijos();
            foreach (var item in GetAll("=" + familia.Id))
            {
                familia.AgregarHijo(item);
            }
        }
        public void Consultar(string query)
        {
            acceso.ejecutarconsulta(query);
        }
    }
}

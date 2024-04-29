using Seguridad.Composite;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;


namespace WebApplication10
{
    public partial class Permisos_usuarios : System.Web.UI.Page
    {

        BLL.Usuario repo;
        BLL.Patentes patentebll;
        Familia seleccion;
        Patente_Usuario tmp;
        BE.Bitacora bit;
        BLL.Bitacora bitbll = new BLL.Bitacora();
        string usser;
        protected void Page_Load(object sender, EventArgs e)
        {
            repo = new BLL.Usuario();
            patentebll = new BLL.Patentes();

            if (!IsPostBack)
            {
                llenarlistas();
            }

            inicializarpermisos();
            validarpermisos();
        }

        protected void btnconfigusuarios_Click(object sender, EventArgs e)
        {
            string valorseleccionado = ddlusuarios.SelectedValue;

            var usuarioselec = repo.Listar().FirstOrDefault(item => valorseleccionado == item.Usuarios);
            if (usuarioselec!=null)
            {
                tmp = new Patente_Usuario();
                tmp.Idusuarios = usuarioselec.Idusuario;
                tmp.Nombre = valorseleccionado;

                patentebll.FillUserComponents(tmp);

                mostrarpermisos(tmp);
            }
        }
        protected void btnconfigflias_Click(object sender, EventArgs e)
        {
            //TreeView1.Nodes.Clear();
            if (tmp!=null)
            {
                Familia flia=patentebll.GetAllFamilias().FirstOrDefault(f => f.Nombre == ddlfamilias.SelectedItem.ToString());
                if (flia != null)
                {
                    var esta = false;
                    //verifico que ya no tenga el permiso. TODO: Esto debe ser parte de otra capa.
                    foreach (var item in tmp.Permisos)
                    {
                        if (patentebll.Existe(item, flia.Id))
                        {
                            esta = true;
                        }
                    }

                    if (esta)
                        Response.Write("El usuario ya tiene la familia indicada");
                    else
                    {
                        {
                            patentebll.FillFamilyComponents(flia);

                            tmp.Permisos.Add(flia);
                            mostrarpermisos(tmp);
                        }
                    }
                }
            }
        }

        protected void btnconfigpatentes_Click(object sender, EventArgs e)
        {
            //TreeView1.Nodes.Clear();
            if (tmp!=null)
            {
                Patente patent=patentebll.GetAllPatentes().FirstOrDefault(p => p.Nombre == ddlpatentes.SelectedItem.ToString());
                if (patent!=null)
                {
                    var esta = false;

                    foreach (var item in tmp.Permisos)
                    {
                        if (patentebll.Existe(item, patent.Id))
                        {
                            esta = true;
                            break;
                        }
                    }
                    if (esta)
                        Response.Write("El usuario ya tiene la patente indicada");
                    else
                    {
                        tmp.Permisos.Add(patent);
                        mostrarpermisos(tmp);
                    }
                }
            }
        }
        
        protected void btnguardarcambios_Click(object sender, EventArgs e)
        {
            
            rellenarpermisos();
            repo.GuardarPermisos(tmp);

            Response.Write("<script>alert('La configuracion fue guardada correctamente');</script>");

        }

        /////Procedimientos y funciones
        //////
        /////
        ///

        public void validarpermisos()
        {
            if (SingletonSesion.Instancia.IsLogged())
            {
                usser = Session["Nick"].ToString();
                var lista = repo.Listar();
                try
                {
                    foreach (var item in lista)
                    {
                        tmp.Idusuarios = item.Idusuario;
                        tmp.Nombre = item.Nombre;
                        if (usser == item.Usuarios)
                        {
                            if (patentebll.BuscarPermisos(Tipopatente.puedeconfigurarseguridadusuarios, tmp) == true)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Bienvenido usuario:';+permisos.Nombre = item.Nombre);</script>");
                                break;
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No se puede acceder a la pagina deseada');</script>");
                                SingletonSesion.Instancia.Logout();
                                Response.Redirect("Inicio.aspx");
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No hay una sesion logueada');</script>");
                Response.Redirect("Inicio.aspx");
            }
        }
    
        public void GrabarBitacora(string detallebit)
        {
            bit = new BE.Bitacora();
            bit.id_usuario = SingletonSesion.Instancia.Usuario.idusuario;
            bit.actividad = detallebit;
            bit.criticidad = "Media";
            bit.fecha = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            bit.hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            bitbll.InsertarEvento(bit);
            bit = null;
        }
        public void llenarlistas()
        {
            List<string> usuarios = new List<string>();
            foreach (BE.Usuario item in repo.Listar())
            {
                usuarios.Add(item.Usuarios);
            }

            ddlusuarios.DataSource = usuarios;
            ddlusuarios.DataBind();
            this.ddlfamilias.DataSource = patentebll.GetAllFamilias();
            ddlfamilias.DataBind();
            this.ddlpatentes.DataSource = patentebll.GetAllPatentes();
            ddlpatentes.DataBind();
        }

        private void inicializarpermisos()
        {
            string valorseleccionado = ddlusuarios.SelectedValue;
            var usuarioseleccionado = repo.Listar().FirstOrDefault(item => valorseleccionado == item.Usuarios);

            if (usuarioseleccionado!=null)
            {
                tmp = new Patente_Usuario();
                tmp.Idusuarios = usuarioseleccionado.Idusuario;
                tmp.Nombre = valorseleccionado;
                patentebll.FillUserComponents(tmp);
                mostrarpermisos(tmp);
            }
        }

        public void mostrarpermisos(Patente_Usuario u)
        {
            this.TreeView1.Nodes.Clear();
            TreeNode root = new TreeNode(u.Nombre);

            foreach (var item in u.Permisos)
            {
                llenartree(root,item);
            }

            this.TreeView1.Nodes.Add(root);
            this.TreeView1.ExpandAll();
        }

        public void llenartree(TreeNode padre, Componente c)
        {
            TreeNode hijo = new TreeNode(c.Nombre);
            hijo.Value = c.ToString();
            padre.ChildNodes.Add(hijo);

            foreach (var item in c.Hijos)
            {
                llenartree(hijo, item);
            }
        }

        

        
       

        public void rellenarpermisos()
        {
            Familia flia = patentebll.GetAllFamilias().FirstOrDefault(f => f.Nombre == ddlfamilias.SelectedItem.ToString());
            if (flia != null)
            {
                var esta = false;
                
                foreach (var item in tmp.Permisos)
                {
                    if (patentebll.Existe(item, flia.Id))
                    {
                        esta = true;
                    }
                }

                if (esta)
                    Response.Write("El usuario ya tiene la familia indicada");
                else
                {
                    {
                        patentebll.FillFamilyComponents(flia);

                        tmp.Permisos.Add(flia);
                        
                    }
                }
            }
            Patente patent = patentebll.GetAllPatentes().FirstOrDefault(p => p.Nombre == ddlpatentes.SelectedItem.ToString());
            if (patent != null)
            {
                var esta = false;

                foreach (var item in tmp.Permisos)
                {
                    if (patentebll.Existe(item, patent.Id))
                    {
                        esta = true;
                        break;
                    }
                }
                if (esta)
                    Response.Write("El usuario ya tiene la patente indicada");
                else
                {
                    tmp.Permisos.Add(patent);
                    
                }
            }
        }
    }
    
}

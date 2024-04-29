using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad.Composite;
using Seguridad.Singleton;

namespace WebApplication10
{
    public partial class Seguridad : System.Web.UI.Page
    {
        BLL.Patentes patentebll;
        string usser;
        BE.Usuario user = new BE.Usuario();
        BLL.Usuario gestoruser = new BLL.Usuario();
        Patente_Usuario permisos = new Patente_Usuario();
        
        /////EVENTOS
        ////
        /// 
        // 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //validarpermisos();

            string ingreo = Session["Permisos"].ToString();
            string usuario = Session["Nick"].ToString();
            if (ingreo=="puedeaccederseg")
            {
                Response.Write("<script>alert('Bienvenido usuario: " + usuario + " ');</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Bienvenido usuario: "+usuario+" ');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No se puede acceder a la pagina deseada');</script>");
            }
        }

        ////// FUNCIONES Y PROCEDIEMIENTOS
        ////
        ///
        //
        
        public void validarpermisos()
        {
            if (SingletonSesion.Instancia.IsLogged())
            {
                usser = Session["Nick"].ToString();
                var lista = gestoruser.Listar();
                try
                {
                    foreach (var item in lista)
                    {
                        permisos.Idusuarios = item.Idusuario;
                        permisos.Nombre = item.Nombre;
                        if (usser == item.Usuarios)
                        {
                            if (patentebll.BuscarPermisos(Tipopatente.puedeaccederseg, permisos) == true)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Bienvenido usuario:';+permisos.Nombre = item.Nombre);</script>");
                                break;
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No se puede acceder a la pagina deseada');</script>");
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
        public void validaropciones()
        {
            usser = Session["Nick"].ToString();
            var lista = gestoruser.Listar();
            try
            {
                foreach (var item in lista)
                {
                    permisos.Idusuarios = item.Idusuario;
                    permisos.Nombre = item.Nombre;
                    if (usser == item.Usuarios)
                    {
                        if (patentebll.BuscarPermisos(Tipopatente.puedeconfigurarpatentesyfamilias, permisos) == true)
                        {
                           
                            
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No se puede acceder a la pagina deseada');</script>");
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
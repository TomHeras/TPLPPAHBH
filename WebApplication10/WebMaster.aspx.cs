using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad.Singleton;
using Seguridad.Composite;
using BE;
namespace WebApplication10
{
    public partial class WebMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            
            //validarpermisos();
        }
        string usser;
        BE.Usuario user = new BE.Usuario();
        BLL.Usuario gestoruser = new BLL.Usuario();
        BLL.Patentes gestorpatentes = new BLL.Patentes();
        Patente_Usuario permisos = new Patente_Usuario();
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

                       if (item.Usuarios==usser)
                       {
                           if (gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederwm, permisos) == true)
                           {
                               Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Bienvenido usuario:';+permisos.Nombre = item.Nombre);</script>");
                               break;
                           }
                           else
                           {
                               Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No se puede acceder a la pagina deseada');</script>");
                               
                               gestoruser.Logout();
                               //break;
                               //Response.Redirect("Inicio.aspx");
                           }
                       }
                        
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No hay una sesion logueada');</script>");
                Response.Redirect("Inicio.aspx");
            }
            
        }
    }
}
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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Perfil"] = 0;
            Session["Nick"] = null;

        }
        BE.Usuario user = new BE.Usuario();
        BLL.Usuario gestoruser = new BLL.Usuario();
        BLL.Patentes gestorpatentes = new BLL.Patentes();
        Patente_Usuario permisos = new Patente_Usuario();
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        protected void LinkButtonRecuperar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }


        public void Ingresar()
        {

            BE.Usuario temp = new BE.Usuario();
            temp.Usuarios = TxtNick.Text;
            Session["Nick"] = temp.Usuarios;

            gestoruser.login(TxtNick.Text, TxtContraseña.Text);

            if (SingletonSesion.Instancia.IsLogged())
            {
                foreach (BE.Usuario user in gestoruser.Listar())
                {

                    if (user.Usuarios == TxtNick.Text)
                    {

                        if (user.Estado == true)
                        {
                            validarpermisos();

                        }
                        else
                        {
                            gestoruser.Logout();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('usuario bloqueado');</script>");
                        }
                    }

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Datos mal cargados');</script>");
            }
        }

        public void validarpermisos()
        {
            var lista = gestoruser.Listar();
            foreach (var item in lista)
            {
                if (SingletonSesion.Instancia.Usuario.usuario==item.Usuarios)
                {
                    permisos.Idusuarios = item.Idusuario;
                    permisos.Nombre = item.Nombre;

                    if (gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederseg,permisos)==true)
                    {
                        Session["Permisos"] = "puedeaccederseg";
                            Response.Redirect("Seguridad.aspx");
                        

                    }
                    else if (gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederwm, permisos) == true)
                    {
                        
                            Response.Redirect("WebMaster.aspx");///solo webmaster
                        

                    }
                    else if (gestorpatentes.BuscarPermisos(Tipopatente.puedecomprarproductos, permisos) == true)
                    {
                        Response.Redirect("ClientesWeb");
                    }
                    else
                    {
                        Response.Redirect("Informacion.aspx");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('No posee los permisos');</script>");
                        //gestoruser.Logout();
                        //break;
                    }
                    



                }
            }
        }
    }
}
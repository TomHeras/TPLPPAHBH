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
    public partial class Bitacora : System.Web.UI.Page
    {
        ///// declaraciones
        ///
        //
        private const int registrosporpagina = 10;
        private readonly BLL.Bitacora mapperbitacora = new BLL.Bitacora();
        private BLL.Usuario usuariobll;
        BLL.Patentes permisosbll;
        Patente_Usuario usupat;
        /// Eventos
        /// 
        //

        protected void Page_Load(object sender, EventArgs e)
        {
            usuariobll = new BLL.Usuario();
            permisosbll = new BLL.Patentes();
            ///agragar validacion permisos
            ///
            if (!IsPostBack)
            {
                usuariobll = new BLL.Usuario();
                bindusuarios();
                listarbitacora();
            }
        }

        protected void Btnfiltrar_Click(object sender, EventArgs e)
        {
            try
            {

                
                string fechai = txtFechaInicio.Text;

                string ff = txtFechaFin.Text;

                var bit = mapperbitacora.GetBitacoraUserByFilter(ddlusuarios.Text, fechai, ff, ddlcriticidad.Text);

                rptBitacora.DataSource = bit;
                rptBitacora.DataBind();
                lblpagina.Text = "1";
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnanterior_Click(object sender, EventArgs e)
        {
            try
            {
                int paginaActual = int.Parse(lblpagina.Text);
                int numInicio = (paginaActual - 1) * registrosporpagina;
                int numFin = numInicio + registrosporpagina;

                CargarBitacora(numInicio, numFin);

                lblpagina.Text = (paginaActual - 1).ToString();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Puedes mostrar un mensaje más amigable al usuario o registrar el error
                Response.Write(ex.Message);
            }
        }

        protected void btnsiguiente_Click(object sender, EventArgs e)
        {
            int paginaActual = int.Parse(lblpagina.Text);
            int numInicio = paginaActual * registrosporpagina;
            int numFin = numInicio + registrosporpagina;

            CargarBitacora(numInicio, numFin);

            lblpagina.Text = (paginaActual + 1).ToString();
        }
        protected void ddlusuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ddlusuarios.Text))
            {
                var bit = mapperbitacora.GetBitacoraUser(ddlusuarios.Text);
                rptBitacora.DataSource = bit;
                rptBitacora.DataBind();
                lblpagina.Text = "1";
            }
        }
        protected void ddlcriticidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlcriticidad.Text))
            {
                var bit = mapperbitacora.FiltrarCriticidad(ddlcriticidad.Text);
                rptBitacora.DataSource = bit;
                rptBitacora.DataBind();
                lblpagina.Text = "1";
            }
        }
        /////////Funciones y procedimientos
        //////
        ////
        ///
        //

        private void bindusuarios()
        {
            List<string> usuarios = new List<string>();
            foreach (BE.Usuario item in usuariobll.Listar())
            {
                usuarios.Add(item.Usuarios);
            }
            ddlusuarios.DataSource = usuarios;
            ddlusuarios.DataBind();
        }
        private void listarbitacora()
        {
            rptBitacora.DataSource = mapperbitacora.Listar_Bitacora();
            rptBitacora.DataBind();
            lblpagina.Text = "1";
        }
        public List<BE.Detalle_Bitacora> ObtenerRegistrosPaginados(int pagina, int registrosPorPagina)
        {
            int skip = (pagina - 1) * registrosPorPagina;

            var registros = mapperbitacora.Listar_Bitacora()
                .OrderBy(t => t.Hora) // Cambia esto a la columna apropiada para ordenar
                .Skip(skip)
                .Take(registrosPorPagina)
                .ToList();
            return registros;
        }
        private void CargarPagina(int pagina)
        {
            int registrosPorPagina = 10; // Cambia esto según tu necesidad

            var registros = ObtenerRegistrosPaginados(pagina, registrosPorPagina).ToList();

            rptBitacora.DataSource = registros;
            rptBitacora.DataBind();

            btnanterior.Enabled = (pagina > 1);
            btnsiguiente.Enabled = (registros.Count == registrosPorPagina);
        }
        protected void CambiarPagina(object sender, CommandEventArgs e)
        {
            int pagina = int.Parse(e.CommandArgument.ToString());
            CargarPagina(pagina);
        }
        private void CargarBitacora(int numInicio, int numFin)
        {
            rptBitacora.DataSource = mapperbitacora.Listar_BitacoraSiguiente(numInicio, numFin);
            rptBitacora.DataBind();
        }
        private int ObtenerPaginaActual()
        {
            if (ViewState["Bitacora.aspx"] != null)
            {
                return (int)ViewState["Bitacora.aspx"];
            }
            return 1;
        }
    }
}
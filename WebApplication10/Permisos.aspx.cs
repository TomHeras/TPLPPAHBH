using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad.Composite;
using Seguridad.Singleton;
using BE;


namespace WebApplication10
{
    public partial class Permisos : System.Web.UI.Page
    {


        Familia seleccion;
        BLL.Patentes patentebll;
        string usser;
        BE.Usuario user = new BE.Usuario();
        BLL.Usuario gestoruser = new BLL.Usuario();
        Patente_Usuario permisos = new Patente_Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            patentebll = new BLL.Patentes();
            
            if (!IsPostBack)
            {
                this.cmbpatenes.DataSource = patentebll.GetAllPermission();
                llenarpatentesyflias();
            }
            validarpermisos();
        }

        protected void Btnguardarpermiso_Click(object sender, EventArgs e)//para nuevo permiso
        {


            Patente p = new Patente()
            {
                Nombre = this.Txtpatentes.Text,

            };

            patentebll.guardarcomponente(p, false);

            Txtpatentes.Text = "";


            Response.Write("<script>alert('Se guardo correctamente la nueva patente');</script>");
            llenarpatentesyflias();
        }

        protected void btnguardarflia_Click(object sender, EventArgs e)// para nueva flia
        {
            Familia p = new Familia()
            {
                Nombre = this.TextBox1.Text,
                
            };
            patentebll.guardarcomponente(p, true);

            TextBox1.Text = "";
            Response.Write("<script>alert('Se guardo correctamente la nueva familia');</script>");
            llenarpatentesyflias();
        }
       
       

        
        protected void btnconfigflias_Click(object sender, EventArgs e)//para poder configurar la familia
        {
            string seleccionado = cmbflias.SelectedValue;

            var fs= patentebll.GetAllFamilias().FirstOrDefault(item=> seleccionado ==item.Nombre);
            seleccion = new Familia();
            seleccion.Id = fs.Id;
            seleccion.Nombre = fs.Nombre;
            Mostrar_familias(true);
        }

        
        protected void bntconfigpatenes_Click(object sender, EventArgs e)//Para agregar patentes a las flias
        {
            try
            {
                validarflias();
                
                if (seleccion!=null)
                {
                    Patente patente = patentebll.GetAllPatentes().FirstOrDefault(p => p.Nombre == cmbpatenes.SelectedItem.ToString());

                    if (patente!=null)
                    {
                        var esta = patentebll.Existe(seleccion, patente.Id);
                        if (esta)
                            Response.Redirect("Ya existe la patente seleccionada");
                        else
                        {
                            seleccion.AgregarHijo(patente);
                            Mostrar_familias(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                rellenarseleccion(TreeView1.Nodes);
                reemplazar();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Configuracion guardada correctamente');</script>");
                Response.Write("<script>alert('La configuracion fue guardada correctamente');</script>");

            }
            catch (Exception)
            {

                throw;
            }
        }

        ////FUNCIONES Y PROCEDIMIENTOS
        ////
        ////
        /////
        public void validarflias()
        {
            string seleccionado = cmbflias.SelectedValue;

            var fs = patentebll.GetAllFamilias().FirstOrDefault(item => seleccionado == item.Nombre);
            seleccion = new Familia();
            seleccion.Id = fs.Id;
            seleccion.Nombre = fs.Nombre;
        }
        public void llenarpatentesyflias()
        {
            this.cmbpatenes.DataSource = patentebll.GetAllPatentes();
            cmbpatenes.DataBind();
            this.cmbflias.DataSource = patentebll.GetAllFamilias();
            cmbflias.DataBind();
        }

        public void Mostrar_familias( bool init)
        {
            //validarflias(); 

            if (seleccion == null) return;

            IList<Componente> family = null;
            if (init)
            {
                family = patentebll.GetAll("=" + seleccion.Id);


                foreach (var i in family)
                {
                    seleccion.AgregarHijo(i);
                }
            }
            else
            {
                family = seleccion.Hijos;
            }

            this.TreeView1.Nodes.Clear();

            TreeNode root = new TreeNode(seleccion.Nombre);
            root.Target = seleccion.ToString();
            this.TreeView1.Nodes.Add(root);


            foreach (var item in family)
            {
                MostrarTreeView(root, item);
            }

            TreeView1.ExpandAll();
        }

        public void MostrarTreeView(TreeNode tn, Componente c)
        {
            TreeNode n = new TreeNode(c.Nombre);
            tn.Value = c.ToString();
            tn.ChildNodes.Add(n);

            if (c.Hijos!=null)
                foreach (var item in c.Hijos)
                {
                    MostrarTreeView(n, item);
                }
        }
       
        public void rellenarseleccion(TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
             
              Permisostree.Add(item);

                if (item.ChildNodes.Count>0)
                {
                    rellenarseleccion(item.ChildNodes);
                }
            }
        }

        public void reemplazar()
        {
            try
            {
                foreach (TreeNode itemtree in Permisostree)
                {
                    if (itemtree.Depth == 0)
                    {
                        foreach (Componente item in patentebll.GetAllFamilias())
                        {
                            if (item.Nombre == itemtree.Text)
                            {
                                familia = item.Id;
                            }
                        }
                    }
                    else
                    {
                        foreach (Componente itempate in patentebll.GetAllPatentes())
                        {
                            if (itempate.Nombre == itemtree.Text)
                            {
                                patente = itempate.Id;
                                string consulta = "insert into Patente_Patente (id_patente_padre, id_patente_hijo) values(" + familia+  "," + patente + ")";
                                patentebll.Consulta(consulta);
                                

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        int familia = 0,patente=0;
        List<TreeNode> Permisostree = new List<TreeNode>();
        List<string> lista = new List<string>();
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
                        if (usser==item.Usuarios)
                        {
                            if (patentebll.BuscarPermisos(Tipopatente.puedeconfigurarpatentesyfamilias, permisos)==true)
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
    }
}
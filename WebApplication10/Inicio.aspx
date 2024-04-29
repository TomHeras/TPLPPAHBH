<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApplication10.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio</title>
    <link href="HBH_diseño mas grandde2.png" rel="icon" type="image/gif"/>
    
       <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />   <%-- esto trae las hojas de estilo --%>
    <link href="Estilos.css" rel="stylesheet" />
    <style>
.body_body{
    overflow-x: hidden;
    font-family: sans-serif;
    color: #505962;
}

/*---Navbar --*/
.navbar{
    text-transform: uppercase;
    font-weight: 500;
    font-size: 1rem;
    letter-spacing: .1rem;
    background: rgba(0,0,0,0.6)!important;
}

.navbar-brand img{
    height: 386px;
            width: 404px;
        }

.navbar-nav li{
    padding-right: .7rem;
}

.navbar-dark .navbar-nav .nav-link {
    color: white;
    padding-top: .8rem;
}

.navbar-dark .navbar-nav .nav-link.active,
.navbar-dark .navbar-nav .nav-link:hover{
    color: #87CEFA;
}

/*--- Background: ---*/
.home-inner{
    background-image: url('fondopantalla.jpg');
}

.caption{
    width: 100%;
    max-width: 100%;
    position: absolute;
    top: 45%;
    z-index: 1;
    /*text-transform: uppercase;*/
}

.caption h1{
    font-size: 6.5em;
    font-weight: 600;
    letter-spacing: .3rem;
    padding-bottom: 1rem;
    font-family: sans-serif;
}

p{
    font-size:2em;
}
</style>
</head>
<body class="body_body" data-spy="scroll" data-target="#navbarNav"> 
    <div id="Home">
        <form id="form1" runat="server">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
            <a class="navbar-brand" href="#"><img src="HBH_diseño%20mas%20grandde2.png"/></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" 
            data-target="#navbarResponsive">
                <span class="navbar-toggler-icon"></span>
            </button> 

            <div class="collapse navbar-collapse" id="navbarResponsive">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="#Home">Inicio</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#Quienes">Quienes Somos</a>
                    </li>
                    <li class="nav-item" style="float:right">
                        <a class="nav-link" href="Login.aspx">Iniciar Sesion</a>
                    </li>
                </ul>
            </div>
        </nav>
            <div class="landing">
               <div class="home-wrap">
                   <div class="home-inner">
                   </div>
               </div>
            </div>   

            <div class="caption text-center">
               <h1>BIENVENIDO</h1>
            </div>
             </form>
        </div>   
    
        <div id="Quienes">
            
        </div>       
</body>
</html>

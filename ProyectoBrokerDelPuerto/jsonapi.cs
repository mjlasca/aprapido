using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{

    /*
    Modificaciones a la base de datos en la nube:

    propuestas:
        agregar columna codempresa *
        agregar columna idpropuesta *


    lineas_propuestas : 
        agregar columna codempresa * 

    clientes : 
        agregar columna codempresa *

    barrios_propuestas : 
        agregar columna codempresa *

    users : 
        agregar columna perfil  *
        agregar columna codempresa *
        
    Crear tabla usuarios *

    Crear tabla perfiles *

    Crear tabla payregistries

     

        */
    class jsonapi
    {
        public string api_token { get; set; }
        
        public string apiversion { get; set; }
        public string rolpuntodeventa { get; set; }
        public string prefpuntodeventa { get; set; }
        public string fechamigracion { get; set; }
        public List<propuestas> listpropuestas { get; set; }
        public List<usuarios> listusuarios { get; set; }
        public List<perfiles> listperfiles { get; set; }
        public List<lineas_propuestas> listlineaspropuestas { get; set; }
        public List<clientes> listtomador { get; set; }
        public List<arqueos> listarqueos { get; set; }
        public List<rendiciones> listrendiciones { get; set; }
        public List<lineas_rendiciones> listlineasrendiciones { get; set; }
        public List<barrios_propuesta> listbarriospropuestas { get; set; }
        public List<barrios> listbarrios { get; set; }
        public List<gruposbarrios> listgrupobarrios { get; set; }
        
        public List<actividades> listactividades { get; set; }
        public List<clasificaciones> listclasificaciones { get; set; }
        public List<coberturas> listcoberturas { get; set; }
        

    }
}

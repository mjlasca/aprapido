using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class ImportListObject
    {
        public bool propuestas(List<propuestas> listobj)
        {
            var concat_ = new System.Text.StringBuilder();
            List<string> listAux = new List<string>();
            int aux = 0;
            try
            {
                for (int i = aux; i < listobj.Count; i++)
                {
                    listobj[i].denube = true;
                    listobj[i].envionube = "1";
                    listobj[i].save_import();
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.setError("IMPPROPUESTAS", "Error al importar propuestas de la nube "+ex.Message);
                return false;
            }
        }


        public bool clientes(List<clientes> listobj)
        {
            try
            {
                foreach (clientes cliente in listobj)
                {
                    cliente.envionube = 1;
                    cliente.save();
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.setError("IMPCLIENTES", "Error al guardar clientes de la nube" + ex.Message);
                return false;
            }
        }

        public bool lineas_propuestas(List<lineas_propuestas> listobj)
        {
            try
            {
                List<string> listAux = new List<string>();
                for (int i = 0; i < listobj.Count; i++)
                {
                    listobj[i].delete_idpropuesta(listobj[i].id_propuesta, listobj[i].prefijo);
                }

                for (int i = 0; i < listobj.Count; i++)
                {
                    listobj[i].delete_idpropuesta_doc(listobj[i].id_propuesta, listobj[i].prefijo,listobj[i].documento);
                    listobj[i].save();
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.setError("IMPLINEASPRO","Error al guardar Líneas Propuestas " + ex.Message);
                return false;
            }
        }
    }
}

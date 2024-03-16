using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    class importapi
    {
        public string api_token;
        public string rolpuntodeventa;
        private string urlapi;
        parametros para = new parametros();
        migraciones miPropuestas, miActividades, miClasificacion, miCoberturas,miBarrios,miGruposBarrios;
        public string concattextbox = "";
        string fechaimportacion = "";
        logs log = new logs();

        public importapi()
        {
            puntodeventa punt = new puntodeventa();
            if (punt.get_principal())
            {
                this.api_token = punt.apitoken;
                this.rolpuntodeventa = punt.rol;
                
                this.urlapi = punt.urlapi;
            }
            else if (punt.get_colaborador())
            {
                this.api_token = punt.apitoken;
                this.rolpuntodeventa = punt.rol;
                this.urlapi = punt.urlapi;
            }
            else
            {
                MessageBox.Show("Por favor debe validar el token, el sistema no encuentra un asociado a éste punto de venta",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public async Task<bool> parametroscolaborador(string fecha_ , solicitudes s, bool reset = false, bool solopropuestas = false, bool get_prefix_own = false)
        {
            this.para.api_token = this.api_token;

            if(solopropuestas)
                this.para.solopropuestas = "1";
            if (get_prefix_own)
                this.para.get_prefix_own = "1";
            if (reset)
                this.para.reset = "1";

            this.para.apiversion = MDIParent1.apiversion;
            this.para.codempresa = MDIParent1.codempresa;
            this.para.prefijositio = MDIParent1.prefijo;
            this.para.rolpuntodeventa = this.rolpuntodeventa;
            this.para.fecha_actualizacion_desde = fecha_;
            this.para.fecha_actualizacion_hasta = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string jsonlistpropuestas = "";
            bool res = false;


            string errores = "";

            if (s.solicitud_propuestas)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_propuestas";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (res)
                    {
                        
                        miPropuestas.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        miPropuestas.save();
                    }
                    else
                    {
                        errores += "No se puedo importar los datos de propuestas";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}"+ex.Message);
                }
                
                    
                
            }

            if (s.solicitud_lineas_propuestas)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_lineas_propuestas";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de lineas propuestas";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                
            }

            

            if (s.solicitud_clientes)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_clientes";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de clientes";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_usuarios)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_usuarios";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de usuarios";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    

            }

            if (s.solicitud_perfiles)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_perfiles";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de perfiles";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_arqueos)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_arqueos";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de arqueos";
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                
            }

            if (s.solicitud_rendiciones)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_rendiciones";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de rendiciones";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_lineas_rendiciones)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_lineas_rendiciones";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de lineas rendiciones";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
             
            }

            if (s.solicitud_actividades)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_actividades";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de actvidades";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_coberturas)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_coberturas";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de coberturas";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_clasificaciones)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_clasificaciones";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de clasificaciones";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_barrios)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_barrios";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de barrios";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
            }

            if (s.solicitud_gruposbarrios)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_gruposbarrios";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de gruposbarrios";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
                
            }

            if (s.solicitud_provincias)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_provincias";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de provincias";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }
                    
            }

            /*if (s.solicitud_barrios_propuestas)
            {
                try
                {
                    jsonlistpropuestas = "";
                    this.para.solicitud = "solicitud_barrios_propuestas";
                    jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
                    res = await this.enviardatos(jsonlistpropuestas);
                    if (!res)
                    {
                        errores += "No se puedo importar los datos de barrios propuestas";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Excepción {this.para.solicitud}" + ex.Message);
                }


            }*/


            if (errores != "")
            {
                logs log = new logs();
                log.newError("IMPORTE 404", errores);
                return false;

            }
            else
            {
                return true;
            }
                
        }

        public async Task<bool> solicitarpuntosdeventa()
        {
            this.para.api_token = this.api_token;
            this.para.apiversion = MDIParent1.apiversion;
            this.para.codempresa = MDIParent1.codempresa;
            this.para.prefijositio = MDIParent1.prefijo;
            this.para.rolpuntodeventa = this.rolpuntodeventa;
            

            string jsonlistpropuestas = JsonConvert.SerializeObject(this.para);
            

            bool res = await this.getpuntosdeventa(jsonlistpropuestas);
            if (res)
            {
                return true;
            }



            return false;
        }


        private async Task<bool> enviardatos(string json)
        {

            Console.WriteLine("JSON IMP \n"+json);
            /*try
            {*/
                string url = MDIParent1.apiuri + "/api/parametros";
                Console.WriteLine("URL IMP \n"+url);
                WebRequest _request = WebRequest.Create(url);
                
                _request.Method = "POST";
                _request.ContentType = "application/json;charset=UTF-8";
                _request.Timeout = 36000000;

                using (var osw = new StreamWriter(_request.GetRequestStream()))
                {
                    osw.Write(json);
                    osw.Flush();
                    osw.Close();
                }

                WebResponse _response = _request.GetResponse();
                validaciones val = new validaciones();
                string res = "";
                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    res = ors.ReadToEnd().Trim();
                }
                if (res != "")
                    {


                    if (!File.Exists(@"file_importaciones/importacion_" + this.para.solicitud + ".txt"))
                    {
                        System.IO.Directory.CreateDirectory("file_importaciones");
                        System.IO.File.WriteAllText(@"file_importaciones/importacion_" + this.para.solicitud + ".txt", " ");
                    }
                    else
                    {
                        if (this.para.reset == "1")
                        {
                            System.IO.File.WriteAllText(@"file_importaciones/importacion_" + this.para.solicitud + ".txt", " ");
                        }

                    }

                    System.IO.File.WriteAllText(@"file_importaciones/importacion_" + this.para.solicitud + "1.txt", res);
                    if (val.FileCompare(@"file_importaciones/importacion_" + this.para.solicitud + ".txt", @"file_importaciones/importacion_" + this.para.solicitud + "1.txt"))
                    {
                        return false;
                    }
                    
                        
                    
                        




                    JsonTextReader reader = new JsonTextReader(new StringReader(res));
                        JObject obj = JObject.Load(reader);
                        /*if (obj.Count > 0)
                        {
                            this.fechaimportacion = obj["fechaimportacion"].ToString();
                        }*/
                        await Task.Run(() =>
                        {
                            if (this.para.rolpuntodeventa == "COLABORADOR" && this.para.solopropuestas != "1")
                            {
                                //if (this.para.solicitud == "solicitud_actividades")
                                    //this.tarea_actividades(res);
                                //if (this.para.solicitud == "solicitud_clasificaciones")
                                    //this.tarea_clasificaciones(res);
                                //if (this.para.solicitud == "solicitud_coberturas")
                                    //this.tarea_coberturas(res);
                                //if (this.para.solicitud == "solicitud_barrios")
                                    //this.tarea_barrios(res);
                                //if (this.para.solicitud == "solicitud_gruposbarrios")
                                    //this.tarea_grupos_barrios(res);
                                if (this.para.solicitud == "solicitud_provincias")
                                    this.tarea_provincias(res);
                            }
                                
                        });
                        
                        await Task.Run(() =>
                        {
                            if (this.para.solicitud == "solicitud_propuestas")
                                this.tarea_propuestas(res);
                            if (this.para.solicitud == "solicitud_lineas_propuestas")
                                this.tarea_lineas_propuestas(res);
                            //if (this.para.solicitud == "solicitud_barrios_propuestas")
                              //  this.tarea_barrios_propuestas(res);
                            if (this.para.solicitud == "solicitud_clientes")
                                this.tarea_clientes(res);
                            if (this.para.solicitud == "solicitud_arqueos")
                                this.tarea_arqueos(res);
                            if (this.para.solicitud == "solicitud_rendiciones")
                                this.tarea_rendiciones(res);
                            if (this.para.solicitud == "solicitud_lineas_rendiciones")
                                this.tarea_lineas_rendiciones(res);
                            /*if (this.para.solicitud == "solicitud_usuarios")
                                this.tarea_usuarios(res);*/
                            if (this.para.solicitud == "solicitud_perfiles")
                                this.tarea_perfiles(res);
                            if (this.para.solicitud == "solicitud_barrios" )
                                this.tarea_barrios(res);

                        });

                    }
                    
                

                System.IO.File.WriteAllText(@"file_importaciones/importacion_" + this.para.solicitud + ".txt", res);

                return true;

            /*}
            catch (Exception ex)
            {
                log.coderror = "I130";
                log.mensaje = "Error al importar datos " +  ex.Message;
                log.save();
                MDIParent1.server500 = ex.Message;
                return false;
            }*/


            
        }

        private async Task<bool> getpuntosdeventa(string json)
        {
            Console.WriteLine("JSON PUNTOS " + json);
            try
            {
                string url = MDIParent1.apiuri + "/api/getPuntos";
                Console.WriteLine("URL GETPUNTOS "+url);
                WebRequest _request = WebRequest.Create(url);
                _request.Method = "POST";
                _request.ContentType = "application/json;charset=UTF-8";

                using (var osw = new StreamWriter(_request.GetRequestStream()))
                {
                    osw.Write(json);
                    osw.Flush();
                    osw.Close();
                }



                WebResponse _response = _request.GetResponse();
                
                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    string res = ors.ReadToEnd().Trim();

                    if (res != "")
                    {
                        JsonTextReader reader = new JsonTextReader(new StringReader(res));
                        JObject obj = JObject.Load(reader);
                        
                    }
                    
                    await Task.Run(() =>
                    {
                        this.prosspuntodeventa(res);
                    });
                }

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al importar los datos " + ex.Message);
                return false;
            }



        }


        private void prosspuntodeventa(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);


            if (obj["puntos"] != null)
            {
                List<puntodeventa> listpuntos  = (from dynamic val in obj["puntos"].AsEnumerable().ToList()
                                    select new puntodeventa()
                                    {
                                        nombre = val["name"] == null ? "" : val["name"],
                                        usuario = val["email"] == null ? "" : val["email"],
                                        prefijo = val["prefijo"] == null ? "" : val["prefijo"],
                                        perfil = val["perfil"] == null ? "" : val["perfil"],
                                        aseguradora = val["aseguradora"] == null ? "" : val["aseguradora"],
                                        master = val["master"] == null ? "" : val["master"],
                                        organizador = val["organizador"] == null ? "" : val["organizador"],
                                        codmaster = val["codmaster"] == null ? "" : val["codmaster"],
                                        codorganizador = val["codorganizador"] == null ? "" : val["codorganizador"],
                                        apitoken = val["api_token"] == null ? "" : val["api_token"],
                                        rol = val["rol"] == null ? "" : val["rol"],
                                        codestado = "1",
                                        user_edit = MDIParent1.sesionUser,
                                        ultmod = DateTime.Now.ToString("yyyy-MM-dd"),
                                        codempresa = val["codempresa"],
                                        

                                    }).ToList();
                for(int i = 0; i< listpuntos.Count; i++)
                {
                    listpuntos[i].save_nube();
                }

            }
        }

        private void tarea_propuestas(string json)
        {
            
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            

            if (obj["propuestas"] != null)
            {
                if(obj["propuestas"].Count() > 0)
                {
                    Console.WriteLine("Importando  propuestas (" + obj["propuestas"].Count() + ") " + DateTime.Now);

                    this.concattextbox += "IMPORTACIÓN PROPUESTAS / " + obj["propuestas"].Count() + " Registros " + Environment.NewLine;

                    List<propuestas> listobj = (from dynamic val in obj["propuestas"].AsEnumerable().ToList()
                                                select new propuestas()
                                                {
                                                    denube = true,
                                                    documento = val["documento"] == null ? "" : val["documento"],
                                                    num_polizas = val["num_polizas"] == null ? "" : val["num_polizas"],
                                                    meses = val["meses"] == null ? "" : val["meses"],
                                                    id_cobertura = val["id_cobertura"] == null ? "" : val["id_cobertura"],
                                                    id_barrio = val["id_barrio"] == null ? "" : val["id_barrio"],
                                                    nueva_poliza = val["nueva_poliza"] == null ? "" : val["nueva_poliza"],
                                                    premio = val["premio"] == null ? "" : val["premio"],
                                                    premio_total = val["premio_total"] == null ? "" : val["premio_total"],
                                                    fechaDesde = val["fechaDesde"] == null ? "" : val["fechaDesde"],
                                                    fechaHasta = val["fechaHasta"] == null ? "" : val["fechaHasta"],
                                                    clausula = val["clausula"] == null ? "" : val["clausula"],
                                                    barrio_beneficiario = val["barrio_beneficiario"] == null ? "" : val["barrio_beneficiario"],
                                                    ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                                    user_edit = val["useredit"] == null ? "online" : val["useredit"],
                                                    codestado = val["codestado"] == null ? "0" : val["codestado"],
                                                    cobertura_suma = val["cobertura_suma"] == null ? "0" : val["cobertura_suma"],
                                                    cobertura_deducible = val["cobertura_deducible"] == null ? "0" : val["cobertura_deducible"],
                                                    cobertura_gastos = val["cobertura_gastos"] == null ? "0" : val["cobertura_gastos"],
                                                    promocion = val["promocion"] == null ? "" : val["promocion"],
                                                    paga = val["paga"] == null ? "0" : val["paga"],
                                                    fecha_paga = val["fecha_paga"] == null ? "1000-01-01 01:00:00" : val["fecha_paga"],
                                                    referencia = val["referencia"] == null ? "" : val["referencia"],
                                                    prima = val["prima"] == null ? "0" : val["prima"],
                                                    master = val["master"] == null ? "" : val["master"],
                                                    organizador = val["organizador"] == null ? "" : val["organizador"],
                                                    productor = val["productor"] == null ? "" : val["productor"],
                                                    prefijo = val["prefijo"] == null ? "" : val["prefijo"],
                                                    tipopago = val["tipopago"] == null ? "" : val["tipopago"],
                                                    formadepago = val["formadepago"] == null ? "" : val["formadepago"],
                                                    compformapago = val["compformadepago"] == null ? "" : val["compformadepago"],
                                                    usuariopaga = val["usuariopaga"] == null ? "" : val["usuariopaga"],
                                                    idpropuesta = val["reg"] == null ? "" : val["reg"],
                                                    nota = val["nota"] == null ? "" : val["nota"],
                                                    envionube = "1",
                                                    data_barrios = val["data_barrios"] == null ? "{\"barrios\":[]}" : val["data_barrios"],
                                                    version = val["version"] == null ? "" : val["version"],
                                                    valor_pagado = val["valor_pagado"] == null ? "" : val["valor_pagado"],
                                                    imputacion = val["imputacion"] == null ? "" : val["imputacion"],
                                                    fecha_comprobante = val["fecha_comprobante"] == null ? "1000-01-01" : val["fecha_comprobante"],

                                                }).ToList();

                    var concat_ = new System.Text.StringBuilder();
                    List<string> listAux = new List<string>();
                    string concat_1 = "";
                    int limit = 500;
                    int aux = 0;
                    try
                    {
                            for (int i = aux; i < listobj.Count; i++)
                            {
                                listobj[i].save_import();
                                concat_1 += listobj[i].prefijo + listobj[i].idpropuesta + ",";
                            }

                        if (concat_1 != "")
                        {
                                this.concattextbox += Environment.NewLine + Environment.NewLine;
                                migraciones mig = new migraciones();
                                mig.tabla = "propuestas";
                                mig.tipo = "IMPORTACION";
                                mig.numeracion = concat_1;
                                mig.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                mig.cantidad_registros = obj["propuestas"].Count().ToString();
                                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                mig.useredit = MDIParent1.sesionUser;
                                miPropuestas = mig;
                        }

                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I110";
                        log.mensaje = "Error al guardar Propuesta " + ex.Message;
                        log.save();
                    }
                }
                

            }


        }

        private void tarea_lineas_propuestas(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            List<string> listAux = new List<string>();

            if (obj["lineas_propuestas"] != null)
            {

                Console.WriteLine("Importando Líneas propuestas (" + obj["lineas_propuestas"].Count() + ") " + DateTime.Now);


                List<lineas_propuestas> listobj = (from dynamic val in obj["lineas_propuestas"].AsEnumerable().ToList()
                                                   select new lineas_propuestas()
                                                   {
                                                       id = val["reg"] == null ? "" : val["id"],
                                                       id_propuesta = val["id_propuesta"] == null ? "" : val["id_propuesta"],
                                                       idprefijo = val["id_propuesta"] == null ? "" : val["id_propuesta"],
                                                       documento = val["documento"] == null ? "" : val["documento"],
                                                       tipo_documento = val["tipo_documento"] == null ? "" : val["tipo_documento"],
                                                       apellidos = val["apellidos"] == null ? "" : val["apellidos"],
                                                       nombres = val["nombres"] == null ? "" : val["nombres"],
                                                       fecha_nacimiento = val["fecha_nacimiento"] == null ? "" : val["fecha_nacimiento"],
                                                       id_actividad = val["id_actividad"] == null ? "" : val["id_actividad"],
                                                       id_clasificacion = val["id_clasificacion"] == null ? "" : val["id_clasificacion"],
                                                       premio = val["premio"] == null ? "" : val["premio"],
                                                       ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                                       user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                                       codestado = val["codestado"] == null ? "" : val["codestado"],
                                                       prefijo = val["prefijo"] == null ? "" : val["prefijo"],
                                                       actividad = val["actividad"] == null ? "" : val["actividad"],
                                                       clasificacion = val["clasificacion"] == null ? "" : val["clasificacion"],
                                                       fechaDesde = val["fechaDesde"] == null ? "" : val["fechaDesde"],
                                                       fechaHasta = val["fechaHasta"] == null ? "" : val["fechaHasta"]

                                                   }).ToList();
                System.IO.File.WriteAllText("lineaspropuestasjson.json", json);
                var concat_ = new System.Text.StringBuilder();
                
                try
                {


                    for (int i = 0; i < listobj.Count; i++)
                    {

                        if (listAux.IndexOf(listobj[i].prefijo + listobj[i].id_propuesta ) < 0)
                        {
                            if( (listobj[i].prefijo + listobj[i].id_propuesta) != "")
                            {
                                listAux.Add(listobj[i].prefijo + listobj[i].id_propuesta);
                                listobj[i].delete_idpropuesta(listobj[i].id_propuesta, listobj[i].prefijo);
                            }
                        }

                        if(concat_.ToString() != "")
                            concat_.AppendLine(", "+listobj[i].concat_sql());
                        else
                            concat_.AppendLine(listobj[i].concat_sql());
                    }

                    


                    if (concat_.ToString() != "")
                    {
                        lineas_propuestas li = new lineas_propuestas();
                        li.save_concat(concat_.ToString());
                        System.IO.File.WriteAllText("lineaspropuestassql.txt", concat_.ToString());
                    }

                }
                catch (Exception ex)
                {
                    log.coderror = "I121";
                    log.mensaje = "Error al guardar Líneas Propuestas " +  ex.Message;
                    log.save();
                }



            }
        }

        private void tarea_barrios_propuestas(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));

            JObject obj = JObject.Load(reader);
            List<string> listAux = new List<string>();

            if (obj["barrios_propuestas"] != null)
            {

                Console.WriteLine("Importando Barrios propuestas (" + obj["barrios_propuestas"].Count() + ") " + DateTime.Now);

                List<barrios_propuesta> listobj = (from dynamic val in obj["barrios_propuestas"].AsEnumerable().ToList()
                                                   select new barrios_propuesta()
                                                   {
                                                       id = val["reg"] == null ? "" : val["id"],
                                                       id_propuesta = val["id_propuesta"] == null ? "" : val["id_propuesta"],
                                                       idprefijo = val["id_propuesta"] == null ? "" : val["id_propuesta"],
                                                       id_barrio = val["id_barrio"] == null ? "" : val["id_barrio"],
                                                       nombre = val["nombre"] == null ? "" : val["nombre"],
                                                       prefijo = val["prefijo"] == null ? "" : val["prefijo"],
                                                       ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                                       user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                                       codestado = val["codestado"] == null ? "" : val["codestado"]

                                                   }).ToList();

                var concat_ = new System.Text.StringBuilder();
                listAux = new List<string>();
                try
                {

                    int bandera_concat = 200;
                    for (int i = 0; i < listobj.Count; i++)
                    {

                        if (listAux.IndexOf(listobj[i].prefijo + listobj[i].id_propuesta + listobj[i].id_barrio) < 0)
                        {
                            if ((listobj[i].prefijo + listobj[i].id_propuesta + listobj[i].id_barrio) != "")
                            {
                                listAux.Add(listobj[i].prefijo + listobj[i].id_propuesta + listobj[i].id_barrio);
                                listobj[i].delete_idpropuesta(listobj[i].id_propuesta, listobj[i].prefijo);
                            }
                        }

                        if (concat_.ToString() != "")
                            concat_.AppendLine(", " + listobj[i].concatc_insert());
                        else
                            concat_.AppendLine(listobj[i].concatc_insert());


                        if ((i >= bandera_concat && concat_.ToString() != "") || i >= (listobj.Count - 1))
                        {
                            barrios_propuesta b = new barrios_propuesta();
                            b.save_insert_masive(concat_.ToString());

                            concat_ = new System.Text.StringBuilder();
                            bandera_concat = bandera_concat + 200;
                        }


                    }
                    

                }
                catch (Exception ex)
                {

                    log.coderror = "I111";
                    log.mensaje = "Error al guardar Barrios Propuestas " + ex.Message;
                    log.save();
                }


                

            }


        }


        private void tarea_clientes(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            if (obj["clientes"] != null)
            {
                Console.WriteLine("Importando CLIENTES (" + obj["clientes"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTAR CLIENTES / " + obj["clientes"].Count() + " Registros " + Environment.NewLine;

                List<clientes> listobj = (from dynamic val in obj["clientes"].AsEnumerable().ToList()
                                          select new clientes()
                                          {
                                              id = val["id"] == null ? "" : val["id"],
                                              nombres = val["nombres"] == null ? "" : val["nombres"],
                                              apellidos = val["apellidos"] == null ? "" : val["apellidos"],
                                              tipo_id = val["tipo_id"] == null ? "" : val["tipo_id"],
                                              direccion = val["direccion"] == null ? "" : val["direccion"],
                                              telefono = val["telefono"] == null ? "" : val["telefono"],
                                              email = val["email"] == null ? "" : val["email"],
                                              codpostal = val["codpostal"] == null ? "" : val["codpostal"],
                                              localidad = val["localidad"] == null ? "" : val["localidad"],
                                              ciudad = val["ciudad"] == null ? "" : val["ciudad"],
                                              sexo = val["sexo"] == null ? "" : val["sexo"],
                                              fecha_nacimiento = val["fecha_nacimiento"] == null ? "" : val["fecha_nacimiento"],
                                              situacion = val["situacion"] == null ? "" : val["situacion"],
                                              categoria = val["categoria"] == null ? "" : val["categoria"],
                                              ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                              user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                              codestado = "1"

                                          }).ToList();

                var concat_ = new System.Text.StringBuilder();
                try
                {
                    int bandera_concat = 1000;
                    for (int i = 0; i < listobj.Count; i++)
                    {
                    
                        //listobj[i].delete_id();
                        if (concat_.ToString().Trim() != "")
                        {
                            if(listobj[i].concat_sql() != "")
                                concat_.AppendLine(", " + listobj[i].concat_sql());
                        }
                        else
                            concat_.AppendLine(listobj[i].concat_sql());


                        if ((i >= bandera_concat &&  concat_.ToString() != "") || i >= (listobj.Count - 1))
                        {
                            clientes c = new clientes();
                            c.save_concat(concat_.ToString());
                            
                            concat_ = new System.Text.StringBuilder();
                            bandera_concat = bandera_concat + 1000;
                        }

                    }
                    

                }
                catch (Exception ex)
                {
                    log.coderror = "I109";
                    log.mensaje = "Error al guardar Cliente " + ex.Message;
                    log.save();
                }


            }
        }


        private void tarea_arqueos(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            if (obj["arqueos"] != null)
            {
                Console.WriteLine("Importando Arqueos (" + obj["arqueos"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTAR Arqueos / " + obj["arqueos"].Count() + " Registros " + Environment.NewLine;

                List<arqueos> listobj = (from dynamic val in obj["arqueos"].AsEnumerable().ToList()
                                         select new arqueos()
                                         {
                                             id = val["id"] == null ? "" : val["id"],
                                             usuario = val["usuario"] == null ? "" : val["usuario"],
                                             nombre = val["nombre"] == null ? "" : val["nombre"],
                                             fechadia = val["fechadia"] == null ? "" : val["fechadia"],
                                             valorinicial = val["valorinicial"] == null ? "" : val["valorinicial"],
                                             dinerorealcaja = val["dinerorealcaja"] == null ? "" : val["dinerorealcaja"],
                                             valormanual = val["valormanual"] == null ? "" : val["valormanual"],
                                             cuadredescuadre = val["cuadredescuadre"] == null ? "" : val["cuadredescuadre"],
                                             supervisor = val["supervisor"] == null ? "" : val["supervisor"],
                                             nombresupervisor = val["nombresupervisor"] == null ? "" : val["nombresupervisor"],
                                             observaciones = val["observaciones"] == null ? "" : val["observaciones"],
                                             ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                             user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                             codestado = val["codestado"] == null ? "" : val["codestado"],
                                             rendicion = val["rendicion"] == null ? "" : val["rendicion"],
                                             codempresa = val["codempresa"] == null ? "" : val["codempresa"],
                                             cantpolizas = val["cantpolizas"] == null ? "" : val["cantpolizas"]

                                         }).ToList();

                var concat_ = new System.Text.StringBuilder();

                try
                {
                    for (int i = 0; i < listobj.Count; i++)
                    {
                    
                        listobj[i].delete_userdate();
                        if (concat_.ToString() != "")
                            concat_.AppendLine(", " + listobj[i].concat_sql());
                        else
                            concat_.AppendLine(listobj[i].concat_sql());
                    
                    }
                    if (concat_.ToString() != "")
                    {
                        arqueos ar = new arqueos();
                        ar.save_concat(concat_.ToString());
                    }

                }
                catch (Exception ex)
                {
                    log.coderror = "I301";
                    log.mensaje = "Error al guardar Barrios Propuestas " + ex.Message;
                    log.save();
                }

            }

        }

        private void tarea_rendiciones(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            if (obj["rendiciones"] != null)
            {
                Console.WriteLine("Importando rendiciones (" + obj["rendiciones"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTAR Rendiciones / " + obj["rendiciones"].Count() + " Registros " + Environment.NewLine;

                List<rendiciones> listobj = (from dynamic val in obj["rendiciones"].AsEnumerable().ToList()
                                             select new rendiciones()
                                             {
                                                 reg = val["reg"] == null ? "" : val["reg"],
                                                 idarqueos = val["idarqueos"] == null ? "" : val["idarqueos"],
                                                 detalle = val["detalle"] == null ? "" : val["detalle"],
                                                 valor = val["valor"] == null ? "" : val["valor"],
                                                 nocomprobante = val["nocomprobante"] == null ? "" : val["nocomprobante"],
                                                 entregadopor = val["entregadopor"] == null ? "" : val["entregadopor"],
                                                 entregadoa = val["entregadoa"] == null ? "" : val["entregadoa"],
                                                 ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                                 user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                                 codestado = val["codestado"] == null ? "" : val["codestado"],
                                                 codempresa = val["codempresa"] == null ? "" : val["codempresa"]

                                             }).ToList();

                var concat_ = new System.Text.StringBuilder();
                try
                {
                    for (int i = 0; i < listobj.Count; i++)
                    {
                    
                            listobj[i].delete_idarqueos();
                            if (concat_.ToString() != "")
                                concat_.AppendLine(", " + listobj[i].concat_sql());
                            else
                                concat_.AppendLine(listobj[i].concat_sql());
                    
                    }
                    if (concat_.ToString() != "")
                    {
                        rendiciones ren = new rendiciones();
                        ren.save_concat(concat_.ToString());
                    }
                }
                catch (Exception ex)
                {
                    log.coderror = "I302";
                    log.mensaje = "Error al guardar Barrios Propuestas " + ex.Message;
                    log.save();
                }

            }

        }

        private void tarea_lineas_rendiciones(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json));
            JObject obj = JObject.Load(reader);

            if (obj["lineas_rendiciones"] != null)
            {

                Console.WriteLine("Importando lineas_rendiciones (" + obj["lineas_rendiciones"].Count() + ") " + DateTime.Now);

                List<lineas_rendiciones> listobj = (from dynamic val in obj["lineas_rendiciones"].AsEnumerable().ToList()
                                                    select new lineas_rendiciones()
                                                    {
                                                        id = val["id"] == null ? "" : val["id"],
                                                        idrendicion = val["idrendicion"] == null ? "" : val["idrendicion"],
                                                        fechaarqueo = val["fechaarqueo"] == null ? "" : val["fechaarqueo"],
                                                        ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                                        valordia = val["valordia"] == null ? "" : val["valordia"],
                                                        codempresa = val["codempresa"] == null ? "" : val["codempresa"]

                                                    }).ToList();
                string aux = "";

                var concat_ = new System.Text.StringBuilder();
                try
                {
                    for (int i = 0; i < listobj.Count; i++)
                    {
                    
                        if (aux != listobj[i].idrendicion)
                        {
                            listobj[i].delete();
                            aux = listobj[i].idrendicion;
                        }
                        if (concat_.ToString() != "")
                            concat_.AppendLine(", " + listobj[i].concat_sql());
                        else
                            concat_.AppendLine(listobj[i].concat_sql());
                        
                    
                    }
                    if (concat_.ToString() != "")
                    {
                        lineas_rendiciones li = new lineas_rendiciones();
                        li.save_concat(concat_.ToString());
                    }

                }
                catch (Exception ex)
                {
                    log.coderror = "I303";
                    log.mensaje = "Error al guardar Barrios Propuestas " + ex.Message;
                    log.save();
                }


            }

        }

        private void tarea_actividades(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);



            if (obj["actividades"] != null)
            {
                string consecutivos = "";
                int cont = 0;
                Console.WriteLine("Importando actividades (" + obj["actividades"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Actividades / " + obj["actividades"].Count() + " Registros " + Environment.NewLine;


                var concat_ = new System.Text.StringBuilder();
                

                foreach (dynamic val in obj["actividades"])
                {
                    actividades act = new actividades();
                    act.id = val["reg"];
                    act.cod = val["cod"];
                    act.nombre = val["nombre"];
                    act.ultmod = val["ultmod"];
                    act.user_edit = val["user_edit"];
                    act.codestado = val["codestado"];

                    try
                    {
                        concat_.AppendLine(act.concat_sql());
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I101";
                        log.mensaje = "Error al guardar actividad " + act.nombre + ex.Message;
                        log.save();
                    }


                    consecutivos += act.cod + ",";
                    cont++;
                }

                if (concat_.ToString() != "")
                {
                    actividades ac = new actividades();
                    ac.save_concat(concat_.ToString());
                }

                migraciones mig = new migraciones();
                mig.tabla = "actividades";
                mig.tipo = "IMPORTACION";
                mig.numeracion = consecutivos;
                mig.fecha = para.fecha_actualizacion_hasta;
                mig.cantidad_registros = cont.ToString();
                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mig.useredit = MDIParent1.sesionUser;
                miActividades = mig;
            }
        }

        private void tarea_clasificaciones(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            if (obj["clasificaciones"] != null)
            {
                string consecutivos = "";
                int cont = 0;

                Console.WriteLine("Importando clasificaciones (" + obj["clasificaciones"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Clasificaciones / " + obj["clasificaciones"].Count() + " Registros " + Environment.NewLine;

                var concat_ = new System.Text.StringBuilder();

                foreach (dynamic val in obj["clasificaciones"])
                {
                    clasificaciones cla = new clasificaciones();
                    cla.id = val["reg"];
                    cla.cod = val["cod"];
                    cla.nombre = val["nombre"];
                    cla.id_actividad = val["id_actividad"];
                    cla.ultmod = val["ultmod"];
                    cla.user_edit = val["user_edit"];
                    cla.codestado = val["codestado"];


                    try
                    {
                        concat_.AppendLine(cla.concat_sql());
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I102";
                        log.mensaje = "Error al guardar clasificación " + cla.nombre + ex.Message;
                        log.save();
                    }

                    consecutivos += cla.cod + ",";
                    cont++;
                }

                if (concat_.ToString() != "")
                {
                    clasificaciones cla1 = new clasificaciones();
                    cla1.save_concat(concat_.ToString());
                }

                migraciones mig = new migraciones();
                mig.tabla = "clasificaciones";
                mig.tipo = "IMPORTACION";
                mig.numeracion = consecutivos;
                mig.fecha = para.fecha_actualizacion_hasta;
                mig.cantidad_registros = cont.ToString();
                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mig.useredit = MDIParent1.sesionUser;
                miClasificacion = mig;
            }
        }

        private void tarea_coberturas(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);
            if (obj["coberturas"] != null)
            {
                string consecutivos = "";
                int cont = 0;

                Console.WriteLine("Importando coberturas (" + obj["coberturas"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Coberturas / " + obj["coberturas"].Count() + " Registros " + Environment.NewLine;

                var concat_ = new System.Text.StringBuilder();

                foreach (dynamic val in obj["coberturas"])
                {
                    coberturas cob = new coberturas();
                    cob.nombre = val["nombre"];
                    cob.suma = val["suma"];
                    cob.gastos = val["gastos"];
                    cob.deducible = val["deducible"];
                    cob.vrMensual = val["vrMensual"];
                    cob.vrTrimestral = val["vrTrimestral"];
                    cob.vrSemestral = val["vrSemestral"];
                    cob.x21 = val["x21"];
                    cob.x32 = val["x32"];
                    cob.x64 = val["x64"];
                    cob.ultmod = val["ultmod"];
                    cob.user_edit = val["user_edit"];
                    cob.codestado = val["codestado"];


                    try
                    {
                        concat_.AppendLine(cob.concat_sql());
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I103";
                        log.mensaje = "Error al guardar clasificación " + cob.nombre + ex.Message;
                        log.save();
                    }

                    consecutivos += cob.nombre + ",";
                    cont++;
                }

                if (concat_.ToString() != "")
                {
                    coberturas co = new coberturas();
                    co.save_concat(concat_.ToString());
                }

                migraciones mig = new migraciones();
                mig.tabla = "coberturas";
                mig.tipo = "IMPORTACION";
                mig.numeracion = consecutivos;
                mig.fecha = para.fecha_actualizacion_hasta;
                mig.cantidad_registros = cont.ToString();
                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mig.useredit = MDIParent1.sesionUser;
                miCoberturas = mig;
            }
        }

        private void tarea_barrios(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            if (obj["barrios"] != null)
            {

                Console.WriteLine( "IMPORTACIÓN Barrios / " + obj["barrios"].Count() + " Registros " + Environment.NewLine);
                this.concattextbox += "IMPORTACIÓN Barrios / " + obj["barrios"].Count() + " Registros " + Environment.NewLine;

                List<barrios> listobj = (from dynamic val in obj["barrios"].AsEnumerable().ToList()
                                         select new barrios()
                                         {
                                             nombre = val["nombre"] == null ? "" : val["nombre"],
                                             id = val["id"] == null ? "" : val["id"],
                                             telefono = val["telefono"] == null ? "" : val["telefono"],
                                             direccion = val["direccion"] == null ? "" : val["direccion"],
                                             email = val["email"] == null ? "" : val["email"],
                                             sub_barrio = val["sub_barrio"] == null ? "" : val["sub_barrio"],
                                             clase_barrio = val["clase_barrio"] == null ? "" : val["clase_barrio"],
                                             suma_muerte = val["suma_muerte"] == null ? "" : val["suma_muerte"],
                                             suma_gm = val["suma_gm"] == null ? "" : val["suma_gm"],
                                             suma_rc = val["suma_rc"] == null ? "" : val["suma_rc"],
                                             exige = val["exige"] == null ? "" : val["exige"],
                                             observaciones = val["observaciones"] == null ? "" : val["observaciones"],
                                             ultmod = val["ultmod"] == null ? "" : val["ultmod"],
                                             user_edit = val["user_edit"] == null ? "" : val["user_edit"],
                                             codestado = val["codestado"] == null ? "" : val["codestado"]

                                         }).ToList();
                
                var concat_ = new System.Text.StringBuilder();

                for (int i = 0; i < listobj.Count; i++)
                {
                    listobj[i].envionube = 0;
                    listobj[i].save();
                }
                


                migraciones mig = new migraciones();
                mig.tabla = "barrios";
                mig.tipo = "IMPORTACION";
                mig.numeracion = "";
                mig.fecha = para.fecha_actualizacion_hasta;
                mig.cantidad_registros = listobj.Count().ToString();
                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mig.useredit = MDIParent1.sesionUser;
                miBarrios = mig;
            }
        }

        private void tarea_grupos_barrios(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            List<string> listAux = new List<string>();
            

                if (obj["gruposbarrios"] != null)
            {
                List<gruposbarrios> listobj = (from dynamic val in obj["gruposbarrios"].AsEnumerable().ToList()
                                               select new gruposbarrios()
                                               {
                                                   nombre = val["nombre"],
                                                   id = val["id"],
                                                   idbarrio = val["idbarrio"],
                                                   nombrebarrio = val["nombrebarrio"]

                                               }).ToList();

                Console.WriteLine("Importando gruposbarrios (" + listobj.Count + ") " + DateTime.Now);
                var concat_ = new System.Text.StringBuilder();

                for (int i = 0; i < listobj.Count; i++)
                {
                    if (listAux.IndexOf(listobj[i].id + listobj[i].idbarrio) < 0)
                    {
                        listobj[i].delete();
                        listAux.Add(listobj[i].id + listobj[i].idbarrio);
                        try
                        {
                            if (concat_.ToString() != "")
                                concat_.AppendLine(", " + listobj[i].concat_sql());
                            else
                                concat_.AppendLine(listobj[i].concat_sql());
                        }
                        catch (Exception ex)
                        {
                            log.coderror = "I105";
                            log.mensaje = "Error al guardar Grupo de Barrio" + ex.Message;
                            log.save();
                        }
                    }


                }

                if (concat_.ToString() != "")
                {
                    gruposbarrios gb = new gruposbarrios();
                    gb.save_concat(concat_.ToString());
                }
                

                migraciones mig = new migraciones();
                mig.tabla = "gruposbarrios";
                mig.tipo = "IMPORTACION";
                mig.numeracion = "";
                mig.fecha = para.fecha_actualizacion_hasta;
                mig.cantidad_registros = listobj.Count().ToString();
                mig.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mig.useredit = MDIParent1.sesionUser;
                miGruposBarrios = mig;
            }
        }


        private void tarea_provincias(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            if (obj["provincias"] != null)
            {

                Console.WriteLine("Importando provincias (" + obj["provincias"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Provincias / " + obj["provincias"].Count() + " Registros " + Environment.NewLine;

                List<provincias> listobj = (from dynamic val in obj["provincias"].AsEnumerable().ToList()
                                            select new provincias()
                                            {
                                                codpostal = val["codpostal"],
                                                provincia = val["provincia"],
                                                ciudad = val["ciudad"],
                                                codestado = val["codestado"],
                                                ultmod = val["ultmod"],
                                                user_edit = val["user_edit"]

                                            }).ToList();

                var concat_ = new System.Text.StringBuilder();
                provincias prov = new provincias();
                if(obj["provincias"].Count()  > 1000)
                {
                    prov.delete_all();
                }

                for (int i = 0; i < listobj.Count; i++)
                {
                    try
                    {
                        if (concat_.ToString() != "")
                            concat_.AppendLine(", " + listobj[i].concat_sql());
                        else
                            concat_.AppendLine(listobj[i].concat_sql());
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I108";
                        log.mensaje = "Error al guardar Provincia" + listobj[i].codpostal + " / " + listobj[i].ciudad + ex.Message;
                        log.save();
                    }
                }
                if (concat_.ToString() != "")
                {
                    prov.save_concat(concat_.ToString());
                }


            }


        }


        
        private void tarea_usuarios(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            if (obj["usuarios"] != null)
            {
                Console.WriteLine("Importando usuarios (" + obj["usuarios"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Usuarios / " + obj["usuarios"].Count() + " Registros " + Environment.NewLine;

                string concat_ = "";

                foreach (dynamic val in obj["usuarios"])
                {
                    usuarios usu = new usuarios();
                    usu.passhash = true;
                    usu.loggin = val["loggin"];
                    usu.pass = val["pass"];
                    usu.nombre = val["nombre"];
                    usu.mail = val["mail"];
                    usu.perfil = val["perfil"];
                    usu.allow = val["allow"];
                    usu.comisionprima = val["comisionprima"];
                    usu.comisionpremio = val["comisionpremio"];
                    usu.codigoproductor = val["codigoproductor"];
                    usu.codorganizador = val["codorganizador"];
                    usu.codestado = val["codestado"];
                    usu.codempresa = val["codempresa"];
                    usu.adminempresa = val["adminempresa"];

                    try
                    {
                        concat_ += usu.concat_sql();
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I106";
                        log.mensaje = "Error al guardar Usuario" + usu.loggin + ex.Message;
                        log.save();
                    }

                }

                usuarios u = new usuarios();
                u.save_sql(concat_);
            }
        }

        private void tarea_perfiles(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);
            if (obj["perfiles"] != null)
            {
                string aux = "";
                Console.WriteLine("Importando perfiles (" + obj["perfiles"].Count() + ") " + DateTime.Now);
                this.concattextbox += "IMPORTACIÓN Perfiles / " + obj["perfiles"].Count() + " Registros " + Environment.NewLine;

                string concat_ = "";
                foreach (dynamic val in obj["perfiles"])
                {
                    perfiles per = new perfiles();
                    per.reg = val["reg"];
                    per.nombre = val["nombre"];
                    per.modulo = val["modulo"];
                    per.access = val["access"];
                    per.vista = val["vista"];
                    per.edicion = val["edicion"];
                    per.eliminar = val["eliminar"];
                    per.exportar = val["exportar"];
                    per.codempresa = val["codempresa"];

                    if (aux != per.nombre)
                    {
                        if (aux != "")
                            per.delete();
                        aux = per.nombre;
                    }

                    try
                    {
                        string aaa = per.concat_sql();
                        if (concat_ != "")
                            concat_ += "," + aaa;
                        else
                            concat_ += aaa;
                    }
                    catch (Exception ex)
                    {
                        log.coderror = "I107";
                        log.mensaje = "Error al guardar Perfil" + per.nombre + ex.Message;
                        log.save();
                    }


                }
                Console.WriteLine();
                if (concat_.ToString() != "")
                {
                    perfiles pe = new perfiles();
                    pe.save_concat_sql(concat_);
                }
                Console.WriteLine("\n\n" + "TERMINA perfiles " + DateTime.Now);
            }
        }

    }


    class parametros{
        public string reset { get; set; }
        public string apiversion { get; set; }
        public string solopropuestas { get; set; }
        public string get_prefix_own { get; set; }
        public string prefijositio { get; set; }
        public string codempresa { get; set; }
        public string api_token { get; set; }
        public string fecha_actualizacion_desde { get; set; }
        public string fecha_actualizacion_hasta { get; set; }
        public string rolpuntodeventa { get; set; }

        public string solicitud { get; set; } = string.Empty;
        

    }

}

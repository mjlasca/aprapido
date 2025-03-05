using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProyectoBrokerDelPuerto
{
    class ApiMissing
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiMissing()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api/propuestas/missing";
            this.repen = new RegisterPending();
        }


        public async Task<int> Get(string date, string prefijo, string idpropuesta)
        {
            int rest = 0;
            List<coberturas> ls = new List<coberturas>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/" + date + "/" + MDIParent1.codempresa + "/" + prefijo + "/" + idpropuesta, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    if(jsonContent != "" && jsonContent != "[]")
                    {
                        var jsonObject = JObject.Parse(jsonContent);
                        rest = jsonObject["cantProp"].ToObject<int>();
                        var propuestas = jsonObject["report"]["propuestas"].ToObject<List<propuestas>>();
                        var lineas = jsonObject["report"]["lineas"].ToObject<List<lineas_propuestas>>();
                        var clientes = jsonObject["report"]["clientes"].ToObject<List<clientes>>();

                        ImportListObject imp = new ImportListObject();
                        imp.propuestas(propuestas);
                        imp.lineas_propuestas(lineas);
                        imp.clientes(clientes);
                        
                    }
                }
                else
                {
                    logs.setError("REPORT", "Erro al hacer la consulta en la nube");
                }
            }
            catch (Exception ex)
            {
                logs.setError("REPORT", "Ha ocurrido un error al obtener los datos " + ex.Message);
            }

            return rest;
        }

    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProyectoBrokerDelPuerto
{
    class ApiPropuestas
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiPropuestas()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api/propuestas";
            this.repen = new RegisterPending();
        }


        public async Task<List<ReferenceCloud>> GetRef(IEnumerable<RegistroRef> lsrefs)
        {
            List<ReferenceCloud> lsUs = new List<ReferenceCloud>();
            var resultados = new { registros = lsrefs.ToList() };
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var jsonData = JsonConvert.SerializeObject(resultados);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/refpropuesta/" + MDIParent1.codempresa, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")

            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);
            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ReferenceCloud>>(jsonContent);
                    lsUs = result;
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"GET" + this.path, "No se ha podido obtener los ReferenceCloud " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo una excepción en la tarea SendAsync: " + ex.InnerException.Message);
                logs log = new logs();
                log.newError(@"GET" + this.path, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }

            return lsUs;

        }



        public async Task<List<RegistroRef>> SetRef(string jsonData)
        {
            List<RegistroRef> lsUs = new List<RegistroRef>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/setreference/"+MDIParent1.codempresa, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<RegistroRef>>(jsonContent);
                    lsUs = result;

                    this.repen.path = this.path + "/setreference/" + MDIParent1.codempresa;
                    this.repen.data = jsonData;
                    this.repen.verbo = "Post";
                    this.repen.codestado = 1;
                    this.repen.save();
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"GET" + this.path, "No se ha podido obtener los ReferenceCloud " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo una excepción en la tarea SendAsync: " + ex.InnerException.Message);
                logs log = new logs();
                log.newError(@"GET" + this.path, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }



            return lsUs;

        }

    }

    
}

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProyectoBrokerDelPuerto
{
    class ApiGrupoBarrios
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiGrupoBarrios()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api/grupobarrios";
            this.repen = new RegisterPending();
        }


        public async Task<List<gruposbarrios>> Get()
        {
            List<gruposbarrios> lsUs = new List<gruposbarrios>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path+ "/" + MDIParent1.codempresa, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<gruposbarrios>>(jsonContent);
                    lsUs = result;
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"GET"+this.path, "No se ha podido obtener los gruposbarrios "+response.StatusCode);
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


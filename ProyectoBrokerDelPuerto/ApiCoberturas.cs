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
    class ApiCoberturas
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiCoberturas()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api/coberturas";
            this.repen = new RegisterPending();
        }


        public async Task<List<coberturas>> Get()
        {
            List<coberturas> ls = new List<coberturas>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/" + MDIParent1.codempresa, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<coberturas>>(jsonContent);
                    ls = result;
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"GET" + this.path, "No se ha podido obtener los usuarios " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo una excepción en la tarea SendAsync: " + ex.InnerException.Message);
                logs log = new logs();
                log.newError(@"GET" + this.path, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }



            return ls;

        }

        public async Task<bool> Post(List<coberturas> lscoberturas)
        {
            bool rest = false;

            var jsonData = JsonConvert.SerializeObject(lscoberturas);
            var client = new HttpClient();

            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path+ "/setcoberturas/" + MDIParent1.codempresa, UriKind.Relative),
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
                    var result = JsonConvert.DeserializeObject<List<coberturas>>(jsonContent);
                    rest = true;

                    this.repen.path = this.path + "/setcoberturas/" + MDIParent1.codempresa;
                    this.repen.data = jsonData;
                    this.repen.verbo = "Post";
                    this.repen.codestado = 1;
                    this.repen.save();
                }
                else
                {
                    rest = false;
                    logs log = new logs();
                    log.newError(@"POST" + this.path + "/setcoberturas/" + MDIParent1.codempresa, "No se ha podido enviar los datos" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                rest = false;
                logs log = new logs();
                log.newError(@"POST" + this.path + "/setcoberturas/" + MDIParent1.codempresa, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }

            this.repen.path = this.path + "/setcoberturas/" + MDIParent1.codempresa;
            this.repen.data = jsonData;
            this.repen.verbo = "Post";
            this.repen.save();


            return rest;

        }

    }

    
}

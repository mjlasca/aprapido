using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProyectoBrokerDelPuerto
{
    class ApiUsuarios
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiUsuarios()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api/usuarios";
            this.repen = new RegisterPending();
        }


        public async Task<List<usuarios>> Get()
        {
            List<usuarios> lsUs = new List<usuarios>();

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
                    var result = JsonConvert.DeserializeObject<List<usuarios>>(jsonContent);
                    lsUs = result;
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"GET"+this.path, "No se ha podido obtener los usuarios "+response.StatusCode);
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

        public async Task<bool> Post(List<usuarios> lsusuarios)
        {
            bool rest = false;

            var jsonData = JsonConvert.SerializeObject(lsusuarios);
            var client = new HttpClient();

            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path, UriKind.Relative),
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
                    var result = JsonConvert.DeserializeObject<List<usuarios>>(jsonContent);
                    rest = true;

                    this.repen.path = this.path;
                    this.repen.data = jsonData;
                    this.repen.verbo = "Post";
                    this.repen.codestado = 1;
                    this.repen.save();
                }
                else
                {
                    rest = false;
                    logs log = new logs();
                    log.newError(@"POST" + this.path, "No se ha podido enviar los datos" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                rest = false;
                logs log = new logs();
                log.newError(@"POST" + this.path, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }

            this.repen.path = this.path;
            this.repen.data = jsonData;
            this.repen.verbo = "Post";
            this.repen.save();


            return rest;
            
        }
    }
}


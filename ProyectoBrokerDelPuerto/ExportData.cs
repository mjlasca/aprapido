using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProyectoBrokerDelPuerto
{
    class ExportData
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }

        public ExportData(string path)
        {
            this.baseEndPoint = MDIParent1.apiuri;
            puntodeventa punt = new puntodeventa();
            if (!punt.get_principal())
            {
                punt.get_colaborador();
            }
            this.apiKey = punt.apitoken;
            this.path = path;
        }


        public async Task<bool> Post(string data)
        {
            bool rest = false;

            var client = new HttpClient();

            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    rest = true;
                }
                else
                {
                    logs log = new logs();
                    log.newError(@"POST-pending" + this.path, "No se ha podido enviar los datos" + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                rest = false;
                logs log = new logs();
                log.newError(@"POST-pending" + this.path, "Ha ocurrido un error al obtener los datos " + ex.Message);
            }


            return rest;
        }

    }
}


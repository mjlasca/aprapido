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
    class ApiStateUser
    {
        protected string baseEndPoint { get; set; }
        protected string apiKey { get; set; }
        protected string path { get; set; }
        protected puntodeventa punt { get; set; }
        protected RegisterPending repen { get; set; }

        public ApiStateUser()
        {
            this.baseEndPoint = MDIParent1.apiuri;
            this.punt = new puntodeventa();
            if (!this.punt.get_principal())
            {
                this.punt.get_colaborador();
            }
            this.apiKey = this.punt.apitoken;
            this.path = "/api";
            this.repen = new RegisterPending();
        }


        public async Task<bool> Get(string email)
        {
            

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/stateuser/" + email, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonContent);
                    string res = json["res"]?.ToString();

                    return res == "1";
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }



            return false;
        }

        public async Task<bool> ValidateToken(string email, string token_)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/stateuser/" + email, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token_);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonContent);
                    return json != null;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public async Task<string> Post(string email, int active)
        {


            var client = new HttpClient();
            client.BaseAddress = new Uri(this.baseEndPoint);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(this.path + "/enabled/" + email + "/" + active , UriKind.Relative),
                Method = HttpMethod.Post,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.apiKey);

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonContent);
                    string res = json["res"]?.ToString();
                    string tok = json["token"]?.ToString();
                    if (res == "success")
                        return tok;
                }

            }
            catch (Exception ex)
            {
                return null;
            }



            return null;
        }


    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace web.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantesController : Controller
    {
        [HttpGet("[action]")]
        public async Task<ResponseModel<IEnumerable<Restaurante>>> Get(string name)
        {
            IEnumerable<Restaurante> restaurantes = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57659");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //TODO: Não implementado
                    CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;

                    HttpResponseMessage response = await client.GetAsync($"api/estabelecimento?name={name}", token);
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    restaurantes = JsonConvert.DeserializeObject<IEnumerable<Restaurante>>(responseAsString);
                }
                return await Task.FromResult(new ResponseModel<IEnumerable<Restaurante>>(true, "", restaurantes));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<IEnumerable<Restaurante>>(false, "Ocorreu um erro interno ao buscar os registros", restaurantes));
            }
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<Restaurante>> Get([FromRoute] long id)
        {
            Restaurante restaurante = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57659");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;
                    HttpResponseMessage response = await client.GetAsync($"api/estabelecimento/{id}", token);
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    restaurante = JsonConvert.DeserializeObject<Restaurante>(responseAsString);
                }
                return await Task.FromResult(new ResponseModel<Restaurante>(true, "", restaurante));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<Restaurante>(false, "Ocorreu um erro interno ao salvar o registro", restaurante));
            }
        }

        [HttpPost]
        public async Task<ResponseModel<Restaurante>> Save([FromBody] Restaurante restaurante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:57659");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(new { restaurante.Id, restaurante.Nome }), Encoding.UTF8, "application/json");
                        HttpResponseMessage response;
                        CancellationTokenSource source = new CancellationTokenSource();
                        CancellationToken token = source.Token;
                        if (restaurante.Id == 0)
                        {
                            response = await client.PostAsync($"api/estabelecimento", content, token);
                        }
                        else
                        {
                            response = await client.PutAsync($"api/estabelecimento/{restaurante.Id}", content, token);
                        }

                        response.EnsureSuccessStatusCode();
                        var responseAsString = await response.Content.ReadAsStringAsync();
                        restaurante = JsonConvert.DeserializeObject<Restaurante>(responseAsString);
                    }
                }
                else
                {
                    return await Task.FromResult(new ResponseModel<Restaurante>(false, "Por favor preencha os dados corretamente", restaurante));
                }
                return await Task.FromResult(new ResponseModel<Restaurante>(true, "Registro salvo com sucesso!", restaurante));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<Restaurante>(false, "Ocorreu um erro interno ao salvar o registro", restaurante));
            }
        }

        [HttpDelete("[action]")]
        public async Task<bool> Delete(long id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57659");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("estabelecimento/json"));
                    CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;
                    HttpResponseMessage response = await client.DeleteAsync($"api/estabelecimento/{id}", token);
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                }
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }


        }
    }
}

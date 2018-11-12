using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace web.Controllers
{
    [Route("api/[controller]")]
    public class PratosController : Controller
    {
        [HttpGet("[action]")]
        public async Task<ResponseModel<IEnumerable<Prato>>> Get(string name)
        {
            IEnumerable<Prato> pratos = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57659");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/prato?name={name}");
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    pratos = JsonConvert.DeserializeObject<IEnumerable<Prato>>(responseAsString);
                }
                return await Task.FromResult(new ResponseModel<IEnumerable<Prato>>(true, "", pratos));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<IEnumerable<Prato>>(false, "Ocorreu um erro interno ao buscar os registros", pratos));
            }
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<Prato>> Get([FromRoute] long id)
        {
            Prato prato = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57659");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/prato/{id}");
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    prato = JsonConvert.DeserializeObject<Prato>(responseAsString);
                }
                return await Task.FromResult(new ResponseModel<Prato>(true, "", prato));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<Prato>(false, "Ocorreu um erro interno ao buscar o registro", prato));
            }
        }

        [HttpPost]
        public async Task<ResponseModel<Prato>> Save([FromBody] Prato prato)
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
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(new { prato.Id, prato.Nome, prato.Valor, prato.EstabelecimentoId }), Encoding.UTF8, "application/json");
                        HttpResponseMessage response;
                        if (prato.Id == 0)
                        {
                            response = await client.PostAsync($"api/prato", content);
                        }
                        else
                        {
                            response = await client.PutAsync($"api/prato/{prato.Id}", content);
                        }

                        response.EnsureSuccessStatusCode();
                        var responseAsString = await response.Content.ReadAsStringAsync();
                        prato = JsonConvert.DeserializeObject<Prato>(responseAsString);
                    }
                }
                else
                {
                    return await Task.FromResult(new ResponseModel<Prato>(false, "Por favor preencha os dados corretamente", prato));
                }
                return await Task.FromResult(new ResponseModel<Prato>(true, "Registro salvo com sucesso!", prato));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel<Prato>(false, "Ocorreu um erro interno ao salvar o registro", prato));
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
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"api/prato/{id}");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Restaurante.Appication.WebApi.Controllers
{
    [Route("api/index")]
    public class HomeController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Index()
        {            
            return await Task.FromResult("RestauranteWeb - Serviços");
        }
    }
}
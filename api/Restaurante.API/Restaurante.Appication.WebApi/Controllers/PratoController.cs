using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurante.Appication.TO;
using Restaurante.Domain.Entity;

namespace Restaurante.Appication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PratoController : Controller
    {
        private readonly IPratoManager _manager;
        private readonly ILogger _logger;

        public PratoController(IPratoManager manager,
        ILoggerFactory loggerFactory)
        {
            _manager = manager;
            _logger = loggerFactory.CreateLogger<PratoController>();
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<PratoDTO>> Get(string name = null)
        {
            var result = await (string.IsNullOrEmpty(name) ? _manager.GetAllAsync(x => x.Estabelecimento) : _manager.GetWithFilterAsync(a => a.Nome.Contains(name), x => x.Estabelecimento));
            var model = result.SetMapping<Prato, PratoDTO>();
            return model;
        }

        // GET api/values
        [HttpGet("{id}")]
        public async Task<PratoDTO> Get([FromRoute] long id)
        {
            var result = await  _manager.GetByIdAsync(id, a => a.Estabelecimento);
            var model = result.SetMapping<Prato, PratoDTO>();
            return model;
        }

        [HttpPost]
        public async Task<PratoDTO> Create([FromBody] Prato prato)
        {
            prato.DataInclusao = DateTime.Now;
            //TODO: autenticação não solicitada no teste
            prato.UsuarioUltimaAlteracao = "thiago.inacio";
            var result = await _manager.CreateAsync(prato);
            return prato.SetMapping<Prato, PratoDTO>();
        }

        [HttpPut("{id}")]
        public async Task<PratoDTO> Edit([FromRoute] int id, [FromBody] Prato prato)
        {
            var model = await _manager.GetByIdAsync(id);
            if (model != null)
            {
                model.Nome = prato.Nome;
                model.Valor = prato.Valor;
                model.EstabelecimentoId = prato.EstabelecimentoId;
                model.DataUltimaAlteracao = DateTime.Now;
                //TODO: autenticação não solicitada no teste
                model.UsuarioUltimaAlteracao = "thiago.inacio";
            }
            var result = await _manager.UpdateAsync(model);
            return prato.SetMapping<Prato, PratoDTO>();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _manager.RemoveAsync(id);
            return Ok(true);
        }
    }
}

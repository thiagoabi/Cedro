using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurante.Appication.TO;
using Restaurante.Domain.Entity;

namespace Restaurante.Appication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EstabelecimentoController : Controller
    {
        private readonly IEstabelecimentoManager _manager;
        private readonly ILogger _logger;

        public EstabelecimentoController(IEstabelecimentoManager manager,
        ILoggerFactory loggerFactory)
        {
            _manager = manager;
            _logger = loggerFactory.CreateLogger<EstabelecimentoController>();
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<EstabelecimentoDTO>> Get(string name = null, CancellationToken token = new CancellationToken())
        {
            var result = await (string.IsNullOrEmpty(name) ? _manager.GetAllAsync(x => x.Pratos) : _manager.GetWithFilterAsync(a => a.Nome.Contains(name), x => x.Pratos));
            var model = result.SetMapping<Estabelecimento, EstabelecimentoDTO>();
            return model;
        }

        // GET api/values
        [HttpGet("{id}")]
        public async Task<EstabelecimentoDTO> Get([FromRoute] long id)
        {
            var result = await  _manager.GetByIdAsync(id, a => a.Pratos);
            var model = result.SetMapping<Estabelecimento, EstabelecimentoDTO>();
            return model;
        }

        [HttpPost]
        public async Task<EstabelecimentoDTO> Create([FromBody] Estabelecimento Estabelecimento)
        {
            Estabelecimento.DataInclusao = DateTime.Now;
            Estabelecimento.UsuarioUltimaAlteracao = "thiago.inacio";
            var result = await _manager.CreateAsync(Estabelecimento);
            return Estabelecimento.SetMapping<Estabelecimento, EstabelecimentoDTO>();
        }

        [HttpPut("{id}")]
        public async Task<EstabelecimentoDTO> Edit([FromRoute] int id, [FromBody] Estabelecimento Estabelecimento)
        {
            var model = await _manager.GetByIdAsync(id);
            if (model != null)
            {
                model.Nome = Estabelecimento.Nome;
                model.DataUltimaAlteracao = DateTime.Now;
                //TODO: autenticação não solicitada no teste
                model.UsuarioUltimaAlteracao = "thiago.inacio";
            }
            var result = await _manager.UpdateAsync(model);
            return Estabelecimento.SetMapping<Estabelecimento, EstabelecimentoDTO>();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _manager.RemoveAsync(id);
            return Ok(true);
        }
    }
}

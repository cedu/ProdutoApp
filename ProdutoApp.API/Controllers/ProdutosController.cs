using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutoApp.API.Models;
using ProdutoApp.Domain.Entities;
using ProdutoApp.Domain.Entities.Enums;
using ProdutoApp.Domain.Exceptions;
using ProdutoApp.Domain.Interfaces.Services;

namespace ProdutoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoDomainService _produtoDomainService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoDomainService produtoDomainService, IMapper mapper)
        {
            _produtoDomainService = produtoDomainService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProdutoGetResponseModel), 201)]
        public IActionResult Post(ProdutoPostRequestModel model)
        {
            try
            {

                var produto = _produtoDomainService.Adicionar(_mapper.Map<Produto>(model));

                var response = _mapper.Map<ProdutoGetResponseModel>(produto);

                return StatusCode(201, response);
            }

            catch (DomainException e)
            {
                return StatusCode(422, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProdutoGetResponseModel), 200)]
        public IActionResult Put(ProdutoPutRequestModel model)
        {
            try
            {
                var produto = _produtoDomainService.Atualizar(_mapper.Map<Produto>(model));


                var response = _mapper.Map<ProdutoGetResponseModel>(produto);

                return StatusCode(200, response);
            }
            catch (DomainException e)
            {
                return StatusCode(422, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProdutoGetResponseModel), 200)]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                var produto = _produtoDomainService.Excluir(id.Value);

                var response = _mapper.Map<ProdutoGetResponseModel>(produto);

                return StatusCode(200, response);
            }
            catch (DomainException e)
            {
                //HTTP 422 (UNPROCESSABLE CONTENT)
                return StatusCode(422, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{dataMin}/{dataMax}")]
        [ProducesResponseType(typeof(ProdutoGetResponseModel), 200)]
        public IActionResult Get(DateTime dataMin, DateTime dataMax)
        {
            try
            {
                var produtos = _produtoDomainService.Consultar(dataMin, dataMax);

                //verificar se nenhum produto foi encontrado
                if (!produtos.Any())
                    return StatusCode(204);

                //objeto para retornar os dados da resposta
                var response = _mapper.Map<List<ProdutoGetResponseModel>>(produtos);
                return StatusCode(200, response);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoGetResponseModel), 200)]
        public IActionResult Get(Guid id)
        {
            try
            {
                //consultar 1 produto pelo id
                var produto = _produtoDomainService.ObterPorId(id);

                if(produto == null)
                    return StatusCode(204);

                //objeto para retornar os dados da resposta
                var response = _mapper.Map<ProdutoGetResponseModel>(produto);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}

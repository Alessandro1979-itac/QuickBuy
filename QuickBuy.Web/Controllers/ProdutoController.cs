using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBuy.Dominio.Contratos;
using QuickBuy.Dominio.Entidades;
using System;

namespace QuickBuy.Web.Controllers
{
    [Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private IHttpContextAccessor _httpContextAccessor;
        //private IWebHostEnvironment __hostingEnvironment;
        public ProdutoController(IProdutoRepositorio produtoRepositorio,
            IHttpContextAccessor httpContextAccessor)
        {
            _produtoRepositorio = produtoRepositorio;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Json(_produtoRepositorio.ObterTodos());
            } catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                produto.Validate();
                if (!produto.EhValido)
                {
                    return BadRequest(produto.ObterMensagensValidacao());
                }
                if (produto.Id > 0)
                {
                    _produtoRepositorio.Atualizar(produto);
                }
                else
                {
                    _produtoRepositorio.Adicionar(produto);
                }

                return Created("api/produto", produto);
            } catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[HttpPost("Deletar")]
        //public IActionResult Deletar([FromBody] Produto produto)
        //{
        //    try
        //    {
        //        // produto recebido do FromBody, deve ter a propriedade Id > 0
        //        _produtoRepositorio.Remover(produto);
        //        return Json(_produtoRepositorio.ObterTodos());
        //    }
        //    catch (Exception ex)
        //    {
        //        BadRequest(ex.ToString());
        //    }
        //}

        //[HttpPost("EnviarArquivo")]
        //public IActionResult EnviarArquivo()
        //{
        //    try
        //    {
        //        var formFile 
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}

using Alura_Challange_Transacao_Financeira.Models;
using Alura_Challange_Transacao_Financeira.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Alura_Challange_Transacao_Financeira.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransacaoRepository _transacaoRepository;
        private readonly ImportacoesRepository _importacoesRepository;

        public HomeController(ILogger<HomeController> logger, TransacaoRepository transacaoRepository, ImportacoesRepository importacoesRepository)
        {
            _logger = logger;
            _transacaoRepository = transacaoRepository;
            _importacoesRepository = importacoesRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            return View(await _importacoesRepository.GetAllRegistros());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("getFile")]
        public async Task<IActionResult> GetFile()
        {
            try
            {
                string conteudo = await GetConteudoArquivo(HttpContext);
                string[][] linhas = conteudo.ToString().Split('\n').Select(l => l.Split(',')).ToArray();
                List<Transacao> transacoes = Transacao.CreateList(linhas).ToList();
                if (_transacaoRepository.CollectionExist(transacoes.First().DataHora.Date.ToString()))
                    throw new BussinessException("As transações deste dia já foram adicionadas.");
                _transacaoRepository.CreateCollection(transacoes);
                await _importacoesRepository.AddRegistro(new Importacao { DataImportacao = DateTime.Now.ToString(), DataTransacoes = transacoes.First().DataHora.Date.ToShortDateString() });
                return Ok(new BaseRetorno { Mensagem = "Transações importadas com sucesso" });
            } catch (BussinessException ex)
            {
                return BadRequest(new BaseRetorno { Mensagem = ex.Message, Status = 1 });
            }
        }

        public async Task<string> GetConteudoArquivo(HttpContext httpContext)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            byte[] request = new byte[(int)(HttpContext.Request.ContentLength ?? 0)];
            await httpContext.Request.Body.ReadAsync(request, 0, request.Length);
            string bodyComplete = Encoding.UTF8.GetString(request);
            if (bodyComplete.Length < 21)
                throw new BussinessException("O arquivo deve ter algum conteudo dentro");
            string base64 = uTF8Encoding.GetString(request).Remove(0, 21);

            byte[] arquivoBinario = Convert.FromBase64String(base64);
            StringBuilder conteudo = new StringBuilder();
            foreach (byte b in arquivoBinario)
            {
                conteudo.Append((char)b);
            }
            return conteudo.ToString();
        }
    }
}
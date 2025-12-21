using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.FormLab.Models;

namespace MVC.FormLab.Controllers
{
    [Route("meus-alunos")]
    public class AlunoController : Controller
    {
        private AppDbContext DbContext;

        public AlunoController(AppDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Sucesso = "Olá!";

            var alunos = await DbContext.Alunos.ToListAsync();
            return View(alunos);
        }

        [Route("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var aluno = await ObterAluno(id);

            return View(aluno);
        }

        [HttpGet("novo")]
        public async Task<IActionResult> Criar()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Nome,DataNascimento,Email,EmailConfirmacao,Ativo")]Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return View(aluno);
            }

            DbContext.Alunos.Add(aluno);
            await DbContext.SaveChangesAsync();

            return View();
        }

        public async Task<Aluno?> ObterAluno(int id)
        {
            return await DbContext.Alunos.FirstOrDefaultAsync(a => a.Id == id);
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await ObterAluno(id);            

            return View(aluno);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Nome,DataNascimento,Email,Ativo")] Aluno aluno)
        {
            if (id != aluno.Id) return NotFound();

            // Não queremos validar esse campo.
            ModelState.Remove("EmailConfirmacao");

            if (!ModelState.IsValid)
            {
                return View(aluno);
            }

            DbContext.Alunos.Update(aluno);
            await DbContext.SaveChangesAsync();

            // TempData sobrevive a Redirect e pode ser usado depois.
            // ViewData ou ViewBag não iria funcionar nesse cenário.
            TempData["Sucesso"] = $"Estudante {aluno.Nome} atualizado com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var aluno = await ObterAluno(id);

            return View(aluno);
        }

        [HttpPost("excluir/{id:int}")]
        [ActionName("excluir")]
        public async Task<IActionResult> ExcluirConfirmar(int id)
        {
            var aluno = await ObterAluno(id);

            DbContext.Alunos.Remove(aluno);
            await DbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

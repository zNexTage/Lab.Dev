using Lab.MVC.AppSemTemplate.Data;
using Lab.MVC.AppSemTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.MVC.AppSemTemplate.Controllers
{
    [Route("meus-produtos")]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            ViewData["Horario"] = DateTime.Now;

            return View(await _context.Produto.ToListAsync());
        }

        [HttpGet("detalhes/{id:int}")]
        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpGet("criar")]
        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("criar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Upload,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                (var resultado, var nomeArquivo) = await UploadArquivo(produto.Upload, Guid.NewGuid().ToString());

                if(!resultado)
                {
                    ModelState.AddModelError("Upload", "Falha em salvar arquivo.");

                    return View(produto);
                }

                produto.Image = nomeArquivo;

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet("editar/{id:int}")]
        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Upload,Valor")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            var prod = await _context
                .Produto
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (ModelState.IsValid)
            {
                produto.Image = prod.Image;

                if (produto.Upload != null)
                {
                    (var resultado, var nomeArquivo) = await UploadArquivo(produto.Upload, Guid.NewGuid().ToString());
                    
                    if(!resultado)
                    {
                        ModelState.AddModelError("Upload", "Falha em salvar arquivo.");

                        return View(produto);
                    }
                    
                    produto.Image = nomeArquivo;
                }


                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet("excluir/{id:int}")]
        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost("excluir/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Tuple<bool, string>> UploadArquivo(IFormFile arquivo, string prefixo)
        {
            if (arquivo == null || arquivo.Length <= 0) return Tuple.Create(false, string.Empty);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{prefixo}{arquivo.FileName}");

            using var stream = new FileStream(path, FileMode.Create);
            await arquivo.CopyToAsync(stream);

            return Tuple.Create(true, path);
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
    }
}

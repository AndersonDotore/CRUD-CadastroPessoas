using CadastroPessoa.Data;
using CadastroPessoa.Models;
using CadastroPessoa.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoa.Controllers
{
    public class PessoasController : Controller
    {
        private readonly CadastroPessoaDbContext cadastroPessoaDbContext;

        public PessoasController(CadastroPessoaDbContext cadastroPessoaDbContext)
        {
            this.cadastroPessoaDbContext = cadastroPessoaDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var pessoas = await cadastroPessoaDbContext.Pessoas.ToListAsync();
            return View(pessoas);
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPessoaViewModel addPessoaRequest)
        {
            var pessoa = new Pessoa()
            {
                Id = Guid.NewGuid(),
                Nome = addPessoaRequest.Nome,
                Sobrenome = addPessoaRequest.Sobrenome,
                DataNascimento = addPessoaRequest.DataNascimento,
                EstadoCivil = addPessoaRequest.EstadoCivil,
                RG = addPessoaRequest.RG,
                CPF = addPessoaRequest.CPF
            };

            await cadastroPessoaDbContext.Pessoas.AddAsync(pessoa);
            await cadastroPessoaDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var pessoa = await cadastroPessoaDbContext.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa != null)
            {

                var viewModel = new UpdatePessoaViewModel()
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    DataNascimento = pessoa.DataNascimento,
                    EstadoCivil = pessoa.EstadoCivil,
                    RG = pessoa.RG,
                    CPF = pessoa.CPF

                };
                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePessoaViewModel model)
        {
            var pessoa = await cadastroPessoaDbContext.Pessoas.FindAsync(model.Id);

            if (pessoa != null)
            {
                pessoa.Nome = model.Nome;
                pessoa.Sobrenome = model.Sobrenome;
                pessoa.DataNascimento = model.DataNascimento;
                pessoa.EstadoCivil = model.EstadoCivil;
                pessoa.RG = model.RG;
                pessoa.CPF = model.CPF;

                await cadastroPessoaDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]

        public async Task<IActionResult> Delete(UpdatePessoaViewModel model)
        {
            var pessoa = await cadastroPessoaDbContext.Pessoas.FindAsync(model.Id);

            if (pessoa != null)
            {
                cadastroPessoaDbContext.Pessoas.Remove(pessoa);
                await cadastroPessoaDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}


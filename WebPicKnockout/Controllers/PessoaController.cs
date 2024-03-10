using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebPicKnockout.BLL;
using WebPicKnockout.Models;

namespace WebPicKnockout.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaBLL pessoaBLL = new PessoaBLL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            List<Pessoa> pessoas = pessoaBLL.ListarPessoas();
            return Json(pessoas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pessoa pessoa)
        {
            if (!pessoaBLL.ValidaCPF(pessoa.CPF)) //Verifica o cpf conforme calculo dos numeros
            {
                return Json(new { success = false, errors = "Este não é um CPF valido!" });
            }

            if (pessoaBLL.VerificarExistenciaCPF(pessoa.CPF)) //cpf já cadastrado no banco de dados
            {
                return Json(new { success = false, errors = "Este CPF ja esta em uso!" });
            }

            if (!pessoaBLL.ValidaRG(pessoa.RG)) //Verifica o rg conforme calculo dos numeros
            {
                return Json(new { success = false, errors = "Este não é um RG valido!" });
            }

            if (pessoaBLL.VerificarExistenciaRG(pessoa.RG)) //rg já cadastrado no banco de dados
            {
                return Json(new { success = false, errors = "Este RG ja esta em uso!" });
            }

            if (ModelState.IsValid)
            {
                pessoaBLL.AdicionarPessoa(pessoa);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Pessoa model = null;

            Pessoa pessoas = pessoaBLL.DetalhesPessoa(id);

            if (pessoas != null)
            {
                model = new Pessoa()
                {
                    Codigo = pessoas.Codigo,
                    Nome = pessoas.Nome,
                    Sobrenome = pessoas.Sobrenome,
                    EstadoCivil = pessoas.EstadoCivil,
                    DataNasc = pessoas.DataNasc,
                    CPF = pessoas.CPF,
                    RG = pessoas.RG
                };
            }

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(Pessoa pessoa)
        {
            Pessoa pessoasBD = pessoaBLL.DetalhesPessoa(pessoa.Codigo);

            if (!pessoaBLL.ValidaCPF(pessoa.CPF)) //Verifica o cpf conforme calculo dos numeros
            {
                return Json(new { success = false, errors = "Este não é um CPF valido!" });
            }

            if (pessoasBD.CPF != pessoa.CPF)
            {
                if (pessoaBLL.VerificarExistenciaCPF(pessoa.CPF))
                {
                    return Json(new { success = false, errors = "Este CPF ja esta em uso!" });
                }
            }

            if (!pessoaBLL.ValidaRG(pessoa.RG)) //Verifica o rg conforme calculo dos numeros
            {
                return Json(new { success = false, errors = "Este não é um RG valido!" });
            }

            if (pessoasBD.RG != pessoa.RG)
            {
                if (pessoaBLL.VerificarExistenciaRG(pessoa.RG))
                {
                    return Json(new { success = false, errors = "Este RG ja esta em uso!" });
                }
            }

            if (ModelState.IsValid)
            {
                pessoaBLL.EditarPessoa(pessoa);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        [HttpGet]
        public ActionResult DetailsDelete(int id)
        {
            Pessoa model = null;

            Pessoa pessoas = pessoaBLL.DetalhesPessoa(id);

            if (pessoas != null)
            {
                model = new Pessoa()
                {
                    Codigo = pessoas.Codigo,
                    Nome = pessoas.Nome,
                    Sobrenome = pessoas.Sobrenome,
                    EstadoCivil = pessoas.EstadoCivil,
                    DataNasc = pessoas.DataNasc,
                    CPF = pessoas.CPF,
                    RG = pessoas.RG
                };
            }

            return View("Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(int codigo)
        {
            pessoaBLL.ExcluirPessoa(codigo);
            return Json(new { success = true });
        }
    }
}

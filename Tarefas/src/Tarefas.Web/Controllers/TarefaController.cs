using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;

namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
    {
        private readonly TarefaDAO _tarefaDAO;
        public List<TarefaViewModel> listaDeTarefas { get; set; }

        public TarefaController(TarefaDAO tarefaDAO)
        {
            _tarefaDAO = tarefaDAO;
        }
        
        public IActionResult Details(int id)
        {
            var tarefaDTO = _tarefaDAO.Consultar(id);

            var tarefa=new TarefaViewModel()
            {
                Id=tarefaDTO.Id,
                Titulo=tarefaDTO.Titulo,
                Descricao=tarefaDTO.Descricao,
                Concluida=tarefaDTO.Concluida
            };

            return View(tarefa);
        }

        public IActionResult Index()
        {   
            var listaDeTarefasDTO = _tarefaDAO.Consultar();

            var listaDeTarefas = new List<TarefaViewModel>();
            
            foreach (var tarefaDTO in listaDeTarefasDTO)
            {
                listaDeTarefas.Add(new TarefaViewModel()
                {
                Id=tarefaDTO.Id,
                Titulo=tarefaDTO.Titulo,
                Descricao=tarefaDTO.Descricao,
                Concluida=tarefaDTO.Concluida
                });
            }


            return View(listaDeTarefas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TarefaViewModel tarefa)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }

            var tarefaDTO = new TarefaDTO 
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };

            _tarefaDAO.Criar(tarefaDTO);

            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {
            var tarefaDTO = _tarefaDAO.Consultar(id);

            var tarefa = new TarefaViewModel()
            {
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida = tarefaDTO.Concluida
            };
                
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Update(TarefaViewModel tarefa)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }

            var tarefaDTO = new TarefaDTO 
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };

            _tarefaDAO.Atualizar(tarefaDTO);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _tarefaDAO.Excluir(id);

            return RedirectToAction("Index");
        }
    }
}
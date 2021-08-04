using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TodoApp.API.Models;
using TodoApp.API.Repositories;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoListRepository _todoListRepository;
        public TodosController(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _todoListRepository.GetAll();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _todoListRepository.GetById(id);
            if (result==null)
            {
                return null;
            }
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddTodoList(TodoList todoList)
        {
            await _todoListRepository.Add(todoList);           
            return Ok();
        }
        [HttpPost("edit")]
        public IActionResult EditTodoList(TodoList todoList)
        {
            _todoListRepository.Update(todoList);
            return Ok(todoList);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTodoList(Guid id)
        {
            _todoListRepository.Delete(id);
            return Ok(id);
        }
    }
}

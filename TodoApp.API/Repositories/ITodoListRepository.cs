using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.API.Models;

namespace TodoApp.API.Repositories
{
    public interface ITodoListRepository
    {
        List<TodoList> GetAll();
        TodoList GetById(Guid id);
        Task Add(TodoList todoList);
        void Update(TodoList todoList);
        void Delete(Guid id);
    }
}

using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TodoApp.API.Controllers;
using TodoApp.API.Models;
using TodoApp.API.Repositories;

namespace TodoApp.UnitTests.Controllers
{
    public class TodosControllerTests
    {
        private readonly TodosController _todosController;
        private readonly Mock<ITodoListRepository> _todoListRepositoryMock = new Mock<ITodoListRepository>();
        public TodosControllerTests()
        {
             _todosController = new TodosController(_todoListRepositoryMock.Object);
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_TodoList()
        {
            var todoList = _todosController.GetAll();
        }
        //[Test]
        //public void GetById_TodoList()
        //{
        //    var todoListId = Guid.NewGuid();
        //    var todoListContent = "Uygulama Geliþtirme";
        //    var todoListDto = new TodoList
        //    {
        //        Id = todoListId,
        //        Content = todoListContent
        //    };
        //    _todoListRepositoryMock.Setup(x => x.GetById(todoListId)).Returns(todoListDto);
        //    var todoList = _todosController.GetById(todoListId);
        //    Assert.AreEqual(expected: todoListId, actual: todoList.Id);
        //    Assert.AreEqual(expected: todoListContent, actual: todoList.Content);
        //}
        [Test]
        public void GetById_ShouldReturnAnything_WhenTodoListDoesNotExist()
        {
            _todoListRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(valueFunction: () => null);
            var todoList = _todosController.GetById(Guid.NewGuid());
            Assert.Null(todoList);
        }
    }
}
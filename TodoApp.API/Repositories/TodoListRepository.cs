
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.API.Models;

namespace TodoApp.API.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly IBucket _bucket;
        public TodoListRepository(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("TodoListBucket");
        }
        public async Task Add(TodoList todoList)
        {
            await _bucket.InsertAsync(Guid.NewGuid().ToString(), todoList);
        }

        public void Delete(Guid id)
        {
            _bucket.Remove(id.ToString());
        }

        public TodoList GetById(Guid id)
        {
            var result = _bucket.Get<TodoList>(id.ToString());
            return result.Value;
        }

        public List<TodoList> GetAll()
        {
            var n1ql = @"SELECT t.*,META(t).id
                        FROM TodoListBucket t";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = _bucket.Query<TodoList>(query);
            return result.Rows;
        }
        public void Update(TodoList todoList)
        {
            if (string.IsNullOrEmpty(todoList.Id))
                todoList.Id = Guid.NewGuid().ToString();
            _bucket.Upsert(todoList.Id.ToString(), new
            {
                Content = todoList.Content,
                IsCompleted = todoList.IsCompleted,
                CreatedOn = todoList.CreatedOn
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.API.Models
{
    public class TodoList
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

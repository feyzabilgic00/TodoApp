using System.Threading.Tasks;
using TodoApp.API.Models;

namespace TodoApp.API.Repositories
{
    public interface IUserRepository
    {
        Task<Couchbase.IOperationResult<User>> SignUp(User user);
        User Login(string email, string password);
    }
}

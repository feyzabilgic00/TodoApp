using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using System;
using System.Linq; 
using System.Threading.Tasks;
using TodoApp.API.Models;

namespace TodoApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBucket _bucket;
        public UserRepository(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("UserBucket");
        }
        public User Login(string email, string password)
        {
            var n1ql = @"SELECT u.*
                            FROM UserBucket u";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            return _bucket.Query<User>(query).SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public async Task<Couchbase.IOperationResult<User>> SignUp(User user)
        {
            return await _bucket.InsertAsync(Guid.NewGuid().ToString(), user);
        }
    }
}

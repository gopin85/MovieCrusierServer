using AuthServer.Data.Models;
using System.Linq;

namespace AuthServer.Data.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDbContext _dbContext;

        /*Constructor*/
        public UserRepository(IUserDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        /*To check user exists in DB*/
        public bool IsUserExists(string UserId)
        {
            var isUserExists = _dbContext.Users.Any(e => e.UserId == UserId);
            return isUserExists;
        }

        /*To login user*/
        public User Login(string UserId, string Password)
        {
            var user = _dbContext.Users.Where(e => e.UserId == UserId && e.Password == Password).First();
            return user;
        }

        /*To register user*/
        public User RegisterUser(User UserDetails)
        {
            _dbContext.Users.Add(UserDetails);
            _dbContext.SaveChanges();
            return UserDetails;
        }
    }
}
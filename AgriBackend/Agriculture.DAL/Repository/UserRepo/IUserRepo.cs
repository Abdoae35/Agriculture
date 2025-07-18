

namespace Agriculture.DAL.Repository;

public interface IUserRepo
{
    public IQueryable<User> GetAllUsers();
    public User GetUserById(int id);
    public void InsertUser(User user);
    public void Update(User user);
    public void DeleteUser(User user);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User user);
}
using TodoApi.Data;
using TodoApi.Data.UnitOfWork;
using TodoApi.Repositories.ToDoRepository;
using TodoApi.Repositories.UserRepository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoContext _context;
    private IUserRepository? _userRepository;
    private IToDoRepository? _toDoRepository;

    public UnitOfWork(ToDoContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _userRepository ??= new UserRepository(_context);
    public IToDoRepository ToDos => _toDoRepository ??= new ToDoRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

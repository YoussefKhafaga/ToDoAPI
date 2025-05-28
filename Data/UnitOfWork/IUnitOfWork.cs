using System;
using TodoApi.Repositories.ToDoRepository;
using TodoApi.Repositories.UserRepository;

namespace TodoApi.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> CompleteAsync();
    IUserRepository Users { get; }
    IToDoRepository ToDos { get; }

}

using Common.Interfaces.Repositories;
using Common.Interfaces.Services;

namespace WebApi.Services
{
    public class ToDoITemService : IToDoITemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        public ToDoITemService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
    }
}

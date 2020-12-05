using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using Common.Models;
using System.Collections.Generic;

namespace WebApi.Services
{
    public class ToDoITemService : IToDoITemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        public ToDoITemService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }
        public void AddItem(ToDoItem item)
        {
            _toDoItemRepository.AddItem(item);
        }

        public void AddMultipleItems(IEnumerable<ToDoItem> items)
        {
            _toDoItemRepository.AddMultipleItems(items);
        }

        public void DeleteItem(int id)
        {
            var item = new ToDoItem { Id = id };
            _toDoItemRepository.DeleteItem(item);
        }

        public IEnumerable<ToDoItem> GetAllUserItems(int id)
        {
            return _toDoItemRepository.GetAllUserItems(id);
        }

        public ToDoItem GetItemById(int id)
        {
            return _toDoItemRepository.GetItemById(id);
        }

        public void UpdateItem(ToDoItem item)
        {
            _toDoItemRepository.UpdateItem(item);
        }
    }
}

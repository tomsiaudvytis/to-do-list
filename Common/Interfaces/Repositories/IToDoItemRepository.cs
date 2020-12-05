using Common.Models;
using System.Collections.Generic;

namespace Common.Interfaces.Repositories
{
    public interface IToDoItemRepository
    {
        ToDoItem GetItemById(int id);
        IEnumerable<ToDoItem> GetAllUserItems(User user);
        void AddItem(ToDoItem item);
        void AddMultipleItems(IEnumerable<ToDoItem> items);
        void DeleteItem(ToDoItem item);
        void UpdateItem(ToDoItem item);
    }
}

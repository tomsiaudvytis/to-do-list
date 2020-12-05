
using Common.Models;
using System.Collections.Generic;

namespace Common.Interfaces.Services
{
    public interface IToDoITemService
    {
        ToDoItem GetItemById(int id);
        IEnumerable<ToDoItem> GetAllUserItems(int id);
        void AddItem(ToDoItem item);
        void AddMultipleItems(IEnumerable<ToDoItem> items);
        void DeleteItem(int id);
        void UpdateItem(ToDoItem item);
    }
}

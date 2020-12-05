using System.Collections.Generic;
using System.Linq;
using Common.Interfaces.Repositories;
using Common.Models;

namespace DataAccessLayer.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ToDoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
        }

        public void AddMultipleItems(IEnumerable<ToDoItem> items)
        {
            _context.ToDoItems.AddRange(items);
            _context.SaveChanges();
        }

        public void DeleteItem(ToDoItem item)
        {
            _context.ToDoItems.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<ToDoItem> GetAllUserItems(int id)
        {
            return _context.ToDoItems.Where(items => items.AssignedToId == id);
        }

        public ToDoItem GetItemById(int id)
        {
            return _context.ToDoItems.Where(item => item.Id == id).FirstOrDefault();
        }

        public void UpdateItem(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void AddMultipleItems(IEnumerable<ToDoItem> items)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(ToDoItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoItem> GetAllUserItems(User user)
        {
            throw new NotImplementedException();
        }

        public ToDoItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(ToDoItem item)
        {
            throw new NotImplementedException();
        }
    }
}

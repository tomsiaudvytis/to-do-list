using Common;
using Common.Models;

namespace DataAccessLayer
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var users = new User[]
            {
            new User { FirstName = "Test", LastName = "User", Password = "1234567891012", Email = "user@email.com", Role = UserRoles.User },
            new User { FirstName = "Test2", LastName = "Admin1", Password = "1234567891012", Email = "admin1@email.com", Role = UserRoles.Admin },
            new User { FirstName = "Test3", LastName = "Admin2", Password = "1234567891012", Email = "admin1@email.com", Role = UserRoles.Admin }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            var items = new ToDoItem[]
            {
                new ToDoItem {AssignedToId = 1, IsComplete = false, Name = "Task 1"},
                new ToDoItem {AssignedToId = 2, IsComplete = true, Name = "Task 2"},
                new ToDoItem {AssignedToId = 1, IsComplete = false, Name = "Task 3"},
                new ToDoItem {AssignedToId = 2, IsComplete = false, Name = "Task 4"},
                new ToDoItem {AssignedToId = 1, IsComplete = true, Name = "Task 5"},
                new ToDoItem {AssignedToId = 2, IsComplete = false, Name = "Task 6"},
                new ToDoItem {AssignedToId = 1, IsComplete = false, Name = "Task 7"},
                new ToDoItem {AssignedToId = 3, IsComplete = false, Name = "Task 8"},
                new ToDoItem {AssignedToId = 3, IsComplete = false, Name = "Task 9"},
                new ToDoItem {AssignedToId = 3, IsComplete = false, Name = "Task 10"},
            };
            foreach (var item in items)
            {
                context.ToDoItems.Add(item);
            }
            context.SaveChanges();
        }
    }
}

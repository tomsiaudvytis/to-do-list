using Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoITemService _toDoITemService;

        public ToDoItemController(IToDoITemService toDoITemService)
        {
            _toDoITemService = toDoITemService;
        }
    }
}

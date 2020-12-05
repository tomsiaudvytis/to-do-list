using Common.Interfaces.Services;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoITemService _toDoITemService;

        public ToDoItemController(IToDoITemService toDoITemService)
        {
            _toDoITemService = toDoITemService;
        }

        [HttpPost]
        public IActionResult AddToDoItem([FromBody] ToDoItem item)
        {
            _toDoITemService.AddItem(item);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddMultipleToDoItem([FromBody] IEnumerable<ToDoItem> items)
        {
            _toDoITemService.AddMultipleItems(items);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _toDoITemService.DeleteItem(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetByUserId(int id)
        {
            return Ok(_toDoITemService.GetAllUserItems(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] ToDoItem item)
        {
            _toDoITemService.UpdateItem(item);
            return Ok();
        }

    }
}

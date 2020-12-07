using Common;
using Common.Interfaces.Services;
using Common.Models;
using Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoITemService _toDoITemService;
        private readonly ILogger _logger;

        public ToDoItemController(IToDoITemService toDoITemService, ILogger logger)
        {
            _toDoITemService = toDoITemService;
            _logger = logger;
        }

        [HttpPost("Add")]
        [Authorize(Policy = UserRoles.User)]
        public IActionResult AddToDoItem([FromBody] ToDoItem item)
        {
            try
            {
                _toDoITemService.AddItem(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("AddMany")]
        [Authorize(Policy = UserRoles.User)]
        public IActionResult AddMultipleToDoItem([FromBody] IEnumerable<ToDoItem> items)
        {
            try
            {
                _toDoITemService.AddMultipleItems(items);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [AuthorizedToDoAction]
        public IActionResult Delete(int id)
        {
            try
            {
                _toDoITemService.DeleteItem(id);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [AuthorizedToDoAction]
        public IActionResult GetByUserId(int id)
        {
            try
            {
                return Ok(_toDoITemService.GetAllUserItems(id));
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AuthorizedToDoAction]
        public IActionResult Update([FromBody] ToDoItem item)
        {
            try
            {
                _toDoITemService.UpdateItem(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}

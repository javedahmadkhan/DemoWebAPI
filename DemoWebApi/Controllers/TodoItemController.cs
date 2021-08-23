using Demo.BusinessLogic.Contract;
using Demo.Models;
using Demo.WebAPI.Attributes;
using Demo.WebAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    //[Authorize(Policy = "p-web-api-with-roles-user")]
    // [Authorize(Policy = "ValidateAccessTokenPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemManagementService service;
        private readonly ILogger _logger;

        public TodoItemController(ITodoItemManagementService service, ILogger<TodoItemController> logger)
        {
            this.service = service;
            _logger = logger;
        }

        [Route("TodoItems")]
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            try
            {
                _logger.LogInformation("Log message in Get to do items method");
                return Ok(await service.GetTodoItemList());
            }
            catch (BrokenCircuitException ex)
            {
                _logger.LogError($"Broken Circuit Exception {ex.Message} Exception occured in Get to do items method");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Exception occured in Get to do items method");
                throw new PresentationException(ex.Message, ex.InnerException);
            }
        }

        [Route("TodoItem/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodoItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var result = await service.GetTodoItem(id);
                _logger.LogInformation("Log message in Get to do item based on id method");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Exception occured in Get to do item based on id method");
                throw new PresentationException(ex.Message, ex.InnerException);
            }
        }

        [Route("TodoItem/CreateOrUpdate")]
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateTodoItem([FromBody] TodoItemCreateOrUpdateDTO todoItemDto)
        {
            try
            {
                if (todoItemDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await service.CreateOrUpdateTodoItem(todoItemDto);
                if (result > 0)
                {
                    _logger.LogInformation("Log message in create / update to do items method");
                    return Ok(new { id = result });
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Exception occured in create / update to do items method");
                throw new PresentationException(ex.Message, ex.InnerException);
            }
        }


        [Route("TodoItem/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {

            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var result = await service.DeleteTodoItem(id);
                if (result > 0)
                {
                    _logger.LogInformation("Log message in delete to do items method");
                    return Ok(new { id = result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Exception occured in delete to do items method");
                throw new PresentationException(ex.Message, ex.InnerException);
            }
        }
    }
}

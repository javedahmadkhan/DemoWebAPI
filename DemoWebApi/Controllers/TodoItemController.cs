using Demo.BusinessLogic.Contract;
using Demo.Models;
using Demo.WebAPI.Attributes;
using Demo.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemManagementService service;

        public TodoItemController(ITodoItemManagementService service)
        {
            this.service = service;
        }

        [Route("TodoItems")]
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            try
            {
                return Ok(await service.GetTodoItemList());
            }
            catch (BrokenCircuitException)
            {
                throw;
            }
            catch (Exception ex)
            {
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

                return Ok(result);
            }
            catch (Exception ex)
            {
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
                    return Ok(new { id = result });
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
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
                    return Ok(new { id = result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new PresentationException(ex.Message, ex.InnerException);
            }
        }
    }
}

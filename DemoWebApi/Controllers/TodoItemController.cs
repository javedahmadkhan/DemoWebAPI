using Demo.BusinessLogic.Contract;
using Demo.Models;
using Demo.Services.HTTPClientFactory.Contract;
using Demo.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    //[Authorize(Policy = "p-web-api-with-roles-user")]
    // [Authorize(Policy = "ValidateAccessTokenPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemManagementService service;
        private readonly ILogger<TodoItemController> _logger;
        private readonly IHttpClientService _httpClientService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        /// <param name="httpClientService"></param>
        public TodoItemController(ITodoItemManagementService service, ILogger<TodoItemController> logger, IHttpClientService httpClientService)
        {
            this.service = service;
            _logger = logger;
            _httpClientService = httpClientService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="todoItemDto"></param>
        /// <returns></returns>
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Sample")]
        [HttpGet]
        public async Task<IActionResult> GetSample()
        {
            try
            {
                _logger.LogInformation("Log message in sample method");
                var response = await _httpClientService.GetListWithHttpRequestMessageAsync("https://api.twilio.com/2010-04-01/");
                return Ok(response);
            }
            catch (BrokenCircuitException ex)
            {
                _logger.LogError($"Broken Circuit Exception {ex.Message} Exception occured in Get to do items method");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Exception occured");
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}

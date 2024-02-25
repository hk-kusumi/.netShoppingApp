using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.EfContext;
using ShoppingApplication.EfCore;
using ShoppingApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingApiController : ControllerBase
    {
        private readonly DbHelper _db;
        public ShoppingApiController(EF_DataContext eF_DataContext)
        {
               _db= new DbHelper(eF_DataContext);
        }


        // GET: api/<ShoppingApiController>
        [HttpGet]
        [Route("api/[controller]/Products")]
        public IActionResult Get()
        {
            ResponseType type=ResponseType.Success;
            try
            {
                IEnumerable<ProductModel> data = _db.GetProducts();
                if(!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
            
        }

        // GET api/<ShoppingApiController>/5
        [HttpGet]
        [Route("api/[controller]/Products/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ProductModel data = _db.getProductById(id);
                if (data ==null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }

        }

        // POST api/<ShoppingApiController>
        [HttpPost]
        [Route("api/[controller]/Order")]
        public IActionResult Post([FromBody] OrderModel model)
        {
            try
            {
                _db.SaveOrder(model);
                ResponseType type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<ShoppingApiController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateOrder")]
        public IActionResult Put( [FromBody] OrderModel model)
        {
            try
            {
                _db.SaveOrder(model);
                ResponseType type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<ShoppingApiController>/5
        [HttpDelete]
        [Route("api/[controller]/Order/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _db.DeleteOrder(id);
                ResponseType type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, "delete success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}

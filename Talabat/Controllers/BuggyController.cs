//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Talabat.APIs.Errors;
//using Talabat.Repositery.Data;

//namespace Talabat.Controllers
//{

//    public class BuggyController : BaseApiController
//    {
//        public readonly ApplicationDbContext context;
//        public BuggyController(ApplicationDbContext context)
//        {
//            this.context = context;
//        }
//        [HttpGet("notfound")]
//        public ActionResult GetNotFoundResult()
//        {
//            var product = context.products.Find(100);
//            if (product == null)
//                return NotFound(new ApiResponse(404));
//            return Ok(product);
//        }
//        [HttpGet("servererror")]
//        public ActionResult GetServerError()
//        {
//            var product = context.products.Find(100);
//            var productReturn = product.ToString();
//            return Ok(productReturn);
//        }
//        [HttpGet("badrequest")]
//        public ActionResult GetErrorBadRequest()
//        {
//            return BadRequest();
//        }
//        [HttpGet("badrequest/{id}")]
//        public ActionResult GetErrorBadRequest(int id)
//        {
//            return Ok();
//        }
//    }
//}

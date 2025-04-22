using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            //if (result.Status == HttpStatusCode.NoContent) // reponse un body sinde bişey göndermicez
            //{
            //    //return NoContent();
            //    return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
            //}

            //if(result.Status == HttpStatusCode.Created)
            //{
            //    return Created(urlAsCreated,result.data);
            //}

            //return  new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };

            return result.Status switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created => Created(result.UrlAsCreated, result),
                _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
            }; //.net 9 ile gelen switch özelliği

        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.Status == HttpStatusCode.NoContent) // reponse un body sinde bişey göndermicez
            {
                return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
            }

            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }

    }
}

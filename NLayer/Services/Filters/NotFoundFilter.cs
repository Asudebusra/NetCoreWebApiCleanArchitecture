using App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Services.Filters
{
    public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> genericRepository) : Attribute,IAsyncActionFilter where T : class where TId : struct
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //action method çalışmadan önce
            //var idValue = context.ActionArguments.Values.FirstOrDefault();


            var idValue = context.ActionArguments.TryGetValue("id",out var idAsObject ) ? idAsObject : null;

            if (idAsObject is not TId id)
            {
                await next();
                return;
            }

            //var idKey = context.ActionArguments.Keys.First();

            //if (idValue == null && idKey != "id")
            //{
            //    await next();//bir sonraki isteği çalıştır. Bir sonraki endpointi çalıştır.FAST FAİL yapıyorum
            //    return;
            //}

            //if (!int.TryParse(idValue.ToString(),out int id))
            //{
            //    await next();//bir sonraki isteği çalıştır. Bir sonraki endpointi çalıştır.FAST FAİL yapıyorum
            //    return;
            //}

            //if (idAsObject is not TId id)
            //{
            //    await next();//bir sonraki isteği çalıştır. Bir sonraki endpointi çalıştır.FAST FAİL yapıyorum
            //    return;
            //}


            var anyEntity = await genericRepository.AnyAsync(id);

            if (anyEntity)
            {
                await next();
                return;
            }

            var entityName = typeof(T).Name;
            //action method name
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var result = ServiceResult.Fail($"data bulunamamıştır.({entityName})({actionName})");

            context.Result = new NotFoundObjectResult(result);
            return;



            //await next();
            //action method çalıştıktan sonra
        }
    }
}

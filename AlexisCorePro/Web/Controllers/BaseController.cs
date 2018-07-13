using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlexisCorePro.Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseContext ctx;

        public BaseController(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public Response<T> OkResponse<T>(T data)
        {
            return new Response<T>(data);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using WebApi.Models;
using System.Web.Http.Controllers;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public void Post()
        {
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent("hello", Encoding.Unicode);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };
            return response;
        }

        public HttpResponseMessage Get2()
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            var controller = dependencyResolver.GetService(typeof(ProductsController));
            ProductsController productsController = controller as ProductsController;
            //productsController.ControllerContext //might need set value here

            // Get a list of products from a database.
            IEnumerable<Product> products = productsController.GetAllProducts();

            // Write the list to the response body.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, products);
            return response;
        }
    }
}
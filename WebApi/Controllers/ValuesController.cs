using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        private ProductsController productsController;

        public ValuesController()
        {

            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            var controller = dependencyResolver.GetService(typeof(ProductsController));
            productsController = controller as ProductsController;
            //productsController.ControllerContext //might need set value here
        }

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
            // Get a list of products from a database.
            IEnumerable<Product> products = productsController.GetAllProducts();

            // Write the list to the response body.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, products);
            return response;
        }

        public IHttpActionResult Get3()
        {
            return new TextResult("hello", Request);
        }

        public IHttpActionResult Get4(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Returns a NotFoundResult
            }
            return Ok(product);  // Returns an OkNegotiatedContentResult
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.IRepositories;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly ICustomerRepository repo = null;
        public ValuesController(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = repo.GetAll();
            if(customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = repo.GetCustomerById(id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] string value)
        {
            var id = repo.InsertCustomer(value);
            return Ok(id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] string value)
        {
            var cust = repo.UpdateCustomer(id, value);
            return Ok(cust);
        }
    }
}

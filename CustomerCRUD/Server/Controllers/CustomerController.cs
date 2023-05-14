using CustomerCRUD.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCRUD.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly NHibernate.ISession _session;

        public CustomerController(ILogger<CustomerController> logger, NHibernate.ISession session)
        {
            _logger = logger;
            _session = session;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _session.Query<Customer>().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id == 0) return BadRequest();

            var customer = _session.Query<Customer>().First(x => x.Id == id);

            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();

            _session.Save(customer);
            _session.Flush();

            return Ok(customer);
        }

        [HttpPut]
        public ActionResult<Customer> Put([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();

            _session.Update(customer);
            _session.Flush();

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            if (id == 0) return BadRequest();
            
            var customer = _session.Query<Customer>().FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            _session.Delete(customer);
            _session.Flush();

            return NoContent();
        }
    }
}
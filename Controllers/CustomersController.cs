using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevCars.API.Controllers
{
    [Route("api/costumers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return NoContent();
        }
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {

            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);

            var order = new Order(model.IdCar, model.IdCustomer, car.Price, extraItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = order.IdCustomer, orderid = order.Id},
                model
                );
        }

        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderid);

            if(order == null)
            {
                return NotFound();
            }

            var extraItems = order
                .ExtraItems
                .Select(e => e.Description)
                .ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using GroceryStoreAPI.IRepositories;
using Microsoft.AspNetCore.Mvc;
using GroceryStoreAPI.Models;
using System.Linq;

namespace GroceryStoreAPI.Controllers.Tests
{
    [TestClass()]
    public class ValuesControllerTests
    {
        private Mock<ICustomerRepository> _mockRepo;
        private ValuesController _controller;
        private List<Customer> customerList = new List<Customer>() { new Customer { id = 1, name = "bob" }, new Customer { id = 2, name = "steve" }, new Customer { id = 3, name = "susy" } };

        public ValuesControllerTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _controller = new ValuesController(_mockRepo.Object);
        }

        [TestMethod()]
        public void Get_ReturnsOk()
        {
            _mockRepo.Setup(repo => repo.GetAll()).Returns(customerList);
            var okResult = _controller.Get();
            Assert.IsInstanceOfType(okResult.Result, typeof(OkObjectResult));
        }

        [TestMethod()]
        public void Get_ReturnsCorrectValues()
        {
            _mockRepo.Setup(repo => repo.GetAll()).Returns(customerList);
            var result = _controller.Get().Result as OkObjectResult;
            var customers = result.Value as List<Customer>;
            var firstCust = new Customer { id = 1, name = "bob" };
            var secondCust = new Customer { id = 2, name = "steve" };
            var thirdCust = new Customer { id = 3, name = "susy" };
            Assert.AreEqual(firstCust.name, customers[0].name);
            Assert.AreEqual(secondCust.name, customers[1].name);
            Assert.AreEqual(thirdCust.name, customers[2].name);
            Assert.AreEqual(3, customers.Count());
        }

        [TestMethod()]
        public void Get_ById_ReturnsCorrectValues()
        {
            var custName = "Batman";
            var cust = new Customer { id = 1, name = custName };
            _mockRepo.Setup(repo => repo.GetCustomerById(1)).Returns(cust);
            var result = _controller.Get(1).Result as OkObjectResult;
            var customer = result.Value as Customer;
            Assert.AreEqual(custName, customer.name);
        }

        [TestMethod()]
        public void Post_ReturnsCustomer()
        {
            var custName = "Batman";
            _mockRepo.Setup(repo => repo.InsertCustomer(custName)).Returns(new Customer { id = 1, name = custName });
            var result = _controller.Post(custName).Result as OkObjectResult;
            var cust = result.Value as Customer;
            Assert.AreEqual(custName, cust.name);
        }

        [TestMethod()]
        public void Put_ReturnsCustomer()
        {
            var custId = 1;
            var custName = "Batman";
            var newCustName = "Robin";
            _mockRepo.Setup(repo => repo.UpdateCustomer(custId, custName)).Returns(new Customer { id = 1, name = newCustName });
            var result = _controller.Put(custId, custName).Result as OkObjectResult;
            var cust = result.Value as Customer;
            Assert.AreEqual(newCustName, cust.name);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.IRepositories
{
    public interface ICustomerRepository
    {
        public void Save();
        public Root GetRoot();
        public List<Customer> GetAll();
        public Customer GetCustomerById(int id);
        public Customer InsertCustomer(string name);
        public Customer UpdateCustomer(int id, string name);

    }
}

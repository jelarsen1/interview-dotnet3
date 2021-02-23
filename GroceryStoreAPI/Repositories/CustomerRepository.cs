using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GroceryStoreAPI.IRepositories;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private Root root { get; }
        string filePath = Path.Combine(Environment.CurrentDirectory, "database.json");

        public CustomerRepository()
        {
            var jsonString = File.ReadAllText(filePath);
            root = JsonConvert.DeserializeObject<Root>(jsonString);
        }

        public async void Save()
        {
            var text = JsonConvert.SerializeObject(root);
            await File.WriteAllTextAsync(filePath, text);
        }

        public Root GetRoot() { return root; }

        public List<Customer> GetAll()
        {
            return root.customers;
        }

        public Customer GetCustomerById(int id)
        {
            return root.customers.Where(x => x.id == id).First();
        }

        public Customer InsertCustomer(string name)
        {
            var nextId = root.customers.Max(x => x.id) + 1;
            var newCust = new Customer() { id = nextId, name = name };
            root.customers.Add(newCust);
            Save();
            return newCust;
        }

        public Customer UpdateCustomer(int id, string name)
        {
            var customer = root.customers.Where(x => x.id == id).First();
            customer.name = name;
            Save();
            return customer;
        }
    }
}

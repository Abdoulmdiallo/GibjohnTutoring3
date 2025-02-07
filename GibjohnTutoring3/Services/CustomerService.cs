using GibjohnTutoring3.Models;
using Microsoft.EntityFrameworkCore;


namespace GibjohnTutoring3.Services
{
    public class CustomerService
    {
        private readonly TlS2300852GibjohnContext _context;

        public CustomerService(TlS2300852GibjohnContext context)
        {
            _context = context;
        }
        //adds customer to database
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        //login
        public async Task<Customer?> Login(Customer customer)
        {
            return await _context.Customers.FirstOrDefaultAsync
            (c => c.Username == customer.Username && c.Password == customer.Password);
        
        }
    }
}

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

        public async Task AddCustomerAsync(Customer customer) 
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}

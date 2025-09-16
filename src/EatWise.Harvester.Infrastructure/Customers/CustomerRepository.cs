using EatWise.Customers;
using EatWise.Harvester.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EatWise.Harvester.Infrastructure.Customers;

internal sealed class CustomerRepository(HarvesterDbContext context): ICustomerRepository
{
    public async Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Customer customer)
    {
        context.Customers.Add(customer);
    }
}

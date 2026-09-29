using SupportDesk.Application.Interfaces;
using SupportDesk.Domain;
using SupportDesk.Infrastructure.Data;

namespace SupportDesk.Infrastructure.Repositories;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly JsonFileHandler _fileHandler;

    public JsonCustomerRepository(JsonFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var data = await _fileHandler.ReadAsync<CustomerData>(
            DataPaths.CustomersFile);

        return [.. data.Select(c =>
            Customer.Rehydrate(
                c.Id,
                c.Name,
                c.Email))];
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customers = await GetAllAsync();

        return customers.FirstOrDefault(c => c.Id == id);
    }

    public async Task AddAsync(Customer customer)
    {
        var customers = await GetAllAsync();

        customers.Add(customer);

        await _fileHandler.WriteAsync(
            DataPaths.CustomersFile,
            customers);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var customers = await GetAllAsync();

        var index = customers.FindIndex(c => c.Id == customer.Id);

        if (index == -1)
        {
            throw new ArgumentException("Customer was not found.");
        }

        customers[index] = customer;

        await _fileHandler.WriteAsync(
            DataPaths.CustomersFile,
            customers);
    }
}

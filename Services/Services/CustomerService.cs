using System.Collections;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;

public class CustomerService : ICustomerService
{
    private readonly BankAppDataContext _context;
    private readonly IMapper _mapper;

    public CustomerService(BankAppDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDetailsDto?> GetCustomerDetailsByIdAsync(int customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.Dispositions)
            .ThenInclude(d => d.Account)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        return customer == null ? null : _mapper.Map<CustomerDetailsDto>(customer);
    }

    public async Task<CustomerSearchResultDto?> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        return customer == null ? null : _mapper.Map<CustomerSearchResultDto>(customer);
    }

    public async Task<int> GetCustomerCountAsync()
    {
        return await _context.Customers.CountAsync();
    }

    public async Task<List<CustomerSearchResultDto>> GetCustomersAsync(
        string sortColumn, string sortOrder, int page,int pageSize, string givenName, string surName, string city)
    {
        if (page < 1) page = 1;

        if (string.IsNullOrEmpty(sortOrder))
        {
            sortOrder = "asc";
        }

        if (string.IsNullOrEmpty(sortColumn))
        {
            sortColumn = "Givenname";
        }

        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrEmpty(givenName))
        {
            query = query.Where(c => c.Givenname.Contains(givenName));
        }

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(c => c.City.Contains(city));
        }

        if (!string.IsNullOrEmpty(surName))
        {
            query = query.Where(c => c.Surname.Contains(surName));
        }

        if (sortColumn == "Givenname")
        {
            query = sortOrder == "desc"
                ? query.OrderByDescending(c => c.Givenname)
                : query.OrderBy(c => c.Givenname);
        }

        if (sortColumn == "Surname")
        {
            query = sortOrder == "desc"
                ? query.OrderByDescending(c => c.Surname)
                : query.OrderBy(c => c.Surname);
        }

        if (sortColumn == "City")
        {
            query = sortOrder == "desc"
                ? query.OrderByDescending(c => c.City)
                : query.OrderBy(c => c.City);
        }

        int firstItemIndex = (page - 1) * pageSize;

        var customers = await query
            .Skip(firstItemIndex)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<List<CustomerSearchResultDto>>(customers);
    }
}

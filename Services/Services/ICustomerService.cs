using Services.DTOs;

public interface ICustomerService
{
    Task<CustomerSearchResultDto?> GetCustomerByIdAsync(int customerId);
    Task<int> GetCustomerCountAsync();
    Task<CustomerDetailsDto?> GetCustomerDetailsByIdAsync(int customerId);
    Task<List<CustomerSearchResultDto>> GetCustomersAsync(string sortColumn, string sortOrder, int page, int pageSize, string givenName, string surName, string city);
}
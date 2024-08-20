using Entities;

namespace RepositoryContracts
{
    public interface ICountriesRepository
    {
        Task<Country> AddCountry(Country country);

        Task<IEnumerable<Country>> GetAllCountries();

        Task<Country?> GetCountryByCountryId(Guid countryID);

        Task<Country?> GetCountryByCountryName(string countryName);
    }
}

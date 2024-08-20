using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Country> AddCountry(Country country)
        {
            _db.Add(country);
            await _db.SaveChangesAsync();

            return country;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByCountryId(Guid countryID)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => c.CountryID.Equals(countryID));
        }

        public async Task<Country?> GetCountryByCountryName(string countryName)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => c.CountryName.Equals(countryName));
        }
    }
}

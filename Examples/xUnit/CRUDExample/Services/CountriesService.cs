using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validation: countryAddRequest parameter can't be null
            if (countryAddRequest == null) 
                throw new ArgumentNullException(nameof(countryAddRequest));

            // Validation: CountryName can't be null
            if (countryAddRequest.CountryName == null)
                throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validation: CountryName can't be duplicate
            if (_countries.Where(c => c.Name == countryAddRequest.CountryName).Count() > 0)
                throw new ArgumentException("Given country name already exists");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.Id = Guid.NewGuid();

            // Add country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            var list = _countries.Select(c => c.ToCountryResponse()).ToList();
            return list;
        }

        public CountryResponse? GetCountryByCountryId(Guid? id)
        {
            if (id == null) return null;

            var country_from_list = _countries.FirstOrDefault(c => c.Id == id);

            if (country_from_list == null) return null;

            return country_from_list.ToCountryResponse();
        }
    }
}

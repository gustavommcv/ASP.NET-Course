using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        // When CountryAddRequest is null, it should throw ArgumentNullExceptions
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        // When CountryName is null, it shoult throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        // When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        // When you supply proper CountryName, it should insert (add) the country
        // to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };

            // Act
            var response = _countriesService.AddCountry(request);
            var countries_from_GetAllCountries = _countriesService.GetAllCountries();

            // Assert
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }
        #endregion

        #region GetAllCountries

        [Fact]
        // The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            // Acts
            var actual_country_response_list = _countriesService.GetAllCountries();

            // Assert
            Assert.Empty(actual_country_response_list);

        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange
            var country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "USA" },
                new CountryAddRequest() { CountryName = "UK" }
            };

            // Act
            var countries_list_from_add_country = new List<CountryResponse>();
            foreach (var country in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country));
            }

            var actualCountryResponseList = _countriesService.GetAllCountries();

            // Read each element from countries_list_from_add_country
            foreach (var expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }

        }
        #endregion

        #region GetCountryByID
        // If we supply null as CountryId, it should return null as CountryResponse
        [Fact]
        public void GetCountryByID_NullCountryID()
        {
            // Arrange
            Guid? id = null;

            // Act
            var response = _countriesService.GetCountryByCountryId(id);

            // Assert
            Assert.Null(response);
        }

        // If we supply a valid country id, it should return the matching country details
        // as CountryResponse object
        [Fact]
        public void GetCountryByID_()
        {
            // Arrange
            var country_add_request = new CountryAddRequest() { CountryName = "China" };
            var country_response_from_add = _countriesService.AddCountry(country_add_request);
            
            // Act
            var country_response_from_get = _countriesService.GetCountryByCountryId(country_response_from_add.CountryId);

            // Assert
            Assert.Equal(country_response_from_add, country_response_from_get);
        }
        #endregion
    }
}

using Contracts.Interfaces;
using Contracts.Models;

namespace Infrastructure.Data
{
    public class CitiesRepository : ICitiesService
    {
        private List<City> _cities;

        public CitiesRepository()
        {
            _cities = new List<City>()
            {
                new City()
                {
                    CityUniqueCode = "LDN",
                    CityName = "London",
                    DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                    TemperatureFahrenheit = 33
                },
                new City()
                {
                    CityUniqueCode = "NYC",
                    CityName = "New York",
                    DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                    TemperatureFahrenheit = 60
                },
                new City()
                {
                    CityUniqueCode = "PAR",
                    CityName = "Paris",
                    DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                    TemperatureFahrenheit = 82
                }
            };
        }

        public List<City> GetCities()
        {
            return _cities;
        }
    }
}

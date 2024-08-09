using CountriesApp.Models;

public interface ICountryService
{
    List<Country> GetAll();
    Country GetById(int id);
}

public class CountryContext : ICountryService
{
    private readonly List<Country> _countries;

    public CountryContext()
    {
        _countries = new List<Country>
        {
            new Country { Id = 1, Name = "United States" },
            new Country { Id = 2, Name = "Canada" },
            new Country { Id = 3, Name = "United Kingdom" },
            new Country { Id = 4, Name = "India" },
            new Country { Id = 5, Name = "Japan" }
        };
    }

    public List<Country> GetAll()
    {
        return _countries;
    }

    public Country GetById(int id)
    {
        return _countries.FirstOrDefault(c => c.Id == id);
    }
}

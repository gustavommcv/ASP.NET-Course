using Entities;
using System.Linq.Expressions;

namespace RepositoryContracts
{
    public interface IPersonsRepository
    {
        Task<Person> AddPerson(Person person);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person?> GetPersonByPersonID(Guid personID);
        /// <summary>
        /// Returns all person objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching persons with given condition</returns>
        Task<IEnumerable<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);

        Task<bool> DeletePersonByPersonID(Guid personID);
        Task<Person> UpdatePerson(Person person);
    }
}

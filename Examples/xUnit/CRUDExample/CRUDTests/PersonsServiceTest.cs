using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;

        public PersonsServiceTest(ITestOutputHelper outputHelper)
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
            _outputHelper = outputHelper;
        }

        #region AddPerson
        // When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => _personsService.AddPerson(personAddRequest));
        }

        // When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = null,
            };

            // Act
            Assert.Throws<ArgumentException>(() => _personsService.AddPerson(personAddRequest));
        }

        // When we supply proper person details, it should insert the person into the persons
        // list; And it should return an object of PersonResponse, which includes with the
        // newly generated personID
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = "Gustavo",
                Email = "gustavo@email.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2004-05-16"),
                ReceiveNewsLetters = false
            };

            // Act
            var response = _personsService.AddPerson(personAddRequest);
            var persons_list = _personsService.GetAllPersons();

            // Assert
            Assert.True(response.PersonId != Guid.Empty);
            Assert.Contains(response, persons_list);
        }
        #endregion

        #region GetPersonByID
        //If we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            // Arrange
            Guid? personID = null;

            // Act
            var person_response_from_get = _personsService.GetPersonByID(personID);

            // Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid PersonID, it should return the valid person details as PersonResponse
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            // Arrange
            var country = new CountryAddRequest() { CountryName = "USA" };
            var country_response = _countriesService.AddCountry(country);

            var person_request = new PersonAddRequest()
            {
                Name = "Gustavo",
                Email = "gustavo@email.com",
                Address = "sample address",
                CountryID = country_response.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2004-05-16"),
                ReceiveNewsLetters = false
            };

            // Act
            var person_response_from_add = _personsService.AddPerson(person_request);
            var person_response_from_get = _personsService.GetPersonByID(person_response_from_add.PersonId);

            // Assert
            Assert.Equal(person_response_from_add, person_response_from_get);

        }

        #endregion

        #region GetAllPersons
        // The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Act
            List<PersonResponse> persons_from_get = _personsService.GetAllPersons();

            // Assert
            Assert.Empty(persons_from_get);
        }

        // First, we will add a few persons; and then when we call GetAllPersons(), it should return
        // the same persons that were added
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            // Arrange
            CountryAddRequest country_request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            var country_response1 = _countriesService.AddCountry(country_request1);
            var country_response2 = _countriesService.AddCountry(country_request2);

            PersonAddRequest person_request1 = new PersonAddRequest()
            {
                Name = "Gustavo",
                Email = "gustavo@email.com",
                Address = "sample address",
                CountryID = country_response1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2004-05-16"),
                ReceiveNewsLetters = false
            };

            PersonAddRequest person_request2 = new PersonAddRequest()
            {
                Name = "Harsha",
                Email = "harsha@email.com",
                Address = "sample address",
                CountryID = country_response2.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1980-05-16"),
                ReceiveNewsLetters = false
            };

            PersonAddRequest person_request3 = new PersonAddRequest()
            {
                Name = "Smith",
                Email = "smith@email.com",
                Address = "sample address",
                CountryID = country_response1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2003-01-4"),
                ReceiveNewsLetters = false
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request1,
                person_request2,
                person_request3
            };

            List<PersonResponse> person_responses = new List<PersonResponse>();

            foreach (PersonAddRequest request in person_requests)
            {
                person_responses.Add(_personsService.AddPerson(request));
            }

            // Print person_responses
            _outputHelper.WriteLine("Expected:");
            foreach (var person_response in person_responses)
            {
                _outputHelper.WriteLine(person_response.ToString());
            }

            // Act
            List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons();

            // Print persons_list_from_get
            _outputHelper.WriteLine("Actual:");
            foreach (var person_response in persons_list_from_get)
            {
                _outputHelper.WriteLine(person_response.ToString());
            }

            foreach (var person_response_from_add in person_responses)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }

        #endregion

        #region GetFilteredPersons
        // If the search text is empty and search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange
            CountryAddRequest country_request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            var country_response1 = _countriesService.AddCountry(country_request1);
            var country_response2 = _countriesService.AddCountry(country_request2);

            PersonAddRequest person_request1 = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request2 = new PersonAddRequest() { Name = "Harsha", Email = "harsha@email.com", Address = "sample address", CountryID = country_response2.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("1980-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request3 = new PersonAddRequest() { Name = "Smith", Email = "smith@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2003-01-4"), ReceiveNewsLetters = false };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request1, person_request2, person_request3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                person_response_list_from_add.Add(_personsService.AddPerson(person_request));
            }

            // Act
            string? searchString = "";
            string? searchBy = nameof(Person.PersonName);
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(searchBy, searchString);

            // Print person_responses
            _outputHelper.WriteLine("Expected:");
            foreach (var person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.GetType().GetProperty(searchBy)?.GetValue(person_response_from_add)?.ToString()?.Contains(searchString) == true)
                    _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Print persons_list_from_get
            _outputHelper.WriteLine("Actual:");
            foreach (var person_response_from_get in persons_list_from_search)
            {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            foreach (var person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add is not null)
                    if (person_response_from_add.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }

        // First we will add few persons; and then we will search based on person name with some search string
        // It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // Arrange
            CountryAddRequest country_request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            var country_response1 = _countriesService.AddCountry(country_request1);
            var country_response2 = _countriesService.AddCountry(country_request2);

            PersonAddRequest person_request1 = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request2 = new PersonAddRequest() { Name = "Harsha", Email = "harsha@email.com", Address = "sample address", CountryID = country_response2.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("1980-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request3 = new PersonAddRequest() { Name = "Smith", Email = "smith@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2003-01-4"), ReceiveNewsLetters = false };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request1, person_request2, person_request3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                person_response_list_from_add.Add(_personsService.AddPerson(person_request));
            }

            // Act
            string? searchString = "a";
            string? searchBy = nameof(Person.PersonName);
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(searchBy, searchString);

            // Print person_responses
            _outputHelper.WriteLine("Expected:");
            foreach (var person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.GetType().GetProperty(searchBy)?.GetValue(person_response_from_add)?.ToString()?.Contains(searchString) == true)
                    _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Print persons_list_from_get
            _outputHelper.WriteLine("Actual:");
            foreach (var person_response_from_get in persons_list_from_search)
            {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            foreach (var person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add is not null)
                    if (person_response_from_add.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }

        #endregion

        #region GetSortedPersons
        // When we sort based on PersonName is DESC, it should return persons list in descending on PersonName
        [Fact]
        public void GetSortedPersons_()
        {
            // Arrange
            CountryAddRequest country_request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            var country_response1 = _countriesService.AddCountry(country_request1);
            var country_response2 = _countriesService.AddCountry(country_request2);

            PersonAddRequest person_request1 = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request2 = new PersonAddRequest() { Name = "Harsha", Email = "harsha@email.com", Address = "sample address", CountryID = country_response2.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("1980-05-16"), ReceiveNewsLetters = false };

            PersonAddRequest person_request3 = new PersonAddRequest() { Name = "Smith", Email = "smith@email.com", Address = "sample address", CountryID = country_response1.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2003-01-4"), ReceiveNewsLetters = false };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request1, person_request2, person_request3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                person_response_list_from_add.Add(_personsService.AddPerson(person_request));
            }

            List<PersonResponse> allPersons = _personsService.GetAllPersons();

            // Act
            string searchBy = nameof(Person.PersonName);
            SortOrderOptions sortOrderOption = SortOrderOptions.ASC;

            List<PersonResponse> persons_list_from_sort = _personsService.GetSortedPersons(allPersons, searchBy, sortOrderOption);

            if (sortOrderOption == SortOrderOptions.ASC)
            {
                person_response_list_from_add = person_response_list_from_add
                    .OrderBy(p => p.GetType().GetProperty(searchBy).GetValue(p, null))
                    .ToList();
            }
            else if (sortOrderOption == SortOrderOptions.DESC)
            {
                person_response_list_from_add = person_response_list_from_add
                    .OrderByDescending(p => p.GetType().GetProperty(searchBy).GetValue(p, null))
                    .ToList();
            }

            // Print person_responses
            _outputHelper.WriteLine("Expected:");
            foreach (var person_response_from_add in person_response_list_from_add)
            {
                _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            // Print persons_list_from_get
            _outputHelper.WriteLine("Actual:");
            foreach (var person_response_from_get in persons_list_from_sort)
            {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            // Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], persons_list_from_sort[i]);
            }
        }
        #endregion

        #region UpdatePerson
        // When we supplu null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            // Arrange
            PersonUpdateRequest? person_update_request = null;

            // Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.UpdatePerson(person_update_request);
            });
        }

        // When we supply invalid person id, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            // Arrange
            PersonUpdateRequest? person_update_request = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid(),
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(person_update_request);
            });
        }

        // When the person name is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            // Arrange
            CountryAddRequest country_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            var country_response = _countriesService.AddCountry(country_request);

            var person_request = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            var person_response_from_add = _personsService.AddPerson(person_request);

            var person_update_request = person_response_from_add.ToPersonUpdateRequest();

            person_update_request.Name = null;

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(person_update_request);
            });
        }

        // First, add a new person and try to update the person name and email
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdation()
        {
            // Arrange
            CountryAddRequest country_request = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            var country_response = _countriesService.AddCountry(country_request);

            var person_request = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            var person_response_from_add = _personsService.AddPerson(person_request);

            var person_update_request = person_response_from_add.ToPersonUpdateRequest();

            person_update_request.Name = "Andrew Ryan";
            person_update_request.Email = "ryan@corporation.net";

            // Act
            var person_response_from_update = _personsService.UpdatePerson(person_update_request);

            var person_response_from_get = _personsService.GetPersonByID(person_response_from_update.PersonId);

            // Assert
            Assert.Equal(person_response_from_get, person_response_from_update);
        }
        #endregion

        #region DeletePerson
        // If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            var person_add_request = new PersonAddRequest() { Name = "Gustavo", Email = "gustavo@email.com", Address = "sample address", CountryID = country_response_from_add.CountryId, Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2004-05-16"), ReceiveNewsLetters = false };

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            // Act
            bool isDeleted = _personsService.DeletePerson(person_response_from_add.PersonId);

            // Assert
            Assert.True(isDeleted);
        }

        // If you supply an invalid PersonID, it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            // Act
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}

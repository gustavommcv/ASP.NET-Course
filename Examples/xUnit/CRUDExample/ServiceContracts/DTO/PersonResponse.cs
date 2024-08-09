﻿using Entities;
using ServiceContracts.Enums;
using System.Xml.Linq;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Persons Service
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// Compares the current object data with the parameter object
        /// </summary>
        /// <param name="obj">The Personresponse Object to compare</param>
        /// <returns>True or false, indicating whether all person details are matched 
        /// with the specified parameter</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;

            return PersonId == person.PersonId
                   && PersonName == person.PersonName
                   && Email == person.Email
                   && DateOfBirth == person.DateOfBirth
                   && Gender == person.Gender
                   && CountryID == person.CountryID
                   && Address == person.Address
                   && ReceiveNewsLetters == person.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID: {PersonId}\n" +
                $"Name: {PersonName}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonID = PersonId,
                Name = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Address = Address,
                CountryID = CountryID,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                ReceiveNewsLetters = ReceiveNewsLetters,

            };
        }
    }

    public static class PersonExtensions
    {
        /// An extension method to convert an object of Person class into PersonResponse
        /// </summary>
        /// <param name="person">The Person object to convert</param>
        /// <returns>Returns the converted PersonResponse object</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            // person => PersonResponse
            return new PersonResponse()
            {
                PersonId = person.Id,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}
namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using System.Collections.Generic;
    using MyEcommerce.Core.Domain;

    public class Address : ValueObject
    {
        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }
        
        public string Street1 { get; private set; }

        public string Street2 { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

        public Address()
        {
        }

        public Address(
            string firstName,
            string lastName,
            string street1,
            string street2,
            string city,
            string state,
            string country,
            string zipCode
        ) {
            FirstName = firstName;
            LastName = lastName;
            Street1 = street1;
            Street2 = street2;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Street1;
            yield return Street2;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
namespace MyEcommerce.Core.Domain
{
    using System;
    using System.Collections.Generic;

    public class Identifier : ValueObject
    {
        public string Value { get; protected set; }

        protected Identifier()
        {
            Value = $"{GetType().Name}-{Guid.NewGuid()}";
        }

        protected Identifier(Guid value)
        {
            Value = $"{GetType().Name}-{value.ToString()}";
        }

        protected Identifier(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
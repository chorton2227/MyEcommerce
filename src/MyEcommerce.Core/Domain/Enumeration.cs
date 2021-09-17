namespace MyEcommerce.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration : IComparable
    {
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Enumeration)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration enumeration)
            {
                return false;
            }

            var checkType = GetType().Equals(enumeration.GetType());
            var checkId = Id.Equals(enumeration.Id);
            return checkType && checkId;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<TEnum> GetAll<TEnum>()
            where TEnum : Enumeration
        {
            return typeof(TEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(x => x.GetValue(null))
                .Cast<TEnum>();
        }

        public static TEnum Parse<TEnum>(int id)
            where TEnum : Enumeration
        {
            return GetAll<TEnum>()?.FirstOrDefault(x => x.Id == id);
        }

        public static TEnum Parse<TEnum>(string name)
            where TEnum : Enumeration
        {
            return GetAll<TEnum>()?.FirstOrDefault(x => x.Name == name);
        }
    }
}
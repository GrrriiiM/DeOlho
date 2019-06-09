using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeOlho.Services.Politicos.Api.Domain.SeedWork
{
    public class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T Get<T>(int id) where T : Enumeration
        {
            var fields = GetAll<T>();

            return fields.SingleOrDefault(_ => _.Id == id);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
}
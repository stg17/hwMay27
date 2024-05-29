using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace hwMay27.Data
{
    public class PersonRepo
    {
        private readonly string _connectionString;
        public PersonRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }

        public void DeleteAll()
        {
            var context = new PeopleDataContext(_connectionString);
            context.People.RemoveRange(GetPeople());
            context.SaveChanges();
        }

        public void AddPeople(List<Person> people)
        {
            var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }

        public List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person()
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(1, 120),
                Address = Address.StreetAddress(),
                Email = Internet.Email()
            }).ToList();
        }
    }
}

using CsvHelper;
using CsvHelper.Configuration;
using hwMay27.Data;
using hwMay27.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Globalization;
using System.Text;

namespace hwMay27.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly string _connectionString;
        public PersonController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet("generate")]
        public IActionResult Generate(int amount)
        {
            var repo = new PersonRepo(_connectionString);
            var people = repo.GeneratePeople(amount);

            var writer = new StringWriter();
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(people);
            byte[] csvBytes = Encoding.UTF8.GetBytes(writer.ToString());
            return File(csvBytes, "text/csv", "people.csv");
        }

        [HttpPost("addpeople")]
        public void AddPeople(AddPeopleViewModel addPeopleViewModel)
        {
            var repo = new PersonRepo(_connectionString);
            int indexOfComma = addPeopleViewModel.Base64.IndexOf(',');
            string base64 = addPeopleViewModel.Base64.Substring(indexOfComma + 1);
            byte[] csvBytes = Convert.FromBase64String(base64);
            List<Person> people = GetFromCsvBytes(csvBytes);
            Console.WriteLine(people[0]);
            repo.AddPeople(people);
        }

        private List<Person> GetFromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Person>().ToList();
        }

        [HttpGet("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PersonRepo(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost("deleteall")]
        public void DeleteAll()
        {
            var repo = new PersonRepo(_connectionString);
            repo.DeleteAll();
        }


    }
}

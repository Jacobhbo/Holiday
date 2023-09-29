using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Holiday;
using Holiday.Shared;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace Holiday.Pages
{
    public partial class Index
    {
        private List<HolidayService.HolidayInfo> holidays;
        private async Task GetHolidays()
        {
            // Hent ferieoplysninger for Danmark i 2023
            holidays = await holidayService.GetHolidaysForCountryAsync("DK", 2023);
        }

        // Opret en ordbog til oversættelse af helligdagsnavne
        private Dictionary<string, string> holidayTranslations = new Dictionary<string, string> {
            { "New Year's Day", "Nytårsdag" },
            { "Christmas Day", "Juledag" },
            { "Maundy Thursday", "Skærtorsdag" },
            { "Good Friday", "Langfredag" },
            { "Easter Sunday", "Påske & Påskedag" },
            { "Easter Monday", "2. Påskedag" },
            { "General Prayer Day", "Stor Bededag" },
            { "Ascension Day", "Kristi Himmelfart" },
            { "Bank closing day", "Bankdag" },
            { "Pentecost", "Pinse & Pinsedag" },
            { "Whit Monday", "2. Pinsedag" },
            { "Constitution Day", "Grundlovsdag & Fars dag" },
            { "Christmas Eve", "Juleaften" },
            { "St. Stephen's Day", "2. Juledag" },
            { "New Year's Eve", "Nytårsaften" } };

        private string GetDanishHolidayName(string englishName)
        {
            if (holidayTranslations.TryGetValue(englishName, out var danishName))
            {
                return danishName;
            }

            return englishName;
        }


        private List<Person> people { get; set; } = new List<Person>();
        private bool queryExecuted = false;
        private bool holidaysLoaded = false;
        

        public async Task ExecuteSqlQuery()
        {
            if (!queryExecuted)
            {
                try
                {
                    string connectionString = "Data Source=192.168.23.210,1433;Database=BirthdayDatabase;User=Egon;Password=Passw0rd;Encrypt=FALSE";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string sqlQuery = "SELECT * FROM Birthday";

                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var person = new Person
                                    {
                                        Navn = reader["navn"].ToString(),
                                        Alder = Convert.ToInt32(reader["alder"]),
                                        Fødselsdato = Convert.ToDateTime(reader["birthdaydato"]),
                                        Køn = reader["køn"].ToString()
                                    };
                                    people.Add(person);
                                    foreach (var person1 in people)
                                    {
                                        Console.WriteLine(person.Navn);
                                    }
                                }
                            }
                        }
                    }

                    queryExecuted = true;
                    StateHasChanged();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("FEJL: " + ex.Message);
                }
            }
        }

        public async Task GetHolidaysFromDatabase()
        {
            if (!holidaysLoaded)
            {
                try
                {
                    // Hent ferieoplysninger her...
                    holidays = await holidayService.GetHolidaysForCountryAsync("DK", 2023);


                    holidaysLoaded = true;
                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FEJL i GetHolidaysFromDatabase " + ex.Message);
                }
            }
        }



        public class Person
        {
            public string Navn { get; set; }
            public int Alder { get; set; }
            public DateTime Fødselsdato { get; set; }
            public string Køn { get; set; }
        }

    }
}

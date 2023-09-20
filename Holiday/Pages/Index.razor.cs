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
        private Dictionary<string, string> holidayTranslations = new Dictionary<string, string> { { "New Year's Day", "Nytårsdag" }, { "Christmas Day", "Juledag" }, { "Maundy Thursday", "Skærtorsdag" }, { "Good Friday", "Langfredag" }, { "Easter Sunday", "Påske & Påskedag" }, { "Easter Monday", "2. Påskedag" }, { "General Prayer Day", "Stor Bededag" }, { "Ascension Day", "Kristi Himmelfart" }, { "Bank closing day", "Bankdag" }, { "Pentecost", "Pinse & Pinsedag" }, { "Whit Monday", "2. Pinsedag" }, { "Constitution Day", "Grundlovsdag & Fars dag" }, { "Christmas Eve", "Juleaften" }, { "St. Stephen's Day", "2. Juledag" }, { "New Year's Eve", "Nytårsaften" } };
        private string GetDanishHolidayName(string englishName)
        {
            if (holidayTranslations.TryGetValue(englishName, out var danishName))
            {
                return danishName;
            }

            return englishName;
        }

        public async Task ExecuteSqlQuery()
        {
            try
            {
                string connectionString = "Data Source=192.168.23.210,1433;Database=BirthdayDatabase;User=Egon;Password=Passw0rd;";
                

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM BirthdayDatabase";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string columnName = reader["ColumnName"].ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("FEJL: " + ex.Message);
            }

        }

    }
}

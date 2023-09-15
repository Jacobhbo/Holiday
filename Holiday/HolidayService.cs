using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic; // Tilføj dette using statement

public class HolidayService
{
    private readonly HttpClient _httpClient;

    public HolidayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<HolidayInfo>> GetHolidaysForCountryAsync(string countryCode, int year)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<HolidayInfo>>($"https://date.nager.at/api/v3/PublicHolidays/2023/DK");
            return response;
        }
        catch (Exception ex)
        {
            // Håndter fejl efter behov
            Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            return null;
        }
    }

    // Opret en klasse, der repræsenterer ferieoplysningerne
    public class HolidayInfo
    {
        public DateTime date { get; set; }
        public string name { get; set; }
    }

}

using Newtonsoft.Json.Linq;
using System.Web;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;

        var queryParams = new Dictionary<string, string>
        {
            { "year", year.ToString() },
            { "team1", team }
        };
        string url = BuildUrl(queryParams);
        totalGoals += await ObterGols(url, "team1");

        queryParams["team1"] = null;
        queryParams["team2"] = team;
        url = BuildUrl(queryParams);
        totalGoals += await ObterGols(url, "team2");

        return totalGoals;
    }

    private static string BuildUrl(Dictionary<string, string> queryParams)
    {
        var uriBuilder = new UriBuilder(baseUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var param in queryParams)
        {
            if (!string.IsNullOrEmpty(param.Value))
            {
                query[param.Key] = param.Value;
            }
        }

        uriBuilder.Query = query.ToString();
        return uriBuilder.ToString();
    }

    private static async Task<int> ObterGols(string url, string teamField)
    {
        int totalGols = 0;
        int currentPage = 1;
        int totalPages;

        do
        {
            string currentUrl = $"{url}&page={currentPage}";
            string responseBody = await client.GetStringAsync(currentUrl);
            JObject responseJson = JObject.Parse(responseBody);

            totalPages = responseJson["total_pages"].Value<int>();

            foreach (var match in responseJson["data"])
            {
                totalGols += int.Parse(match[$"{teamField}goals"].ToString());
            }

            currentPage++;
        } while (currentPage <= totalPages);

        return totalGols;
    }
}
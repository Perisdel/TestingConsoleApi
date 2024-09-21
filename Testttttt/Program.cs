using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
	private static readonly HttpClient _httpClient = new HttpClient();

	static async Task Main(string[] args)
	{
        //get current  DateTime
        var TimestampStart = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
		// Define API URLs
		string api1 = "http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=96ec15b308bdac142df51c4041058aa1";
		string api2 = "https://datausa.io/api/data?drilldowns=Nation&measures=Population";
		string api3 = "https://catfact.ninja/fact";


		

		// Call APIs asynchronously
		Task<string> task1 = FetchDataFromApi(api1);
		Task<string> task2 = FetchDataFromApi(api2);
		Task<string> task3 = FetchDataFromApi(api3);

		// Wait for all the tasks to complete
		await Task.WhenAll(task1,task2,task3);

        //get current  DateTime
        var TimestampFinish = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

		//Get the difference
		int diffInMilsec= Convert.ToInt32(TimestampFinish-TimestampStart);
		// Output the results to the console
		Console.WriteLine("API 1 Response:");
		Console.WriteLine(task1.Result);
		Console.WriteLine("\nAPI 2 Response:");
		Console.WriteLine(task2.Result);
		Console.WriteLine("\nAPI 3 Response:");
		Console.WriteLine(task3.Result);
		Console.WriteLine("\nmillisecs");
		Console.WriteLine(diffInMilsec);
	}

	private static async Task<string> FetchDataFromApi(string url)
	{
		string data = "";
		try
		{
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			data = await response.Content.ReadAsStringAsync();
			return data;
		}
		catch (Exception ex)
		{

			 data = ex.ToString();
			return  data;
		}
		
	}
}

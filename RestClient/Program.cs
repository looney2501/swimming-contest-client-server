using System;
using System.Linq;
using System.Threading.Tasks;
using Model.Domain.Enums;
using RestClient.client;

namespace RestClient;

internal class Program
{
    private static RestClient.client.RestClient _restClient = new RestClient.client.RestClient();

    public static async Task GetAll()
    {
        Race[] races = await _restClient.GetAll();
        races.ToList().ForEach(Console.WriteLine);
    }

    public static async Task GetById()
    {
        Console.WriteLine(await _restClient.GetById("16"));
        Console.WriteLine(await _restClient.GetById("166"));
    }

    public static async Task Create()
    {
        Race race = new Race(SwimmingDistance._50m, SwimmingStyle.Butterfly, 50);
        Console.WriteLine(await _restClient.Create(race));
        Console.WriteLine(await _restClient.Create(race));
        Console.WriteLine(await _restClient.Create(race));
    }

    public static async Task Update()
    {
        Race race = new Race(39, SwimmingDistance._1500m, SwimmingStyle.Mixed, 145);
        await _restClient.Update(race);
        Console.WriteLine(await _restClient.GetById("39"));
    }

    public static async Task Delete()
    {
        Console.WriteLine(await _restClient.GetById("40"));
        await _restClient.Delete("40");
        Console.WriteLine(await _restClient.GetById("40"));
    }

    public static void Main(string[] args)
    {
        // GetById().Wait();

        // Create().Wait();


        // Update().Wait();


        Delete().Wait();
        GetAll().Wait();
    }
}
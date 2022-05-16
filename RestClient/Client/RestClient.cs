using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Model.Domain.Entities;
using Model.Domain.Enums;

namespace RestClient.client;

public class RestClient
{
    private static readonly HttpClient Client = new HttpClient();

    public RestClient()
    {
        Client.BaseAddress = new Uri("http://localhost:8080/swimming-contest/races");
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<Race[]> GetAll()
    {
        Race[] allRaces = null;
        var responseMessage = await Client.GetAsync(Client.BaseAddress);
        if (responseMessage.IsSuccessStatusCode)
        {
            allRaces = await responseMessage.Content.ReadAsAsync<Race[]>();
        }
        else
        {
            Console.WriteLine(responseMessage.StatusCode.ToString());
        }

        return allRaces;
    }

    public async Task<Race> GetById(String id)
    {
        Race race = null;
        var responseMessage = await Client.GetAsync(String.Format("{0}/{1}", Client.BaseAddress, id));
        if (responseMessage.IsSuccessStatusCode)
        {
            race = await responseMessage.Content.ReadAsAsync<Race>();
        }
        else
        {
            Console.WriteLine(responseMessage.StatusCode);
        }

        return race;
    }

    public async Task<Race> Create(Race newRace)
    {
        Race addedRace = null;
        var responseMessage = await Client.PostAsJsonAsync(Client.BaseAddress, newRace);

        if (responseMessage.IsSuccessStatusCode)
        {
            addedRace = await responseMessage.Content.ReadAsAsync<Race>();
        }
        else
        {
            Console.WriteLine(responseMessage.StatusCode);
        }
        return addedRace;
    }

    public async Task<Race> Update(Race race)
    {
        var responseMessage = await Client.PutAsJsonAsync(String.Format("{0}/{1}", Client.BaseAddress, race.id), race);
        if (!responseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine(responseMessage.StatusCode);
        }

        return null;
    }

    public async Task<Race> Delete(String id)
    {
        var responseMessage = await Client.DeleteAsync(String.Format("{0}/{1}", Client.BaseAddress, id));
        if (!responseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine(responseMessage.StatusCode);
        }

        return null;
    }
}

public class Race : Identifiable<int>
{
    public Race()
    {
    }

    public Race(int id, SwimmingDistance distance, SwimmingStyle style, int swimmersNumber)
    {
        this.id = id;
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    public Race(SwimmingDistance distance, SwimmingStyle style, int swimmersNumber)
    {
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    public SwimmingDistance distance { get; set; }
    public SwimmingStyle style { get; set; }
    public int swimmersNumber { get; set; }
    public int id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        var other = obj as Race;
        if (other == null) return false;
        return other.distance == distance && other.style == style && other.swimmersNumber == swimmersNumber;
    }

    public bool Equals(Race other)
    {
        if (other == null) return false;
        return other.distance == distance && other.style == style && other.swimmersNumber == swimmersNumber;
    }

    public override string ToString()
    {
        return "Race{" +
               "ID=" + id +
               ", distance=" + distance +
               ", style=" + style +
               ", swimmersNumber=" + swimmersNumber +
               '}';
    }
}
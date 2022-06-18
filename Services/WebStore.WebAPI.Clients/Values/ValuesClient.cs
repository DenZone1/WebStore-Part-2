using System.Net;
using System.Net.Http.Json;

using Newtonsoft.Json.Linq;

using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Values;

public class ValuesClient : BaseClient, IValuesService
{
    public ValuesClient(HttpClient Client) : base(Client, "api/[controller]") 
    {

    }

    public IEnumerable<string> GetValues()
    {
        var response = Http.GetAsync(Address).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return Enumerable.Empty<string>();

        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;

        return Enumerable.Empty<string>();
    }


    public string GetById(int Id)
    {
        var response = Http.GetAsync($"{Address}/{Id}").Result;

        if (response.IsSuccessStatusCode)
            return response.Content.ReadFromJsonAsync<string>().Result!;

        return null;
    }

    public void Add(string value)
    {
        var response = Http.PostAsJsonAsync(Address, value).Result;
        response.EnsureSuccessStatusCode();

    }

    public bool Delete(int Id)
{
        var response = Http.DeleteAsync($"{Address}/{Id}").Result;

        if(response.IsSuccessStatusCode)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        throw new InvalidOperationException();
    }

    public void Edit(int Id, string value)
    {
        var response = Http.PutAsJsonAsync($"{Address}/{Id}", value).Result;
        response.EnsureSuccessStatusCode();
    }

   
    
}

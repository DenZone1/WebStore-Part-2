

using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers;

[ApiController]
//[Route("api/values")]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private static readonly Dictionary<int, string> _Values = Enumerable.Range(1,10)
        .Select(i =>(Id:i, Value:$"Valuee - {i}")).ToDictionary(v =>v.Id, v =>v.Value);
}

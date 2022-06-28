using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers.API;
[ApiController]
[Route("api/console")]
public class ConsoleAPIControoler : ControllerBase
{
    [HttpGet("clear")]
    public void Clear() => Console.Clear();

    [HttpGet("write")]
    [HttpGet("write({str})")]
    public void Write(string str) => Console.WriteLine();
}

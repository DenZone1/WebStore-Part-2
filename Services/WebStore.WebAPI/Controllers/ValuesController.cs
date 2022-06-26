

using Microsoft.AspNetCore.Mvc;

using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers;

[ApiController]
//[Route("api/values")]
[Route(WebAPIAddresses.V1.Values)]
public class ValuesController : ControllerBase
{
    private const int __ValueCount = 10;

    private static readonly Dictionary<int, string> __Values = Enumerable.Range(1, 10)
        .Select(i => (Id: i, Value: $"Value - {i}"))
        .ToDictionary(v => v.Id, v => v.Value);

    private static int _LastFreeId = __ValueCount + 1;


    private readonly ILogger<ValuesController> _Logger;

    public ValuesController(ILogger<ValuesController> Logger)
    {
        _Logger = Logger;
    }
    [HttpGet]
    //[HttpPost]
    //[HttpPut]
    //[HttpDelete]
    public IActionResult GetAll() //метод получения всех данных из  WebAPI
    {
        if (__Values.Count == 0)
            return NoContent();
        var values = __Values.Values;
        return Ok(values);
    }

    [HttpGet("{Id:int}")]//GET -> api/values/5
    public IActionResult GetById(int Id) //метод получения данных по идентификатору
    {
        if(__Values.TryGetValue(Id, out var value))
            return Ok(value);

        return NotFound(new { Id });
    }

    [HttpPost]//post -> api/values || Body:Value=qwe;; POST ->api/Values?Value=qwe (Body:Value=qwe - параметр передается в теле запроса)
    [HttpPost("{Value}")] //POST -> api/Values/qwe
    public IActionResult Add(/*[FromBody]*/ string Value) //метод добавления
    {
        var id = _LastFreeId;
        __Values[id] = Value;

        _Logger.LogInformation("Value {0} added with Id:{1}", Value, id);
        _LastFreeId++;

        return CreatedAtAction(nameof(GetById),new {Id = id }, Value);
    }

    [HttpPut("{Id:Int}")]
    public IActionResult Edit(int Id, [FromBody] string Value)//метод редактирования
    {
        if (!__Values.ContainsKey(Id)) 
        {
            _Logger.LogInformation("{0} - NotFound",Id);
           return NotFound(new { Id });
        }

        var old_value = __Values[Id];
        __Values[Id] = Value;
        _Logger.LogInformation("{0} - Redact(Old Value {1}, New Value{2})", Id,old_value, Value);
        return Ok(new {Id, OldValue = old_value, NewValue = Value });
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        if (!__Values.ContainsKey(Id))
        {
            _Logger.LogInformation("{0} - NotFound", Id);
            return NotFound(new { Id });
        }

        var value = __Values[Id];
        __Values.Remove(Id);
        _Logger.LogInformation("{0} - Deleted(Old Value {1})", Id, value);

        return Ok(new { Id, Value = value});

    }


}



namespace WebStore.WebAPI.Infarastructure.Middleware;
// проежутоное ПО которое ловит ошибку
public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate Next, ILogger<ExceptionHandler> Logger)
    {
        _next = Next;
        _logger = Logger;
    }

    public async Task Invoke(HttpContext Context)
    {
        try
        {
            await _next(Context);
        }
        catch (Exception error)
        {
            HandlerException(Context,  error);
            throw;
        }
    }

    private void HandlerException(HttpContext Context, Exception Error)
    {
        _logger.LogError(Error, "Ошибка в процесе запроса к {0} ", Context.Request.Path);
    }
}

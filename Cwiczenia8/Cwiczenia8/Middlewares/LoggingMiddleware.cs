namespace Cwiczenia8.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            { 
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected problem!");
                using (StreamWriter sw = new StreamWriter("..\\..\\..\\Logs\\log.txt", true))
                {
                    sw.WriteLine(ex);
                }
                return;
            }
        }
    }
}

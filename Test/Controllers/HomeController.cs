using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly DbApp _dbApp;

        public HomeController(DbApp dbApp)
        {
            _dbApp = dbApp;
        }


        [HttpGet("{ipAddress}")]
        public async Task<IActionResult> GetAdress(string ipAddress) 
        {
            try
            {
                var client = new HttpClient();
                var url = $"https://ipinfo.io/{ipAddress}/geo";
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var apiResult = await result.Content.ReadAsStringAsync();

                    var resDb = new Obj
                    {
                        Address = ipAddress,
                        TimeRequest = DateTime.UtcNow,
                    };

                    _dbApp.DbObjects.Add(resDb);
                    await _dbApp.SaveChangesAsync();


                    return Ok(apiResult);
                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                    
                }
            } 
            catch (Exception ex) 
            {
                return StatusCode(500);
            }
        }
    }
}

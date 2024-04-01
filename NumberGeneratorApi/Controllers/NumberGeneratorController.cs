using Microsoft.AspNetCore.Mvc;
using NumberGeneratorApi.Application.Interfaces;
using NumberGeneratorApi.Constants;
using NumberGeneratorApi.Utils;

namespace NumberGeneratorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NumberGeneratorController : ControllerBase
    {
        
       
      
        private readonly INumberGeneratorService _numberGeneratorService;
        private readonly SessionHandler _sessionHandler;

        public NumberGeneratorController(INumberGeneratorService numberGeneratorService,
            IHttpContextAccessor httpContextAccessor)
        {
            _numberGeneratorService = numberGeneratorService;
            _sessionHandler = new SessionHandler(httpContextAccessor.HttpContext.Session);
        }

        //מחזיר רק את מספר הקומבינציות האפשריות
        [HttpGet(Name = "StartAPI")]
        public ActionResult<long> StartAPI(int n)
        {
            if (n < 1 || n > 20)
            {
                return BadRequest("The value of n must be between 1 and 20.");
            }
            _sessionHandler.Set<int>(SessionKeys.N, n);
            _sessionHandler.Set<long>(SessionKeys.CurrentCombination, 1);
            var total = _numberGeneratorService.TotalCombinations(n);
            _sessionHandler.Set<long>(SessionKeys.Total, total);
            return Ok(total);
        }


        [HttpGet(Name = "GetNextAPI")]
        public ActionResult<List<int>> GetNextAPI()
        {
            var n = _sessionHandler.Get<int>(SessionKeys.N);
            var current = _sessionHandler.Get<long>(SessionKeys.CurrentCombination);
            var total = _sessionHandler.Get<long>(SessionKeys.Total);

            if (current >total)
            {
                return BadRequest("no more combinations.");
            }

            //res=מכיל ברשימה את הקומבינציה
            var res = _numberGeneratorService.GetNthCombination(n, current);
            _sessionHandler.Set<long>(SessionKeys.CurrentCombination, ++current);
            return Ok(res);
        }
        
        //מחזיר מספר קומבינציות לפי מה שאמרו לו שמותר בדף
        [HttpGet(Name = "GetAllAPI")]
        public ActionResult<long> GetAllAPI(int numOfRows)
        {

            var n =  _sessionHandler.Get<int>(SessionKeys.N);
            var current =  _sessionHandler.Get<long>(SessionKeys.CurrentCombination);
            var total =  _sessionHandler.Get<long>(SessionKeys.Total);
            var untill = current + numOfRows ;
            var till = untill > total ? total : untill;
            _sessionHandler.Set<long>(SessionKeys.CurrentCombination, till +1);
            return Ok(_numberGeneratorService.GetCombinationsRange(n, current, till-1));
        }
    }
}

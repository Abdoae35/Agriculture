namespace Agriculture.API.Controllers;
    [ApiController]
    [Route("api/afforestation")]
    public class AfforestationController : ControllerBase
    {
        private readonly IAfforestationAgricultureAchievementManager _manager;

        public AfforestationController(IAfforestationAgricultureAchievementManager manager)
        {
            _manager = manager;
        }

        [HttpPost("search")]
        public IActionResult Search( [FromBody] AfforestationSearchRequest request)
        {
            var result = _manager.Search(request);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Insert([FromBody] AfforestationAddDto afforestationAddDto)
        {
            await _manager.insert(afforestationAddDto);
            return Ok(afforestationAddDto);
        }

    }
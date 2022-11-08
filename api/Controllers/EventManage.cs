namespace PressYourLuckApi.Controllers
{
    [ApiController]
    [Route("api/eventmanage")]
    public class EventManageController : ControllerBase
    {
        private readonly ILogger<EventManageController> _logger;
        private readonly IManageEventService _manageEventService;
        private readonly ILoginService _loginService;

        public EventManageController(ILogger<EventManageController> logger, IManageEventService manageEventService,
                ILoginService loginService)
        {
            _logger = logger;
            _manageEventService = manageEventService;
            _loginService = loginService;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("cleanScore")]
        [SwaggerOperation(Summary = "Clean all results out of system.")]
        public async Task<ActionResult<string>> ResetEventAsync()
            => Ok(await _manageEventService.ResetEvent());

        [AllowAnonymous]
        [HttpPost("login/admin")]
        [SwaggerOperation(Summary = "Login from admin user.")]
        public async Task<ActionResult<BearerDto>> AdminLoginAsync(AdminLogin loginInfo)
            => Ok(await _loginService.AdminLoginAsync(loginInfo));

        [AllowAnonymous]
        [HttpPost("login/geekomatic")]
        [SwaggerOperation(Summary = "Login from GeekOMatic")]
        public async Task<ActionResult<BearerDto>> GeekLoginAsync(string token)
            => Ok(await _loginService.GeekOMaticLoginAsync(token));

    }
}

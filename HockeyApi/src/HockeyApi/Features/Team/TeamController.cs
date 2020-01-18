using Microsoft.AspNetCore.Mvc;

namespace HockeyApi.Features
{
	[ApiController]
	[Route("team")]
	public class TeamController : Controller {
		private readonly ITeamService _teamService;
		private readonly ITeamDetailsService _teamPlayerService;

		public TeamController(ITeamService teamService, ITeamDetailsService teamPlayerService) {
			_teamService = teamService;
			_teamPlayerService = teamPlayerService;
		}

		[HttpGet]
		public IActionResult Get() => 
			Json(_teamService.List());

		[HttpGet]
		[Route("{teamCode}")]
		public IActionResult GetTeam(string teamCode) =>
			Json(_teamPlayerService.List(teamCode));
	}
}

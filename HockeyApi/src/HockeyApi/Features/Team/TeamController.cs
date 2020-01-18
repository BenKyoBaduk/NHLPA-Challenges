using Microsoft.AspNetCore.Mvc;

namespace HockeyApi.Features
{
	[ApiController]
	[Route("team")]
	public class TeamController : Controller {
		private readonly ITeamService _teamService;
		private readonly ITeamDetailsService _teamDetailsService;

		public TeamController(ITeamService teamService, ITeamDetailsService teamDetailsService) {
			_teamService = teamService;
			_teamDetailsService = teamDetailsService;
		}

		[HttpGet]
		public IActionResult Get() => 
			Json(_teamService.List());

		[HttpGet]
		[Route("{teamCode}")]
		public IActionResult GetTeam(string teamCode) =>
			Json(_teamDetailsService.List(teamCode));
	}
}

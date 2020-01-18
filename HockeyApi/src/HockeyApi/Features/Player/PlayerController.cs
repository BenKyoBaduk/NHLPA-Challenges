using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HockeyApi.Features
{
	[ApiController]
	[Route("player")]
	public class PlayerController : Controller
	{
		private readonly IPlayerService _playerService;
		private readonly ICreatePlayerService _createPlayerService;
		private readonly IUpdatePlayerService _updatePlayerService;

		public PlayerController(IPlayerService playerService, ICreatePlayerService createPlayerService, IUpdatePlayerService updatePlayerService)
		{
			_playerService = playerService;
			_createPlayerService = createPlayerService;
			_updatePlayerService = updatePlayerService;
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreatePlayerModel player)
		{
			_createPlayerService.Create(player);
			return Ok();
		}

		[HttpPost]
		[Route("{player_id}/trade")]
		public IActionResult Trade([FromBody] UpdatePlayerModel player)
		{
			_updatePlayerService.Trade(player);
			return Ok();
		}

		[HttpPost]
		[Route("{player_id}/healthy")]
		public IActionResult Healthy([FromBody] UpdatePlayerModel player)
		{
			player.TeamCode = _playerService.GetCurrentTeamCode(player.PlayerId);
			_updatePlayerService.Healthy(player);
			return Ok();
		}

		[HttpPost]
		[Route("{playerId}/injured")]
		public IActionResult Injured([FromBody] UpdatePlayerModel player)
		{
			player.TeamCode = _playerService.GetCurrentTeamCode(player.PlayerId);
			_updatePlayerService.Injured(player);
			return Ok();
		}

		[HttpGet]
		[Route("{playerId}")]
		public IActionResult Get(int playerId) =>
			Json(_playerService.Get(playerId));

		[HttpGet]
		public IActionResult Search(string q) =>
			Json(_playerService.Search(q));
	}
}

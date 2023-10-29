using Azure.Messaging;
using DataAccess.DAO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		private IPlayerRepository _playerRepository;

		public PlayerController(IPlayerRepository playerRepository)
		{
			_playerRepository = playerRepository;
		}

		[HttpGet]
		public IActionResult GetPlayers()
		{
			try
			{
				var players = _playerRepository.GetPlayers();
				return Ok(players);
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
	}
}

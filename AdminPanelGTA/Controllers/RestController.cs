using System;
using System.Linq;
using System.Text.Json;
using AdminPanelGTA.Models;
using AdminPanelGTA.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelGTA.Controllers
{
	[Route("rest")]
	public class RestController : ControllerBase
	{
		private readonly Filter _filter = new Filter();
		private const string Error404 = "Error 404";
		private const string Error400 = "Error 400";
		private string _playerError = Error400;
		private readonly RpgContext _db;
		public RestController(RpgContext context)
		{
			_db = context;
		}

		[Route("getplayers")]
		public IActionResult GetPlayers([FromBody]PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			players = _filter.GetPlayers(request, players);
			return new JsonResult(players);
		} 

		[Route("getcount")]
		public IActionResult GetCount([FromBody]PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			players = _filter.GetPlayers(request, players);
			return new JsonResult(players.Count());
		} 

		[Route("CreatePlayer")]
		public IActionResult CreatePlayer([FromBody]PlayerCreate request)
		{
			var player = new Player();
			try
			{
				if (string.IsNullOrEmpty(request.Name) ||
				    request.Name.Length > 12 ||
				    request.Title.Length > 30 ||
				    request.Experience > 10000000 ||
				    request.Experience < 0 ||
				    request.BirthDay < 2000 ||
				    request.BirthDay > 3000) throw new Exception();

				player = _filter.Create(request, player);
				_db.Players.Add(player);
				_db.SaveChanges();
			}
			catch
			{
				return new JsonResult("Ошибка 404");
			}

			return new JsonResult(player);
		} 

		[Route("getplayer")]
		public IActionResult GetPlayer([FromBody]JsonElement data)
		{
			
			int idFromJson;
			//var playerError = Error400;
			try
			{
				idFromJson = data.GetProperty("Id").GetInt32();
				IQueryable<Player> players = _db.Players;
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault()?.ID;
				if (idFromBd < idFromJson) throw new Exception();
				players = players.Where(p => p.ID == idFromJson);
				return new JsonResult(players);
			}
			catch 
			{
				return new JsonResult(_playerError);
			}
		}

		[Route("DeletePlayer")]
		public IActionResult DeletePlayer([FromBody] JsonElement data)
		{
			IQueryable<Player> players = _db.Players;
			int idFromJson;
			try
			{
				idFromJson = data.GetProperty("Id").GetInt32();
				var player = _db.Players.Single(p => p.ID == idFromJson);
				if (player == null)
				{
					_playerError = Error404;
					throw new Exception();
				}
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault()?.ID;
				if (idFromBd < idFromJson) throw new Exception();
				_db.Players.Remove(player);
				_db.SaveChanges();
				return new JsonResult("OK");
			}
			catch
			{
				return new JsonResult(_playerError);
			}
		}

		[Route("UpdatePlayer")]
		public IActionResult UpdatePlayer([FromBody] PlayerRequest request)
		{
			Player player;
			try
			{
				player = _db.Players.Single(p => p.ID == request.ID);
				if (player == null)
				{
					_playerError = Error404;
					throw new Exception();
				}
				_filter.Update(player, request);
				_db.SaveChanges();
			}
			catch
			{
				return new JsonResult(_playerError);
			}
			return new JsonResult(player);
		}
	}
}
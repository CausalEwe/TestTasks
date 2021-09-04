using Microsoft.AspNetCore.Mvc;
using PixlTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PixlTest.Controllers
{
	public class HomeController : Controller
	{
		private const string requestTokenUrl = "http://api.pixlpark.com/oauth/requesttoken";
		private const string accessTokenUrl = "http://api.pixlpark.com/oauth/accesstoken";
		private const string refreshTokenUrl = "http://api.pixlpark.com/oauth/refreshtoken";
		private const string unauthorizeTokenUrl = "http://api.pixlpark.com/oauth/unauthorize";
		private const string ordersUrl = "http://api.pixlpark.com/orders"; 
		private const string publicKey = "38cd79b5f2b2486d86f562e3c43034f8";
		private const string privateKey = "8e49ff607b1f46e1a5e8f6ad5d312a80";
		private static readonly HttpClient Client = new HttpClient();

		public string GetRequestToken()
		{
			using (var request = new HttpRequestMessage(HttpMethod.Get, requestTokenUrl))
			{
				//request.Content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = Client.Send(request);
				var resultStr = response.Content.ReadAsStringAsync().Result;
				var responceItem = JsonSerializer.Deserialize<ResponseClass>(resultStr);
				return responceItem.RequestToken;
			}
		}

		private static string Hash(string requestPass)
		{
			using (SHA1 sha1Hash = SHA1.Create())
			{
				byte[] takeBytes = Encoding.UTF8.GetBytes(requestPass);
				byte[] hashBytes = sha1Hash.ComputeHash(takeBytes);
				string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
				return hash;
			}
		}
		
		public string GetAccessToken(string requestToken, string hash)
		{
			var param = $"oauth_token={requestToken}&grant_type=api&username={publicKey}&password={hash}";
			var url = $"{accessTokenUrl}?{param}";
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				var response = Client.Send(request);
				var resultStr = response.Content.ReadAsStringAsync().Result;
				var accessItem = JsonSerializer.Deserialize<AccessResponseClass>(resultStr);
				return accessItem.AccessToken;
			}
		}

		public string RefreshToken(string accesToken)
		{
			var url = $"{refreshTokenUrl}?refreshToken={accesToken}";
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				var response = Client.Send(request);
				var resultStr = response.Content.ReadAsStringAsync().Result;
				var accessItem = JsonSerializer.Deserialize<AccessResponseClass>(resultStr);
				return accessItem.RefreshToken;
			}
		}

		public bool Unauthorize(string accessToken)
		{
			var url = $"{unauthorizeTokenUrl}?oauth_token={accessToken}";
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				var response = Client.Send(request);
				var resultStr = response.Content.ReadAsStringAsync().Result;
				var unauthorizeItem = JsonSerializer.Deserialize<Unauthorize>(resultStr);
				return unauthorizeItem.Success;
			}
		}
		public IActionResult Index()
		{
			var requestToken = GetRequestToken();
			var hash = Hash(requestToken + privateKey);
			var accessToken = GetAccessToken(requestToken, hash);
			var check = GetOrders(accessToken);
			return View(check);
		}

		public List<Order> GetOrders(string accessToken)
		{
			var url = $"{ordersUrl}?oauth_token={accessToken}";
			List<Order> orders = new List<Order>();
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				var response = Client.Send(request);
				var resultStr = response.Content.ReadAsStringAsync().Result;
				orders = JsonSerializer.Deserialize<OrderResponse>(resultStr).Result;
				return orders;
			}
		}
	}
}

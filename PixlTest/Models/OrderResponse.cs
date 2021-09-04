using System.Collections.Generic;

namespace PixlTest.Models
{
	public class OrderResponse
	{
		public string ApiVersion { get; set; }
		public List<Order> Result { get; set; }
		public int ResponseCode { get; set; }
	}
}

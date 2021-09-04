namespace PixlTest.Models
{
	public class AccessResponseClass
	{
		public string AccessToken { get; set; }
		public int Expires { get; set; }
		public string RefreshToken { get; set; }
		public bool Success { get; set; }
	}
}

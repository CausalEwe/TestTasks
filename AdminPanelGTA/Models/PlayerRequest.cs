namespace AdminPanelGTA.Models
{
	public class PlayerRequest
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Race { get; set; }
		public string Profession { get; set; }
		public int After { get; set; }
		public int Before { get; set; }
		public bool Banned { get; set; }
		public int MinExperience { get; set; }
		public int MaxExperience { get; set; }
		public int MinLevel { get; set; }
		public int MaxLevel { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int Experience { get; set; }
		public int BirthDay { get; set; }

	}
}

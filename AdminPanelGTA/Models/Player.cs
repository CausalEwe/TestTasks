using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelGTA.Models
{
	public class Player
	{
		public int ID { get; set; }
		[MaxLength(12)]
		[Required]
		public string Name { get; set; }
		[MaxLength(30)]
		[Required]
		public string Title { get; set; }
		[MaxLength(20)]
		[Required]
		public string Race { get; set; }
		[MaxLength(20)]
		[Required]
		public string Profession { get; set; }
		public DateTime Birthday { get; set; }
		public bool Banned { get; set; }
		[MaxLength(10)]
		[Required]
		public int Experience { get; set; }
		[MaxLength(10)]
		[Required]
		public int Level { get; set; }
		[MaxLength(10)]
		public int UntilNextLevel { get; set; }
	}
}

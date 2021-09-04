using Microsoft.EntityFrameworkCore;

namespace AdminPanelGTA.Models
{
	public class RpgContext : DbContext
	{
		public DbSet<Player> Players { get; set; }

		public RpgContext(DbContextOptions<RpgContext> options)
			: base(options)
			{
			}
	}
}

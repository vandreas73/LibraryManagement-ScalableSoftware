using Microsoft.EntityFrameworkCore;
using UserManagementService.Models;

namespace UserManagementService
{
	public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
	{
		public DbSet<UserModel> Users { get; set; }
	}
}

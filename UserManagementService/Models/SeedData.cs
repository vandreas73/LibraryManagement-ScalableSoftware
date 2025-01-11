namespace UserManagementService.Models
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<UserContext>();
			if (!context.Users.Any())
			{
				context.Users.AddRange(
					new UserModel
					{
						Name = "András",
						Email = "a@a.com",
						Address = "1111 Budapest, Műegyetem rkp. 3."
					},
					new UserModel
					{
						Name = "Béla",
						Email = "b@b.com",
						Address = "1234 Alsókarakószörcsög, Kossuth u. 1."
					});
				context.SaveChanges();
			}
		}
	}
}
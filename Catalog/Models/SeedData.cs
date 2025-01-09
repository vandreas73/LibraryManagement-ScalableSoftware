namespace Catalog.Models
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			var author1 = new Author
			{
				Name = "Victor Hugo",
				Bio = "Victor Hugo was a French poet, novelist, and dramatist of the Romantic movement. He is considered one of the greatest and best-known French writers.",
				BirthDate = new DateOnly(1802, 2, 26),
				Created = DateTime.Now,
				Updated = DateTime.Now
			};
			var author2 = new Author
			{
				Name = "Doris Kearns Goodwin",
				Bio = "Doris Helen Kearns Goodwin is an American biographer, historian, former sports journalist and political commentator.",
				BirthDate = new DateOnly(1943, 1, 4),
				Created = DateTime.Now,
				Updated = DateTime.Now
			};
			var author3 = new Author
			{
				Name = "Alice Schroeder",
				Bio = "Alice Schroeder is an American author. She is the author of The Snowball, a biography of Warren Buffett.",
				BirthDate = new DateOnly(1958, 11, 14),
				Created = DateTime.Now,
				Updated = DateTime.Now
			};

			var context = serviceProvider.GetRequiredService<CatalogContext>();
			if (!context.Books.Any())
			{
				context.Books.AddRange(
					new Book
					{
						Title = "Les Miserables",
						Author = author1,
						Description = "Novel by Victor Hugo",
						Pages = 1488,
						Language = "English",
						Publisher = "Signet",
						ISBN = "978-0451419439",
						PublishingYear = 1862,
						Created = DateTime.Now,
						Updated = DateTime.Now
					},
					new Book
					{
						Title = "Team of Rivals",
						Author = author2,
						Description = "The political genius of Abraham Lincoln",
						Pages = 944,
						Language = "English",
						Publisher = "Simon & Schuster",
						ISBN = "978-0743270755",
						PublishingYear = 2005,
						Created = DateTime.Now,
						Updated = DateTime.Now
					},
					new Book
					{
						Title = "The Snowball",
						Author = author3,
						Description = "Warren Buffett and the business of life",
						Pages = 832,
						Language = "English",
						Publisher = "Bantam",
						Created = DateTime.Now,
						Updated = DateTime.Now,
						ISBN = "978-0553805093",
						PublishingYear = 2008
					});
				context.SaveChanges();
			}

			if (!context.Authors.Any())
			{
				context.Authors.AddRange(author1, author2, author3);
				context.SaveChanges();
			}
		}
	}
}
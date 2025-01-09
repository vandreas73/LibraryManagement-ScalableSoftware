using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
	public class Book
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public int AuthorId { get; set; }
		public string Description { get; set; }
		public int Pages { get; set; }
		public string Language { get; set; }
		public string Publisher { get; set; }
		public string ISBN { get; set; }
		[Required]
		public int PublishingYear { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Author Author { get; set; }
	}
}

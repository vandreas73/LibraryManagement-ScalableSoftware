using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
	public class Author
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Bio { get; set; }
		[Required]
		public DateOnly BirthDate { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}

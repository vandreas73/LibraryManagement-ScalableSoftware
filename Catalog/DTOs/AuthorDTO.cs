namespace Catalog.DTOs
{
	public class AuthorDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Bio { get; set; }
		public DateOnly BirthDate { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;
using Catalog.DTOs;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly CatalogContext _context;
		private readonly IMapper mapper;

		public BooksController(CatalogContext context, IMapper mapper)
		{
			_context = context;
			this.mapper = mapper;
		}

		// GET: api/Books
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
		{
			var books = await _context.Books
				.Include(b => b.Author)
				.ToListAsync();
			return Ok(mapper.Map<IEnumerable<BookDTO>>(books));
		}

		// GET: api/Books/5
		[HttpGet("{id}")]
		public async Task<ActionResult<BookDTO>> GetBook(int id)
		{
			var book = await _context.Books.FindAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return mapper.Map<BookDTO>(book);
		}

		// PUT: api/Books/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBook(int id, BookDTO bookDto)
		{
			var book = mapper.Map<Book>(bookDto);

			if (id != book.Id)
			{
				return BadRequest();
			}

			if (!_context.Authors.Any(a => book.AuthorId == a.Id))
				throw new ValidationException("No author with the given id");

			_context.Entry(book).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BookExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Books
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDto)
		{
			if (bookDto.Id != 0)
			{
				return BadRequest("Id must be empty.");
			}
			var book = mapper.Map<Book>(bookDto);
			if (!_context.Authors.Any(a => book.AuthorId == a.Id))
				throw new ValidationException("No author with the given id");

			_context.Books.Add(book);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetBook", new { id = book.Id }, mapper.Map<BookDTO>(book));
		}

		// DELETE: api/Books/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BookExists(int id)
		{
			return _context.Books.Any(e => e.Id == id);
		}

		[HttpGet("books_from_authors")]
		public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksOfAuthor(int authorId)
		{
			var books = await _context.Books.Where(b => b.AuthorId == authorId).Include(b => b.Author).ToListAsync();
			var bookDtos = mapper.Map<IEnumerable<BookDTO>>(books);
			return Ok(bookDtos);
		}

		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks(string title = "", string authorName = "")
		{
			var books = await _context.Books
				.Include(b => b.Author)
				.Where(b => b.Title.Contains(title) && b.Author.Name.Contains(authorName))
				.ToListAsync();
			return Ok(mapper.Map<IEnumerable<BookDTO>>(books));
		}
	}
}

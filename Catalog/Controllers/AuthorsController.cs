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

namespace Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly CatalogContext context;
		private readonly IMapper mapper;

		public AuthorsController(CatalogContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		// GET: api/Authors
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
		{
			var authors = await context.Authors
				.Include(a => a.Books)
				.ToListAsync();
			return Ok(mapper.Map<IEnumerable<AuthorDTO>>(authors));
		}

		// GET: api/Authors/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
		{
			var author = await context.Authors.FindAsync(id);

			if (author == null)
			{
				return NotFound();
			}

			return mapper.Map<AuthorDTO>(author);
		}

		// PUT: api/Authors/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDto)
		{
			var author = mapper.Map<Author>(authorDto);
			if (id != author.Id)
			{
				return BadRequest();
			}

			context.Entry(author).State = EntityState.Modified;

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AuthorExists(id))
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

		// POST: api/Authors
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDto)
		{
			if (authorDto.Id != 0)
			{
				return BadRequest("Id must be empty.");
			}
			var author = mapper.Map<Author>(authorDto);
			context.Authors.Add(author);
			await context.SaveChangesAsync();

			return CreatedAtAction("GetAuthor", new { id = author.Id }, mapper.Map<AuthorDTO>(author));
		}

		// DELETE: api/Authors/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
		{
			var author = await context.Authors.FindAsync(id);
			if (author == null)
			{
				return NotFound();
			}

			context.Authors.Remove(author);
			await context.SaveChangesAsync();

			return NoContent();
		}

		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<AuthorDTO>>> SearchAuthors(string name)
		{
			var authors = await context.Authors
				.Where(a => a.Name.Contains(name))
				.ToListAsync();
			return Ok(mapper.Map<IEnumerable<AuthorDTO>>(authors));
		}

		private bool AuthorExists(int id)
		{
			return context.Authors.Any(e => e.Id == id);
		}
	}
}

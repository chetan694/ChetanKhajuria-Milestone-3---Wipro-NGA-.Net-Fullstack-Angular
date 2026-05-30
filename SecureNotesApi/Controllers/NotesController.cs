using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureNotesApi.Data;
using SecureNotesApi.Models;
using System.Security.Claims;

namespace SecureNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/notes
        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            note.UserId = userId;

            _context.Notes.Add(note);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Note added successfully.",
                noteId = note.Id
            });
        }

        // GET: api/notes
        [HttpGet]
        public IActionResult GetNotes()
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var notes = _context.Notes
                .Where(n => n.UserId == userId)
                .ToList();

            return Ok(notes);
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.Id == id &&
                    n.UserId == userId);

            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }

            return Ok(note);
        }

        // PUT: api/notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(
            int id,
            Note updatedNote)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.Id == id &&
                    n.UserId == userId);

            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }

            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Note updated successfully."
            });
        }

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var note = _context.Notes
                .FirstOrDefault(n =>
                    n.Id == id &&
                    n.UserId == userId);

            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }

            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Note deleted successfully."
            });
        }
    }
}
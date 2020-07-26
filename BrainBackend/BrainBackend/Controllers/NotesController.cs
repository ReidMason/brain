using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrainBackend.Services;
using BrainBackend.DAL;

namespace BrainBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(NotesService.GetAllNotes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(string id)
        {
            return Ok(NotesService.GetNote(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteDAL newNote)
        {
            try
            {
                return Ok(NotesService.CreateNote(newNote));
            }
            catch (NoteAlreadyExistsException)
            {
                return BadRequest("A Note with that name already exists.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            NotesService.DeleteNote(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(string id, NoteDAL newNote)
        {
            try
            {
                return Ok(NotesService.UpdateNote(id, newNote));
            }
            catch (NoteNotFound)
            {
                return BadRequest("A note with that id doesn't exist");
            }
        }
    }
}
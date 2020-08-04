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
            return Ok(NotesService.GetRootFolder());
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
                NotesService.CreateNote(newNote);
                return Ok(NotesService.GetRootFolder());
            }
            catch (NoteAlreadyExistsException)
            {
                return BadRequest("A Note with that name already exists.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(string id, NoteDAL newNote)
        {
            try
            {
                NotesService.UpdateNote(id, newNote);
                return Ok(NotesService.GetRootFolder());
            }
            catch (NoteNotFound)
            {
                return BadRequest("A note with that id doesn't exist");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            NotesService.DeleteNote(id);
            return Ok(NotesService.GetRootFolder());
        }
    }
}
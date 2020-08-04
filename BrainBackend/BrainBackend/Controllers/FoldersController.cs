using BrainBackend.DAL;
using BrainBackend.Models;
using BrainBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoldersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolder(string id)
        {
            return Ok(FoldersService.GetFolder(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFolder(FolderDAL newFolder)
        {
            try
            {
                FoldersService.CreateFolder(newFolder);
                return Ok(NotesService.GetRootFolder());
            }
            catch (NoteAlreadyExistsException)
            {
                return BadRequest("A Note with that name already exists.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolder(string id, FolderDAL newFolder)
        {
            try
            {
                FoldersService.UpdateFolder(id, newFolder);
                return Ok(NotesService.GetRootFolder());
            }
            catch (NoteNotFound)
            {
                return BadRequest("A note with that id doesn't exist");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(string id)
        {
            FoldersService.DeleteFolder(id);
            return Ok(NotesService.GetRootFolder());
        }
    }
}

using BrainBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BrainBackend.DAL
{
    public class NoteDAL
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<string> FolderTree { get; set; }
        public string Filepath { get { return Path.Combine(Constants.NotesDirectory, String.Join(Path.DirectorySeparatorChar, FolderTree), $"{Name}.md"); } }
    }
}

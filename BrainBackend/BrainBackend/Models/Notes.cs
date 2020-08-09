using BrainBackend.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainBackend.Models
{
    public class Notes
    {
        public RootFolder Root { get; set; }

        public Notes()
        {
            Folder.AllNotes = new List<Note>();
            Folder.AllFolders = new List<Folder>();
            Note.AllTags = new List<String>();
            Root = new RootFolder(Constants.NotesDirectory, null);
        }

        public List<Note> AllNotes
        {
            get
            {
                return Folder.AllNotes;
            }
        }
    }

}
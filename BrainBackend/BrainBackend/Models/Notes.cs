using BrainBackend.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainBackend.Models
{
    public class Notes
    {
        public Folder Root { get; }

        public Notes()
        {
            Folder.AllNotes = new List<Note>();
            Folder.AllFolders = new List<Folder>();
            Root = new Folder(Constants.NotesDirectory, null);
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
using BrainBackend.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainBackend.Models
{
    public class Notes
    {

        public List<Note> AllNotes { get; }

        public Notes()
        {
            AllNotes = new List<Note>();
            GetAllNotes(Constants.NotesDirectory);
        }

        private void GetAllNotes(string filepath)
        {
            AllNotes.AddRange(Directory.GetFiles(filepath).Select(file => new Note(file)).ToList());
            foreach(string dir in Directory.GetDirectories(filepath))
                GetAllNotes(dir);
        }

        public Note CreateNote(NoteDAL newNote)
        {
            var note = new Note(newNote.Name, newNote.FolderTree, newNote.Content);
            AllNotes.Add(note);
            return note;
        }
    }
}
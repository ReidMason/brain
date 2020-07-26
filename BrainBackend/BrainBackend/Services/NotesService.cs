using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrainBackend.DAL;
using BrainBackend.Models;

namespace BrainBackend.Services
{
    public static class NotesService
    {
        internal static List<Note> GetAllNotes()
        {
            return new Notes().AllNotes;
        }

        internal static Note GetNote(string noteId)
        {
            var notes = new Notes();

            return notes.AllNotes.FirstOrDefault(x => x.Id == noteId);
        }

        internal static Note CreateNote(NoteDAL newNote)
        {
            var notes = new Notes();
            if (notes.AllNotes.Where(note => note.Filepath == newNote.Filepath).FirstOrDefault() != null)
            {
                throw new NoteAlreadyExistsException();
            }

            return notes.CreateNote(newNote);
        }

        internal static void DeleteNote(string id)
        {
            Note targetNote = GetNote(id);
            if (targetNote != null)
            {
                targetNote.Delete();
            }
        }

        internal static Note UpdateNote(string id, NoteDAL newNote)
        {
            Notes notes = new Notes();
            Note targetNote = notes.AllNotes.FirstOrDefault(x => x.Id == id);
            if (targetNote == null)
            {
                throw new NoteNotFound();
            }

            // Rename or file moved
            if (targetNote.Filepath != newNote.Filepath)
            {
                targetNote.Move(newNote.FolderTree);
            }

            // Content change
            if (targetNote.Content != newNote.Content)
            {
                targetNote.Content = newNote.Content;
                targetNote.Save();
            }

            return targetNote;
        }
    }

    public class NoteAlreadyExistsException : Exception
    {
        public NoteAlreadyExistsException()
        { }
    }

    public class NoteNotFound : Exception
    {
        public NoteNotFound()
        { }
    }
}
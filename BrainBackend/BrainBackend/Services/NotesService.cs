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
        internal static Folder GetRootFolder()
        {
            return new Notes().Root;
        }

        internal static Note GetNote(string noteId)
        {
            var notes = new Notes();
            Note note = notes.AllNotes.FirstOrDefault(x => x.Id == noteId);
            if (note != null)
            {
                note.Content = note.GetContent();
            }
                
            return note;
        }

        internal static Note CreateNote(NoteDAL newNote)
        {
            var notes = new Notes();
            Folder parent = Folder.AllFolders.Where(x => x.Id == newNote.ParentId).FirstOrDefault();
            if (parent == null)
            {
                throw new FolderNotFound();
            }
            return parent.CreateNote(newNote);
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

            // Note moved
            if (newNote.ParentId != null && targetNote.ParentId != newNote.ParentId)
            {
                Folder parent = Folder.AllFolders.Where(x => x.Id == newNote.ParentId).FirstOrDefault();
                if (parent == null)
                {
                    throw new FolderNotFound();
                }

                targetNote.Move(parent);
            }

            // Note renamed
            if (newNote.Name != null && targetNote.Name != newNote.Name)
            {
                targetNote.Rename(newNote.Name);
            }

            // Content change
            if (newNote.Content != null && targetNote.Content != newNote.Content)
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

    public class FolderNotFound : Exception
    {
        public FolderNotFound()
        { }
    }
}
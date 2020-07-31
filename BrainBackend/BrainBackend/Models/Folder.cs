using BrainBackend.DAL;
using BrainBackend.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrainBackend.Models
{
    public class Folder : BrainFile
    {
        public string Folderpath { get; set; }
        public static List<Note> AllNotes = new List<Note>();
        public static List<Folder> AllFolders = new List<Folder>();
        public List<Note> Notes { get; set; }
        public List<Folder> Folders { get; set; }
        public string Id
        {
            get
            {
                var file = new FileInfo(Filepath);
                return file.CreationTime.Ticks.ToString();
            }
        }
        public string Filepath
        {
            get
            {
                return Path.Combine(Folderpath, Name);
            }
        }

        // Creating new folder
        public Folder(string name, string folderpath, string parentId)
        {
            Name = name;
            Folderpath = folderpath;
            ParentId = parentId;

            AllFolders.Add(this);
            Directory.CreateDirectory(Filepath);
        }

        // Loading existing folder
        public Folder(string path, string parentId)
        {
            ParentId = parentId;
            Notes = new List<Note>();
            Folders = new List<Folder>();
            Folderpath = Path.GetDirectoryName(path);
            var file = new FileInfo(path);
            Name = file.Name;
            
            AllFolders.Add(this);
            GetNotes();
            GetFolders();
        }

        internal void Move(Folder newParent)
        {
            if (newParent.Folders.Select(x => x.Name == Name).FirstOrDefault())
            {
                throw new FolderAlreadyExistsException();
            }

            Directory.Move(Filepath, Path.Combine(newParent.Filepath, Name));
            ParentId = newParent.Id;
            Folderpath = newParent.Filepath;
        }

        private void GetNotes()
        {
            Notes.AddRange(Directory.GetFiles(Filepath).Select(file => new Note(file, Id)).ToList());
            AllNotes.AddRange(Notes);
        }

        internal void Delete()
        {
            Directory.Delete(Filepath);
        }

        private void GetFolders()
        {
            Folders.AddRange(Directory.GetDirectories(Filepath).Select(x => new Folder(x, Id)).ToList());
        }

        public Note CreateNote(NoteDAL newNote)
        {
            return new Note(newNote.Name, newNote.Content, Filepath, Id);
        }

        internal Folder CreateFolder(FolderDAL newFolder)
        {
            return new Folder(newFolder.Name, Filepath, Id);
        }

        internal void Rename(string newName)
        {
            string oldFilepath = Filepath;
            Name = newName;

            Directory.Move(oldFilepath, Filepath);
        }
    }
}

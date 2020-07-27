using BrainBackend.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainBackend.Models
{
    public class Note : BrainFile
    {
        public string Folderpath { get; set; }
        public string Content { get; set; }
        public string Filename { get
            {
                return $"{Name}.md";
            } 
        }
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
                return Path.Combine(Folderpath, Filename);
            }
        }

        public string GetContent()
        {
            return File.ReadAllText(Filepath);   
        }

        // Creating new note with file
        public Note(string name, string content, string folderpath, string parentId)
        {
            Name = name;
            Content = content;
            Folderpath = folderpath;
            ParentId = parentId;
            Save();
        }

        // Loading note where file already exists
        public Note(string filepath, string parentId)
        {
            var file = new FileInfo(filepath);
            Folderpath = Path.GetDirectoryName(filepath);
            Name = file.Name.Replace(file.Extension, "");
            ParentId = parentId;
        }

        public void Save()
        {
            File.WriteAllText(Filepath, Content);
        }

        public void Move(Folder newParent)
        {
            if (newParent.Notes.Select(x => x.Name == Name).FirstOrDefault())
            {
                throw new NoteAlreadyExistsException();
            }
            string newFilepath = Path.Combine(newParent.Filepath, Filename);
            File.Move(Filepath, newFilepath);
            
            ParentId = newParent.Id;
            Folderpath = newParent.Filepath;
        }

        public void Delete()
        {
            File.Delete(Filepath);
        }

        internal void Rename(string newName)
        {
            string oldFilepath = Filepath;
            Name = newName;

            if (File.Exists(Filepath))
            {
                throw new NoteAlreadyExistsException();
            }    

            File.Move(oldFilepath, Filepath);
        }
    }
}
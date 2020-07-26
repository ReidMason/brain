using BrainBackend.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainBackend.Models
{
    public class Note
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Content { get; set; }
        public List<string> FolderTree { get; set; }
        public string Filepath { get { return Path.Combine(Constants.NotesDirectory, String.Join(Path.DirectorySeparatorChar, FolderTree) , $"{Name}.md"); } }

        // Creating new note with file
        public Note(string name, List<string> folderTree, string content)
        {
            Name = name;
            FolderTree = folderTree;
            Content = content;
            Save();
            var file = new FileInfo(Filepath);
            Id = file.CreationTime.Ticks.ToString();
        }

        // Loading note where file already exists
        public Note(string filepath)
        {
            var file = new FileInfo(filepath);
            Name = file.Name.Replace(file.Extension, "");
            //Filepath = filepath.TrimStart(Constants.NotesDirectory.ToCharArray()).TrimEnd($"{Name}.md".ToCharArray());
            FolderTree = filepath.TrimStart(Constants.NotesDirectory.ToCharArray()).TrimEnd($"{Name}.md".ToCharArray()).Split(Path.DirectorySeparatorChar).ToList();
            Id = file.CreationTime.Ticks.ToString();
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.Combine(Constants.NotesDirectory, String.Join(Path.DirectorySeparatorChar, FolderTree)));
            File.WriteAllText(Filepath, Content);
        }

        public void Move(List<string> folderTree)
        {
            if (File.Exists(Path.Combine(Constants.NotesDirectory, String.Join(Path.DirectorySeparatorChar, folderTree), $"{Name}.md")))
            {
                throw new NoteAlreadyExistsException();
            }

            string oldFilepath = Filepath;
            FolderTree = folderTree;
            File.Move(oldFilepath, Filepath);
        }

        public void Delete()
        {
            File.Delete(Filepath);
        }
    }
}
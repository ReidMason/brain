using BrainBackend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BrainBackend.Models
{
    public class Note : BrainFile
    {
        public static List<String> AllTags { get; set; }
        public string Folderpath { get; set; }
        public string Content { get; set; }
        public List<String> Tags { get; set; }
    public string Filename { get
            {
                return $"{Name}.md";
            } 
        }
        public string Id
        {
            get
            {
                //var file = new FileInfo(Filepath);
                //return file.CreationTime.Ticks.ToString();
                return Filepath.GetHashCode().ToString().TrimStart('-');
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
            FindTags();
        }

        // Loading note where file already exists
        public Note(string filepath, string parentId)
        {
            var file = new FileInfo(filepath);
            Folderpath = Path.GetDirectoryName(filepath);
            Name = file.Name.Replace(file.Extension, "");
            ParentId = parentId;

            // Loading note content for now
            Content = GetContent();
            FindTags();
        }

        private void FindTags()
        {
            Tags = new List<string>();

            // Find all tags in the note
            Regex regex = new Regex(@"#[^\s || #]{1,}");
            MatchCollection matches = regex.Matches(Content);
            foreach (Match match in matches)
            {
                // Add to tag list for the specific note
                if (!Tags.Contains(match.Value))
                {
                    Tags.Add(match.Value);
                }

                // Add to global tags list
                if (!AllTags.Contains(match.Value))
                {
                    AllTags.Add(match.Value);
                }
            }
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
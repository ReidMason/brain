using System;
using System.Collections.Generic;

namespace BrainBackend.Models
{
    public class RootFolder : Folder
    {
        public List<String> AllTags { get { return Note.AllTags; } }

        public RootFolder(string name, string folderpath, string parentId) : base(name, folderpath, parentId)
        {
        }

        public RootFolder(string path, string parentId) : base(path, parentId)
        {
        }
    }
}
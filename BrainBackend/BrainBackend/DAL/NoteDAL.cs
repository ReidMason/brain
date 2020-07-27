using BrainBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BrainBackend.DAL
{
    public class NoteDAL
    {
        public string Name { get; set; }
        public string Content { get; set;  }
        public string ParentId { get; set; }
        public string Filename
        {
            get
            {
                return $"{Name}.md";
            }
        }
    }
}

using BrainBackend.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrainBackend
{
    public static class Utils
    {
        public static void CreateDataDirectory()
        {
            if (!Directory.Exists(Constants.NotesDirectory))
            {
                Directory.CreateDirectory(Constants.NotesDirectory);
            }
        }
    }
}

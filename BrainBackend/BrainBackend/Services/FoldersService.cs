using BrainBackend.DAL;
using BrainBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainBackend.Services
{
    public class FoldersService
    {
        public static Folder GetRootFolder()
        {
            return new Notes().Root;
        }
        public static Folder GetFolder(string id)
        {
            var notes = new Notes();

            return Folder.AllFolders.FirstOrDefault(x => x.Id == id);
        }

        internal static Folder CreateFolder(FolderDAL newFolder)
        {
            var notes = new Notes();
            Folder parent = Folder.AllFolders.Where(x => x.Id == newFolder.ParentId).FirstOrDefault();
            if (parent == null)
            {
                throw new FolderNotFound();
            }
            return parent.CreateFolder(newFolder);
        }

        internal static object UpdateFolder(string id, FolderDAL newFolder)
        {
            Notes notes = new Notes();
            Folder targetFolder = Folder.AllFolders.FirstOrDefault(x => x.Id == id);
            if (targetFolder == null)
            {
                throw new NoteNotFound();
            }

            // Note moved
            if (newFolder.ParentId != null && targetFolder.ParentId != newFolder.ParentId)
            {
                Folder parent = Folder.AllFolders.Where(x => x.Id == newFolder.ParentId).FirstOrDefault();
                if (parent == null)
                {
                    throw new FolderNotFound();
                }

                targetFolder.Move(parent);
            }

            // Note renamed
            if (newFolder.Name != null && targetFolder.Name != newFolder.Name)
            {
                targetFolder.Rename(newFolder.Name);
            }

            return targetFolder;
        }

        internal static void DeleteFolder(string id)
        {
            Folder targetFolder = GetFolder(id);
            if (targetFolder != null)
            {
                targetFolder.Delete();
            }
        }
    }

    public class FolderAlreadyExistsException : Exception
    {
        public FolderAlreadyExistsException()
        { }
    }
    
}

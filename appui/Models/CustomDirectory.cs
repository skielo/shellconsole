using System;
using System.Collections.Generic;

namespace appui.Models
{
    public class CustomDirectory
    {
        public string Name { get; set; }
        public CustomDirectory Parent { get; set; }
        public List<CustomDirectory> ChildrenFolders { get; set; }
        public List<CustomFile> ChildrenFiles { get; set; }


        public CustomDirectory()
        {
            this.ChildrenFolders = new List<CustomDirectory>();
            this.ChildrenFiles = new List<CustomFile>();
        }

        public void Print()
        {
            Console.WriteLine($"{GetFullPath(Parent)}/{Name}");
        }

        public string GetFullPath(CustomDirectory directory)
        {
            var retval = string.Empty;
            if (directory != null)
            {
                if (directory.Parent != null)
                    retval += $"{GetFullPath(directory.Parent)}/{directory.Name}";
                else
                    retval += $"/{directory.Name}";
            }

            return retval;
        }

        public void ListContent(bool recursive = false)
        {
            if (recursive)
            {
                Print();
                ListFiles();
                foreach (var item in this.ChildrenFolders)
                {
                    item.ListContent(recursive);
                }
            }
            else
            {
                ListFolders();
                ListFiles();
            }

        }

        public void ListFolders()
        {
            foreach (var item in this.ChildrenFolders)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void ListFiles()
        {
            foreach (var item in this.ChildrenFiles)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}

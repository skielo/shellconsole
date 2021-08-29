using System;
namespace appui.Models
{
    public class CustomFile
    {
        public string Name { get; set; }
        public CustomDirectory Parent { get; set; }

        public void Print()
        {
            Console.WriteLine($"{GetFullPath(Parent)}/{Name}");
        }

        public string GetFullPath(CustomDirectory directory)
        {
            var retval = string.Empty;
            if (directory.Parent != null)
            {
                retval += $"{GetFullPath(Parent)}/{Name}";
            }

            return retval;
        }
    }
}

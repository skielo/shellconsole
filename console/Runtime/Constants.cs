
namespace appui.Runtime
{
    public static class Constants
    {
        public static string InvalidArguments => "Please type a valid command or help.";
        public static string Help => "welcom to the shell. \nThose are the available commands.\n\n" +
            "COMMANDS\n quit: Close the application.\npwd: Prints the full path of the current folder.\n" +
            "ls: List the content of the current folder. you can use  -r as argument to be recursive.\n" +
            "mkdir [folder_name]: Creates a new folder in the current folder.\n" +
            "touch [file_name]: Create a new file in the current folder.\n" +
            "cd [folder_name] or [..]: If the `folder_name` exist it navigates to it. otherwise says `Directory not found`.";
        public static string FolderNotFound => "Directory not found";
        public static string FolderAlreadyExist => "Directory Already Exists";
        public static string InvalidFileOrFolder => "Invalid File or Folder Name";
    }
}

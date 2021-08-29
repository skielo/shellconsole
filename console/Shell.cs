using System;
using System.Collections.Generic;
using System.Linq;
using appui.Models;
using appui.Runtime;

namespace appui
{
    public class Shell
    {
        public Instruction Action { get; set; }
        public List<string> Arguments { get; set; }
        public CustomDirectory current { get; set; }

        public Shell()
        {
            this.current = new CustomDirectory { Name = "root" };
            this.Arguments = new List<string>();
        }

        public void SetCommand(Instruction action, List<string> arguments)
        {
            this.Action = action;
            this.Arguments = arguments;
        }

        public void Execute()
        {
            switch (Action)
            {
                case Instruction.QUIT:
                    throw new QuitException("See you!");
                case Instruction.PWD:
                    this.ExecutePWD();
                    break;
                case Instruction.LS:
                    this.ExecuteLS();
                    break;
                case Instruction.MKDIR:
                    this.ExecuteMKDIR();
                    break;
                case Instruction.CD:
                    this.ExecuteCD();
                    break;
                case Instruction.TOUCH:
                    this.ExecuteTOUCH();
                    break;
                case Instruction.HELP:
                    Console.WriteLine(Constants.Help);
                    break;
            }
        }

        private void ExecutePWD()
        {
            this.current.Print();
        }

        private void ExecuteLS()
        {
            if (this.Arguments.Exists(x => x == "-r"))
                this.current.ListContent(true);
            else
                this.current.ListContent();
        }

        private void ExecuteMKDIR()
        {
            if (this.Arguments.Count == 1 && !this.current.ChildrenFolders.Exists(x => x.Name == this.Arguments[0]))
            {
                if (this.Arguments[0].Length <= 100)
                    this.current.ChildrenFolders.Add(new CustomDirectory { Name = this.Arguments[0], Parent = this.current });
                else
                    Console.WriteLine(Constants.InvalidFileOrFolder);
            }
            else
                Console.WriteLine(Constants.FolderAlreadyExist);
        }

        private void ExecuteCD()
        {
            if (this.Arguments.Count == 1 && this.current.ChildrenFolders.Exists(x => x.Name == this.Arguments[0]))
                this.current = this.current.ChildrenFolders.FirstOrDefault(x => x.Name == this.Arguments[0]);
            else if (this.Arguments.Count == 1 && this.Arguments[0] == ".." && this.current.Parent != null)
                this.current = this.current.Parent;
            else
            Console.WriteLine(Constants.FolderNotFound);
        }

        private void ExecuteTOUCH()
        {
            if (this.Arguments.Count == 1 && this.Arguments[0].Length <= 100)
                this.current.ChildrenFiles.Add(new CustomFile { Name = this.Arguments[0], Parent = this.current });
            else
                Console.WriteLine(Constants.InvalidFileOrFolder);
        }
    }
}

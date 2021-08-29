using System;
using System.Collections.Generic;
using System.IO;
using appui;
using appui.Models;
using appui.Runtime;
using NUnit.Framework;

namespace test
{
    public class Tests
    {
        Shell application;

        [SetUp]
        public void Setup()
        {
            application = new Shell();
        }

        [Test]
        public void Shell_Can_Quit()
        {
            var arguments = new List<string>();
            application.SetCommand(appui.Models.Instruction.QUIT, arguments);

            Assert.Throws<QuitException>(() =>
            {
                application.Execute();
            });
        }

        [Test]
        public void Shell_Can_List_Current_Path()
        {
            var arguments = new List<string>();
            application.SetCommand(appui.Models.Instruction.PWD, arguments);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                application.Execute();

                Assert.AreEqual("/root\n", sw.ToString());
            }
        }

        [Test]
        public void Shell_Can_Create_Folder_And_PWD()
        {
            var arguments = new List<string>();
            var folder = "test";
            arguments.Add(folder);
            application.SetCommand(appui.Models.Instruction.MKDIR, arguments);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                application.Execute();
                application.SetCommand(appui.Models.Instruction.CD, arguments);
                application.Execute();
                arguments.Clear();
                application.SetCommand(appui.Models.Instruction.PWD, arguments);
                application.Execute();

                Assert.AreEqual($"/root/{folder}\n", sw.ToString());
            }
        }

        [Test]
        public void Shell_Can_Create_Files_And_List()
        {
            var arguments = new List<string>();
            arguments.Add("file-1");
            application.SetCommand(appui.Models.Instruction.TOUCH, arguments);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                application.Execute();
                arguments.Clear();
                arguments.Add("file-2");
                application.SetCommand(appui.Models.Instruction.TOUCH, arguments);
                application.Execute();
                arguments.Clear();
                application.SetCommand(appui.Models.Instruction.LS, arguments);
                application.Execute();

                Assert.AreEqual($"file-1\nfile-2\n", sw.ToString());
            }
        }

        [Test]
        public void Shell_Can_Create_Files_And_List_Recursive()
        {
            var arguments = new List<string>();
            arguments.Add("file-1");
            application.SetCommand(appui.Models.Instruction.TOUCH, arguments);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                application.Execute();
                arguments.Clear();
                arguments.Add("folder-1");
                application.SetCommand(appui.Models.Instruction.MKDIR, arguments);
                application.Execute();
                arguments.Clear();
                arguments.Add("folder-1");
                application.SetCommand(appui.Models.Instruction.CD, arguments);
                application.Execute();
                arguments.Clear();
                arguments.Add("folder1-file-1");
                application.SetCommand(appui.Models.Instruction.TOUCH, arguments);
                application.Execute();
                arguments.Clear();
                arguments.Add("folder1-file-2");
                application.SetCommand(appui.Models.Instruction.TOUCH, arguments);
                application.Execute();
                arguments.Clear();
                arguments.Add("..");
                application.SetCommand(appui.Models.Instruction.CD, arguments);
                application.Execute();
                arguments.Clear();
                arguments.Add("-r");
                application.SetCommand(appui.Models.Instruction.LS, arguments);
                application.Execute();

                Assert.AreEqual($"/root\nfile-1\n/root/folder-1\nfolder1-file-1\nfolder1-file-2\n", sw.ToString());
            }
        }

        [Test]
        public void Can_Parse_Command()
        {
            var action = Parser.TryParseInstruction<Instruction>("PWD");

            Assert.AreEqual(Instruction.PWD, action);
        }

        [Test]
        public void Wrong_Argument()
        {
            var action = Parser.TryParseInstruction<Instruction>("NOT_VALID");

            Assert.AreEqual(Instruction.WRONG, action);
        }
    }
}

using System;
using appui.Models;
using appui.Runtime;

namespace appui
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var shell = new Shell();
            var keepWorking = true;
            while (keepWorking)
            {
                var command = Console.ReadLine();
                try
                {
                    if (!string.IsNullOrEmpty(command))
                    {
                        var action = Parser.TryParseInstruction<Instruction>(command);
                        if (action == Instruction.WRONG)
                            throw new ArgumentException(Constants.InvalidArguments);
                        var arguments = Parser.TryToGetArguments(command);
                        shell.SetCommand(action, arguments);

                        shell.Execute();
                    }
                    else
                    {
                        Console.WriteLine(Constants.InvalidArguments);
                    }
                }
                catch(QuitException)
                {
                    keepWorking = false;
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something goes wrong: {ex.Message}");
                }
            }
        }
    }
}

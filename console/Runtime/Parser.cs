using System;
using System.Collections.Generic;

namespace appui.Runtime
{
    public static class Parser
    {
        public static T TryParseInstruction<T>(string command) where T : struct
        {
            T retval;
            Enum.TryParse(command.ToUpper().Split(' ')[0], out retval);
            return retval;
        }

        public static List<string> TryToGetArguments(string command)
        {
            var retval = new List<string>();

            var commands = command.Split(' ');
            for (int i = 1; i < commands.Length; i++)
            {
                retval.Add(commands[i]);
            }


            return retval;
        }
    }
}
